using FishNet;
using FishNet.Managing;
using FishNet.Managing.Server;
using FishNet.Connection;
using FishNet.Transporting;
using FishNet.Transporting.KCP.Edgegap;
using FishNet.Object;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

public class NetworkStarter : MonoBehaviour
{
    public static NetworkStarter Instance { get; private set; }

    [Header("Network Settings")]
    public NetworkManager networkManager;
    public GameObject playerPrefab;         // assign "Player" (Male) here
    public GameObject playerPrefab2;        // assign "Player2" (Male2) here
    public GameObject femalePlayerPrefab;   // assign "FPlayer" (Female) here

    [Header("Fallback Direct IP")]
    public string serverIP = "192.168.1.11";

    [Header("Spawn Settings")]
    public bool useSpawnPoints = true;
    public Vector3 defaultSpawnPosition = new Vector3(0, 2f, 0);

    [Header("Input System")]
    public InputActionAsset controlActions;
    private InputAction moveAction;
    private InputAction sprintAction;

    [Header("Mobile Input (Optional)")]
    public SimpleMobileJoystick mobileJoystick;
    public bool useMobileJoystick = false;

    [Header("Debug")]
    public bool showMobileDebugGUI = false;

    private bool mobileSprintPressed = false;
    private GameObject localPlayer;
    private PlayerController activeRunner;
    private CyclingController activeCyclist;
    private RacePlayerTracker playerTracker;

    private Dictionary<NetworkConnection, SpawnPoint> usedSpawnPoints
        = new Dictionary<NetworkConnection, SpawnPoint>();

    // Stores model name sent directly by each client — most reliable for mobile
    private Dictionary<NetworkConnection, string> clientModels
        = new Dictionary<NetworkConnection, string>();

    private bool useRelay = false;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        #if UNITY_ANDROID || UNITY_IOS
                useMobileJoystick = true;
        #endif

        if (networkManager == null)
            networkManager = InstanceFinder.NetworkManager;

        if (networkManager == null)
        {
            return;
        }

        if (LobbyDataTransfer.Instance != null &&
            !string.IsNullOrEmpty(LobbyDataTransfer.Instance.relayHost))
        {
            useRelay = true;

            var transport = networkManager.TransportManager
                .GetTransport<EdgegapKcpTransport>();

            if (transport != null)
            {
                bool isHost = false;
        #if UNITY_EDITOR || UNITY_STANDALONE
                        isHost = true;
        #endif
                var relayData = new EdgegapRelayData(
                    LobbyDataTransfer.Instance.relayHost,
                    LobbyDataTransfer.Instance.relayServerPort,
                    LobbyDataTransfer.Instance.relayClientPort,
                    LobbyDataTransfer.Instance.relayUserToken,
                    LobbyDataTransfer.Instance.relaySessionToken
                );

                transport.SetEdgegapRelayData(relayData);

            }
            else
            {
            }
        }
        else
        {
        }

        playerTracker = Object.FindFirstObjectByType<RacePlayerTracker>();

        networkManager.ServerManager.OnServerConnectionState += OnServerConnectionState;
        networkManager.ClientManager.OnClientConnectionState += OnClientConnectionState;
        networkManager.ServerManager.OnRemoteConnectionState += OnRemoteConnectionState;

        // Always initialize actions regardless of platform
        if (controlActions != null)
        {
            moveAction = controlActions.FindAction("Move", true);
            sprintAction = controlActions.FindAction("Run", false);
            moveAction?.Enable();
            sprintAction?.Enable();
        }

        if (useSpawnPoints)
            StartCoroutine(DebugSpawnPoints());

        #if UNITY_EDITOR || UNITY_STANDALONE
            StartHost();
        #elif UNITY_ANDROID || UNITY_IOS
            useMobileJoystick = true;
            StartCoroutine(StartMobileClientCoroutine());
        #endif
    }

    private void OnDestroy()
    {
        if (networkManager != null)
        {
            networkManager.ServerManager.OnServerConnectionState -= OnServerConnectionState;
            networkManager.ClientManager.OnClientConnectionState -= OnClientConnectionState;
            networkManager.ServerManager.OnRemoteConnectionState -= OnRemoteConnectionState;
        }
    }

    /// <summary>
    /// Called by NetworkPlayer.ServerSendModel when a client tells the server
    /// which model they selected. Stored so SpawnPlayerForConnection can use it.
    /// </summary>
    public void RegisterClientModel(NetworkConnection conn, string modelName)
    {
        bool alreadySpawned = clientModels.ContainsKey(conn); // model was never set = spawned via fallback
        clientModels[conn] = modelName;

        // If the player was already spawned with the wrong prefab, respawn them
        if (alreadySpawned)
        {
            RespawnWithCorrectModelIfNeeded(conn, modelName);
        }
    }

    private void RespawnWithCorrectModelIfNeeded(NetworkConnection conn, string modelName)
    {
        // Find the currently spawned object for this connection
        NetworkObject[] spawnedObjects = FindObjectsByType<NetworkObject>(FindObjectsSortMode.None);
        foreach (var netObj in spawnedObjects)
        {
            if (netObj.Owner != conn) continue;

            string spawnedPrefabName = netObj.gameObject.name
                .Replace("(Clone)", "").Trim();
            GameObject correctPrefab = GetPrefabByModelName(modelName);

            if (spawnedPrefabName == correctPrefab.name)
            {
                return;
            }


            // Save spawn position before despawn
            Vector3 pos = netObj.transform.position;
            Quaternion rot = netObj.transform.rotation;

            // Release old spawn point
            if (usedSpawnPoints.TryGetValue(conn, out SpawnPoint oldPoint))
            {
                oldPoint.Release();
                usedSpawnPoints.Remove(conn);
            }

            networkManager.ServerManager.Despawn(netObj.gameObject);
            Destroy(netObj.gameObject);

            // Spawn new one at same position
            GameObject newPlayer = Instantiate(correctPrefab, pos, rot);
            networkManager.ServerManager.Spawn(newPlayer, conn);

            MultiplayerRaceRanking ranking = FindFirstObjectByType<MultiplayerRaceRanking>();
            if (ranking != null)
                ranking.RefreshRacers();

            if (usedSpawnPoints.TryGetValue(conn, out SpawnPoint point))
                usedSpawnPoints[conn] = point;

            NetworkPlayer np = newPlayer.GetComponent<NetworkPlayer>();
            if (np != null)
                StartCoroutine(ConfirmSpawnAfterDelay(np, pos, rot));

            if (playerTracker != null)
            {
                NetworkPlayer netPlayer = newPlayer.GetComponent<NetworkPlayer>();
                if (netPlayer != null)
                    playerTracker.RegisterNetworkPlayer(netPlayer, conn);
            }
            return;
        }
    }

    public void StartHost()
    {
        if (networkManager == null) return;
        networkManager.ServerManager.StartConnection();
        networkManager.ClientManager.StartConnection();
    }

    public void StartServer()
    {
        if (networkManager == null) return;
        networkManager.ServerManager.StartConnection();
    }

    public void StartClient()
    {
        if (networkManager == null) return;

        if (!useRelay)
        {
            var transport = networkManager.TransportManager
                .GetTransport<EdgegapKcpTransport>();
            if (transport != null)
            {
                transport.SetClientAddress(serverIP);
                transport.SetPort(7777);
            }
        }

        networkManager.ClientManager.StartConnection();
    }

    private IEnumerator StartMobileClientCoroutine()
    {

        yield return new WaitForSeconds(3f);

        float totalTimeout = 30f;
        float elapsed = 0f;

        while (elapsed < totalTimeout)
        {

            networkManager.ClientManager.StartConnection();

            float attemptTime = 0f;
            while (attemptTime < 5f)
            {
                if (networkManager.ClientManager.Connection != null &&
                    networkManager.ClientManager.Connection.IsActive)
                {
                    yield break;
                }
                attemptTime += 0.5f;
                yield return new WaitForSeconds(0.5f);
            }

            networkManager.ClientManager.StopConnection();
            yield return new WaitForSeconds(1f);

            elapsed += 6f;
        }

    }

    private IEnumerator DebugSpawnPoints()
    {
        yield return null;

        SpawnPoint testPoint = SpawnPoint.GetRandomSpawnPoint();

        if (testPoint != null)
        {
            testPoint.GetSpawnTransform(out Vector3 pos, out Quaternion rot);
        }
        else
        {
        }
    }

    private void OnServerConnectionState(ServerConnectionStateArgs args)
    {
    }

    private void OnClientConnectionState(ClientConnectionStateArgs args)
    {

        if (args.ConnectionState == LocalConnectionState.Started)
        else if (args.ConnectionState == LocalConnectionState.Stopped)
    }

    private void OnRemoteConnectionState(NetworkConnection conn, RemoteConnectionStateArgs args)
    {
        if (!networkManager.IsServerStarted) return;

        if (args.ConnectionState == RemoteConnectionState.Started)
        {
            StartCoroutine(WaitAndSpawnPlayer(conn));
        }
        else if (args.ConnectionState == RemoteConnectionState.Stopped)
        {
            if (usedSpawnPoints.ContainsKey(conn))
            {
                usedSpawnPoints[conn].Release();
                usedSpawnPoints.Remove(conn);
            }

            if (clientModels.ContainsKey(conn))
                clientModels.Remove(conn);
        }
    }

    private IEnumerator WaitAndSpawnPlayer(NetworkConnection conn)
    {
        float timeout = 8f;
        float elapsed = 0f;

        while (!clientModels.ContainsKey(conn) && elapsed < timeout)
        {
            elapsed += 0.1f;
            yield return new WaitForSeconds(0.1f);
        }

        if (!clientModels.ContainsKey(conn))

        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();

        SpawnPlayerForConnection(conn);

        // was: yield return new WaitForSeconds(0.5f);
        yield return new WaitForEndOfFrame(); // just one frame is enough

        MultiplayerRaceRanking ranking = FindFirstObjectByType<MultiplayerRaceRanking>();
        if (ranking != null)
            ranking.RefreshRacers();
    }
    private void SpawnPlayerForConnection(NetworkConnection conn)
    {
        if (playerPrefab == null)
        {
            return;
        }

        Vector3 spawnPos = defaultSpawnPosition;
        Quaternion spawnRot = Quaternion.identity;
        SpawnPoint usedPoint = null;

        if (useSpawnPoints)
        {
            SpawnPoint spawnPoint = SpawnPoint.GetRandomSpawnPoint();
            if (spawnPoint != null)
            {
                spawnPoint.GetSpawnTransform(out spawnPos, out spawnRot);
                usedPoint = spawnPoint;
            }
        }

        GameObject prefabToSpawn = GetPrefabForConnection(conn);
        GameObject player = Instantiate(prefabToSpawn, spawnPos, spawnRot);
        networkManager.ServerManager.Spawn(player, conn);

        if (usedPoint != null)
            usedSpawnPoints[conn] = usedPoint;

        if (playerTracker != null)
        {
            NetworkPlayer netPlayer = player.GetComponent<NetworkPlayer>();
            if (netPlayer != null)
                playerTracker.RegisterNetworkPlayer(netPlayer, conn);
        }

        if (LobbyDataTransfer.Instance != null)
        {
            var players = LobbyDataTransfer.Instance.GetAllPlayers();
            // Find the PlayerData that matches this connection's sent model, etc.
            // Then: matchedPlayerData.clientId = conn.ClientId;
        }

        NetworkPlayer np = player.GetComponent<NetworkPlayer>();
        if (np != null)
            StartCoroutine(ConfirmSpawnAfterDelay(np, spawnPos, spawnRot));

    }

    private GameObject GetPrefabForConnection(NetworkConnection conn)
    {
        // 1. Check if client sent their model directly via RPC (most reliable for mobile)
        if (clientModels.TryGetValue(conn, out string sentModel))
        {
            return GetPrefabByModelName(sentModel);
        }

        // 2. Fallback to LobbyDataTransfer (works for host/editor)
        if (LobbyDataTransfer.Instance == null)
        {
            return playerPrefab;
        }

        bool isHostConn = networkManager.ClientManager.Connection == conn;

        LobbyDataTransfer.PlayerData data = isHostConn
            ? LobbyDataTransfer.Instance.GetLocalPlayerData()
            : GetPlayerDataForRemoteConnection(conn);

        if (data == null)
        {
            return playerPrefab;
        }

        return GetPrefabByModelName(data.selectedModel);
    }

    /// <summary>
    /// Single place where model name maps to prefab.
    /// Add new models here.
    /// </summary>
    private GameObject GetPrefabByModelName(string modelName)
    {
        if (modelName == "Male2"  && playerPrefab2       != null) return playerPrefab2;
        if (modelName == "Female" && femalePlayerPrefab  != null) return femalePlayerPrefab;
        return playerPrefab; // default: Male
    }

    private LobbyDataTransfer.PlayerData GetPlayerDataForRemoteConnection(NetworkConnection conn)
    {
        var players = LobbyDataTransfer.Instance.GetAllPlayers();

        // Determine how many remote players have already been spawned
        // by subtracting host connection from total spawned count
        bool hostAlreadySpawned = usedSpawnPoints.ContainsKey(networkManager.ClientManager.Connection);
        int remoteAlreadySpawned = hostAlreadySpawned 
            ? usedSpawnPoints.Count - 1  // subtract host
            : usedSpawnPoints.Count;

        // Index 0 = host/local player, index 1+ = remote players
        int playerIndex = 1 + remoteAlreadySpawned;

        if (playerIndex < players.Count)
            return players[playerIndex];

        // Fallback: find first non-local player
        foreach (var p in players)
        {
            if (!p.isLocalPlayer)
            {
                return p;
            }
        }

        return null;
}
    private IEnumerator ConfirmSpawnAfterDelay(NetworkPlayer netPlayer, Vector3 spawnPos, Quaternion rot)
    {
        yield return new WaitForSeconds(0.4f);

        if (netPlayer == null) yield break;

        float drift = Vector3.Distance(netPlayer.transform.position, spawnPos);
        if (drift < 1f)
        {
            netPlayer.RpcConfirmSpawnPosition(spawnPos, rot);
        }
        else
        {
        }
    }

    private void Update()
    {
        if (localPlayer == null)
        {
            FindLocalPlayer();
            return;
        }

        if (activeRunner == null || activeCyclist == null)
        {
            activeRunner  = localPlayer.GetComponentInChildren<PlayerController>();
            activeCyclist = localPlayer.GetComponentInChildren<CyclingController>();
        }

        ApplyInput();
    }

    private void FindLocalPlayer()
    {
        NetworkPlayer[] players = FindObjectsByType<NetworkPlayer>(FindObjectsSortMode.None);
        foreach (var player in players)
        {
            if (player.IsOwner)
            {
                localPlayer   = player.gameObject;
                activeRunner  = localPlayer.GetComponentInChildren<PlayerController>();
                activeCyclist = localPlayer.GetComponentInChildren<CyclingController>();

                // ── Dust puff: find feet transform ──
                Transform feet = localPlayer.transform.Find("PlayereronFoot") 
                            ?? localPlayer.transform;
                if (DustPuffEffect.Instance != null)
                    DustPuffEffect.Instance.SetPlayerFeet(feet);
                // ────────────────────────────────────
                if (FoamTrailEffect.Instance != null)
                FoamTrailEffect.Instance.SetPlayerFeet(feet);

                break;
            }
        }
    }

    private void ApplyInput()
    {
        Vector2 moveInput = Vector2.zero;
        bool sprint = false;

        // Keyboard/Gamepad always checked first
        if (moveAction != null)
        {
            moveInput = moveAction.ReadValue<Vector2>();
            if (sprintAction != null)
                sprint = sprintAction.IsPressed();
        }

        // Mobile joystick only overrides if keyboard gives no input
        if (useMobileJoystick && mobileJoystick != null && moveInput.magnitude < 0.01f)
        {
            moveInput = mobileJoystick.GetInput();
            sprint = mobileSprintPressed;
        }

        if (moveInput.magnitude < 0.01f)
            moveInput = Vector2.zero;

        bool isSprinting = sprint && moveInput.magnitude > 0.01f;
        bool isMoving    = moveInput.magnitude > 0.01f;

        // Effects
        if (SpeedLinesEffect.Instance != null)
            SpeedLinesEffect.Instance.SetSprinting(isSprinting);

        if (DustPuffEffect.Instance != null)
            DustPuffEffect.Instance.UpdateMovement(isMoving, isSprinting);
        
        // ── Foam trail ──
        if (FoamTrailEffect.Instance != null)
            FoamTrailEffect.Instance.UpdateMovement(isMoving, isSprinting);
        // ────────────────

        // Apply to controllers
        if (activeRunner != null && activeRunner.isActiveModel)
        {
            activeRunner.moveVector = moveInput;
            activeRunner.isRunning  = sprint;
        }
        else if (activeCyclist != null && activeCyclist.isActiveModel)
        {
            activeCyclist.moveInput      = moveInput;
            activeCyclist.isAccelerating = sprint;
        }
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus) ResetAllInput();
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        if (!hasFocus) ResetAllInput();
    }

    private void ResetAllInput()
    {
        if (activeRunner != null)
        {
            activeRunner.moveVector = Vector2.zero;
            activeRunner.isRunning  = false;
        }

        if (activeCyclist != null)
        {
            activeCyclist.moveInput      = Vector2.zero;
            activeCyclist.isAccelerating = false;
        }

        mobileSprintPressed = false;
    }

    public void OnMobileSprintPressed()  => mobileSprintPressed = true;
    public void OnMobileSprintReleased() => mobileSprintPressed = false;

    void OnGUI()
    {
#if UNITY_ANDROID || UNITY_IOS
        if (!showMobileDebugGUI || !useMobileJoystick) return;

        GUILayout.BeginArea(new Rect(10, Screen.height - 250, 450, 250));
        GUI.backgroundColor = new Color(0, 0, 0, 0.8f);
        GUILayout.BeginVertical("box");

        GUILayout.Label("=== MOBILE INPUT DEBUG ===");

        Vector2 joyInput = mobileJoystick?.GetInput() ?? Vector2.zero;
        GUILayout.Label($"Joystick: X={joyInput.x:F3}, Y={joyInput.y:F3}");
        GUILayout.Label($"Sprint: {mobileSprintPressed}");

        if (GUILayout.Button("Force Reset Input", GUILayout.Height(40)))
            ResetAllInput();

        GUILayout.EndVertical();
        GUILayout.EndArea();
#endif
    }
}
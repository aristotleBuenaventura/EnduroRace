// RaceManager.cs
using FishNet.Object;
using FishNet.Object.Synchronizing;
using FishNet.Connection;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class RaceManager : NetworkBehaviour
{
    public enum Segment { Swim, Bike, Run, Finished }

    [System.Serializable]
    public class PlayerRaceData
    {
        public NetworkConnection connection;
        public Segment currentSegment = Segment.Swim;
        public float raceTime = 0f;
        public bool hasFinished = false;

        public PlayerRaceData(NetworkConnection conn)
        {
            connection = conn;
        }
    }

    [Header("UI")]
    public TMP_Text raceTimerText;
    public TMP_Text segmentText;
    public TMP_Text countdownText;

    [Header("Countdown")]
    public float countdownTime = 3f;
    public Camera cutsceneCamera;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip   countdownClip;

    [Header("AI")]
    public NetworkedAIManager aiManager;

    [Header("Tips UI")]
    public TipsAndTricksUI tipsUI;

    [Header("Settings")]
    public InRaceSettingsPanel settingsPanel;
    public Button settingsButton;

    [Header("Podium")]
    public PodiumManager podiumManager;

    private readonly SyncVar<float> countdownTimer  = new SyncVar<float>(0f);
    private readonly SyncVar<bool>  countdownActive  = new SyncVar<bool>(false);
    private readonly SyncVar<bool>  raceStarted      = new SyncVar<bool>(false);

    private Dictionary<NetworkConnection, PlayerRaceData> playerData
        = new Dictionary<NetworkConnection, PlayerRaceData>();

    private Segment mySegment  = Segment.Swim;
    private float   myRaceTime = 0f;

    // Server-side finish tracking
    private int finishPlacementCounter  = 0;
    private int totalExpectedFinishers  = 0;

    public float   GetRaceTime()  => myRaceTime;
    public Segment CurrentSegment => mySegment;

    // ── Lifecycle ─────────────────────────────────────────────────────────────

    public override void OnStartServer()
    {
        base.OnStartServer();
        Debug.Log("[RaceManager] Server started");
        ServerManager.OnRemoteConnectionState += OnPlayerConnectionChanged;
        StartCoroutine(WaitAndStartCountdown());
    }

    public override void OnStartClient()
    {
        base.OnStartClient();

        countdownActive.OnChange += OnCountdownActiveChanged;
        raceStarted.OnChange     += OnRaceStartedChanged;

        mySegment = Segment.Swim;
        UpdateSegmentUI();
        UpdateTimerUI();

        if (tipsUI != null) tipsUI.Show();

        if (settingsButton != null && settingsPanel != null)
            settingsButton.onClick.AddListener(() => settingsPanel.Toggle());
    }

    private void OnDestroy()
    {
        if (IsServerInitialized)
            ServerManager.OnRemoteConnectionState -= OnPlayerConnectionChanged;

        countdownActive.OnChange -= OnCountdownActiveChanged;
        raceStarted.OnChange     -= OnRaceStartedChanged;

        if (settingsButton != null)
            settingsButton.onClick.RemoveAllListeners();
    }

    private void OnPlayerConnectionChanged(
        NetworkConnection conn,
        FishNet.Transporting.RemoteConnectionStateArgs args)
    {
        if (!IsServerInitialized) return;

        if (args.ConnectionState == FishNet.Transporting.RemoteConnectionState.Started)
        {
            if (!playerData.ContainsKey(conn))
            {
                playerData[conn] = new PlayerRaceData(conn);
                Debug.Log($"[RaceManager] Player {conn.ClientId} registered");
            }
        }
        else if (args.ConnectionState == FishNet.Transporting.RemoteConnectionState.Stopped)
        {
            if (playerData.ContainsKey(conn))
            {
                playerData.Remove(conn);
                Debug.Log($"[RaceManager] Player {conn.ClientId} removed");
            }
        }
    }

    // ── Countdown ─────────────────────────────────────────────────────────────

    private IEnumerator WaitAndStartCountdown()
    {
        if (!IsServerInitialized) yield break;

        while (cutsceneCamera != null && cutsceneCamera.enabled)
            yield return null;

        Debug.Log("[RaceManager] Waiting 2 seconds for clients to connect...");
        yield return new WaitForSeconds(2f);

        StartCountdown();
    }

    [Server]
    private void StartCountdown()
    {
        if (countdownActive.Value) return;
        StartCoroutine(CountdownRoutine());
    }

    [Server]
    private IEnumerator CountdownRoutine()
    {
        countdownActive.Value  = true;
        countdownTimer.Value   = countdownTime;
        finishPlacementCounter = 0;

        // Snapshot expected human finisher count at race start
        totalExpectedFinishers = playerData.Count;
        Debug.Log($"[RaceManager] Countdown started — expecting {totalExpectedFinishers} human finisher(s)");

        DisableAllPlayerInputRpc();

        yield return new WaitForSeconds(0.1f);

        PlayCountdownSoundRpc();

        while (countdownTimer.Value > 0f)
        {
            countdownTimer.Value -= Time.deltaTime;
            yield return null;
        }

        countdownTimer.Value = 0f;
        yield return new WaitForSeconds(1f);

        countdownActive.Value = false;

        EnableAllPlayerInputRpc();

        if (aiManager != null)
            aiManager.StartRace();
        else
            Debug.LogWarning("[RaceManager] No AI Manager assigned - AI won't start!");

        StartRace();
    }

    [Server]
    private void StartRace()
    {
        raceStarted.Value = true;
        Debug.Log("[RaceManager] Race started!");
    }

    // ── Update ────────────────────────────────────────────────────────────────

    private void Update()
    {
        UpdateCountdownUI();

        if (raceStarted.Value && mySegment != Segment.Finished)
        {
            myRaceTime += Time.deltaTime;
            UpdateTimerUI();
        }

        if (IsServerInitialized && raceStarted.Value)
        {
            foreach (var data in playerData.Values)
            {
                if (!data.hasFinished)
                    data.raceTime += Time.deltaTime;
            }
        }
    }

    // ── UI ────────────────────────────────────────────────────────────────────

    private void UpdateCountdownUI()
    {
        if (countdownText == null) return;

        if (countdownActive.Value)
        {
            countdownText.gameObject.SetActive(true);
            countdownText.text = countdownTimer.Value > 0f
                ? Mathf.CeilToInt(countdownTimer.Value).ToString()
                : "GO!";
        }
        else
        {
            countdownText.gameObject.SetActive(false);
        }
    }

    private void UpdateTimerUI()
    {
        if (raceTimerText == null) return;
        int minutes = Mathf.FloorToInt(myRaceTime / 60f);
        int seconds = Mathf.FloorToInt(myRaceTime % 60f);
        raceTimerText.text = $"{minutes:00}:{seconds:00}";
    }

    private void UpdateSegmentUI()
    {
        if (segmentText == null)
        {
            Debug.LogWarning("[RaceManager] Segment Text is not assigned!");
            return;
        }

        switch (mySegment)
        {
            case Segment.Swim:     segmentText.text = "Segment: SWIM"; break;
            case Segment.Bike:     segmentText.text = "Segment: BIKE"; break;
            case Segment.Run:      segmentText.text = "Segment: RUN";  break;
            case Segment.Finished: segmentText.text = "FINISHED!";     break;
        }

        segmentText.gameObject.SetActive(true);
    }

    // ── Segment Progression ───────────────────────────────────────────────────

    public void ProgressToSegment(Segment newSegment)
    {
        Debug.Log($"[RaceManager] Progressing to segment: {newSegment}");
        mySegment = newSegment;
        UpdateSegmentUI();
        ServerUpdatePlayerSegment(newSegment);
    }

    [ServerRpc(RequireOwnership = false)]
    private void ServerUpdatePlayerSegment(Segment newSegment, NetworkConnection sender = null)
    {
        if (sender == null || !playerData.ContainsKey(sender)) return;

        playerData[sender].currentSegment = newSegment;

        // hasFinished is owned by ServerRpcPlayerFinished — only log segment here
        Debug.Log($"[RaceManager] Player {sender.ClientId} → {newSegment}");
    }

    // ── Finish Race ───────────────────────────────────────────────────────────

    public void FinishRace()
    {
        ProgressToSegment(Segment.Finished);
        CoinManager.Instance?.SaveCoinsToFirebase();
        Debug.Log("[RaceManager] Race finished — coins saved");

        // Report finish time to live ranking display
        MultiplayerRaceRanking rankingSystem = FindFirstObjectByType<MultiplayerRaceRanking>();
        NetworkPlayer localPlayer = null;
        NetworkPlayer[] players = FindObjectsByType<NetworkPlayer>(FindObjectsSortMode.None);
        foreach (var player in players)
        {
            if (player.IsOwner) { localPlayer = player; break; }
        }

        if (rankingSystem != null && localPlayer != null)
            rankingSystem.ReportFinish(localPlayer, myRaceTime);

        // Tell the server this player is done — server assigns placement
        // and triggers the global podium broadcast once everyone finishes
        ServerRpcPlayerFinished(myRaceTime);
    }

    // ── Server: player finish handling ────────────────────────────────────────

    [ServerRpc(RequireOwnership = false)]
    private void ServerRpcPlayerFinished(float raceTime, NetworkConnection sender = null)
    {
        if (sender == null || !playerData.ContainsKey(sender)) return;

        // Guard against double-call
        if (playerData[sender].hasFinished)
        {
            Debug.LogWarning($"[RaceManager] Player {sender.ClientId} already marked finished — ignoring duplicate");
            return;
        }

        playerData[sender].hasFinished = true;
        playerData[sender].raceTime    = raceTime;
        finishPlacementCounter++;

        int placement = finishPlacementCounter;
        Debug.Log($"[RaceManager] Player {sender.ClientId} finished {placement} place — time: {raceTime:F2}s");

        // Find this player's NetworkPlayer so we can TargetRpc them
        NetworkPlayer finishedPlayer = null;
        NetworkPlayer[] allPlayers = FindObjectsByType<NetworkPlayer>(FindObjectsSortMode.None);
        foreach (var np in allPlayers)
        {
            if (np.Owner == sender) { finishedPlayer = np; break; }
        }

        // Send personal result immediately to that client only
        if (podiumManager != null && finishedPlayer != null)
            podiumManager.NotifyPlayerFinished(finishedPlayer, placement, raceTime);
        else
            Debug.LogWarning("[RaceManager] PodiumManager or NetworkPlayer not found for TargetRpc");

        CheckAllFinished();
    }

    [Server]
    private void CheckAllFinished()
    {
        foreach (var data in playerData.Values)
        {
            if (!data.hasFinished) return;
        }

        Debug.Log("[RaceManager] All players finished — triggering podium");

        if (podiumManager != null)
            podiumManager.ShowPodium(finishPlacementCounter, 0f);
        else
            Debug.LogError("[RaceManager] PodiumManager not assigned on RaceManager!");
    }

    // ── SyncVar Callbacks ─────────────────────────────────────────────────────

    private void OnCountdownActiveChanged(bool prev, bool next, bool asServer)
    {
        Debug.Log($"[RaceManager] Countdown active: {next}");
        if (next && tipsUI != null)
            tipsUI.Hide();
    }

    private void OnRaceStartedChanged(bool prev, bool next, bool asServer)
    {
        Debug.Log($"[RaceManager] Race started: {next}");
    }

    // ── RPCs ──────────────────────────────────────────────────────────────────

    [ObserversRpc]
    private void PlayCountdownSoundRpc()
    {
        if (audioSource == null || countdownClip == null) return;
        audioSource.clip = countdownClip;
        audioSource.Play();
    }

    [ObserversRpc]
    private void DisableAllPlayerInputRpc()
    {
        Debug.Log("[RaceManager] Disabling player movement");
        NetworkPlayer[] players = FindObjectsByType<NetworkPlayer>(FindObjectsSortMode.None);
        foreach (var player in players)
        {
            if (player.IsOwner)
            {
                player.ServerSetCanMove(false);
                Debug.Log("[RaceManager] Movement locked for local player");
                break;
            }
        }
    }

    [ObserversRpc]
    private void EnableAllPlayerInputRpc()
    {
        Debug.Log("[RaceManager] Enabling player movement");
        CoinManager.Instance?.ResetSessionCoins();
        NetworkPlayer[] players = FindObjectsByType<NetworkPlayer>(FindObjectsSortMode.None);
        foreach (var player in players)
        {
            if (player.IsOwner)
            {
                player.ServerSetCanMove(true);
                Debug.Log("[RaceManager] Movement unlocked for local player");
                break;
            }
        }
    }

    // ── Helpers ───────────────────────────────────────────────────────────────

    public void OpenTipsPanel()
    {
        if (tipsUI != null) tipsUI.Show();
    }

    [Server]
    public List<PlayerRaceData> GetLeaderboard()
    {
        var leaderboard = new List<PlayerRaceData>(playerData.Values);
        leaderboard.Sort((a, b) => a.raceTime.CompareTo(b.raceTime));
        return leaderboard;
    }
}
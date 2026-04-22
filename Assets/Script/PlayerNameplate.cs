using UnityEngine;
using TMPro;
using FishNet.Object;

/// <summary>
/// Attach this to your player prefab (same GameObject as NetworkPlayer or a child).
/// It reads the PlayerName SyncVar and displays it in world space above the player's head.
/// The nameplate always faces the camera (billboard effect).
/// </summary>
public class PlayerNameplate : NetworkBehaviour
{
    [Header("Nameplate Settings")]
    [SerializeField] private Vector3 offset = new Vector3(0f, 2.2f, 0f);
    [SerializeField] private float textSize = 3f;
    [SerializeField] private Color nameColor = Color.white;
    [SerializeField] private Color localPlayerColor = Color.yellow; // own name shows in yellow

    private NetworkPlayer networkPlayer;
    private GameObject nameplateObject;
    private TextMeshPro nameText;
    private Camera mainCamera;

    public override void OnStartClient()
    {
        base.OnStartClient();

        networkPlayer = GetComponent<NetworkPlayer>();
        if (networkPlayer == null)
            networkPlayer = GetComponentInParent<NetworkPlayer>();

        if (networkPlayer == null)
        {
            Debug.LogError("[PlayerNameplate] NetworkPlayer not found!");
            return;
        }

        CreateNameplate();

        // Listen for name changes so it updates even if name arrives late
        networkPlayer.PlayerName.OnChange += OnNameChanged;

        // Set name immediately if already available
        if (!string.IsNullOrEmpty(networkPlayer.PlayerName.Value))
            UpdateNameplate(networkPlayer.PlayerName.Value);

        // If this is our own player, set name from LobbyDataTransfer
        if (IsOwner)
            SetNameFromLobbyData();

        mainCamera = Camera.main;
    }

    private void OnDestroy()
    {
        if (networkPlayer != null)
            networkPlayer.PlayerName.OnChange -= OnNameChanged;
    }

    private void CreateNameplate()
    {
        // Create a world-space TextMeshPro object
        nameplateObject = new GameObject("Nameplate");
        nameplateObject.transform.SetParent(transform);
        nameplateObject.transform.localPosition = offset;
        nameplateObject.transform.localRotation = Quaternion.identity;

        nameText = nameplateObject.AddComponent<TextMeshPro>();
        nameText.alignment = TextAlignmentOptions.Center;
        nameText.fontSize = textSize;
        nameText.color = IsOwner ? localPlayerColor : nameColor;
        nameText.text = "";

        // Make it render on top (optional — remove if you want depth occlusion)
        nameText.fontMaterial.renderQueue = 3000;
    }

    private void SetNameFromLobbyData()
    {
        if (LobbyDataTransfer.Instance == null)
        {
            Debug.LogWarning("[PlayerNameplate] LobbyDataTransfer not found!");
            return;
        }

        var localData = LobbyDataTransfer.Instance.GetLocalPlayerData();
        if (localData == null)
        {
            Debug.LogWarning("[PlayerNameplate] Local player data not found in LobbyDataTransfer!");
            return;
        }

        Debug.Log($"[PlayerNameplate] Setting name: {localData.displayName}");
        networkPlayer.ServerSetPlayerName(localData.displayName);
    }

    private void OnNameChanged(string prev, string next, bool asServer)
    {
        UpdateNameplate(next);
    }

    private void UpdateNameplate(string playerName)
    {
        if (nameText == null) return;
        nameText.text = playerName;
    }

    private void LateUpdate()
    {
        if (nameplateObject == null) return;

        // Billboard — always face the camera
        if (mainCamera == null)
            mainCamera = Camera.main;

        if (mainCamera != null)
            nameplateObject.transform.rotation = Quaternion.LookRotation(
                nameplateObject.transform.position - mainCamera.transform.position
            );
    }
}
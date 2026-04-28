using UnityEngine;
using TMPro;
using UnityEngine.UI;
using FishNet.Object;

public class PlayerNameplate : NetworkBehaviour
{
    [Header("Settings")]
    [SerializeField] private Vector3 offset = new Vector3(0f, 2.2f, 0f);
    [SerializeField] private float paddingX = 0.25f;
    [SerializeField] private float paddingY = 0.12f;

    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI frontText;
    [SerializeField] private Image frontBackground;

    [SerializeField] private TextMeshProUGUI backText;
    [SerializeField] private Image backBackground;

    private RectTransform frontBgRect;
    private RectTransform backBgRect;

    private NetworkPlayer networkPlayer;
    private GameObject root;
    private Camera mainCamera;

    private string currentName;

    public override void OnStartClient()
    {
        base.OnStartClient();

        networkPlayer = GetComponent<NetworkPlayer>();
        if (networkPlayer == null)
            networkPlayer = GetComponentInParent<NetworkPlayer>();

        if (networkPlayer == null)
            return;

        CreateNameplate();

        networkPlayer.PlayerName.OnChange += OnNameChanged;

        if (!string.IsNullOrEmpty(networkPlayer.PlayerName.Value))
            UpdateNameplate(networkPlayer.PlayerName.Value);

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
        root = new GameObject("NameplateRoot");
        root.transform.SetParent(transform);
        root.transform.localPosition = offset;
        root.transform.localRotation = Quaternion.identity;

        if (frontText == null || backText == null || frontBackground == null || backBackground == null)
            return;

        frontBgRect = frontBackground.GetComponent<RectTransform>();
        backBgRect = backBackground.GetComponent<RectTransform>();

        frontText.enableWordWrapping = false;
        backText.enableWordWrapping = false;

        frontText.overflowMode = TextOverflowModes.Overflow;
        backText.overflowMode = TextOverflowModes.Overflow;

        frontText.alignment = TextAlignmentOptions.Center;
        backText.alignment = TextAlignmentOptions.Center;

        frontBackground.color = Color.white;
        backBackground.color = Color.white;

        frontText.color = Color.white;
        backText.color = Color.white;
    }

    private void SetNameFromLobbyData()
    {
        if (LobbyDataTransfer.Instance == null)
            return;

        var localData = LobbyDataTransfer.Instance.GetLocalPlayerData();
        if (localData == null)
            return;

        networkPlayer.ServerSetPlayerName(localData.displayName);
    }

    private void OnNameChanged(string prev, string next, bool asServer)
    {
        UpdateNameplate(next);
    }

    private void UpdateNameplate(string playerName)
    {
        if (frontText == null || backText == null) return;

        currentName = playerName;

        frontText.text = playerName;
        backText.text = playerName;

        frontText.color = Color.white;
        backText.color = Color.white;

        Canvas.ForceUpdateCanvases();
        LayoutRebuild();
    }

    private void LayoutRebuild()
    {
        frontText.ForceMeshUpdate();

        Vector2 size = frontText.GetPreferredValues(currentName);

        float width = size.x + paddingX;
        float height = size.y + paddingY;

        SetBg(frontBackground, frontBgRect, width, height);
        SetBg(backBackground, backBgRect, width, height);
    }

    private void SetBg(Image img, RectTransform rect, float w, float h)
    {
        if (img == null || rect == null) return;

        rect.sizeDelta = new Vector2(w, h);
        img.color = Color.white;
    }

    private void LateUpdate()
    {
        if (root == null) return;

        if (mainCamera == null)
            mainCamera = Camera.main;

        if (mainCamera == null) return;

        root.transform.position = transform.position + offset;

        Vector3 dir = (mainCamera.transform.position - root.transform.position).normalized;

        root.transform.rotation = Quaternion.LookRotation(dir, Vector3.up);
    }
}
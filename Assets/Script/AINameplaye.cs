using UnityEngine;
using TMPro;
using UnityEngine.UI;
using FishNet.Object;

public class AINameplaye : NetworkBehaviour
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
    private NetworkedAIOpponent aiOpponent;
    private GameObject root;
    private Camera mainCamera;
    private string currentName;

    public override void OnStartClient()
    {
        base.OnStartClient();

        aiOpponent = GetComponent<NetworkedAIOpponent>();
        if (aiOpponent == null)
            aiOpponent = GetComponentInParent<NetworkedAIOpponent>();

        if (aiOpponent == null)
            return;

        CreateNameplate();
        UpdateNameplate(aiOpponent.opponentName);
        mainCamera = Camera.main;
    }

    private void CreateNameplate()
    {
        root = new GameObject("AINameplateRoot");
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
    }

    private void UpdateNameplate(string aiName)
    {
        if (frontText == null || backText == null)
            return;

        if (string.IsNullOrWhiteSpace(aiName))
            aiName = "Opponent";

        currentName = aiName;
        frontText.text = aiName;
        backText.text = aiName;

        Canvas.ForceUpdateCanvases();
        LayoutRebuild();
    }

    private void LayoutRebuild()
    {
        frontText.ForceMeshUpdate();
        Vector2 size = frontText.GetPreferredValues(currentName);

        float width = size.x + paddingX;
        float height = size.y + paddingY;

        SetBackground(frontBgRect, width, height);
        SetBackground(backBgRect, width, height);
    }

    private static void SetBackground(RectTransform rect, float width, float height)
    {
        if (rect == null)
            return;

        rect.sizeDelta = new Vector2(width, height);
    }

    private void LateUpdate()
    {
        if (root == null)
            return;

        if (mainCamera == null)
            mainCamera = Camera.main;

        if (mainCamera == null)
            return;

        if (aiOpponent != null && currentName != aiOpponent.opponentName)
            UpdateNameplate(aiOpponent.opponentName);

        root.transform.position = transform.position + offset;

        Vector3 direction = (mainCamera.transform.position - root.transform.position).normalized;
        root.transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
    }
}

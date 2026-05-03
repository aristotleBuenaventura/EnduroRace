using UnityEngine;

public class AIColors : MonoBehaviour
{
    public const int PaletteCount = 20;

    [Header("Color Targets (3 GameObjects)")]
    [SerializeField] private GameObject colorTarget1;
    [SerializeField] private GameObject colorTarget2;
    [SerializeField] private GameObject colorTarget3;

    [Header("Color Settings")]
    [SerializeField] private bool includeChildrenRenderers = true;

    private static readonly Color[] ColorPalette = new Color[]
    {
        new Color32(58, 46, 73, 255),   // dark violet
        new Color32(42, 66, 98, 255),   // steel blue
        new Color32(36, 78, 82, 255),   // deep teal
        new Color32(63, 73, 46, 255),   // olive
        new Color32(74, 49, 40, 255),   // chestnut
        new Color32(49, 55, 66, 255),   // slate
        new Color32(72, 57, 92, 255),   // muted purple
        new Color32(39, 71, 64, 255),   // pine
        new Color32(84, 64, 38, 255),   // bronze
        new Color32(63, 42, 52, 255),   // wine
        new Color32(55, 68, 39, 255),   // moss
        new Color32(44, 49, 78, 255),   // indigo slate
        new Color32(88, 70, 56, 255),   // clay
        new Color32(46, 58, 48, 255),   // forest gray
        new Color32(70, 53, 67, 255),   // dusty plum
        new Color32(36, 63, 74, 255),   // ocean dusk
        new Color32(77, 60, 35, 255),   // dark ochre
        new Color32(57, 43, 41, 255),   // cocoa
        new Color32(41, 54, 60, 255),   // blue gray
        new Color32(66, 61, 43, 255),   // faded olive
    };

    private NetworkedAIOpponent aiOpponent;
    private int appliedColorIndex = -1;

    private void Awake()
    {
        aiOpponent = GetComponent<NetworkedAIOpponent>();
        if (aiOpponent == null)
            aiOpponent = GetComponentInParent<NetworkedAIOpponent>();
    }

    private void OnEnable()
    {
        TryApplySyncedColor();
    }

    private void Update()
    {
        TryApplySyncedColor();
    }

    private void TryApplySyncedColor()
    {
        if (aiOpponent == null)
        {
            aiOpponent = GetComponent<NetworkedAIOpponent>();
            if (aiOpponent == null)
                aiOpponent = GetComponentInParent<NetworkedAIOpponent>();
        }

        if (aiOpponent == null)
            return;

        int syncedIndex = aiOpponent.ColorPaletteIndex;
        if (syncedIndex < 0)
            return;

        int paletteIndex = syncedIndex % ColorPalette.Length;
        if (paletteIndex < 0)
            paletteIndex += ColorPalette.Length;

        if (appliedColorIndex == paletteIndex)
            return;

        appliedColorIndex = paletteIndex;
        ApplyColorToTargets(ColorPalette[paletteIndex]);
    }

    private void ApplyColorToTargets(Color colorValue)
    {
        ApplyColorToTarget(colorTarget1, colorValue);
        ApplyColorToTarget(colorTarget2, colorValue);
        ApplyColorToTarget(colorTarget3, colorValue);
    }

    private void ApplyColorToTarget(GameObject target, Color colorValue)
    {
        if (target == null)
            return;

        Renderer[] renderers = includeChildrenRenderers
            ? target.GetComponentsInChildren<Renderer>(true)
            : target.GetComponents<Renderer>();

        for (int i = 0; i < renderers.Length; i++)
        {
            Renderer currentRenderer = renderers[i];
            if (currentRenderer == null)
                continue;

            Material[] mats = currentRenderer.materials;
            for (int j = 0; j < mats.Length; j++)
            {
                Material mat = mats[j];
                if (mat == null)
                    continue;

                if (mat.HasProperty("_BaseColor"))
                    mat.SetColor("_BaseColor", colorValue);
                else if (mat.HasProperty("_Color"))
                    mat.color = colorValue;
            }
        }
    }
}

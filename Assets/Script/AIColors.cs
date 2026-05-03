using UnityEngine;

public class AIColors : MonoBehaviour
{
    public const int PaletteCount = 20;

    [Header("Color Targets (3 GameObjects)")]
    [SerializeField] private GameObject colorTarget1;
    [SerializeField] private GameObject colorTarget2;
    [SerializeField] private GameObject colorTarget3;

    [Header("Material Settings")]
    [SerializeField] private bool includeChildrenRenderers = true;
    [SerializeField] private Material[] aiMaterials = new Material[PaletteCount];
    [SerializeField] private string replaceMaterialNameContains = "AICOLOR";

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

        if (aiMaterials == null || aiMaterials.Length < PaletteCount)
        {
            Debug.LogWarning($"[AIColors] Please assign {PaletteCount} materials on {gameObject.name}.", this);
            return;
        }

        int paletteLength = PaletteCount;
        int paletteIndex = syncedIndex % paletteLength;
        if (paletteIndex < 0)
            paletteIndex += paletteLength;

        if (appliedColorIndex == paletteIndex)
            return;

        Material selectedMaterial = aiMaterials[paletteIndex];
        if (selectedMaterial == null)
        {
            Debug.LogWarning($"[AIColors] Missing material at index {paletteIndex} on {gameObject.name}.", this);
            return;
        }

        appliedColorIndex = paletteIndex;
        ApplyMaterialToTargets(selectedMaterial);
        Debug.Log($"[AIColors] Applied palette index {paletteIndex} to {gameObject.name}.", this);
    }

    private void ApplyMaterialToTargets(Material material)
    {
        ApplyMaterialToTarget(colorTarget1, material, "target1");
        ApplyMaterialToTarget(colorTarget2, material, "target2");
        ApplyMaterialToTarget(colorTarget3, material, "target3");
    }

    private void ApplyMaterialToTarget(GameObject target, Material material, string targetLabel)
    {
        if (target == null)
        {
            Debug.LogWarning($"[AIColors] {targetLabel} is not assigned on {gameObject.name}.", this);
            return;
        }

        Renderer[] renderers = includeChildrenRenderers
            ? target.GetComponentsInChildren<Renderer>(true)
            : target.GetComponents<Renderer>();

        if (renderers == null || renderers.Length == 0)
        {
            Debug.LogWarning($"[AIColors] No renderers found on {targetLabel} ({target.name}) for {gameObject.name}.", this);
            return;
        }

        for (int i = 0; i < renderers.Length; i++)
        {
            Renderer currentRenderer = renderers[i];
            if (currentRenderer == null)
                continue;

            Material[] existing = currentRenderer.sharedMaterials;
            if (existing == null || existing.Length == 0)
            {
                currentRenderer.sharedMaterial = material;
                continue;
            }

            Material[] replaced = new Material[existing.Length];
            bool hasReplacement = false;
            string keyword = replaceMaterialNameContains ?? string.Empty;

            for (int j = 0; j < existing.Length; j++)
            {
                Material current = existing[j];
                bool matchesKeyword =
                    !string.IsNullOrEmpty(keyword)
                    && current != null
                    && current.name.IndexOf(keyword, System.StringComparison.OrdinalIgnoreCase) >= 0;

                if (matchesKeyword)
                {
                    replaced[j] = material;
                    hasReplacement = true;
                }
                else
                {
                    replaced[j] = current;
                }
            }

            if (hasReplacement)
                currentRenderer.sharedMaterials = replaced;
        }
    }
}

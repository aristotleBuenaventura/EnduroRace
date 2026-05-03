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
    }

    private void ApplyMaterialToTargets(Material material)
    {
        ApplyMaterialToTarget(colorTarget1, material);
        ApplyMaterialToTarget(colorTarget2, material);
        ApplyMaterialToTarget(colorTarget3, material);
    }

    private void ApplyMaterialToTarget(GameObject target, Material material)
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

            Material[] existing = currentRenderer.sharedMaterials;
            if (existing == null || existing.Length == 0)
            {
                currentRenderer.sharedMaterial = material;
                continue;
            }

            Material[] replaced = new Material[existing.Length];
            for (int j = 0; j < replaced.Length; j++)
                replaced[j] = material;

            currentRenderer.sharedMaterials = replaced;
        }
    }
}

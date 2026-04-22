using UnityEngine;
using FishNet.Object;
using FishNet.Object.Synchronizing;

/// <summary>
/// Handles Male / Male2 appearance:
///   1. Team color tint (from OwnerId — same as before)
///   2. Outfit material swap (Nike / Adidas / Underarmor etc.)
///      The equipped item's material is used as the base, then the team color
///      is applied on top via _BaseColor so every player looks distinct.
/// </summary>
public class PlayerAppearance : NetworkBehaviour
{
    [Header("On-Foot Cloth")]
    public SkinnedMeshRenderer footRenderer;
    public int footClothMaterialIndex = 0;

    [Header("Bike Outfit")]
    public SkinnedMeshRenderer jerkinRenderer;
    public int jerkinMaterialIndex = 0;
    public SkinnedMeshRenderer shotsRenderer;
    public int shotsMaterialIndex = 0;

    [Header("Color Property")]
    public string colorProperty = "_BaseColor";

    // ── SyncVars ──────────────────────────────────────────────────────────────

    private readonly SyncVar<Color> _clothColor = new SyncVar<Color>(
        new SyncTypeSettings(WritePermission.ServerOnly, ReadPermission.Observers)
    );

    // Synced item id — empty string means "use default (no outfit swap)"
    private readonly SyncVar<string> _equippedItemId = new SyncVar<string>(
        new SyncTypeSettings(WritePermission.ServerOnly, ReadPermission.Observers)
    );

    // ── Team Colors ───────────────────────────────────────────────────────────

    private static readonly Color[] TeamColors = new Color[]
    {
        new Color(0.9f, 0.2f, 0.2f),
        new Color(0.2f, 0.4f, 0.9f),
        new Color(0.2f, 0.8f, 0.3f),
        new Color(0.9f, 0.8f, 0.1f),
        new Color(0.8f, 0.3f, 0.9f),
        new Color(0.95f, 0.5f, 0.1f),
        new Color(0.1f, 0.8f, 0.8f),
        new Color(0.9f, 0.3f, 0.6f),
    };

    // ── Network Callbacks ─────────────────────────────────────────────────────

    public override void OnStartNetwork()
    {
        base.OnStartNetwork();
        _clothColor.OnChange     += OnClothColorChanged;
        _equippedItemId.OnChange += OnEquippedItemChanged;
    }

    public override void OnStopNetwork()
    {
        base.OnStopNetwork();
        _clothColor.OnChange     -= OnClothColorChanged;
        _equippedItemId.OnChange -= OnEquippedItemChanged;
    }

    public override void OnStartServer()
    {
        base.OnStartServer();
        int colorIndex = (int)(OwnerId % TeamColors.Length);
        _clothColor.Value = TeamColors[colorIndex];
        _equippedItemId.Value = "";   // server will get updated by owner RPC below
    }

    public override void OnStartClient()
    {
        base.OnStartClient();

        // Apply whatever is already synced
        ApplyAppearance(_clothColor.Value, _equippedItemId.Value);

        // Owner tells server which item they have equipped
        if (IsOwner)
        {
            string equipped = PlayerPrefs.GetString("EquippedItem", "");
            ServerSetEquippedItem(equipped);
        }
    }

    // ── SyncVar Change Handlers ───────────────────────────────────────────────

    private void OnClothColorChanged(Color prev, Color next, bool asServer)
        => ApplyAppearance(next, _equippedItemId.Value);

    private void OnEquippedItemChanged(string prev, string next, bool asServer)
        => ApplyAppearance(_clothColor.Value, next);

    // ── Server RPC ────────────────────────────────────────────────────────────

    /// <summary>
    /// Owner calls this to tell the server (and all observers) which item is equipped.
    /// </summary>
    [ServerRpc(RequireOwnership = true)]
    public void ServerSetEquippedItem(string itemId)
    {
        _equippedItemId.Value = itemId ?? "";
    }

    // ── Appearance Application ────────────────────────────────────────────────

    private void ApplyAppearance(Color teamColor, string itemId)
    {
        ShopItem item = EquipmentManager.Instance != null
            ? EquipmentManager.Instance.FindItem(itemId)
            : null;

        // On-foot renderer
        ApplyToRenderer(footRenderer,   footClothMaterialIndex,
                        item?.maleFootMaterial,   teamColor);

        // Bike jerkin
        ApplyToRenderer(jerkinRenderer, jerkinMaterialIndex,
                        item?.maleJerkinMaterial, teamColor);

        // Bike shorts
        ApplyToRenderer(shotsRenderer,  shotsMaterialIndex,
                        item?.maleShotsMaterial,  teamColor);
    }

    /// <summary>
    /// If an outfit material exists, swap it in and tint with team color.
    /// If no outfit, just apply team color to the existing material.
    /// </summary>
    private void ApplyToRenderer(SkinnedMeshRenderer renderer, int matIndex,
                                  Material outfitMaterial, Color teamColor)
    {
        if (renderer == null) return;

        // ── Material swap ──────────────────────────────────────────────────
        if (outfitMaterial != null)
        {
            // Build a material array with the outfit mat slotted in
            Material[] mats = renderer.materials;
            if (matIndex < mats.Length)
            {
                // Instance the material so we don't modify the shared asset
                mats[matIndex] = new Material(outfitMaterial);
                renderer.materials = mats;
            }
        }

        // ── Team color tint on top (property block — non-destructive) ──────
        var block = new MaterialPropertyBlock();
        renderer.GetPropertyBlock(block, matIndex);
        block.SetColor(colorProperty, teamColor);
        renderer.SetPropertyBlock(block, matIndex);
    }
}
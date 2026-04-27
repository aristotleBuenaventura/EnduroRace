using UnityEngine;
using FishNet.Object;
using FishNet.Object.Synchronizing;

public class PlayerAppearanceFemale : NetworkBehaviour
{
    [Header("On-Foot Body (Female Runner)")]
    public SkinnedMeshRenderer footBodyRenderer;
    public int footBodyMaterialIndex = 0;

    [Header("On-Bike Body (Female Cyclist)")]
    public SkinnedMeshRenderer bikeBodyRenderer;
    public int bikeBodyMaterialIndex = 0;

    [Header("Default Female Texture Variants (team colors)")]
    [Tooltip("One texture per team color, same order as TeamColors")]
    public Texture2D[] footBodyTextures;
    public Texture2D[] bikeBodyTextures;

    [Header("Texture Property")]
    public string textureProperty = "_BaseMap";

    // ── SyncVars ──────────────────────────────────────────────────────────────

    private readonly SyncVar<int> _colorIndex = new SyncVar<int>(
        new SyncTypeSettings(WritePermission.ServerOnly, ReadPermission.Observers)
    );

    private readonly SyncVar<string> _equippedItemId = new SyncVar<string>(
        new SyncTypeSettings(WritePermission.ServerOnly, ReadPermission.Observers)
    );

    // ── Team Colors order (must match male) ───────────────────────────────────

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
        _colorIndex.OnChange     += OnColorIndexChanged;
        _equippedItemId.OnChange += OnEquippedItemChanged;
    }

    public override void OnStopNetwork()
    {
        base.OnStopNetwork();
        _colorIndex.OnChange     -= OnColorIndexChanged;
        _equippedItemId.OnChange -= OnEquippedItemChanged;
    }

    public override void OnStartServer()
    {
        base.OnStartServer();
        _colorIndex.Value     = (int)(OwnerId % TeamColors.Length);
        _equippedItemId.Value = "";
    }

    public override void OnStartClient()
    {
        base.OnStartClient();
        ApplyAppearance(_colorIndex.Value, _equippedItemId.Value);

        if (IsOwner)
        {
            string equipped = PlayerPrefs.GetString("EquippedItem", "");
            ServerSetEquippedItem(equipped);
        }
    }

    // ── SyncVar Handlers ──────────────────────────────────────────────────────

    private void OnColorIndexChanged(int prev, int next, bool asServer)
        => ApplyAppearance(next, _equippedItemId.Value);

    private void OnEquippedItemChanged(string prev, string next, bool asServer)
        => ApplyAppearance(_colorIndex.Value, next);

    // ── Server RPC ────────────────────────────────────────────────────────────

    [ServerRpc(RequireOwnership = true)]
    public void ServerSetEquippedItem(string itemId)
    {
        _equippedItemId.Value = itemId ?? "";
    }

    // ── Appearance Application ────────────────────────────────────────────────

    private void ApplyAppearance(int colorIndex, string itemId)
    {
        if (colorIndex < 0 || colorIndex >= TeamColors.Length) return;

        ShopItem item = EquipmentManager.Instance != null
            ? EquipmentManager.Instance.FindItem(itemId)
            : null;

        bool hasOutfit = item != null;

        ApplyToRenderer(footBodyRenderer, footBodyMaterialIndex,
                        footBodyTextures, colorIndex,
                        item?.femaleFootTexture, hasOutfit);

        ApplyToRenderer(bikeBodyRenderer, bikeBodyMaterialIndex,
                        bikeBodyTextures, colorIndex,
                        item?.femaleBikeTexture, hasOutfit);
    }

    private void ApplyToRenderer(SkinnedMeshRenderer renderer, int matIndex,
                                  Texture2D[] defaultTextures, int colorIndex,
                                  Texture2D outfitTexture, bool hasOutfit)
    {
        if (renderer == null) return;

        var block = new MaterialPropertyBlock();
        renderer.GetPropertyBlock(block, matIndex);

        if (hasOutfit && outfitTexture != null)
        {
            // ✅ Outfit equipped — swap texture, no tint
            block.SetTexture(textureProperty, outfitTexture);
        }
        else
        {
            // ✅ Default — swap to team texture, no tint
            // The texture itself already represents the team color
            if (defaultTextures != null
                && colorIndex < defaultTextures.Length
                && defaultTextures[colorIndex] != null)
            {
                block.SetTexture(textureProperty, defaultTextures[colorIndex]);
            }
            else
            {
            }
        }

        renderer.SetPropertyBlock(block, matIndex);
    }
}
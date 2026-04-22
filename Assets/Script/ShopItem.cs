using UnityEngine;

[CreateAssetMenu(fileName = "NewShopItem", menuName = "Shop/ShopItem")]
public class ShopItem : ScriptableObject
{
    [Header("Identity")]
    public string itemId;           // e.g. "Nike", "Adidas", "Underarmor"
    public string displayName;
    public Sprite previewSprite;    // shown in shop UI
    public int price = 200;

    [Header("Male / Male2 Outfit Materials")]
    public Material maleFootMaterial;    // on-foot cloth (SkinnedMeshRenderer: footRenderer)
    public Material maleJerkinMaterial;  // on-bike jerkin
    public Material maleShotsMaterial;   // on-bike shorts

    [Header("Female Outfit Textures")]
    public Texture2D femaleFootTexture;  // footBodyRenderer _BaseMap
    public Texture2D femaleBikeTexture;  // bikeBodyRenderer _BaseMap

    [Header("Stats")]
    [Tooltip("Flat reduction to stamina decrease rate (e.g. 2 = -2 drain/sec)")]
    public float staminaDecreaseReduction = 0f;

    [Tooltip("Flat bonus added to moveSpeed")]
    public float moveSpeedBonus = 0f;

    [Tooltip("Multiplier on top of runSpeedMultiplier (e.g. 1.1 = +10% sprint)")]
    public float moveSpeedMultiplierBonus = 0f;

    [Tooltip("Flat bonus to swimSpeed")]
    public float swimSpeedBonus = 0f;

    [Tooltip("Flat bonus to cycling moveSpeed")]
    public float cycleSpeedBonus = 0f;
}
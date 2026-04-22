using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopItemCard : MonoBehaviour
{
    [Header("UI References")]
    public Image previewImage;
    public TextMeshProUGUI itemNameText;
    public TextMeshProUGUI statsText;
    public TextMeshProUGUI priceText;
    public Button buyButton;
    public TextMeshProUGUI buyButtonText;

    [Header("State Colors")]
    public Color normalColor   = new Color(0.1f, 0.8f, 0.1f);
    public Color ownedColor    = new Color(0.4f, 0.4f, 0.4f);
    public Color equippedColor = new Color(0.9f, 0.7f, 0.1f);

    private ShopItem item;
    private ShopManager shopManager;

    public void Setup(ShopItem shopItem, bool owned, bool equipped, ShopManager manager)
    {
        item        = shopItem;
        shopManager = manager;

        if (previewImage != null && shopItem.previewSprite != null)
            previewImage.sprite = shopItem.previewSprite;

        if (itemNameText != null)
            itemNameText.text = shopItem.displayName;

        if (statsText != null)
            statsText.text = BuildStatsString(shopItem);

        if (priceText != null)
            priceText.text = owned ? "" : shopItem.price.ToString();

        buyButton?.onClick.RemoveAllListeners();

        if (equipped)
        {
            SetButtonState("EQUIPPED", equippedColor, false);
        }
        else if (owned)
        {
            SetButtonState("OWNED", ownedColor, false);
        }
        else
        {
            SetButtonState("BUY", normalColor, true);
            buyButton?.onClick.AddListener(() => shopManager.TryBuyItem(item));
        }
    }

    private void SetButtonState(string label, Color color, bool interactable)
    {
        if (buyButtonText != null)
            buyButtonText.text = label;

        if (buyButton != null)
        {
            var colors = buyButton.colors;
            colors.normalColor   = color;
            colors.disabledColor = color * 0.7f;
            buyButton.colors     = colors;
            buyButton.interactable = interactable;
        }
    }

    private string BuildStatsString(ShopItem s)
    {
        var sb = new System.Text.StringBuilder();
        if (s.staminaDecreaseReduction != 0f)
            sb.AppendLine($"Stamina Drain -{s.staminaDecreaseReduction}");
        if (s.moveSpeedBonus != 0f)
            sb.AppendLine($"+{s.moveSpeedBonus} Move Speed");
        if (s.moveSpeedMultiplierBonus != 0f)
            sb.AppendLine($"+{s.moveSpeedMultiplierBonus:P0} Sprint Multiplier");
        if (s.swimSpeedBonus != 0f)
            sb.AppendLine($"+{s.swimSpeedBonus} Swim Speed");
        if (s.cycleSpeedBonus != 0f)
            sb.AppendLine($"+{s.cycleSpeedBonus} Cycle Speed");
        return sb.Length > 0 ? sb.ToString().Trim() : "No stat bonus";
    }
}
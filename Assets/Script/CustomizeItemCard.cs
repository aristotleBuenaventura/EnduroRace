using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

/// <summary>
/// Single card in the Customize panel. Shows item name, preview, stats, and an Equip button.
/// </summary>
public class CustomizeItemCard : MonoBehaviour
{
    [Header("UI")]
    public Image previewImage;
    public TextMeshProUGUI itemNameText;
    public TextMeshProUGUI statsText;
    public Button equipButton;
    public TextMeshProUGUI equipButtonText;

    [Header("Colors")]
    public Color equipColor    = new Color(0.1f, 0.6f, 1f);    // blue  - equip
    public Color equippedColor = new Color(0.9f, 0.7f, 0.1f);  // gold  - already equipped

    public void Setup(ShopItem item, bool isEquipped, Action<string> onEquip)
    {
        if (previewImage != null && item.previewSprite != null)
            previewImage.sprite = item.previewSprite;

        if (itemNameText != null)
            itemNameText.text = item.displayName;

        if (statsText != null)
            statsText.text = BuildStatsString(item);

        equipButton?.onClick.RemoveAllListeners();

        if (isEquipped)
        {
            SetButtonState("EQUIPPED", equippedColor, false);
        }
        else
        {
            SetButtonState("EQUIP", equipColor, true);
            equipButton?.onClick.AddListener(() => onEquip(item.itemId));
        }
    }

    public void SetupAsDefault(bool isEquipped, Action onUnequip)
    {
        if (itemNameText != null)
            itemNameText.text = "Default";

        if (statsText != null)
            statsText.text = "Team color only";

        // ✅ Hide the preview image for default
        if (previewImage != null)
            previewImage.gameObject.SetActive(false);

        equipButton?.onClick.RemoveAllListeners();

        if (isEquipped)
            SetButtonState("EQUIPPED", equippedColor, false);
        else
        {
            SetButtonState("EQUIP", equipColor, true);
            equipButton?.onClick.AddListener(() => onUnequip());
        }
    }

    private void SetButtonState(string label, Color color, bool interactable)
    {
        if (equipButtonText != null)
            equipButtonText.text = label;

        if (equipButton != null)
        {
            var colors = equipButton.colors;
            colors.normalColor   = color;
            colors.disabledColor = color * 0.7f;
            equipButton.colors   = colors;
            equipButton.interactable = interactable;
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
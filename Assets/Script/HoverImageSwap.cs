using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HoverImageSwap : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Assign Image and Sprites")]
    public Image targetImage;      // The UI Image to swap
    public Sprite normalSprite;    // Default icon (white)
    public Sprite hoverSprite;     // Hover icon (black)

    void Start()
    {
        if (targetImage != null && normalSprite != null)
        {
            targetImage.sprite = normalSprite; // Set initial sprite
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (targetImage != null && hoverSprite != null)
        {
            targetImage.sprite = hoverSprite; // Swap to hover sprite
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (targetImage != null && normalSprite != null)
        {
            targetImage.sprite = normalSprite; // Revert to normal sprite
        }
    }
}

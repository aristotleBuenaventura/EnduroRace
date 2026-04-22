using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TipsAndTricksUI : MonoBehaviour
{
    [Header("UI References")]
    public GameObject tipsPanel;
    public Image cardImage;
    public Button nextButton;
    public Button prevButton;
    public TextMeshProUGUI pageIndicatorText;

    [Header("Next/Close Button Text")]
    public TextMeshProUGUI nextButtonText; // drag the TMP label inside your Next button here

    [Header("Card Images - assign in order")]
    public Sprite[] cardSprites;

    private int currentCard = 0;

    private void Awake()
    {
        if (tipsPanel != null)
            tipsPanel.SetActive(false);
    }

    private void Start()
    {
        if (nextButton != null)
            nextButton.onClick.AddListener(NextOrClose);

        if (prevButton != null)
            prevButton.onClick.AddListener(PrevCard);
    }

    public void Show()
    {
        currentCard = 0;
        tipsPanel.SetActive(true);
        UpdateCard();
    }

    public void Hide()
    {
        tipsPanel.SetActive(false);
    }

    // Renamed: handles both Next and Close depending on position
    private void NextOrClose()
    {
        if (currentCard < cardSprites.Length - 1)
        {
            currentCard++;
            UpdateCard();
        }
        else
        {
            Hide(); // Last card — act as Close
        }
    }

    private void PrevCard()
    {
        if (currentCard > 0)
        {
            currentCard--;
            UpdateCard();
        }
    }

    private void UpdateCard()
    {
        // Swap card image
        if (cardImage != null && cardSprites != null && currentCard < cardSprites.Length)
            cardImage.sprite = cardSprites[currentCard];

        // Update page indicator
        if (pageIndicatorText != null)
            pageIndicatorText.text = $"{currentCard + 1} / {cardSprites.Length}";

        // Enable/disable Prev button at left edge
        if (prevButton != null)
            prevButton.interactable = currentCard > 0;

        // Next button stays enabled always, but changes label on last card
        if (nextButton != null)
        {
            nextButton.interactable = true;

            if (nextButtonText != null)
                nextButtonText.text = currentCard == cardSprites.Length - 1 ? "Close" : "Next";
        }
    }
}
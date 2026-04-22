using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Firebase.Firestore;
using Firebase.Extensions;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class ShopManager : MonoBehaviour
{
    [Header("References")]
    public EquipmentManager equipmentManager;

    [Header("Coin UI")]
    public TextMeshProUGUI coinBalanceText;

    [Header("Shop Item Cards")]
    public ShopItemCard[] shopItemCards;

    [Header("Feedback UI")]
    public GameObject      feedbackPanel;
    public TextMeshProUGUI feedbackText;
    public float           feedbackDuration = 2f;

    private FirebaseFirestore db;
    private string playerId;
    private int    currentCoins = 0;
    private List<string> ownedItems = new List<string>();

    private void Start()
    {
        if (equipmentManager == null)
            equipmentManager = EquipmentManager.Instance;

        feedbackPanel?.SetActive(false);

        if (FirebaseManager.Instance.IsFirebaseReady) Init();
        else FirebaseManager.Instance.OnFirebaseReady += Init;
    }

    private void Init()
    {
        db       = FirebaseManager.Instance.Db;
        playerId = FirebaseManager.Instance.PlayerId;

        // ✅ Force CoinManager to re-fetch from Firestore so cached value is fresh
        CoinManager.Instance?.RefreshFromFirebase();

        LoadPlayerShopData();
    }

    // ── Firestore Load ────────────────────────────────────────────────────────

    private void LoadPlayerShopData()
    {
        // ✅ Source.Server ensures we always get the latest balance, not a cached value
        db.Collection("players").Document(playerId)
            .GetSnapshotAsync(Source.Server)
            .ContinueWithOnMainThread(task =>
            {
                if (task.IsFaulted || task.IsCanceled) return;

                var snap = task.Result;
                if (!snap.Exists) return;

                currentCoins = snap.ContainsField("coins")
                    ? (int)snap.GetValue<long>("coins") : 0;

                ownedItems = snap.ContainsField("ownedItems")
                    ? new List<string>(snap.GetValue<List<string>>("ownedItems"))
                    : new List<string>();

                string equippedId = snap.ContainsField("equippedItem")
                    ? snap.GetValue<string>("equippedItem") : "";

                RefreshUI(equippedId);
                Debug.Log($"[ShopManager] Loaded — Coins: {currentCoins}, " +
                          $"Owned: {string.Join(", ", ownedItems)}");
            });
    }

    // ── UI ────────────────────────────────────────────────────────────────────

    private void RefreshUI(string equippedId)
    {
        if (coinBalanceText != null)
            coinBalanceText.text = currentCoins.ToString();

        if (shopItemCards == null || equipmentManager == null) return;

        ShopItem[] items = equipmentManager.allItems;

        for (int i = 0; i < shopItemCards.Length; i++)
        {
            if (shopItemCards[i] == null) continue;
            if (i >= items.Length || items[i] == null) continue;

            ShopItem item = items[i];
            bool owned    = ownedItems.Contains(item.itemId);
            bool equipped = item.itemId == equippedId;

            shopItemCards[i].Setup(item, owned, equipped, this);
        }
    }

    // ── Buy ───────────────────────────────────────────────────────────────────

    public void TryBuyItem(ShopItem item)
    {
        if (ownedItems.Contains(item.itemId))
        {
            ShowFeedback("You already own this item!");
            return;
        }

        if (currentCoins < item.price)
        {
            ShowFeedback($"Not enough coins! Need {item.price}, have {currentCoins}.");
            return;
        }

        currentCoins -= item.price;
        ownedItems.Add(item.itemId);

        var updates = new Dictionary<string, object>
        {
            { "coins",      currentCoins },
            { "ownedItems", ownedItems   }
        };

        db.Collection("players").Document(playerId).UpdateAsync(updates)
            .ContinueWithOnMainThread(task =>
            {
                if (task.IsFaulted)
                {
                    // Rollback on failure
                    currentCoins += item.price;
                    ownedItems.Remove(item.itemId);
                    ShowFeedback("Purchase failed. Try again.");
                    RefreshUI(equipmentManager.EquippedItem?.itemId ?? "");
                    return;
                }

                // ✅ Keep CoinManager in sync after purchase
                CoinManager.Instance?.RefreshFromFirebase();

                ShowFeedback($"Purchased {item.displayName}!");
                RefreshUI(equipmentManager.EquippedItem?.itemId ?? "");
                Debug.Log($"[ShopManager] Bought '{item.itemId}'. " +
                          $"Remaining coins: {currentCoins}");
            });
    }

    // ── Feedback ──────────────────────────────────────────────────────────────

    private void ShowFeedback(string message)
    {
        if (feedbackPanel == null) return;
        feedbackText.text = message;
        feedbackPanel.SetActive(true);
        CancelInvoke(nameof(HideFeedback));
        Invoke(nameof(HideFeedback), feedbackDuration);
    }

    public void BackButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
    private void HideFeedback() => feedbackPanel?.SetActive(false);
}
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Firebase.Firestore;
using Firebase.Extensions;
using System.Collections.Generic;

public class CustomizePanel : MonoBehaviour
{
    [Header("Panel")]
    public GameObject panel;

    [Header("Item Card Template")]
    public GameObject itemCardPrefab;
    public Transform  itemCardParent;

    [Header("Equipped Label")]
    public TextMeshProUGUI equippedLabel;

    [Header("Buttons")]
    public Button closeButton;

    private FirebaseFirestore db;
    private string playerId;
    private List<string> ownedItems = new List<string>();
    private string equippedItemId   = "";

    private void Start()
    {
        panel?.SetActive(false);
        closeButton?.onClick.AddListener(Close);

        if (FirebaseManager.Instance.IsFirebaseReady) Init();
        else FirebaseManager.Instance.OnFirebaseReady += Init;
    }

    private void Init()
    {
        db       = FirebaseManager.Instance.Db;
        playerId = FirebaseManager.Instance.PlayerId;
    }

    // ── Open / Close ──────────────────────────────────────────────────────────

    public void Open()
    {
        if (db == null || string.IsNullOrEmpty(playerId))
        {
            if (!FirebaseManager.Instance.IsFirebaseReady)
            {
                ;
                return;
            }
            db       = FirebaseManager.Instance.Db;
            playerId = FirebaseManager.Instance.PlayerId;
        }

        panel?.SetActive(true);
        LoadAndRefresh();
    }

    public void Close()
    {
        panel?.SetActive(false);
    }

    // ── Load ──────────────────────────────────────────────────────────────────

    private void LoadAndRefresh()
    {
        db.Collection("players").Document(playerId).GetSnapshotAsync()
            .ContinueWithOnMainThread(task =>
            {
                if (task.IsFaulted || task.IsCanceled) return;

                var snap = task.Result;
                if (!snap.Exists) return;

                ownedItems = snap.ContainsField("ownedItems")
                    ? new List<string>(snap.GetValue<List<string>>("ownedItems"))
                    : new List<string>();

                equippedItemId = snap.ContainsField("equippedItem")
                    ? snap.GetValue<string>("equippedItem") : "";

                RefreshUI();
            });
    }

    // ── UI ────────────────────────────────────────────────────────────────────

    private void RefreshUI()
    {
        foreach (Transform child in itemCardParent)
            Destroy(child.gameObject);

        ShopItem[] allItems = EquipmentManager.Instance?.allItems;
        if (allItems == null) return;

        if (equippedLabel != null)
        {
            ShopItem equippedItem = EquipmentManager.Instance.FindItem(equippedItemId);
            equippedLabel.text = equippedItem != null
                ? $"Equipped: {equippedItem.displayName}"
                : "Equipped: Default";
        }

        foreach (var item in allItems)
        {
            if (item == null) continue;
            if (!ownedItems.Contains(item.itemId)) continue;

            GameObject cardGO = Instantiate(itemCardPrefab, itemCardParent);
            var card = cardGO.GetComponent<CustomizeItemCard>();
            if (card != null)
                card.Setup(item, item.itemId == equippedItemId, OnEquip);
        }

        if (itemCardPrefab != null)
        {
            GameObject defaultCardGO = Instantiate(itemCardPrefab, itemCardParent);
            var defaultCard = defaultCardGO.GetComponent<CustomizeItemCard>();
            if (defaultCard != null)
                defaultCard.SetupAsDefault(string.IsNullOrEmpty(equippedItemId), OnUnequip);
        }
    }

    // ── Equip / Unequip ───────────────────────────────────────────────────────

    private void OnEquip(string itemId)
    {
        equippedItemId = itemId;

        // ✅ Saves to Firestore + PlayerPrefs — appearance loads from here on race start
        EquipmentManager.Instance?.EquipItem(itemId);

        RefreshUI();
        ;
    }

    private void OnUnequip()
    {
        equippedItemId = "";

        // ✅ Saves to Firestore + PlayerPrefs
        EquipmentManager.Instance?.UnequipItem();

        RefreshUI();
        ;
    }
}
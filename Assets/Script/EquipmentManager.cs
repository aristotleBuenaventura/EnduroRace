using UnityEngine;
using Firebase.Firestore;
using Firebase.Extensions;
using System.Collections.Generic;

/// <summary>
/// Singleton. Loads the player's equipped item from Firestore on startup.
/// Provides stat bonuses to PlayerController / CyclingController.
/// Call EquipItem() from the Customize panel in Profile.
/// </summary>
public class EquipmentManager : MonoBehaviour
{
    public static EquipmentManager Instance { get; private set; }

    [Header("All available shop items (assign in Inspector)")]
    public ShopItem[] allItems;

    // Currently equipped item — null means default (team color only)
    public ShopItem EquippedItem { get; private set; }

    private FirebaseFirestore db;
    private string playerId;

    // ── Events ────────────────────────────────────────────────────────────────
    public System.Action<ShopItem> OnItemEquipped;   // fired when equipment changes

    private void Awake()
    {
        if (Instance == null) { Instance = this; DontDestroyOnLoad(gameObject); }
        else { Destroy(gameObject); return; }
    }

    private void Start()
    {
        if (FirebaseManager.Instance.IsFirebaseReady) Init();
        else FirebaseManager.Instance.OnFirebaseReady += Init;
    }

    private void Init()
    {
        db       = FirebaseManager.Instance.Db;
        playerId = FirebaseManager.Instance.PlayerId;
        LoadEquippedItemFromFirestore();
    }

    // ── Firestore I/O ─────────────────────────────────────────────────────────

    private void LoadEquippedItemFromFirestore()
    {
        db.Collection("players").Document(playerId).GetSnapshotAsync()
            .ContinueWithOnMainThread(task =>
            {
                if (task.IsFaulted || task.IsCanceled) return;

                var snap = task.Result;
                if (!snap.Exists) return;

                string itemId = snap.ContainsField("equippedItem")
                    ? snap.GetValue<string>("equippedItem")
                    : "";

                EquippedItem = FindItem(itemId);
                OnItemEquipped?.Invoke(EquippedItem);

                Debug.Log($"[EquipmentManager] Loaded equipped item: '{itemId}'");
            });
    }

    /// <summary>
    /// Call this from the Customize button in Profile scene.
    /// Saves to Firestore and fires OnItemEquipped so PlayerAppearance refreshes.
    /// </summary>
    public void EquipItem(string itemId)
    {
        EquippedItem = FindItem(itemId);

        db.Collection("players").Document(playerId)
            .UpdateAsync("equippedItem", itemId ?? "");

        PlayerPrefs.SetString("EquippedItem", itemId ?? "");
        PlayerPrefs.Save();

        OnItemEquipped?.Invoke(EquippedItem);
        Debug.Log($"[EquipmentManager] Equipped: '{itemId}'");
    }

    public void UnequipItem()
    {
        EquipItem("");   // empty string = default
    }

    // ── Stat Accessors ────────────────────────────────────────────────────────
    // These are read every frame in PlayerController / CyclingController via
    // EquipmentManager.Instance.GetXxx(). All return 0 if nothing is equipped.

    public float GetStaminaDecreaseReduction()
        => EquippedItem != null ? EquippedItem.staminaDecreaseReduction : 0f;

    public float GetMoveSpeedBonus()
        => EquippedItem != null ? EquippedItem.moveSpeedBonus : 0f;

    public float GetMoveSpeedMultiplierBonus()
        => EquippedItem != null ? EquippedItem.moveSpeedMultiplierBonus : 0f;

    public float GetSwimSpeedBonus()
        => EquippedItem != null ? EquippedItem.swimSpeedBonus : 0f;

    public float GetCycleSpeedBonus()
        => EquippedItem != null ? EquippedItem.cycleSpeedBonus : 0f;

    // ── Helpers ───────────────────────────────────────────────────────────────

    public ShopItem FindItem(string itemId)
    {
        if (string.IsNullOrEmpty(itemId)) return null;
        foreach (var item in allItems)
            if (item != null && item.itemId == itemId) return item;
        return null;
    }

    public bool OwnsItem(string itemId, List<string> ownedItems)
        => ownedItems != null && ownedItems.Contains(itemId);
}
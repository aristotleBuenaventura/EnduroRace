using UnityEngine;
using TMPro;
using Firebase.Firestore;
using Firebase.Extensions;
using System.Collections;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance { get; private set; }

    private int sessionCoins  = 0;
    private int totalCoins    = 0;

    private FirebaseFirestore db;
    private string playerId;
    private bool initialized   = false;
    private bool hasLoadedOnce = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        db = FirebaseFirestore.DefaultInstance;
        StartCoroutine(InitializeCoins());
    }

    private IEnumerator InitializeCoins()
    {
        if (hasLoadedOnce) yield break;
        hasLoadedOnce = true;

        yield return new WaitUntil(() =>
            FirebaseManager.Instance != null &&
            FirebaseManager.Instance.IsFirebaseReady);

        playerId = FirebaseManager.Instance.PlayerId;

        bool done = false;
        db.Collection("players").Document(playerId)
            .GetSnapshotAsync(Source.Server)   // ✅ always fetch fresh from server
            .ContinueWithOnMainThread(task =>
            {
                if (!task.IsFaulted && task.Result.Exists)
                {
                    totalCoins = task.Result.ContainsField("coins")
                        ? (int)task.Result.GetValue<long>("coins") : 0;
                }
                done = true;
            });

        yield return new WaitUntil(() => done);

        initialized = true;
        Debug.Log($"[CoinManager] Initialized with {totalCoins} coins");
    }

    /// <summary>
    /// Force re-fetch coins from Firestore. Call this when entering the Shop
    /// to ensure the displayed balance is always up to date.
    /// </summary>
    public void RefreshFromFirebase()
    {
        if (string.IsNullOrEmpty(playerId))
        {
            Debug.LogWarning("[CoinManager] RefreshFromFirebase: playerId not set yet.");
            return;
        }

        db.Collection("players").Document(playerId)
            .GetSnapshotAsync(Source.Server)
            .ContinueWithOnMainThread(task =>
            {
                if (!task.IsFaulted && task.Result.Exists)
                {
                    totalCoins = task.Result.ContainsField("coins")
                        ? (int)task.Result.GetValue<long>("coins") : 0;

                    Debug.Log($"[CoinManager] Refreshed from Firebase: {totalCoins} coins");
                }
            });
    }

    public void AddCoins(int amount)
    {
        sessionCoins += amount;
        Debug.Log($"[CoinManager] +{amount} coins this session. Session total: {sessionCoins}");
    }

    /// <summary>
    /// Called by RaceManager.FinishRace() when the local player finishes.
    /// Saves session coins to Firestore and resets session count.
    /// </summary>
    public void SaveCoinsToFirebase()
    {
        if (!initialized || string.IsNullOrEmpty(playerId))
        {
            Debug.LogWarning("[CoinManager] Not initialized — skipping save");
            return;
        }

        if (sessionCoins == 0)
        {
            Debug.Log("[CoinManager] No session coins to save");
            return;
        }

        int newTotal = totalCoins + sessionCoins;

        db.Collection("players").Document(playerId)
            .UpdateAsync("coins", newTotal)
            .ContinueWithOnMainThread(task =>
            {
                if (task.IsFaulted)
                {
                    Debug.LogError("[CoinManager] Save failed: " + task.Exception);
                }
                else
                {
                    totalCoins   = newTotal;
                    sessionCoins = 0;
                    Debug.Log($"[CoinManager] Saved. Total coins: {totalCoins}");
                }
            });
    }

    public int GetTotalCoins()   => totalCoins + sessionCoins;
    public int GetSessionCoins() => sessionCoins;

    public void ResetSessionCoins()
    {
        sessionCoins = 0;
        Debug.Log("[CoinManager] Session coins reset");
    }
}
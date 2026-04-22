using UnityEngine;
using TMPro;
using Firebase.Firestore;
using Firebase.Extensions;
using System.Collections;

public class CoinDisplay : MonoBehaviour
{
    public TextMeshProUGUI coinText;

    private void Start()
    {
        StartCoroutine(LoadCoins());
    }

    private IEnumerator LoadCoins()
    {
        yield return new WaitUntil(() =>
            FirebaseManager.Instance != null &&
            FirebaseManager.Instance.IsFirebaseReady);

        string playerId = FirebaseManager.Instance.PlayerId;

        FirebaseFirestore.DefaultInstance
            .Collection("players").Document(playerId)
            .GetSnapshotAsync(Source.Server)
            .ContinueWithOnMainThread(task =>
            {
                if (!task.IsFaulted && task.Result.Exists)
                {
                    int coins = task.Result.ContainsField("coins")
                        ? (int)task.Result.GetValue<long>("coins") : 0;

                    if (coinText != null)
                        coinText.text = $"{coins}";
                }
            });
    }

    /// <summary>
    /// Call this to refresh the display after coins are saved (e.g. after race ends).
    /// </summary>
    public void RefreshDisplay()
    {
        StartCoroutine(LoadCoins());
    }
}
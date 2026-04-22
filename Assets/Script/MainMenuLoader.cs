using UnityEngine;
using Firebase.Firestore;
using Firebase.Extensions;
using System.Collections;

public class MainMenuLoader : MonoBehaviour
{
    private const string CHECKED_KEY = "HasCheckedNewPlayer";

    private void Start()
    {
        // Already checked before, skip Firebase entirely
        if (PlayerPrefs.GetInt(CHECKED_KEY, 0) == 1)
            return;

        StartCoroutine(CheckNewPlayer());
    }

    private IEnumerator CheckNewPlayer()
    {
        yield return new WaitUntil(() =>
            FirebaseManager.Instance != null &&
            FirebaseManager.Instance.IsFirebaseReady);

        string playerId = FirebaseManager.Instance.PlayerId;

        bool done = false;
        bool isNewPlayer = false;

        FirebaseFirestore.DefaultInstance
            .Collection("players").Document(playerId)
            .GetSnapshotAsync(Source.Server)
            .ContinueWithOnMainThread(task =>
            {
                if (!task.IsFaulted && task.Result.Exists)
                {
                    isNewPlayer = !task.Result.ContainsField("hasSeenIntro")
                        || !task.Result.GetValue<bool>("hasSeenIntro");
                }
                done = true;
            });

        yield return new WaitUntil(() => done);

        // Cache so we never check Firebase again
        PlayerPrefs.SetInt(CHECKED_KEY, 1);
        PlayerPrefs.Save();

        if (isNewPlayer)
            MainMenuTutorial.StartTutorial();
    }
}
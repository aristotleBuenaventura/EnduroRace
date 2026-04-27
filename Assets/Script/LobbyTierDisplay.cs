using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Firebase.Firestore;
using Firebase.Extensions;

public class LobbyTierDisplay : MonoBehaviour
{
    [Header("Badge Image")]
    public Image tierBadgeImage;

    void Start()
    {
        StartCoroutine(FetchAndDisplayTier());
    }

    private IEnumerator FetchAndDisplayTier()
    {
        yield return new WaitUntil(() => FirebaseManager.Instance != null && FirebaseManager.Instance.IsFirebaseReady);

        FirebaseFirestore db = FirebaseFirestore.DefaultInstance;

        LobbyManager lobby = FindFirstObjectByType<LobbyManager>();
        yield return new WaitUntil(() => lobby != null && lobby.IsLobbyReady);

        string lobbyId = lobby.GetLobbyId();

        ;

        db.Collection("lobbies")
            .Document(lobbyId)
            .GetSnapshotAsync()
            .ContinueWithOnMainThread(task =>
            {
                if (task.IsFaulted)
                {
                    ;
                    return;
                }

                if (!task.Result.Exists)
                {
                    ;
                    return;
                }

                if (task.Result.ContainsField("tier"))
                {
                    string tier = task.Result.GetValue<string>("tier");
                    SetTierBadge(tier);
                }
                else
                {
                    ;
                }
            });
    }

    private void SetTierBadge(string tier)
    {
        if (tierBadgeImage == null)
        {
            ;
            return;
        }

        string spritePath = tier switch
        {
            "Beginner"     => "Badges/BadgeBeginner",
            "Intermediate" => "Badges/BadgeIntermediate",
            "Pro"          => "Badges/BadgePro",
            _              => "Badges/BadgeBeginner"
        };

        Sprite badge = Resources.Load<Sprite>(spritePath);

        if (badge != null)
            tierBadgeImage.sprite = badge;
        else
            ;

        ;
    }
}
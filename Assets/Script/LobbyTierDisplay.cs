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

        Debug.Log($"[LobbyTierDisplay] Fetching tier for lobby: {lobbyId}");

        db.Collection("lobbies")
            .Document(lobbyId)
            .GetSnapshotAsync()
            .ContinueWithOnMainThread(task =>
            {
                if (task.IsFaulted)
                {
                    Debug.LogError($"[LobbyTierDisplay] Failed to get lobby: {task.Exception}");
                    return;
                }

                if (!task.Result.Exists)
                {
                    Debug.LogWarning("[LobbyTierDisplay] Lobby doesn't exist!");
                    return;
                }

                if (task.Result.ContainsField("tier"))
                {
                    string tier = task.Result.GetValue<string>("tier");
                    SetTierBadge(tier);
                }
                else
                {
                    Debug.LogWarning("[LobbyTierDisplay] Lobby has no tier field!");
                }
            });
    }

    private void SetTierBadge(string tier)
    {
        if (tierBadgeImage == null)
        {
            Debug.LogWarning("[LobbyTierDisplay] tierBadgeImage is not assigned!");
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
            Debug.LogWarning($"[LobbyTierDisplay] Badge sprite not found at: Resources/{spritePath}");

        Debug.Log($"[LobbyTierDisplay] Badge set for tier: {tier}");
    }
}
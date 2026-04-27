using System.Collections;
using UnityEngine;
using Firebase.Firestore;
using TMPro;

public class LobbyPlayerCountAndReady : MonoBehaviour
{
    public TextMeshProUGUI playerStatusText;
    private FirebaseFirestore db;
    private string lobbyId;
    private int maxPlayers = 20;

    void Start()
    {
        StartCoroutine(InitWhenLobbyReady());
    }

    private IEnumerator InitWhenLobbyReady()
    {
        // Wait until Firebase is ready
        yield return new WaitUntil(() => FirebaseManager.Instance != null && FirebaseManager.Instance.IsFirebaseReady);

        db = FirebaseFirestore.DefaultInstance;

        // Wait until LobbyManager is ready
        LobbyManager lobby = FindFirstObjectByType<LobbyManager>();
        yield return new WaitUntil(() => lobby.IsLobbyReady);

        lobbyId = lobby.GetLobbyId();
        ListenForPlayers();
    }

    private void ListenForPlayers()
    {
        if (string.IsNullOrEmpty(lobbyId))
        {
            return;
        }

        db.Collection("lobbies")
          .Document(lobbyId)
          .Collection("players")
          .Listen(snapshot =>
          {
              UpdatePlayerCount(snapshot);
          });
    }

    private void UpdatePlayerCount(QuerySnapshot snapshot)
    {
        int humanPlayers = 0;
        int readyHumans = 0;

        foreach (var doc in snapshot.Documents)
        {
            bool isBot = doc.ContainsField("isBot") && doc.GetValue<bool>("isBot");
            bool isReady = doc.ContainsField("isReady") && doc.GetValue<bool>("isReady");

            if (!isBot)
            {
                humanPlayers++;
                if (isReady) readyHumans++;
            }
        }

        playerStatusText.text = $"Players Ready: {readyHumans} / {humanPlayers}   Players Found: {humanPlayers} / {maxPlayers}";
    }
}

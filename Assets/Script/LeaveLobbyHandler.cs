using UnityEngine;
using Firebase.Firestore;
using UnityEngine.SceneManagement;

public class LeaveLobbyHandler : MonoBehaviour
{
    private FirebaseFirestore db;
    private string lobbyId;
    private string playerId;

    void Start()
    {
        db = FirebaseFirestore.DefaultInstance;

        LobbyManager lobby = FindFirstObjectByType<LobbyManager>();

        if (lobby.IsLobbyReady)
        {
            lobbyId = lobby.GetLobbyId();
            playerId = lobby.GetPlayerId();
        }
        else
        {
            lobby.OnLobbyReady += () =>
            {
                lobbyId = lobby.GetLobbyId();
                playerId = lobby.GetPlayerId();
            };
        }
    }

    public void LeaveLobby()
    {
        if (!string.IsNullOrEmpty(lobbyId) && !string.IsNullOrEmpty(playerId))
        {
            db.Collection("lobbies")
              .Document(lobbyId)
              .Collection("players")
              .Document(playerId)
              .DeleteAsync();
        }

        SceneManager.LoadScene("MainMenu");
    }
}

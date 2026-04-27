using UnityEngine;
using UnityEngine.UI;
using Firebase.Firestore;
using TMPro;
using UnityEngine.EventSystems;
using System.Collections;
using Firebase.Extensions;

public class ReadyButtonHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TextMeshProUGUI buttonText;
    public Button button;

    public Color readyColor = Color.green;
    public Color cancelColor = Color.red;
    public Color readyTextColor = Color.white;
    public Color cancelTextColor = Color.white;
    public Color hoverTextColor = Color.black;
    public Color hoverBackgroundColor = Color.white;

    private FirebaseFirestore db;
    private string lobbyId;
    private string playerId;

    private bool isReady;
    private bool isHovering;

    private ListenerRegistration lobbyListener;

    void Start()
    {
        button.interactable = false;
        StartCoroutine(InitWhenLobbyReady());
    }

    private IEnumerator InitWhenLobbyReady()
    {
        yield return new WaitUntil(() => FirebaseManager.Instance != null && FirebaseManager.Instance.IsFirebaseReady);

        db = FirebaseFirestore.DefaultInstance;

        LobbyManager lobby = FindFirstObjectByType<LobbyManager>();
        yield return new WaitUntil(() => lobby.IsLobbyReady);

        lobbyId = lobby.GetLobbyId();
        playerId = lobby.GetPlayerId();
        
        
        isReady = false;
        UpdateButtonVisual();
        button.interactable = true;

        ListenToLobbyState();
        
    }

    private void ListenToLobbyState()
    {
        
        lobbyListener = db.Collection("lobbies")
            .Document(lobbyId)
            .Listen(snapshot =>
            {
                if (!snapshot.Exists)
                {
                    return;
                }

                string state = snapshot.ContainsField("state") ? snapshot.GetValue<string>("state") : "waiting";
                

                bool canInteract = state == "waiting" || isReady;
                button.interactable = canInteract;
                
            });
    }

    public void ToggleReady()
    {
        
        if (!button.interactable)
        {
            return;
        }

        isReady = !isReady;
        
        UpdateButtonVisual();


        db.Collection("lobbies")
          .Document(lobbyId)
          .Collection("players")
          .Document(playerId)
          .UpdateAsync("isReady", isReady)
          .ContinueWithOnMainThread(task =>
          {
              if (task.IsFaulted)
              {
              }
              else if (task.IsCompleted)
              {
                  
                  // ✅ Verify it was actually written
                  db.Collection("lobbies")
                      .Document(lobbyId)
                      .Collection("players")
                      .Document(playerId)
                      .GetSnapshotAsync()
                      .ContinueWithOnMainThread(verifyTask =>
                      {
                          if (verifyTask.IsCompleted && verifyTask.Result.Exists)
                          {
                              bool firestoreValue = verifyTask.Result.GetValue<bool>("isReady");
                              
                              if (firestoreValue != isReady)
                              {
                              }
                          }
                      });
              }
          });
        
    }

    private void UpdateButtonVisual()
    {
        if (button == null || buttonText == null) return;

        buttonText.text = isReady ? "CANCEL" : "READY";

        if (isHovering && button.interactable)
        {
            button.image.color = hoverBackgroundColor;
            buttonText.color = hoverTextColor;
        }
        else
        {
            button.image.color = isReady ? cancelColor : readyColor;
            buttonText.color = isReady ? cancelTextColor : readyTextColor;
        }
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!button.interactable) return;
        isHovering = true;
        UpdateButtonVisual();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovering = false;
        UpdateButtonVisual();
    }

    private void OnDestroy()
    {
        lobbyListener?.Stop();
    }
}
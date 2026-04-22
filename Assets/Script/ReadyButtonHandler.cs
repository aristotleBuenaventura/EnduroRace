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
        Debug.Log("[ReadyButton] Starting initialization");
        button.interactable = false;
        StartCoroutine(InitWhenLobbyReady());
    }

    private IEnumerator InitWhenLobbyReady()
    {
        Debug.Log("[ReadyButton] Waiting for Firebase...");
        yield return new WaitUntil(() => FirebaseManager.Instance != null && FirebaseManager.Instance.IsFirebaseReady);
        Debug.Log("[ReadyButton] Firebase ready");

        db = FirebaseFirestore.DefaultInstance;

        LobbyManager lobby = FindFirstObjectByType<LobbyManager>();
        Debug.Log("[ReadyButton] Waiting for lobby...");
        yield return new WaitUntil(() => lobby.IsLobbyReady);
        Debug.Log("[ReadyButton] Lobby ready");

        lobbyId = lobby.GetLobbyId();
        playerId = lobby.GetPlayerId();
        
        Debug.Log($"[ReadyButton] Initialized - LobbyId: {lobbyId}, PlayerId: {playerId}");
        
        isReady = false;
        UpdateButtonVisual();
        button.interactable = true;

        ListenToLobbyState();
        
        Debug.Log("[ReadyButton] ✅ Ready button is now active");
    }

    private void ListenToLobbyState()
    {
        Debug.Log("[ReadyButton] Starting lobby state listener");
        
        lobbyListener = db.Collection("lobbies")
            .Document(lobbyId)
            .Listen(snapshot =>
            {
                if (!snapshot.Exists)
                {
                    Debug.LogWarning("[ReadyButton] Lobby snapshot doesn't exist!");
                    return;
                }

                string state = snapshot.ContainsField("state") ? snapshot.GetValue<string>("state") : "waiting";
                
                Debug.Log($"[ReadyButton] Lobby state changed to: {state}");

                bool canInteract = state == "waiting" || isReady;
                button.interactable = canInteract;
                
                Debug.Log($"[ReadyButton] Button interactable: {canInteract} (state={state}, isReady={isReady})");
            });
    }

    public void ToggleReady()
    {
        Debug.Log("═════════════════════════════════════════");
        Debug.Log($"[ReadyButton] ToggleReady called - Current state: {isReady}");
        
        if (!button.interactable)
        {
            Debug.LogWarning("[ReadyButton] Button not interactable, ignoring click");
            return;
        }

        isReady = !isReady;
        Debug.Log($"[ReadyButton] New ready state: {isReady}");
        
        UpdateButtonVisual();

        Debug.Log($"[ReadyButton] Updating Firestore for player {playerId} in lobby {lobbyId}");
        Debug.Log($"[ReadyButton] Setting isReady = {isReady}");

        db.Collection("lobbies")
          .Document(lobbyId)
          .Collection("players")
          .Document(playerId)
          .UpdateAsync("isReady", isReady)
          .ContinueWithOnMainThread(task =>
          {
              if (task.IsFaulted)
              {
                  Debug.LogError($"[ReadyButton] ❌ Failed to update ready state: {task.Exception}");
              }
              else if (task.IsCompleted)
              {
                  Debug.Log($"[ReadyButton] ✅ Successfully updated isReady to {isReady} in Firestore");
                  
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
                              Debug.Log($"[ReadyButton] 🔍 Verification - Firestore has isReady = {firestoreValue}");
                              
                              if (firestoreValue != isReady)
                              {
                                  Debug.LogError($"[ReadyButton] ⚠️ MISMATCH! Local={isReady}, Firestore={firestoreValue}");
                              }
                          }
                      });
              }
          });
        
        Debug.Log("═════════════════════════════════════════");
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
        
        Debug.Log($"[ReadyButton] Visual updated - Text: {buttonText.text}, Color: {(isReady ? "Red" : "Green")}");
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
        Debug.Log("[ReadyButton] Destroying - stopping listener");
        lobbyListener?.Stop();
    }
}
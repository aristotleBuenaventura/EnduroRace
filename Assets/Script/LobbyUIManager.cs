using UnityEngine;
using UnityEngine.UI;
using Firebase.Firestore;
using TMPro;
using System.Collections.Generic;
using System.Linq;  // <<< THIS IS REQUIRED

[RequireComponent(typeof(ScrollRect))]
public class LobbyUIManager : MonoBehaviour
{
    [Header("UI References")]
    public Transform contentParent;      // ScrollRect content
    public GameObject playerSlotPrefab;  // Prefab with TextMeshProUGUI + Image

    private FirebaseFirestore db;
    private string lobbyId;
    private Dictionary<string, GameObject> slots = new Dictionary<string, GameObject>();

    private ListenerRegistration playerListener;

    void Start()
    {
        db = FirebaseFirestore.DefaultInstance;

        LobbyManager lobby = FindFirstObjectByType<LobbyManager>();

        if (lobby.IsLobbyReady)
        {
            lobbyId = lobby.GetLobbyId();
            ListenToPlayers();
        }
        else
        {
            lobby.OnLobbyReady += () =>
            {
                lobbyId = lobby.GetLobbyId();
                ListenToPlayers();
            };
        }
    }

    void ListenToPlayers()
    {
        // Stop previous listener if exists
        playerListener?.Stop();

        playerListener = db.Collection("lobbies")
            .Document(lobbyId)
            .Collection("players")
            .Listen(snapshot =>
            {
                // Use LINQ Count() instead of casting
                int playerCount = snapshot.Documents.Count();
                Debug.Log($"Player snapshot update: {playerCount} players found.");

                HashSet<string> currentIds = new HashSet<string>();

                foreach (var doc in snapshot.Documents)
                {
                    currentIds.Add(doc.Id);
                    UpdateOrCreateSlot(doc);
                }

                // Remove players no longer in Firestore
                var toRemove = slots.Keys.Where(id => !currentIds.Contains(id)).ToList();
                foreach (var id in toRemove)
                    RemoveSlot(id);
            });
    }


    void UpdateOrCreateSlot(DocumentSnapshot doc)
    {
        if (slots.TryGetValue(doc.Id, out GameObject slot))
        {
            if (!slot) return; // destroyed safety
            UpdateSlotVisual(slot, doc);
        }
        else
        {
            GameObject newSlot = Instantiate(playerSlotPrefab, contentParent);
            newSlot.transform.localScale = Vector3.one;
            newSlot.transform.localPosition = Vector3.zero;

            // Force prefab size if layout is used
            RectTransform rt = newSlot.GetComponent<RectTransform>();
            if (rt != null)
                rt.sizeDelta = new Vector2(200, 50); // Adjust size as needed

            slots.Add(doc.Id, newSlot);
            UpdateSlotVisual(newSlot, doc);

            Debug.Log($"Created slot for player {doc.Id}");
        }
    }

    void RemoveSlot(string playerId)
    {
        if (slots.TryGetValue(playerId, out GameObject slot))
        {
            if (slot) Destroy(slot);
            slots.Remove(playerId);
            Debug.Log($"Removed slot for player {playerId}");
        }
    }

    void UpdateSlotVisual(GameObject slot, DocumentSnapshot doc)
    {
        if (!slot) return;

        // Read Firestore fields safely
        string name = doc.ContainsField("name") ? doc.GetValue<string>("name") : "Player";
        bool isReady = doc.ContainsField("isReady") ? doc.GetValue<bool>("isReady") : false;
        bool isBot = doc.ContainsField("isBot") ? doc.GetValue<bool>("isBot") : false;
        string avatarName = doc.ContainsField("avatar") ? doc.GetValue<string>("avatar") : "DefaultAvatar";

        // Avatar
        Transform avatarTransform = slot.transform.Find("Avatar");
        if (avatarTransform)
        {
            Image avatarImage = avatarTransform.GetComponent<Image>();
            if (avatarImage)
            {
                Sprite avatarSprite = Resources.Load<Sprite>("Avatars/" + avatarName);
                if (avatarSprite)
                    avatarImage.sprite = avatarSprite;
            }
        }

        // Texts
        foreach (TextMeshProUGUI text in slot.GetComponentsInChildren<TextMeshProUGUI>())
        {
            if (text.name.Contains("PlayerName"))
                text.text = name;

            if (text.name.Contains("Status"))
                text.text = isBot ? "BOT" : (isReady ? "READY" : "WAITING");
        }
    }

    private void OnDestroy()
    {
        playerListener?.Stop();
        playerListener = null;
    }
}

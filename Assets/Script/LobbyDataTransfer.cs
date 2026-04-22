using UnityEngine;
using System.Collections.Generic;
using Firebase.Firestore;
using Firebase.Extensions;
using System;

public class LobbyDataTransfer : MonoBehaviour
{
    public static LobbyDataTransfer Instance { get; private set; }

    [System.Serializable]
    public class PlayerData
    {
        public string playerId;
        public string displayName;
        public string avatar;
        public string tier;
        public string selectedModel = "Male";
        public int clientId;
        public bool isBot;
        public bool isLocalPlayer;
    }
    public int clientId; // set this when the player joins the lobby/room
    public string lobbyId;
    public string localPlayerId;
    public List<PlayerData> allPlayers = new List<PlayerData>();

    // Relay data
    public string relayHost = "";
    public ushort relayServerPort = 7770;
    public ushort relayClientPort = 7770;
    public uint relaySessionToken = 0;
    public uint relayUserToken = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("[LobbyDataTransfer] Created and persisted");
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetLobbyData(string lobbyId, string localPlayerId, List<PlayerData> players)
    {
        this.lobbyId = lobbyId;
        this.localPlayerId = localPlayerId;
        this.allPlayers = new List<PlayerData>(players);

        // Apply PlayerPrefs immediately as fast fallback
        ApplyLocalPlayerModel();

        // Then fetch from Firebase to override with the most accurate value
        FetchAndApplyModelFromFirebase();

        Debug.Log($"[LobbyDataTransfer] Set lobby data: {players.Count} players");
        foreach (var player in players)
            Debug.Log($"  - {player.displayName} | Model: {player.selectedModel} | Local: {player.isLocalPlayer}");
    }

    /// <summary>
    /// Fetches selectedModel directly from Firestore and applies it to the local player.
    /// Most reliable source since PlayerProfileManager saves there.
    /// </summary>
    public void FetchAndApplyModelFromFirebase(Action onComplete = null)
    {
        if (!FirebaseManager.Instance.IsFirebaseReady)
        {
            Debug.LogWarning("[LobbyDataTransfer] Firebase not ready - using PlayerPrefs fallback");
            onComplete?.Invoke();
            return;
        }

        string playerId = FirebaseManager.Instance.PlayerId;
        var db = FirebaseManager.Instance.Db;

        db.Collection("players").Document(playerId).GetSnapshotAsync()
            .ContinueWithOnMainThread(task =>
            {
                if (task.IsFaulted || task.IsCanceled)
                {
                    Debug.LogWarning("[LobbyDataTransfer] Firebase fetch failed - using PlayerPrefs fallback");
                    onComplete?.Invoke();
                    return;
                }

                var snap = task.Result;
                if (!snap.Exists)
                {
                    Debug.LogWarning("[LobbyDataTransfer] Player document not found - using PlayerPrefs fallback");
                    onComplete?.Invoke();
                    return;
                }

                string modelFromFirebase = snap.ContainsField("selectedModel")
                    ? snap.GetValue<string>("selectedModel")
                    : "Male";

                Debug.Log($"[LobbyDataTransfer] Firebase selectedModel = '{modelFromFirebase}'");

                // Sync PlayerPrefs so both are consistent
                PlayerPrefs.SetString("SelectedModel", modelFromFirebase);
                PlayerPrefs.Save();

                // Apply to local player entry
                PlayerData local = GetLocalPlayerData();
                if (local != null)
                {
                    local.selectedModel = modelFromFirebase;
                    Debug.Log($"[LobbyDataTransfer] Applied Firebase model '{modelFromFirebase}' to {local.displayName}");
                }
                else
                {
                    Debug.LogWarning("[LobbyDataTransfer] Could not find local player entry to apply model!");
                }

                onComplete?.Invoke();
            });
    }

    /// <summary>
    /// Reads from PlayerPrefs as immediate fallback before Firebase responds.
    /// </summary>
    public void ApplyLocalPlayerModel()
    {
        string savedModel = PlayerPrefs.GetString("SelectedModel", "Male");

        PlayerData local = GetLocalPlayerData();
        if (local != null)
        {
            local.selectedModel = savedModel;
            Debug.Log($"[LobbyDataTransfer] Applied PlayerPrefs model '{savedModel}' to {local.displayName}");
        }
        else
        {
            Debug.LogWarning("[LobbyDataTransfer] Could not find local player to apply model!");
        }
    }

    public PlayerData GetLocalPlayerData()
    {
        return allPlayers.Find(p => p.playerId == localPlayerId);
    }

    public List<PlayerData> GetAllPlayers()
    {
        return new List<PlayerData>(allPlayers);
    }

    public void ClearData()
    {
        lobbyId = null;
        localPlayerId = null;
        allPlayers.Clear();
        relayHost = "";
        relayServerPort = 7770;
        relayClientPort = 7770;
        relaySessionToken = 0;
        relayUserToken = 0;
        Debug.Log("[LobbyDataTransfer] Cleared data");
    }
}
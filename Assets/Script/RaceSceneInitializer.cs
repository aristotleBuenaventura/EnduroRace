using UnityEngine;
using System.Collections.Generic;
using FishNet;
using FishNet.Managing;
public class RaceSceneInitializer : MonoBehaviour
{
    [Header("Network")]
    public NetworkManager networkManager;
    
    [Header("References")]
    public RaceManager raceManager;
    
    private void ResolveNetworkManager()
    {
        if (networkManager != null)
            return;

        // If this component is placed on the same object as NetworkManager.
        networkManager = GetComponent<NetworkManager>();
        if (networkManager != null)
            return;

        // Scene-level fallback before FishNet singleton lookup.
        networkManager = Object.FindFirstObjectByType<NetworkManager>();
        if (networkManager != null)
            return;

        networkManager = InstanceFinder.NetworkManager;
    }

    private void Start()
    {
        // Get NetworkManager if not assigned.
        ResolveNetworkManager();

        // Get RaceManager if not assigned
        if (raceManager == null)
            raceManager = Object.FindFirstObjectByType<RaceManager>();

        // Initialize race with lobby data
        InitializeFromLobby();
    }

    private void InitializeFromLobby()
    {
        if (LobbyDataTransfer.Instance == null)
        {
            ;
            return;
        }

        string lobbyId = LobbyDataTransfer.Instance.lobbyId;
        string localPlayerId = LobbyDataTransfer.Instance.localPlayerId;
        List<LobbyDataTransfer.PlayerData> players = LobbyDataTransfer.Instance.GetAllPlayers();

        ;
        ;
        ;

        // Display player info
        foreach (var player in players)
        {
            ;
        }

        // ✅ TODO: Here you can:
        // 1. Set player names/avatars on UI
        // 2. Configure AI for bots
        // 3. Track which NetworkPlayer belongs to which lobby player
        // 4. Update leaderboard with player names
        // 5. Send results back to Firebase after race

        // Example: Store in a manager for later use
        RacePlayerTracker tracker = Object.FindFirstObjectByType<RacePlayerTracker>();
        if (tracker != null)
        {
            tracker.SetPlayers(players);
        }
    }
}
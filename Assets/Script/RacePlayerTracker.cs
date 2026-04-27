using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using FishNet.Object;
using FishNet.Connection;

public class RacePlayerTracker : MonoBehaviour
{
    private Dictionary<string, LobbyDataTransfer.PlayerData> lobbyPlayers
        = new Dictionary<string, LobbyDataTransfer.PlayerData>();

    // firebasePlayerId → NetworkPlayer
    private Dictionary<string, NetworkPlayer> playerIdToNetworkPlayer
        = new Dictionary<string, NetworkPlayer>();

    // -------------------------------------------------------------------------
    // Setup
    // -------------------------------------------------------------------------

    public void SetPlayers(List<LobbyDataTransfer.PlayerData> players)
    {
        lobbyPlayers.Clear();
        foreach (var player in players)
        {
            lobbyPlayers[player.playerId] = player;
            ;
        }
    }

    /// <summary>
    /// Called by NetworkStarter when a NetworkPlayer spawns.
    /// Now matches via NetworkPlayer.firebasePlayerId — no clientId needed.
    /// </summary>
    public void RegisterNetworkPlayer(NetworkPlayer networkPlayer, NetworkConnection conn)
    {
        if (networkPlayer == null) return;

        // firebasePlayerId is a SyncVar set by the owning client on spawn.
        // If it's already populated (common on host), link immediately.
        // If empty (client hasn't sent CmdSetFirebaseId yet), we'll catch it
        // in TryLinkByFirebaseId called from NetworkPlayer once the SyncVar updates.

        // AFTER (fixed):
        if (!string.IsNullOrEmpty(networkPlayer.FirebasePlayerId.Value))
        {
            LinkPlayer(networkPlayer, networkPlayer.FirebasePlayerId.Value);
        }
    }

    /// <summary>
    /// Call this from NetworkPlayer's OnFirebasePlayerIdChanged SyncVar callback
    /// so late-arriving SyncVar values are still caught.
    /// </summary>
    public void TryLinkByFirebaseId(NetworkPlayer networkPlayer, string firebasePlayerId)
    {
        if (networkPlayer == null || string.IsNullOrEmpty(firebasePlayerId)) return;
        LinkPlayer(networkPlayer, firebasePlayerId);
    }

    private void LinkPlayer(NetworkPlayer networkPlayer, string firebasePlayerId)
    {
        playerIdToNetworkPlayer[firebasePlayerId] = networkPlayer;

        string displayName = lobbyPlayers.TryGetValue(firebasePlayerId, out var pd)
            ? pd.displayName
            : "<not in lobby>";

        ;
    }

    // -------------------------------------------------------------------------
    // Lookups
    // -------------------------------------------------------------------------

    /// <summary>
    /// Primary lookup used by MultiplayerRaceRanking.
    /// Finds player data by OwnerId — never swaps names.
    /// </summary>
    public LobbyDataTransfer.PlayerData GetPlayerDataByOwnerId(int ownerId)
    {
        var networkPlayer = playerIdToNetworkPlayer.Values
            .FirstOrDefault(np => np != null && np.OwnerId == ownerId);

        if (networkPlayer == null) return null;

        var entry = playerIdToNetworkPlayer
            .FirstOrDefault(kvp => kvp.Value == networkPlayer);

        if (string.IsNullOrEmpty(entry.Key)) return null;

        return lobbyPlayers.TryGetValue(entry.Key, out var data) ? data : null;
    }

    public LobbyDataTransfer.PlayerData GetPlayerData(NetworkPlayer networkPlayer)
    {
        if (networkPlayer == null) return null;
        var entry = playerIdToNetworkPlayer.FirstOrDefault(x => x.Value == networkPlayer);
        if (string.IsNullOrEmpty(entry.Key)) return null;
        return lobbyPlayers.TryGetValue(entry.Key, out var data) ? data : null;
    }

    public LobbyDataTransfer.PlayerData GetLocalPlayerData()
    {
        if (LobbyDataTransfer.Instance == null) return null;
        string localId = LobbyDataTransfer.Instance.localPlayerId;
        return lobbyPlayers.TryGetValue(localId, out var data) ? data : null;
    }

    public List<LobbyDataTransfer.PlayerData> GetAllPlayers()
        => lobbyPlayers.Values.ToList();

    // -------------------------------------------------------------------------
    // Required by MultiplayerRaceRanking
    // -------------------------------------------------------------------------

    /// <summary>
    /// True when every lobby player has been linked to a NetworkPlayer with a real display name.
    /// MultiplayerRaceRanking polls this before initialising so names are never wrong.
    /// </summary>
    public bool IsDataReady()
    {
        if (lobbyPlayers.Count == 0) return false;
        if (playerIdToNetworkPlayer.Count == 0) return false;

        foreach (var kvp in playerIdToNetworkPlayer)
        {
            if (!lobbyPlayers.TryGetValue(kvp.Key, out var pd)) return false;
            if (string.IsNullOrEmpty(pd.displayName)) return false;
        }

        return true;
    }

    /// <summary>
    /// Logs every OwnerId → firebaseId → displayName mapping for debugging.
    /// </summary>
    public void DebugLogAllPlayers()
    {
        if (lobbyPlayers.Count == 0)
        {
            ;
            return;
        }

        if (playerIdToNetworkPlayer.Count == 0)
        {
            ;
            return;
        }

        ;
        foreach (var kvp in playerIdToNetworkPlayer)
        {
            string firebaseId = kvp.Key;
            NetworkPlayer np  = kvp.Value;
            string name       = lobbyPlayers.TryGetValue(firebaseId, out var pd) ? pd.displayName : "<no lobby entry>";
            int ownerId       = np != null ? np.OwnerId : -1;
            ;
        }

        var unlinked = lobbyPlayers.Keys.Except(playerIdToNetworkPlayer.Keys).ToList();
        foreach (var fid in unlinked)
            ;
    }
}
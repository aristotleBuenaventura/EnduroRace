using FishNet.Object;
using UnityEngine;
using System.Collections;

public class MultiplayerAISpawner : NetworkBehaviour
{
    [Header("AI Manager")]
    public NetworkedAIManager aiManager;

    [Header("Dynamic AI Settings (Fallback)")]
    public int maxTotalRacers = 8;
    public int minAI = 0;
    public int maxAI = 6;

    [Header("Tier-Based Total Racers (Including Players + AI)")]
    public bool useTierBasedTotalRacers = true;
    public int beginnerTotalRacers = 20;
    public int intermediateTotalRacers = 15;
    public int proTotalRacers = 10;

    [Header("Spawn Delay")]
    public float spawnDelay = 2f;

    private bool hasSpawned = false;

    public override void OnStartServer()
    {
        base.OnStartServer();
        ;
        Invoke(nameof(SpawnAIDynamically), spawnDelay);
    }

    [Server]
    private void SpawnAIDynamically()
    {
        if (hasSpawned)
        {
            ;
            return;
        }

        if (aiManager == null)
        {
            ;
            return;
        }

        NetworkPlayer[] players = FindObjectsByType<NetworkPlayer>(FindObjectsSortMode.None);
        int playerCount = players.Length;
        string raceTier = ResolveRaceTier();
        int targetTotalRacers = useTierBasedTotalRacers
            ? GetTierTotalRacers(raceTier)
            : maxTotalRacers;

        int desiredAI = targetTotalRacers - playerCount;
        int aiToSpawn = Mathf.Max(minAI, desiredAI);
        int maxAICap = useTierBasedTotalRacers
            ? Mathf.Max(0, targetTotalRacers)
            : maxAI;
        aiToSpawn = Mathf.Clamp(aiToSpawn, minAI, maxAICap);

        Debug.Log(
            $"[MultiplayerAISpawner] Tier={raceTier}, Players={playerCount}, " +
            $"TargetTotal={targetTotalRacers}, AIToSpawn={aiToSpawn}"
        );

        if (aiToSpawn > 0)
        {
            ;
            aiManager.numberOfOpponents = aiToSpawn;
            aiManager.SpawnOpponents();

            // Refresh rankings after spawn but do NOT start AI yet
            // AI will be started by RaceManager when countdown finishes
            StartCoroutine(RefreshRankingsAfterSpawn());
        }
        else
        {
            ;
        }

        hasSpawned = true;
    }

    // No [Server] attribute on coroutines - it breaks them
    private IEnumerator RefreshRankingsAfterSpawn()
    {
        yield return new WaitForSeconds(1f);

        // Broadcast to all clients to refresh their rankings UI
        RefreshRankingsRpc();
    }

    [ObserversRpc]
    private void RefreshRankingsRpc()
    {
        MultiplayerRaceRanking ranking = FindFirstObjectByType<MultiplayerRaceRanking>();
        if (ranking != null)
        {
            ranking.RefreshRacers();
            ;
        }
        else
        {
            ;
        }
    }

    [Server]
    public void ForceSpawnAI(int count)
    {
        if (aiManager != null)
        {
            aiManager.numberOfOpponents = count;
            aiManager.SpawnOpponents();
            hasSpawned = true;
        }
    }

    private string ResolveRaceTier()
    {
        if (LobbyDataTransfer.Instance != null)
        {
            var localPlayer = LobbyDataTransfer.Instance.GetLocalPlayerData();
            if (localPlayer != null && !string.IsNullOrEmpty(localPlayer.tier))
                return localPlayer.tier;

            var allPlayers = LobbyDataTransfer.Instance.GetAllPlayers();
            if (allPlayers != null && allPlayers.Count > 0 && !string.IsNullOrEmpty(allPlayers[0].tier))
                return allPlayers[0].tier;
        }

        return "Beginner";
    }

    private int GetTierTotalRacers(string tier)
    {
        switch (tier)
        {
            case "Intermediate":
                return Mathf.Max(1, intermediateTotalRacers);
            case "Pro":
                return Mathf.Max(1, proTotalRacers);
            case "Beginner":
            default:
                return Mathf.Max(1, beginnerTotalRacers);
        }
    }
}
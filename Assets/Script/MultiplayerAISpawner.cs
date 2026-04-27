using FishNet.Object;
using UnityEngine;
using System.Collections;

public class MultiplayerAISpawner : NetworkBehaviour
{
    [Header("AI Manager")]
    public NetworkedAIManager aiManager;

    [Header("Dynamic AI Settings")]
    public int maxTotalRacers = 8;
    public int minAI = 0;
    public int maxAI = 6;

    [Header("Spawn Delay")]
    public float spawnDelay = 2f;

    private bool hasSpawned = false;

    public override void OnStartServer()
    {
        base.OnStartServer();
        Debug.Log("[MultiplayerAISpawner] Server started - scheduling AI spawn");
        Invoke(nameof(SpawnAIDynamically), spawnDelay);
    }

    [Server]
    private void SpawnAIDynamically()
    {
        if (hasSpawned)
        {
            Debug.LogWarning("[MultiplayerAISpawner] AI already spawned!");
            return;
        }

        if (aiManager == null)
        {
            Debug.LogError("[MultiplayerAISpawner] AI Manager not assigned!");
            return;
        }

        NetworkPlayer[] players = FindObjectsByType<NetworkPlayer>(FindObjectsSortMode.None);
        int playerCount = players.Length;

        Debug.Log($"[MultiplayerAISpawner] Found {playerCount} players");

        int aiToSpawn = Mathf.Max(minAI, maxTotalRacers - playerCount);
        aiToSpawn = Mathf.Clamp(aiToSpawn, minAI, maxAI);

        if (aiToSpawn > 0)
        {
            Debug.Log($"[MultiplayerAISpawner] Spawning {aiToSpawn} AI opponents (Players: {playerCount})");
            aiManager.numberOfOpponents = aiToSpawn;
            aiManager.SpawnOpponents();

            // Refresh rankings after spawn but do NOT start AI yet
            // AI will be started by RaceManager when countdown finishes
            StartCoroutine(RefreshRankingsAfterSpawn());
        }
        else
        {
            Debug.Log($"[MultiplayerAISpawner] No AI needed (Players: {playerCount})");
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
            Debug.Log("[MultiplayerAISpawner] Rankings refreshed on client");
        }
        else
        {
            Debug.LogWarning("[MultiplayerAISpawner] Ranking system not found!");
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
}
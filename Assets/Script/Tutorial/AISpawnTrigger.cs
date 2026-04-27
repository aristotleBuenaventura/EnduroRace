using UnityEngine;

/// <summary>
/// Add this script to the same GameObject as RaceManagerTutorial
/// It will automatically spawn AI opponents when the race starts
/// </summary>
public class AISpawnTrigger : MonoBehaviour
{
    [Header("References")]
    public AIOpponentManager aiOpponentManager;
    public RaceManagerTutorial raceManager;
    
    [Header("Spawn Timing")]
    public bool spawnOnRaceStart = true;
    public bool spawnOnCountdownEnd = false;
    public bool spawnManually = false; // For testing
    
    private bool hasSpawned = false;
    
    void Start()
    {
        if (raceManager == null)
            raceManager = GetComponent<RaceManagerTutorial>();
            
        if (aiOpponentManager == null)
        {
            Debug.Log("AI Manager not assigned, searching scene...");
            aiOpponentManager = Object.FindFirstObjectByType<AIOpponentManager>();
        }
            
        Debug.Log("=== AISpawnTrigger Ready ===");
        
        if (aiOpponentManager != null)
        {
            Debug.Log($"✓ AI Manager Found: {aiOpponentManager.gameObject.name}");
            Debug.Log($"  - Number of opponents: {aiOpponentManager.numberOfOpponents}");
            Debug.Log($"  - Prefab assigned: {(aiOpponentManager.aiOpponentPrefab != null ? "YES" : "NO")}");
            Debug.Log($"  - Spawn points: {(aiOpponentManager.spawnPoints != null ? aiOpponentManager.spawnPoints.Length : 0)}");
        }
        else
        {
            Debug.LogError("✗ AI Manager is NULL! Add AIOpponentManager to scene or assign reference.");
        }
        
        Debug.Log($"Race Manager: {(raceManager != null ? "Found" : "NULL")}");
    }
    
    void Update()
    {
        // Manual spawn for testing
        if (spawnManually && Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("Manual spawn triggered with 'P' key");
            SpawnAI();
        }
        
        // Auto spawn when race starts
        if (spawnOnRaceStart && !hasSpawned && raceManager != null && raceManager.raceStarted)
        {
            Debug.Log("Race started - spawning AI opponents");
            SpawnAI();
        }
    }
    
    public void SpawnAI()
    {
        Debug.Log(">>> SPAWNING AI OPPONENTS <<<");
        
        if (hasSpawned)
        {
            Debug.LogWarning("AI already spawned!");
            return;
        }
        
        if (aiOpponentManager == null)
        {
            Debug.LogError("AIOpponentManager is NULL! Cannot spawn AI.");
            Debug.LogError("Make sure AIOpponentManager GameObject exists in scene!");
            return;
        }
        
        Debug.Log($"Calling SpawnOpponents() on {aiOpponentManager.gameObject.name}...");
        
        try
        {
            aiOpponentManager.SpawnOpponents();
            hasSpawned = true;
            Debug.Log("SpawnOpponents() call completed");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Exception during spawn: {e.Message}");
            Debug.LogError($"Stack trace: {e.StackTrace}");
        }
    }
    
    // Call this from RaceManagerTutorial.StartRace() if you want
    public void OnRaceStart()
    {
        if (spawnOnCountdownEnd)
            SpawnAI();
    }
}
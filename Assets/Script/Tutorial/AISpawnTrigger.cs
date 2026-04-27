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
            ;
            aiOpponentManager = Object.FindFirstObjectByType<AIOpponentManager>();
        }
            
        ;
        
        if (aiOpponentManager != null)
        {
            ;
            ;
            ;
            ;
        }
        else
        {
            ;
        }
        
        ;
    }
    
    void Update()
    {
        // Manual spawn for testing
        if (spawnManually && Input.GetKeyDown(KeyCode.P))
        {
            ;
            SpawnAI();
        }
        
        // Auto spawn when race starts
        if (spawnOnRaceStart && !hasSpawned && raceManager != null && raceManager.raceStarted)
        {
            ;
            SpawnAI();
        }
    }
    
    public void SpawnAI()
    {
        ;
        
        if (hasSpawned)
        {
            ;
            return;
        }
        
        if (aiOpponentManager == null)
        {
            ;
            ;
            return;
        }
        
        ;
        
        try
        {
            aiOpponentManager.SpawnOpponents();
            hasSpawned = true;
            ;
        }
        catch (System.Exception e)
        {
            ;
            ;
        }
    }
    
    // Call this from RaceManagerTutorial.StartRace() if you want
    public void OnRaceStart()
    {
        if (spawnOnCountdownEnd)
            SpawnAI();
    }
}
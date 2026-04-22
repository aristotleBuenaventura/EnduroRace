using UnityEngine;

public class RaceCheckpoint : MonoBehaviour
{
    public int checkpointIndex = 0;
    
    private RaceRankingSystem rankingSystem;
    private WrongWayDetector wrongWayDetector; // ADD THIS
    
    private void Start()
    {
        rankingSystem = FindFirstObjectByType<RaceRankingSystem>();
        wrongWayDetector = FindFirstObjectByType<WrongWayDetector>(); // ADD THIS
        
        if (rankingSystem == null)
        {
            Debug.LogError($"Checkpoint {checkpointIndex}: RaceRankingSystem not found in scene!");
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"🎯 Checkpoint {checkpointIndex} triggered by: {other.name}");
        
        if (rankingSystem == null) 
        {
            Debug.LogError($"❌ No RankingSystem found!");
            return;
        }
        
        Transform racerTransform = other.transform;
        bool isPlayer = false;
        
        if (other.CompareTag("Player"))
        {
            isPlayer = true;
            if (other.transform.parent != null)
            {
                racerTransform = other.transform.parent;
            }
        }
        else if (other.transform.parent != null && other.transform.parent.CompareTag("Player"))
        {
            isPlayer = true;
            racerTransform = other.transform.parent;
        }
        
        if (isPlayer)
        {
            rankingSystem.OnCheckpointPassed(racerTransform, checkpointIndex);
            Debug.Log($"✅ PLAYER (via {other.name}) passed checkpoint {checkpointIndex}");
            
            // ADD THIS: Notify wrong way detector
            if (wrongWayDetector != null)
            {
                wrongWayDetector.OnPlayerCheckpointPassed(checkpointIndex);
            }
            
            return;
        }
        
        // AI code remains the same...
        AIOpponentController ai = other.GetComponent<AIOpponentController>();
        if (ai == null && other.transform.parent != null)
        {
            ai = other.transform.parent.GetComponent<AIOpponentController>();
            if (ai != null)
            {
                racerTransform = other.transform.parent;
            }
        }
        
        if (ai != null)
        {
            rankingSystem.OnCheckpointPassed(racerTransform, checkpointIndex);
            Debug.Log($"✅ AI {ai.opponentName} (via {other.name}) passed checkpoint {checkpointIndex}");
        }
        else
        {
            Debug.LogWarning($"⚠️ {other.name} hit checkpoint but no Player tag or AIOpponentController found");
        }
    }
}
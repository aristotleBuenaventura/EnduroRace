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
            ;
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        ;
        
        if (rankingSystem == null) 
        {
            ;
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
            ;
            
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
            ;
        }
        else
        {
            ;
        }
    }
}
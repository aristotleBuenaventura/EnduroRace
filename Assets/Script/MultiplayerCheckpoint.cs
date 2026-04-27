using UnityEngine;
using FishNet.Object;

/// <summary>
/// Checkpoint trigger for multiplayer races
/// Detects both NetworkPlayers and AI opponents
/// </summary>
public class MultiplayerCheckpoint : MonoBehaviour
{
    [Header("Checkpoint Settings")]
    public int checkpointIndex = 0;
    
    [Header("Visual")]
    public bool showGizmo = true;
    public Color gizmoColor = Color.yellow;
    
    private MultiplayerRaceRanking rankingSystem;
    private SceneWrongWayDetector wrongWayDetector; // ✅ CHANGED: Scene-based detector
    
    private void Start()
    {
        // Wait a frame for ranking system to initialize
        StartCoroutine(FindRankingSystem());
    }
    
    private System.Collections.IEnumerator FindRankingSystem()
    {
        // Wait for end of frame to ensure all GameObjects are initialized
        yield return new WaitForEndOfFrame();
        
        // Find the multiplayer ranking system
        rankingSystem = FindFirstObjectByType<MultiplayerRaceRanking>();
        
        if (rankingSystem == null)
        {
        }
        else
        {
        }
        
        // ✅ NEW: Find scene-based wrong way detector
        wrongWayDetector = FindFirstObjectByType<SceneWrongWayDetector>();
        if (wrongWayDetector != null)
        {
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (rankingSystem == null)
            return;
        
        // Check if it's a NetworkPlayer (real player)
        NetworkPlayer networkPlayer = other.GetComponentInParent<NetworkPlayer>();
        if (networkPlayer != null)
        {
            // Only count for the owner (prevents double-counting)
            if (networkPlayer.IsOwner)
            {
                rankingSystem.ReportCheckpointPassed(networkPlayer, checkpointIndex);
                
                // ✅ FIXED: Notify scene-based wrong way detector
                if (wrongWayDetector != null)
                {
                    wrongWayDetector.OnPlayerCheckpointPassed(checkpointIndex);
                }
            }
            return;
        }
        
        // Check if it's AI
        NetworkedAIOpponent aiOpponent = other.GetComponentInParent<NetworkedAIOpponent>();
        if (aiOpponent != null)
        {
            // AI checkpoints only processed on server
            if (aiOpponent.IsServerInitialized)
            {
                rankingSystem.OnAICheckpointPassed(aiOpponent.transform, checkpointIndex);
            }
            return;
        }
    }
    
    private void OnDrawGizmos()
    {
        if (!showGizmo) return;
        
        Gizmos.color = gizmoColor;
        
        // Draw checkpoint trigger area
        BoxCollider box = GetComponent<BoxCollider>();
        if (box != null)
        {
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.DrawWireCube(box.center, box.size);
        }
        
        SphereCollider sphere = GetComponent<SphereCollider>();
        if (sphere != null)
        {
            Gizmos.DrawWireSphere(transform.position + sphere.center, sphere.radius);
        }
    }
    
    private void OnDrawGizmosSelected()
    {
        if (!showGizmo) return;
        
        // Draw label
        #if UNITY_EDITOR
        UnityEditor.Handles.Label(
            transform.position + Vector3.up * 2f, 
            $"Checkpoint {checkpointIndex}",
            new GUIStyle() { 
                normal = new GUIStyleState() { textColor = Color.yellow },
                fontSize = 14,
                fontStyle = FontStyle.Bold
            }
        );
        #endif
    }
}
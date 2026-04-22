using UnityEngine;
using System.Collections.Generic;

public class AISegmentCheckpoint : MonoBehaviour
{
    [Header("Checkpoint Settings")]
    public AIOpponentController.AISegment nextSegment;
    public Transform transitionPosition;
    public Transform[] nextWaypointPath;
    
    [Header("Manager Reference")]
    public AIOpponentManager aiManager;

    // Track which AIs have already triggered this checkpoint
    private HashSet<AIOpponentController> triggered = new HashSet<AIOpponentController>();
    
    private void OnTriggerEnter(Collider other)
    {
        AIOpponentController ai = other.GetComponent<AIOpponentController>();
        
        if (ai == null) return;

        // Ignore if this AI already triggered this checkpoint
        if (triggered.Contains(ai)) return;
        triggered.Add(ai);

        if (transitionPosition != null)
            ai.TransitionToSegment(nextSegment, transitionPosition.position);
        else
            ai.SetSegment(nextSegment);
        
        if (nextWaypointPath != null && nextWaypointPath.Length > 0)
            ai.SetWaypointPath(nextWaypointPath);
    }
}
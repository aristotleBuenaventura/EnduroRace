using UnityEngine;
using FishNet.Object;

public class ParentFollowsChild : NetworkBehaviour
{
    [Header("References")]
    [Tooltip("The child object with CharacterController that moves")]
    public Transform childToFollow;
    
    [Header("Settings")]
    [Tooltip("Should parent match child rotation too?")]
    public bool syncRotation = true;
    
    [Tooltip("Offset between parent and child (usually zero)")]
    public Vector3 positionOffset = Vector3.zero;
    
    [Header("Debug")]
    public bool showDebugLogs = false;
    
    private Vector3 lastChildPosition;
    private Quaternion lastChildRotation;
    private bool initialized = false;

    public override void OnStartClient()
    {
        base.OnStartClient();
        
        // Only the owner should update parent position
        if (!IsOwner)
        {
            enabled = false;
            return;
        }
        
        Initialize();
    }

    private void Initialize()
    {
        if (initialized) return;
        
        // Auto-find child if not assigned
        if (childToFollow == null)
        {
            // Try to find child with CharacterController
            CharacterController[] controllers = GetComponentsInChildren<CharacterController>();
            
            if (controllers.Length > 0)
            {
                childToFollow = controllers[0].transform;
            }
            else
            {
                enabled = false;
                return;
            }
        }
        
        if (childToFollow.parent != transform)
        {
        }
        
        // Store initial positions
        lastChildPosition = childToFollow.position;
        lastChildRotation = childToFollow.rotation;
        
        initialized = true;
        
    }

    private void LateUpdate()
    {
        if (!initialized || childToFollow == null) return;
        
        // Only owner updates (prevents remote clients from fighting)
        if (!IsOwner) return;
        
        SyncPosition();
        
        if (syncRotation)
        {
            SyncRotation();
        }
    }

    private void SyncPosition()
    {
        Vector3 currentChildPos = childToFollow.position;
        
        // Check if child moved significantly
        if (Vector3.Distance(currentChildPos, lastChildPosition) > 0.001f)
        {
            // Calculate target position (child position + offset)
            Vector3 targetPosition = currentChildPos + positionOffset;
            
            // Move parent to match child
            transform.position = targetPosition;
            
            if (showDebugLogs)
            {
                Vector3 delta = currentChildPos - lastChildPosition;
            }
            
            lastChildPosition = currentChildPos;
            
            // NetworkTransform will automatically sync this to other clients
        }
    }

    private void SyncRotation()
    {
        Quaternion currentChildRot = childToFollow.rotation;
        
        // Check if child rotated significantly (more than 0.1 degrees)
        if (Quaternion.Angle(currentChildRot, lastChildRotation) > 0.1f)
        {
            // Make parent rotation match child
            transform.rotation = currentChildRot;
            
            if (showDebugLogs)
            {
            }
            
            lastChildRotation = currentChildRot;
        }
    }

    // ✅ Public method to manually sync (can be called if needed)
    public void ForceSync()
    {
        if (!initialized || childToFollow == null) return;
        
        transform.position = childToFollow.position + positionOffset;
        
        if (syncRotation)
        {
            transform.rotation = childToFollow.rotation;
        }
        
        lastChildPosition = childToFollow.position;
        lastChildRotation = childToFollow.rotation;
        
    }

    // ✅ Switch which child to follow (for Runner <-> Cyclist switching)
    public void SetChildToFollow(Transform newChild)
    {
        if (newChild == null)
        {
            return;
        }
        
        childToFollow = newChild;
        lastChildPosition = childToFollow.position;
        lastChildRotation = childToFollow.rotation;
        
        // Immediately sync to new child
        ForceSync();
        
    }

    private void OnDrawGizmosSelected()
    {
        if (childToFollow == null) return;
        
        // Draw line from parent to child
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, childToFollow.position);
        
        // Draw sphere at child position
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(childToFollow.position, 0.5f);
    }
}
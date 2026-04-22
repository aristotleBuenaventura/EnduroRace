using FishNet.Object;
using UnityEngine;

/// <summary>
/// Syncs player position across network for CharacterController-based movement
/// Add this to the Player (root) GameObject
/// IMPORTANT: This syncs the ACTIVE CHILD model (where CharacterController is)
/// </summary>
public class PlayerPositionSync : NetworkBehaviour
{
    [Header("Child Models")]
    [SerializeField] private GameObject runnerModel;
    [SerializeField] private GameObject cyclistModel;
    
    [Header("Sync Settings")]
    [SerializeField] private float sendRate = 0.05f; // Send 20 times per second
    [SerializeField] private float interpolationSpeed = 15f;
    
    private float lastSendTime;
    private Vector3 targetPosition;
    private Quaternion targetRotation;
    private Transform activeChild;
    
    private void Update()
    {
        // Find which child is active
        if (runnerModel != null && runnerModel.activeSelf)
            activeChild = runnerModel.transform;
        else if (cyclistModel != null && cyclistModel.activeSelf)
            activeChild = cyclistModel.transform;
        else
            return;
        
        if (IsOwner)
        {
            // Send active child's position to server at fixed intervals
            if (Time.time - lastSendTime >= sendRate)
            {
                ServerUpdatePosition(activeChild.position, activeChild.rotation);
                lastSendTime = Time.time;
            }
        }
        else
        {
            // Interpolate to target position for remote players
            activeChild.position = Vector3.Lerp(activeChild.position, targetPosition, Time.deltaTime * interpolationSpeed);
            activeChild.rotation = Quaternion.Lerp(activeChild.rotation, targetRotation, Time.deltaTime * interpolationSpeed);
        }
    }
    
    [ServerRpc]
    private void ServerUpdatePosition(Vector3 position, Quaternion rotation)
    {
        // Server receives position from owner, broadcasts to observers
        ObserversUpdatePosition(position, rotation);
    }
    
    [ObserversRpc(ExcludeOwner = true)]
    private void ObserversUpdatePosition(Vector3 position, Quaternion rotation)
    {
        // All clients except owner receive the position
        targetPosition = position;
        targetRotation = rotation;
    }
}
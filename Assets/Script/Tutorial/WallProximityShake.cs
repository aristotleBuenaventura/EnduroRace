using UnityEngine;
using Unity.Cinemachine;

public class WallProximityShake : MonoBehaviour
{
    [Header("Detection Settings")]
    [SerializeField] private float detectionDistance = 2f; // How close before shake triggers
    [SerializeField] private LayerMask wallLayer; // Set to Wall layer
    [SerializeField] private float checkInterval = 0.1f; // How often to check (performance)
    
    [Header("Camera Shake Settings")]
    [SerializeField] private CinemachineImpulseSource impulseSource;
    [SerializeField] private float shakeIntensity = 0.3f; // Continuous shake intensity
    [SerializeField] private float maxShakeIntensity = 0.8f; // Max shake when very close
    
    [Header("Raycast Settings")]
    [SerializeField] private Vector3 raycastOffset = new Vector3(0f, 1f, 0f); // Height offset from player
    [SerializeField] private bool debugRays = true; // Show debug rays in scene view
    
    private float checkTimer = 0f;
    private bool isNearWall = false;
    private float currentProximity = 0f; // 0 = far, 1 = very close

    void Start()
    {
        // Get impulse source if not assigned
        if (impulseSource == null)
            impulseSource = GetComponent<CinemachineImpulseSource>();
        
        if (impulseSource == null)
        {
            ;
        }
    }

    void Update()
    {
        checkTimer += Time.deltaTime;
        
        if (checkTimer >= checkInterval)
        {
            CheckWallProximity();
            checkTimer = 0f;
        }
    }

    private void CheckWallProximity()
    {
        Vector3 origin = transform.position + raycastOffset;
        bool wasNearWall = isNearWall;
        isNearWall = false;
        float closestDistance = detectionDistance;

        // Check in multiple directions (front, left, right, and diagonals)
        Vector3[] directions = new Vector3[]
        {
            transform.forward,           // Front
            transform.right,             // Right
            -transform.right,            // Left
            (transform.forward + transform.right).normalized,   // Front-right diagonal
            (transform.forward - transform.right).normalized    // Front-left diagonal
        };

        foreach (Vector3 direction in directions)
        {
            RaycastHit hit;
            
            if (Physics.Raycast(origin, direction, out hit, detectionDistance, wallLayer))
            {
                isNearWall = true;
                
                // Track the closest wall
                if (hit.distance < closestDistance)
                {
                    closestDistance = hit.distance;
                }
                
                // Debug visualization
                if (debugRays)
                {
                    ;
                }
            }
            else if (debugRays)
            {
                ;
            }
        }

        // Calculate proximity (0 = far, 1 = very close)
        if (isNearWall)
        {
            currentProximity = 1f - (closestDistance / detectionDistance);
            
            // Trigger shake based on proximity
            float intensity = Mathf.Lerp(shakeIntensity, maxShakeIntensity, currentProximity);
            TriggerContinuousShake(intensity);
        }
        else
        {
            currentProximity = 0f;
        }
    }

    private void TriggerContinuousShake(float intensity)
    {
        if (impulseSource != null)
        {
            // Generate small continuous impulses for proximity shake
            impulseSource.GenerateImpulse(intensity);
        }
    }

    // Optional: Visualize detection range in editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Vector3 origin = transform.position + raycastOffset;
        
        // Draw detection sphere
        Gizmos.DrawWireSphere(origin, detectionDistance);
    }
}
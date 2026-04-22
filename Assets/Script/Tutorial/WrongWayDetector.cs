using UnityEngine;
using TMPro;
using System.Collections;

public class WrongWayDetector : MonoBehaviour
{
    [Header("References")]
    public Transform playerTransform; // Assign your Player parent
    public PlayerControllerTutorial playerController; // Reference to your controller
    public Transform[] checkpoints; // Same checkpoints from RaceRankingSystem
    
    [Header("UI")]
    public GameObject wrongWayPanel; // UI panel that shows "WRONG WAY!"
    public TMP_Text wrongWayText; // Main "WRONG WAY!" text
    public TMP_Text countdownText; // Countdown timer text
    
    [Header("Settings")]
    public float checkInterval = 0.2f; // How often to check direction
    public float wrongWayAngleThreshold = 120f; // Degrees - if player facing >120° away from next checkpoint
    public float wrongWayDistanceThreshold = 20f; // How far player must be going wrong way before warning
    public float minSpeedToCheck = 0.5f; // Don't show wrong way if player is barely moving
    
    [Header("Teleport Settings")]
    public bool enableTeleportBack = true;
    public float teleportCountdown = 5f; // Seconds before teleporting back
    public float teleportDistanceBack = 10f; // Distance behind the last checkpoint to teleport to
    public bool lockInputDuringTeleport = true;
    public float teleportFreezeTime = 0.5f; // How long to freeze player after teleport
    
    [Header("Audio (Optional)")]
    public AudioClip wrongWaySound;
    public AudioClip teleportSound;
    private AudioSource audioSource;
    
    private int currentTargetCheckpoint = 0;
    private bool isGoingWrongWay = false;
    private float checkTimer = 0f;
    private float wrongWayTimer = 0f;
    private Vector3 lastPosition;
    private RaceRankingSystem rankingSystem;
    private Vector3 lastCheckpointPosition;
    private bool isTeleporting = false;
    
    private void Start()
    {
        if (wrongWayPanel != null)
            wrongWayPanel.SetActive(false);
        
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null && (wrongWaySound != null || teleportSound != null))
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
        }
        
        // Auto-find player controller if not assigned
        if (playerController == null && playerTransform != null)
        {
            playerController = playerTransform.GetComponentInChildren<PlayerControllerTutorial>();
        }
        
        rankingSystem = FindFirstObjectByType<RaceRankingSystem>();
        lastPosition = GetPlayerPosition();
        lastCheckpointPosition = GetPlayerPosition();
    }
    
    private void Update()
    {
        if (isTeleporting)
            return;
        
        checkTimer += Time.deltaTime;
        
        if (checkTimer >= checkInterval)
        {
            CheckWrongWay();
            checkTimer = 0f;
        }
        
        // Update countdown if going wrong way
        if (isGoingWrongWay && enableTeleportBack)
        {
            wrongWayTimer += Time.deltaTime;
            
            float timeRemaining = teleportCountdown - wrongWayTimer;
            
            if (timeRemaining <= 0f)
            {
                StartCoroutine(TeleportPlayerBack());
            }
            else
            {
                UpdateCountdownUI(timeRemaining);
            }
        }
    }
    
    private void CheckWrongWay()
    {
        if (checkpoints == null || checkpoints.Length == 0)
            return;
        
        if (currentTargetCheckpoint >= checkpoints.Length)
        {
            if (isGoingWrongWay)
            {
                HideWrongWayWarning();
            }
            return;
        }
        
        Vector3 playerPos = GetPlayerPosition();
        Vector3 playerMovement = playerPos - lastPosition;
        float playerSpeed = playerMovement.magnitude / checkInterval;
        
        // Don't check if player is barely moving or if input is locked
        if (playerSpeed < minSpeedToCheck || (playerController != null && playerController.inputLocked))
        {
            lastPosition = playerPos;
            return;
        }
        
        Vector3 toNextCheckpoint = checkpoints[currentTargetCheckpoint].position - playerPos;
        toNextCheckpoint.y = 0;
        
        Vector3 movementDir = playerMovement;
        movementDir.y = 0;
        
        if (movementDir.magnitude < 0.01f)
        {
            lastPosition = playerPos;
            return;
        }
        
        float angle = Vector3.Angle(movementDir.normalized, toNextCheckpoint.normalized);
        
        if (angle > wrongWayAngleThreshold)
        {
            float distanceToCheckpoint = toNextCheckpoint.magnitude;
            
            if (distanceToCheckpoint > wrongWayDistanceThreshold)
            {
                if (!isGoingWrongWay)
                {
                    ShowWrongWayWarning();
                }
            }
        }
        else
        {
            if (isGoingWrongWay)
            {
                HideWrongWayWarning();
            }
        }
        
        lastPosition = playerPos;
    }
    
    private void ShowWrongWayWarning()
    {
        isGoingWrongWay = true;
        wrongWayTimer = 0f;
        
        if (wrongWayPanel != null)
        {
            wrongWayPanel.SetActive(true);
        }
        
        if (wrongWayText != null)
        {
            wrongWayText.text = "WRONG WAY!";
        }
        
        if (audioSource != null && wrongWaySound != null && !audioSource.isPlaying)
        {
            audioSource.PlayOneShot(wrongWaySound);
        }
        
        Debug.Log("⚠️ WRONG WAY!");
    }
    
    private void HideWrongWayWarning()
    {
        isGoingWrongWay = false;
        wrongWayTimer = 0f;
        
        if (wrongWayPanel != null)
        {
            wrongWayPanel.SetActive(false);
        }
    }
    
    private void UpdateCountdownUI(float timeRemaining)
    {
        if (countdownText != null)
        {
            countdownText.text = $"Turn around! {Mathf.Ceil(timeRemaining)}";
            
            // Flash red when time is running out
            if (timeRemaining <= 2f)
            {
                float flash = Mathf.PingPong(Time.time * 3f, 1f);
                countdownText.color = Color.Lerp(Color.white, Color.red, flash);
            }
            else
            {
                countdownText.color = Color.white;
            }
        }
    }
    
    private IEnumerator TeleportPlayerBack()
    {
        Debug.Log("🔄 Teleporting player back to last checkpoint!");
        
        isTeleporting = true;
        
        // Lock input if enabled
        if (lockInputDuringTeleport && playerController != null)
        {
            playerController.inputLocked = true;
        }
        
        // Calculate teleport position
        Vector3 teleportPosition = lastCheckpointPosition;
        Quaternion teleportRotation = Quaternion.identity;
        
        if (currentTargetCheckpoint > 0 && currentTargetCheckpoint <= checkpoints.Length)
        {
            int lastPassedCheckpoint = currentTargetCheckpoint - 1;
            Vector3 checkpointPos = checkpoints[lastPassedCheckpoint].position;
            
            // Calculate direction to next checkpoint
            Vector3 forwardDir = Vector3.forward;
            if (currentTargetCheckpoint < checkpoints.Length)
            {
                forwardDir = (checkpoints[currentTargetCheckpoint].position - checkpointPos).normalized;
                teleportRotation = Quaternion.LookRotation(forwardDir);
            }
            
            // Position player behind the checkpoint, facing forward
            teleportPosition = checkpointPos - (forwardDir * teleportDistanceBack);
            teleportPosition.y = checkpointPos.y; // Match checkpoint height
        }
        
        // Get the active child (PlayeronFoot or PlayeronBike)
        Transform activeChild = GetActivePlayerChild();
        CharacterController controller = null;
        
        if (activeChild != null)
        {
            controller = activeChild.GetComponent<CharacterController>();
        }
        else if (playerTransform != null)
        {
            controller = playerTransform.GetComponent<CharacterController>();
        }
        
        // Disable CharacterController temporarily for teleport
        if (controller != null)
        {
            controller.enabled = false;
        }
        
        // Teleport
        if (activeChild != null)
        {
            activeChild.position = teleportPosition;
            activeChild.rotation = teleportRotation;
        }
        else if (playerTransform != null)
        {
            playerTransform.position = teleportPosition;
            playerTransform.rotation = teleportRotation;
        }
        
        // Re-enable CharacterController
        if (controller != null)
        {
            controller.enabled = true;
        }
        
        // Play teleport sound
        if (audioSource != null && teleportSound != null)
        {
            audioSource.PlayOneShot(teleportSound);
        }
        
        // Hide wrong way warning
        HideWrongWayWarning();
        
        // Freeze player briefly after teleport
        yield return new WaitForSeconds(teleportFreezeTime);
        
        // Unlock input
        if (lockInputDuringTeleport && playerController != null)
        {
            playerController.inputLocked = false;
        }
        
        // Reset tracking
        lastPosition = GetPlayerPosition();
        isTeleporting = false;
        
        Debug.Log("✅ Teleport complete!");
    }
    
    private Transform GetActivePlayerChild()
    {
        if (playerTransform == null)
            return null;
        
        Transform footChild = playerTransform.Find("PlayeronFoot");
        Transform bikeChild = playerTransform.Find("PlayeronBike");
        
        if (footChild != null && footChild.gameObject.activeSelf)
        {
            return footChild;
        }
        if (bikeChild != null && bikeChild.gameObject.activeSelf)
        {
            return bikeChild;
        }
        
        return null;
    }
    
    private Vector3 GetPlayerPosition()
    {
        if (playerTransform != null)
        {
            Transform footChild = playerTransform.Find("PlayeronFoot");
            Transform bikeChild = playerTransform.Find("PlayeronBike");
            
            if (footChild != null && footChild.gameObject.activeSelf)
            {
                return footChild.position;
            }
            if (bikeChild != null && bikeChild.gameObject.activeSelf)
            {
                return bikeChild.position;
            }
            
            return playerTransform.position;
        }
        
        return Vector3.zero;
    }
    
    public void OnPlayerCheckpointPassed(int checkpointIndex)
    {
        if (checkpointIndex >= currentTargetCheckpoint)
        {
            currentTargetCheckpoint = checkpointIndex + 1;
            
            // Update last checkpoint position for teleporting
            if (checkpointIndex < checkpoints.Length)
            {
                lastCheckpointPosition = checkpoints[checkpointIndex].position;
            }
            
            Debug.Log($"Player now targeting checkpoint {currentTargetCheckpoint}");
            
            if (isGoingWrongWay)
            {
                HideWrongWayWarning();
            }
        }
    }
}
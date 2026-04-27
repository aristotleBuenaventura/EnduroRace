using UnityEngine;
using TMPro;
using System.Collections;
using FishNet.Object;

/// <summary>
/// Scene-based wrong way detector
/// Place this on a GameObject IN THE SCENE (not on player prefab)
/// It will automatically find and track the local player
/// </summary>
public class SceneWrongWayDetector : MonoBehaviour
{
    [Header("UI - Assign in Inspector")]
    public GameObject wrongWayPanel;
    public TMP_Text wrongWayText;
    public TMP_Text countdownText;

    [Header("Settings")]
    public float checkInterval          = 0.2f;
    public float wrongWayAngleThreshold = 140f;
    public float minSpeedToCheck        = 1.0f;
    [SerializeField] private float movementSmoothFactor = 0.3f;

    [Header("Teleport Settings")]
    public bool  enableTeleportBack   = true;
    public float teleportCountdown    = 5f;
    public float teleportDistanceBack = 10f;
    public float teleportFreezeTime   = 0.5f;

    [Header("Startup")]
    public float startupDelay = 5f;

    [Header("Audio (Optional)")]
    public AudioClip wrongWaySound;
    public AudioClip teleportSound;
    private AudioSource audioSource;

    // References
    private Transform[]   checkpoints;
    private NetworkPlayer localPlayer;
    private Transform     playerTransform;

    // State
    private int     currentTargetCheckpoint = 0;
    private bool    isGoingWrongWay         = false;
    private float   checkTimer              = 0f;
    private float   wrongWayTimer           = 0f;
    private float   wrongWayShownTime       = 0f;
    private const float minWrongWayDisplayTime = 2f;
    private Vector3 smoothedMovementDir     = Vector3.zero;
    private Vector3 lastPosition;
    private Vector3 lastCheckpointPosition;
    private bool    isTeleporting           = false;
    private float   startupTimer            = 0f;
    private bool    hasStarted              = false;

    // ── Unity ────────────────────────────────────────────────────────────────────

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null && (wrongWaySound != null || teleportSound != null))
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
        }

        if (wrongWayPanel != null)
            wrongWayPanel.SetActive(false);

        StartCoroutine(FindSystems());
    }

    private void Update()
    {
        if (localPlayer == null || playerTransform == null)
            return;

        // Startup delay — keep lastPosition fresh so first check is accurate
        if (!hasStarted)
        {
            startupTimer += Time.deltaTime;
            lastPosition  = GetPlayerPosition();
            if (startupTimer >= startupDelay)
            {
                hasStarted = true;
                ;
            }
            return;
        }

        if (isTeleporting)
            return;

        // Track how long warning has been visible
        if (isGoingWrongWay)
        {
            wrongWayShownTime += Time.deltaTime;

            if (enableTeleportBack)
            {
                wrongWayTimer += Time.deltaTime;
                float timeRemaining = teleportCountdown - wrongWayTimer;
                UpdateCountdownUI(timeRemaining);

                if (timeRemaining <= 0f)
                    StartCoroutine(TeleportPlayer());
            }
        }

        checkTimer += Time.deltaTime;
        if (checkTimer >= checkInterval)
        {
            CheckWrongWay();
            checkTimer = 0f;
        }
    }

    // ── Systems discovery ────────────────────────────────────────────────────────

    private IEnumerator FindSystems()
    {
        int retries = 0;
        while (checkpoints == null && retries < 10)
        {
            MultiplayerRaceRanking ranking = FindFirstObjectByType<MultiplayerRaceRanking>();
            if (ranking != null)
            {
                checkpoints = ranking.checkpoints;
                ;
                break;
            }
            retries++;
            ;
            yield return new WaitForSeconds(0.5f);
        }

        if (checkpoints == null)
        {
            ;
            enabled = false;
            yield break;
        }

        yield return StartCoroutine(FindLocalPlayer());
    }

    private IEnumerator FindLocalPlayer()
    {
        while (localPlayer == null)
        {
            NetworkPlayer[] players = FindObjectsByType<NetworkPlayer>(FindObjectsSortMode.None);
            foreach (var player in players)
            {
                if (player.IsOwner)
                {
                    localPlayer            = player;
                    playerTransform        = player.transform;
                    lastPosition           = GetPlayerPosition();
                    lastCheckpointPosition = GetPlayerPosition();
                    ;
                    yield break;
                }
            }
            yield return new WaitForSeconds(0.5f);
        }
    }

    // ── Core check ───────────────────────────────────────────────────────────────

    private void CheckWrongWay()
    {
        if (checkpoints == null || checkpoints.Length == 0) return;

        if (currentTargetCheckpoint >= checkpoints.Length)
        {
            if (isGoingWrongWay) HideWrongWayWarning();
            return;
        }

        // Skip while player is frozen/stunned
        if (localPlayer != null && !localPlayer.CanPlayerMove)
        {
            lastPosition        = GetPlayerPosition();
            smoothedMovementDir = Vector3.zero;
            return;
        }

        Vector3 playerPos      = GetPlayerPosition();
        Vector3 playerMovement = playerPos - lastPosition;
        float   playerSpeed    = playerMovement.magnitude / checkInterval;

        lastPosition = playerPos;

        if (playerSpeed < minSpeedToCheck)
        {
            // Decay very slowly — a single slow frame won't wipe out accumulated direction
            smoothedMovementDir = Vector3.Lerp(smoothedMovementDir, Vector3.zero, 0.05f);

            // Only hide if direction has fully decayed AND min display time has passed
            if (smoothedMovementDir.magnitude < 0.1f && isGoingWrongWay)
                HideWrongWayWarning();

            return;
        }

        // Accumulate direction over multiple samples — resistant to single bad frames
        Vector3 rawDir = new Vector3(playerMovement.x, 0f, playerMovement.z).normalized;
        smoothedMovementDir = Vector3.Lerp(smoothedMovementDir, rawDir, movementSmoothFactor);

        // Wait until we have enough accumulated direction to make a reliable decision
        if (smoothedMovementDir.magnitude < 0.3f) return;

        // Track forward = direction from last checkpoint to next checkpoint
        Vector3 trackForward;
        if (currentTargetCheckpoint > 0)
        {
            trackForward = checkpoints[currentTargetCheckpoint].position
                         - checkpoints[currentTargetCheckpoint - 1].position;
        }
        else
        {
            trackForward = checkpoints[0].position - lastCheckpointPosition;
        }

        trackForward.y = 0;
        if (trackForward.sqrMagnitude < 0.001f) return;
        trackForward.Normalize();

        float angle = Vector3.Angle(smoothedMovementDir.normalized, trackForward);

        if (angle > wrongWayAngleThreshold)
        {
            if (!isGoingWrongWay)
                ShowWrongWayWarning();
        }
        else
        {
            if (isGoingWrongWay)
                HideWrongWayWarning();
        }
    }

    // ── UI ───────────────────────────────────────────────────────────────────────

    private void ShowWrongWayWarning()
    {
        isGoingWrongWay   = true;
        wrongWayTimer     = 0f;
        wrongWayShownTime = 0f;

        if (wrongWayPanel != null)
            wrongWayPanel.SetActive(true);

        if (wrongWayText != null)
            wrongWayText.text = "WRONG WAY!";

        if (countdownText != null)
        {
            countdownText.gameObject.SetActive(true);
            countdownText.text  = $"Turn around! {Mathf.Ceil(teleportCountdown)}";
            countdownText.color = Color.white;
        }

        if (audioSource != null && wrongWaySound != null && !audioSource.isPlaying)
            audioSource.PlayOneShot(wrongWaySound);

        ;
    }

    private void HideWrongWayWarning()
    {
        // Don't hide until warning has been shown for minimum time
        if (isGoingWrongWay && wrongWayShownTime < minWrongWayDisplayTime)
            return;

        isGoingWrongWay   = false;
        wrongWayTimer     = 0f;
        wrongWayShownTime = 0f;

        if (wrongWayPanel != null)
            wrongWayPanel.SetActive(false);
    }

    private void UpdateCountdownUI(float timeRemaining)
    {
        if (countdownText == null) return;

        countdownText.text = $"Turn around! {Mathf.Ceil(timeRemaining)}";

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

    // ── Teleport ─────────────────────────────────────────────────────────────────

    private IEnumerator TeleportPlayer()
    {
        ;

        isTeleporting = true;

        if (localPlayer != null)
            localPlayer.SetCanMove(false);

        Vector3    teleportPosition = lastCheckpointPosition;
        Quaternion teleportRotation = Quaternion.identity;

        if (currentTargetCheckpoint > 0 && currentTargetCheckpoint <= checkpoints.Length)
        {
            int     lastPassed    = currentTargetCheckpoint - 1;
            Vector3 checkpointPos = checkpoints[lastPassed].position;

            Vector3 forwardDir = Vector3.forward;
            if (currentTargetCheckpoint < checkpoints.Length)
            {
                forwardDir       = (checkpoints[currentTargetCheckpoint].position - checkpointPos).normalized;
                teleportRotation = Quaternion.LookRotation(forwardDir);
            }

            teleportPosition   = checkpointPos - (forwardDir * teleportDistanceBack);
            teleportPosition.y = checkpointPos.y;
        }

        CharacterController cc = GetActiveCharacterController();
        if (cc != null) cc.enabled = false;

        playerTransform.position = teleportPosition;
        playerTransform.rotation = teleportRotation;

        Transform activeChild = GetActiveChild();
        if (activeChild != null)
        {
            activeChild.localPosition = Vector3.zero;
            activeChild.localRotation = Quaternion.identity;
        }

        if (cc != null) cc.enabled = true;

        // Reset smoothed direction so we don't immediately re-trigger wrong way after teleport
        smoothedMovementDir = Vector3.zero;

        // Force hide — bypass min display time since teleport overrides
        isGoingWrongWay   = false;
        wrongWayTimer     = 0f;
        wrongWayShownTime = 0f;
        if (wrongWayPanel != null)
            wrongWayPanel.SetActive(false);

        if (audioSource != null && teleportSound != null)
            audioSource.PlayOneShot(teleportSound);

        lastPosition = GetPlayerPosition();

        yield return new WaitForSeconds(teleportFreezeTime);

        if (localPlayer != null)
            localPlayer.SetCanMove(true);

        isTeleporting = false;

        ;
    }

    // ── Helpers ──────────────────────────────────────────────────────────────────

    private Vector3 GetPlayerPosition()
    {
        Transform child = GetActiveChild();
        return child != null
            ? child.position
            : (playerTransform != null ? playerTransform.position : Vector3.zero);
    }

    private Transform GetActiveChild()
    {
        if (playerTransform == null) return null;

        Transform foot = playerTransform.Find("PlayeronFoot");
        Transform bike = playerTransform.Find("PlayeronBike");

        if (foot != null && foot.gameObject.activeSelf) return foot;
        if (bike != null && bike.gameObject.activeSelf) return bike;

        return null;
    }

    private CharacterController GetActiveCharacterController()
    {
        Transform child = GetActiveChild();
        return child != null ? child.GetComponent<CharacterController>() : null;
    }

    // ── Public API ───────────────────────────────────────────────────────────────

    public void OnPlayerCheckpointPassed(int checkpointIndex)
    {
        if (checkpointIndex >= currentTargetCheckpoint)
        {
            currentTargetCheckpoint = checkpointIndex + 1;

            if (checkpointIndex < checkpoints.Length)
                lastCheckpointPosition = checkpoints[checkpointIndex].position;

            ;

            if (isGoingWrongWay)
                HideWrongWayWarning();
        }
    }
}
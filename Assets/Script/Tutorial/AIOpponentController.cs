using UnityEngine;
using System.Collections;
using TMPro;

public class AIOpponentController : MonoBehaviour
{
    [Header("AI Settings")]
    public string opponentName = "Opponent";
    public float baseSpeed = 4f;
    public float speedVariation = 0.5f;
    public float staminaUsageMultiplier = 1.0f;

    [Header("Segment Models")]
    public GameObject runnerModel;
    public GameObject cyclistModel;

    [Header("Name Tag")]
    public TextMeshProUGUI nameTagText;

    [Header("Path Following")]
    public Transform[] waypointPath;
    public int currentWaypointIndex = 0;
    public float waypointReachDistance = 2f;
    public float rotationSpeed = 5f;
    public enum AISegment { Swim, Bike, Run }

    [Header("Stamina")]
    public float maxStamina = 100f;
    private float currentStamina;
    public float staminaRegenRate = 10f;
    public float staminaDrainRate = 15f;

    [Header("Current State")]
    public AISegment currentSegment = AISegment.Swim;

    [Header("Animation")]
    public Animator runnerAnimator;
    public Animator cyclistAnimator;

    [Header("AI Behavior")]
    public bool useSprintRandomly = true;
    public float sprintChance = 0.3f;
    public float sprintDuration = 3f;
    public float sprintCooldown = 5f;

    [Header("Water Settings")]
    public float swimSpeed = 10f;
    public float swimSpeedMultiplier = 2f;
    public float waterLevelOffset = 2f;
    public float bodyDepthOffset = 0.3f;
    public float playerHeightOffset = 0.3f;
    public float treadOffset = 0.78f;
    public float floatStrength = 0.53f;
    public float waterEntryDelay = 0.25f;

    [Header("Gravity")]
    public float gravity = -9.81f;

    // Used by AIOpponentManager to pause movement during cutscenes
    // WITHOUT disabling the component (which would kill coroutines)
    [HideInInspector] public bool isPaused = false;

    private bool isSprinting = false;
    private float sprintTimer = 0f;
    private float sprintCooldownTimer = 0f;
    private CharacterController characterController;
    private float currentSpeed;
    private Vector3 velocity;
    private Vector3 lastPosition;
    private bool isMoving = false;
    private bool isInWater = false;
    private bool isFullyInWater = false;
    private float waterSurfaceY = 0f;
    private Vector3 pushVelocity = Vector3.zero;
    private float pushSlowdownTimer = 0f;
    private float pushSlowdownAmount = 0f;
    private Coroutine waterEntryCoroutine;
    private Coroutine waterExitCoroutine;

    private void Start()
    {

        characterController = GetComponent<CharacterController>();
        if (characterController == null)
        {
        }
        else
        {
        }

        var colliders = GetComponents<Collider>();
        foreach (var col in colliders)
        {
        }

        currentStamina = maxStamina;
        currentSpeed = baseSpeed + Random.Range(-speedVariation, speedVariation);
        velocity = Vector3.zero;
        lastPosition = transform.position;


        if (runnerModel == null)
        {
        }
        else
        {
            var renderers = runnerModel.GetComponentsInChildren<Renderer>();
            if (renderers.Length == 0)
        }

        if (cyclistModel == null)
        else

        if (nameTagText != null)
            nameTagText.text = opponentName;

        SetSegment(AISegment.Swim);

        if (waypointPath == null || waypointPath.Length == 0)
        else
    }

    private void Update()
    {
        // Skip movement while paused — but keep the component enabled
        // so coroutines (like TransitionRoutine) continue running
        if (isPaused) return;

        HandleStamina();
        HandleSprinting();
        FollowPath();

        if (pushSlowdownTimer > 0f) pushSlowdownTimer -= Time.deltaTime;

        if (pushVelocity.magnitude > 0.1f)
        {
            characterController.Move(pushVelocity * Time.deltaTime);
            pushVelocity = Vector3.Lerp(pushVelocity, Vector3.zero, Time.deltaTime * 5f);
        }

        if (isFullyInWater && currentSegment == AISegment.Swim)
            HandleWaterFloating();
        else
            ApplyGravity();

        float distanceMoved = Vector3.Distance(
            new Vector3(transform.position.x, 0, transform.position.z),
            new Vector3(lastPosition.x, 0, lastPosition.z)
        );

        if (distanceMoved > 0.01f)
            isMoving = true;
        else if (distanceMoved < 0.001f)
            isMoving = false;

        lastPosition = transform.position;

        UpdateAnimations();
    }

    private void LateUpdate()
    {
        if (nameTagText != null && Camera.main != null)
        {
            nameTagText.transform.LookAt(
                nameTagText.transform.position + Camera.main.transform.rotation * Vector3.forward,
                Camera.main.transform.rotation * Vector3.up
            );
        }
    }

    private void HandleStamina()
    {
        if (isSprinting && currentStamina > 0f)
        {
            currentStamina -= staminaDrainRate * staminaUsageMultiplier * Time.deltaTime;
            if (currentStamina <= 0f)
            {
                currentStamina = 0f;
                isSprinting = false;
            }
        }
        else
        {
            currentStamina += staminaRegenRate * Time.deltaTime;
            if (currentStamina > maxStamina)
                currentStamina = maxStamina;
        }
    }

    private void HandleSprinting()
    {
        if (isSprinting)
        {
            sprintTimer -= Time.deltaTime;
            if (sprintTimer <= 0f)
            {
                isSprinting = false;
                sprintCooldownTimer = sprintCooldown;
            }
        }
        else if (useSprintRandomly && sprintCooldownTimer <= 0f)
        {
            if (Random.value < sprintChance * Time.deltaTime && currentStamina > 30f)
            {
                isSprinting = true;
                sprintTimer = sprintDuration;
            }
        }

        if (sprintCooldownTimer > 0f)
            sprintCooldownTimer -= Time.deltaTime;
    }

    private void FollowPath()
    {
        if (waypointPath == null || waypointPath.Length == 0)
        {
            return;
        }

        if (currentWaypointIndex >= waypointPath.Length)
        {
            return;
        }

        Transform targetWaypoint = waypointPath[currentWaypointIndex];

        if (targetWaypoint == null)
        {
            currentWaypointIndex++;
            return;
        }

        Vector3 direction = (targetWaypoint.position - transform.position).normalized;
        direction.y = 0f;

        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        float moveSpeed;

        if (isFullyInWater && currentSegment == AISegment.Swim)
        {
            moveSpeed = swimSpeed;
            if (isSprinting && currentStamina > 0f) moveSpeed *= swimSpeedMultiplier;
        }
        else
        {
            moveSpeed = currentSpeed;
            if (isSprinting && currentStamina > 0f) moveSpeed *= 1.8f;
        }

        if (pushSlowdownTimer > 0f) moveSpeed *= (1f - pushSlowdownAmount);

        // Safety net — currentSpeed should never be 0 outside of a transition
        if (currentSpeed <= 0f)
        {
            currentSpeed = baseSpeed;
            moveSpeed = currentSpeed;
        }

        Vector3 moveDirection = direction * moveSpeed * Time.deltaTime;
        characterController.Move(moveDirection);

        float distance = Vector3.Distance(
            new Vector3(transform.position.x, 0, transform.position.z),
            new Vector3(targetWaypoint.position.x, 0, targetWaypoint.position.z)
        );

        if (distance < waypointReachDistance)
        {
            currentWaypointIndex++;
        }
    }

    private void ApplyGravity()
    {
        if (characterController.isGrounded && velocity.y < 0)
            velocity.y = -2f;
        else
            velocity.y += gravity * Time.deltaTime;

        characterController.Move(velocity * Time.deltaTime);
    }

    private void HandleWaterFloating()
    {
        float targetY = isMoving
            ? (waterSurfaceY + waterLevelOffset - bodyDepthOffset) - playerHeightOffset
            : (waterSurfaceY + waterLevelOffset - bodyDepthOffset) - playerHeightOffset - treadOffset;

        float verticalMove = (targetY - transform.position.y) * floatStrength * Time.deltaTime;
        characterController.Move(new Vector3(0f, verticalMove, 0f));
        velocity.y = 0f;
    }

    private void UpdateAnimations()
    {
        if ((isFullyInWater || isInWater) && currentSegment == AISegment.Swim)
        {
            if (runnerAnimator != null)
            {
                runnerAnimator.SetBool("isSwimming", isMoving);
                runnerAnimator.SetBool("isTreading", !isMoving);
                runnerAnimator.SetBool("isJogging", false);
                runnerAnimator.SetBool("isRunning", false);
            }
        }
        else if (currentSegment == AISegment.Swim || currentSegment == AISegment.Run)
        {
            if (runnerAnimator != null)
            {
                runnerAnimator.SetBool("isSwimming", false);
                runnerAnimator.SetBool("isTreading", false);
                runnerAnimator.SetBool("isJogging", isMoving && !isSprinting);
                runnerAnimator.SetBool("isRunning", isMoving && isSprinting);
            }
        }
        else if (currentSegment == AISegment.Bike)
        {
            if (cyclistAnimator != null)
            {
                if (!isMoving)
                {
                    cyclistAnimator.SetBool("isIdle", true);
                    cyclistAnimator.SetBool("isMove", false);
                    cyclistAnimator.SetBool("isAccel", false);
                }
                else if (isSprinting)
                {
                    cyclistAnimator.SetBool("isIdle", false);
                    cyclistAnimator.SetBool("isMove", false);
                    cyclistAnimator.SetBool("isAccel", true);
                }
                else
                {
                    cyclistAnimator.SetBool("isIdle", false);
                    cyclistAnimator.SetBool("isMove", true);
                    cyclistAnimator.SetBool("isAccel", false);
                }
            }
        }
    }

    public void SetSegment(AISegment segment)
    {
        currentSegment = segment;

        switch (segment)
        {
            case AISegment.Swim:
                runnerModel.SetActive(true);
                cyclistModel.SetActive(false);
                break;
            case AISegment.Bike:
                runnerModel.SetActive(false);
                cyclistModel.SetActive(true);
                break;
            case AISegment.Run:
                runnerModel.SetActive(true);
                cyclistModel.SetActive(false);
                break;
        }
    }

    public void TransitionToSegment(AISegment newSegment, Vector3 transitionPosition)
    {
        StartCoroutine(TransitionRoutine(newSegment, transitionPosition));
    }

    private IEnumerator TransitionRoutine(AISegment newSegment, Vector3 position)
    {
        float originalSpeed = currentSpeed;
        currentSpeed = 0f;


        // WaitForSecondsRealtime is unaffected by Time.timeScale
        // Component stays enabled so this coroutine is never killed by pausing
        yield return new WaitForSecondsRealtime(0.5f);

        transform.position = position;
        SetSegment(newSegment);

        yield return new WaitForSecondsRealtime(0.5f);

        // If originalSpeed is 0 for any reason, fall back to baseSpeed
        currentSpeed = originalSpeed > 0f ? originalSpeed : baseSpeed;
    }

    public void SetWaypointPath(Transform[] newPath)
    {
        waypointPath = newPath;
        currentWaypointIndex = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            if (waterExitCoroutine != null)
            {
                StopCoroutine(waterExitCoroutine);
                waterExitCoroutine = null;
            }
            if (waterEntryCoroutine != null) StopCoroutine(waterEntryCoroutine);
            waterEntryCoroutine = StartCoroutine(EnterWaterDelayed(other.bounds.max.y));
        }
    }

    private IEnumerator EnterWaterDelayed(float surfaceY)
    {
        yield return new WaitForSeconds(waterEntryDelay);
        isInWater = true;
        isFullyInWater = true;
        waterSurfaceY = surfaceY;
        velocity.y = 0f;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            if (waterEntryCoroutine != null)
            {
                StopCoroutine(waterEntryCoroutine);
                waterEntryCoroutine = null;
            }
            if (waterExitCoroutine != null) StopCoroutine(waterExitCoroutine);
            waterExitCoroutine = StartCoroutine(ExitWaterDelayed());
        }
    }

    private IEnumerator ExitWaterDelayed()
    {
        yield return new WaitForSeconds(0.5f);
        isInWater = false;
        isFullyInWater = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            if (waterExitCoroutine != null)
            {
                StopCoroutine(waterExitCoroutine);
                waterExitCoroutine = null;
            }
            if (isInWater)
                waterSurfaceY = other.bounds.max.y;
        }
    }

    public void ApplyCollisionPush(Vector3 direction, float force, float slowdown, float duration)
    {
        pushVelocity = direction * force;
        pushSlowdownAmount = slowdown;
        pushSlowdownTimer = duration;
    }
}
using UnityEngine;
using System.Collections;
using FishNet.Object;
using FishNet.Object.Synchronizing;

public class NetworkedAIOpponent : NetworkBehaviour
{
    [Header("AI Settings")]
    public string opponentName = "Opponent";
    public float baseSpeed = 4f;
    public float speedVariation = 0.5f;
    public float staminaUsageMultiplier = 1.0f;

    [Header("Segment Models")]
    public GameObject runnerModel;
    public GameObject cyclistModel;

    [Header("Path Following")]
    public Transform[] waypointPath;
    public int currentWaypointIndex = 0;
    public float waypointReachDistance = 2f;
    public float rotationSpeed = 5f;
    public float pathLateralOffset = 0f;
    public enum AISegment { Swim, Bike, Run }

    [Header("Stamina")]
    public float maxStamina = 100f;
    private float currentStamina;
    public float staminaRegenRate = 10f;
    public float staminaDrainRate = 15f;

    // FIX: FishNet v4 generic SyncVar<T> — replaces the obsolete [SyncVar] attribute.
    // Subscribe to OnChange in OnStartNetwork so clients react when the server updates the segment.
    private readonly SyncVar<AISegment> _currentSegment = new SyncVar<AISegment>(AISegment.Swim);
    private readonly SyncVar<float> _swimAnimationSpeed = new SyncVar<float>(1f);
    private readonly SyncVar<float> _bikeAnimationSpeed = new SyncVar<float>(1f);
    private readonly SyncVar<float> _runAnimationSpeed = new SyncVar<float>(1f);
    private readonly SyncVar<float> _animationCadenceFrequency = new SyncVar<float>(0.9f);
    private readonly SyncVar<float> _animationCadenceAmplitude = new SyncVar<float>(0.08f);
    private readonly SyncVar<float> _animationCadencePhase = new SyncVar<float>(0f);
    private readonly SyncVar<float> _networkPaceMultiplier = new SyncVar<float>(1f);

    // Public accessor so all existing code (NetworkedAIManager, etc.) compiles unchanged
    public AISegment currentSegment
    {
        get => _currentSegment.Value;
        private set => _currentSegment.Value = value;
    }

    [Header("Animation")]
    public Animator runnerAnimator;
    public Animator cyclistAnimator;

    [Header("AI Behavior")]
    public bool useSprintRandomly = true;
    public float sprintChance = 0.3f;
    public float sprintDuration = 3f;
    public float sprintCooldown = 5f;

    [Header("Water Settings")]
    public float swimSpeed = 2f;
    public float swimSpeedMultiplier = 2f;
    public float waterLevelOffset = 2f;
    public float bodyDepthOffset = 0.3f;
    public float playerHeightOffset = 0.3f;
    public float treadOffset = 0.78f;
    public float floatStrength = 0.53f;
    public float waterEntryDelay = 0.25f;

    [Header("Gravity")]
    public float gravity = -9.81f;

    // Race control
    private bool raceStarted = false;

    private bool isSprinting = false;
    private float sprintTimer = 0f;
    private float sprintCooldownTimer = 0f;
    private CharacterController characterController;
    private float currentSpeed;
    private Vector3 velocity;
    private Vector3 lastPosition;
    private bool isMoving = false;
    private readonly SyncVar<bool> _isInWater = new SyncVar<bool>(false);
    private readonly SyncVar<bool> _isFullyInWater = new SyncVar<bool>(false);
    private float waterSurfaceY = 0f;
    private Vector3 pushVelocity = Vector3.zero;
    private float pushSlowdownTimer = 0f;
    private float pushSlowdownAmount = 0f;
    private Coroutine waterEntryCoroutine;
    private Coroutine waterExitCoroutine;
    private Coroutine delayedRaceStartCoroutine;
    private float raceStartDelay = 0f;
    private float paceShiftTimer = 0f;
    private float paceShiftDurationTimer = 0f;
    private float currentPaceMultiplier = 1f;
    private float minPaceShiftInterval = 2f;
    private float maxPaceShiftInterval = 5f;
    private float minPaceShiftDuration = 0.7f;
    private float maxPaceShiftDuration = 1.8f;
    private float minPaceShiftMultiplier = 0.78f;
    private float maxPaceShiftMultiplier = 1.2f;

    // FIX: Track last position on clients for movement detection
    // (CharacterController.velocity is always zero on non-server clients)
    private Vector3 _lastClientPosition;

    // Expose transform as property for reflection-based systems
    public new Transform transform => base.transform;

    public override void OnStartNetwork()
    {
        base.OnStartNetwork();

        characterController = GetComponent<CharacterController>();

        currentStamina = maxStamina;
        currentSpeed = baseSpeed + Random.Range(-speedVariation, speedVariation);
        velocity = Vector3.zero;
        lastPosition = transform.position;

        // FIX: Seed client position tracker so first frame delta is zero
        _lastClientPosition = transform.position;

        // FIX: Subscribe to SyncVar change — fires on clients when server sets a new segment
        _currentSegment.OnChange += OnSegmentChanged;

        if (runnerModel != null && cyclistModel != null)
            SetSegment(AISegment.Swim);
    }

    // FIX: FishNet v4 SyncVar<T>.OnChange delegate signature:
    //   (T prev, T next, bool asServer)
    private void OnSegmentChanged(AISegment prev, AISegment next, bool asServer)
    {
        if (!asServer)
        {
            SetSegment(next);
        }
    }

    private void Update()
    {
        if (!IsServerInitialized)
        {
            UpdateClientAnimations();
            return;
        }

        if (!raceStarted)
            return;

        HandleStamina();
        HandleSprinting();
        HandlePaceShifting();
        FollowPath();

        if (pushSlowdownTimer > 0f)
            pushSlowdownTimer -= Time.deltaTime;

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

        Vector3 targetPosition = targetWaypoint.position;
        Vector3 flatToTarget = new Vector3(
            targetPosition.x - transform.position.x,
            0f,
            targetPosition.z - transform.position.z
        );

        if (flatToTarget.sqrMagnitude < 0.0001f)
        {
            currentWaypointIndex++;
            return;
        }

        Vector3 forwardDirection = flatToTarget.normalized;
        if (Mathf.Abs(pathLateralOffset) > 0.01f)
        {
            // Keep each AI in its own "lane" so they don't overlap exactly.
            Vector3 right = Vector3.Cross(Vector3.up, forwardDirection);
            targetPosition += right * pathLateralOffset;
            flatToTarget = new Vector3(
                targetPosition.x - transform.position.x,
                0f,
                targetPosition.z - transform.position.z
            );
        }

        Vector3 direction = flatToTarget.normalized;

        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        float moveSpeed;

        if (isFullyInWater && currentSegment == AISegment.Swim)
        {
            moveSpeed = swimSpeed;
            if (isSprinting && currentStamina > 0f)
                moveSpeed *= swimSpeedMultiplier;
        }
        else
        {
            moveSpeed = currentSpeed;
            if (isSprinting && currentStamina > 0f)
                moveSpeed *= 1.8f;
        }

        moveSpeed *= currentPaceMultiplier;

        if (pushSlowdownTimer > 0f)
            moveSpeed *= (1f - pushSlowdownAmount);

        Vector3 moveDirection = direction * moveSpeed * Time.deltaTime;

        if (characterController == null)
        {
            return;
        }

        characterController.Move(moveDirection);

        float distance = Vector3.Distance(
            new Vector3(transform.position.x, 0, transform.position.z),
            new Vector3(targetPosition.x, 0, targetPosition.z)
        );

        if (distance < waypointReachDistance)
            currentWaypointIndex++;
    }

    private void ApplyGravity()
    {
        if (isInWater && currentSegment == AISegment.Swim)
        {
            velocity.y = 0f;
            return;
        }

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

        float newY = Mathf.Lerp(transform.position.y, targetY, floatStrength * Time.deltaTime * 10f);
        float verticalMove = newY - transform.position.y;

        characterController.Move(new Vector3(0f, verticalMove, 0f));
        velocity.y = 0f;
    }

    // Server-side animation update (has full state available)
    private void UpdateAnimations()
    {
        ApplyAnimatorSpeeds();

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
                cyclistAnimator.SetBool("isIdle", !isMoving);
                cyclistAnimator.SetBool("isMove", isMoving && !isSprinting);
                cyclistAnimator.SetBool("isAccel", isMoving && isSprinting);
            }
        }
    }

    // FIX: Client-side animation update, fully rewritten:
    //   1. Uses position delta instead of CharacterController.velocity
    //      (CC.velocity is always zero on clients since Move() only runs on server)
    //   2. Includes the Swim branch which was previously missing entirely
    //   3. Reads currentSegment which is now a SyncVar so clients have the correct value
    private void UpdateClientAnimations()
    {
        ApplyAnimatorSpeeds();

        float distanceMoved = Vector3.Distance(
            new Vector3(transform.position.x, 0, transform.position.z),
            new Vector3(_lastClientPosition.x, 0, _lastClientPosition.z)
        );
        _lastClientPosition = transform.position;
        bool moving = distanceMoved > 0.001f;

        // ↓ Now matches server logic exactly, using synced booleans
        if ((isFullyInWater || isInWater) && currentSegment == AISegment.Swim)
        {
            if (runnerAnimator != null)
            {
                runnerAnimator.SetBool("isSwimming", moving);
                runnerAnimator.SetBool("isTreading", !moving);
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
                runnerAnimator.SetBool("isJogging", moving);
                runnerAnimator.SetBool("isRunning", false);
            }
        }
        else if (currentSegment == AISegment.Bike)
        {
            if (cyclistAnimator != null)
            {
                cyclistAnimator.SetBool("isIdle", !moving);
                cyclistAnimator.SetBool("isMove", moving);
                cyclistAnimator.SetBool("isAccel", false);
            }
        }
    }

    // FIX: Writes through the property so the SyncVar replicates to clients when called on server.
    public void SetSegment(AISegment segment)
    {
        currentSegment = segment; // routes through the property → _currentSegment.Value

        switch (segment)
        {
            case AISegment.Swim:
                if (runnerModel != null) runnerModel.SetActive(true);
                if (cyclistModel != null) cyclistModel.SetActive(false);
                break;
            case AISegment.Bike:
                if (runnerModel != null) runnerModel.SetActive(false);
                if (cyclistModel != null) cyclistModel.SetActive(true);
                break;
            case AISegment.Run:
                if (runnerModel != null) runnerModel.SetActive(true);
                if (cyclistModel != null) cyclistModel.SetActive(false);
                break;
        }
    }

    /// <summary>
    /// Transition to a new segment, teleport to position, and swap to a new waypoint path.
    /// Always pass the new segment's waypoints so the AI doesn't stop at end-of-path.
    /// </summary>
    [Server]
    public void TransitionToSegment(AISegment newSegment, Vector3 transitionPosition, Transform[] newPath)
    {
        SetWaypointPath(newPath);
        StartCoroutine(TransitionRoutine(newSegment, transitionPosition));
    }

    /// <summary>
    /// Legacy overload with no new path — use only if you're managing waypoints separately.
    /// </summary>
    [Server]
    public void TransitionToSegment(AISegment newSegment, Vector3 transitionPosition)
    {
        currentWaypointIndex = 0;
        StartCoroutine(TransitionRoutine(newSegment, transitionPosition));
    }

    private IEnumerator TransitionRoutine(AISegment newSegment, Vector3 position)
    {
        float originalSpeed = currentSpeed;
        currentSpeed = 0f;
        yield return new WaitForSeconds(0.5f);

        try
        {
            characterController.enabled = false;
            transform.position = position;
        }
        finally
        {
            characterController.enabled = true;
        }

        SetSegment(newSegment);
        yield return new WaitForSeconds(0.5f);
        currentSpeed = originalSpeed;
    }

    public void SetWaypointPath(Transform[] newPath)
    {
        waypointPath = newPath;
        currentWaypointIndex = 0;
    }

    private void ApplyAnimatorSpeeds()
    {
        float cadenceWave =
            1f + Mathf.Sin((Time.time + _animationCadencePhase.Value) * _animationCadenceFrequency.Value)
            * _animationCadenceAmplitude.Value;
        float clampedCadence = Mathf.Clamp(cadenceWave, 0.7f, 1.3f);
        float networkPace = Mathf.Clamp(_networkPaceMultiplier.Value, 0.65f, 1.35f);

        if (runnerAnimator != null)
        {
            float baseSpeed = currentSegment == AISegment.Swim
                ? _swimAnimationSpeed.Value
                : _runAnimationSpeed.Value;
            runnerAnimator.speed = Mathf.Clamp(baseSpeed * clampedCadence * networkPace, 0.5f, 1.6f);
        }

        if (cyclistAnimator != null)
        {
            float bikeCadence = 1f
                + Mathf.Cos((Time.time + _animationCadencePhase.Value) * _animationCadenceFrequency.Value * 0.85f)
                * _animationCadenceAmplitude.Value;
            cyclistAnimator.speed = Mathf.Clamp(_bikeAnimationSpeed.Value * bikeCadence * networkPace, 0.5f, 1.6f);
        }
    }

    [Server]
    public void ConfigureMovementProfile(
        float lateralOffset,
        float turnSpeed,
        float reachDistance,
        float swimAnimSpeed,
        float bikeAnimSpeed,
        float runAnimSpeed,
        float cadenceFrequency,
        float cadenceAmplitude,
        float cadencePhase,
        float paceShiftMinInterval,
        float paceShiftMaxInterval,
        float paceShiftMinDuration,
        float paceShiftMaxDuration,
        float paceShiftMinMultiplier,
        float paceShiftMaxMultiplier,
        float startDelay)
    {
        pathLateralOffset = lateralOffset;
        rotationSpeed = turnSpeed;
        waypointReachDistance = reachDistance;

        _swimAnimationSpeed.Value = swimAnimSpeed;
        _bikeAnimationSpeed.Value = bikeAnimSpeed;
        _runAnimationSpeed.Value = runAnimSpeed;
        _animationCadenceFrequency.Value = Mathf.Max(0.05f, cadenceFrequency);
        _animationCadenceAmplitude.Value = Mathf.Clamp(cadenceAmplitude, 0f, 0.25f);
        _animationCadencePhase.Value = cadencePhase;
        _networkPaceMultiplier.Value = 1f;

        minPaceShiftInterval = Mathf.Max(0.1f, Mathf.Min(paceShiftMinInterval, paceShiftMaxInterval));
        maxPaceShiftInterval = Mathf.Max(minPaceShiftInterval, Mathf.Max(paceShiftMinInterval, paceShiftMaxInterval));
        minPaceShiftDuration = Mathf.Max(0.1f, Mathf.Min(paceShiftMinDuration, paceShiftMaxDuration));
        maxPaceShiftDuration = Mathf.Max(minPaceShiftDuration, Mathf.Max(paceShiftMinDuration, paceShiftMaxDuration));
        minPaceShiftMultiplier = Mathf.Clamp(Mathf.Min(paceShiftMinMultiplier, paceShiftMaxMultiplier), 0.5f, 1.5f);
        maxPaceShiftMultiplier = Mathf.Clamp(Mathf.Max(paceShiftMinMultiplier, paceShiftMaxMultiplier), minPaceShiftMultiplier, 1.6f);
        paceShiftTimer = Random.Range(minPaceShiftInterval * 0.3f, maxPaceShiftInterval);
        paceShiftDurationTimer = 0f;
        currentPaceMultiplier = 1f;

        raceStartDelay = Mathf.Max(0f, startDelay);
    }

    private void HandlePaceShifting()
    {
        if (paceShiftDurationTimer > 0f)
        {
            paceShiftDurationTimer -= Time.deltaTime;
            if (paceShiftDurationTimer <= 0f)
            {
                currentPaceMultiplier = 1f;
                _networkPaceMultiplier.Value = currentPaceMultiplier;
                paceShiftTimer = Random.Range(minPaceShiftInterval, maxPaceShiftInterval);
            }
            return;
        }

        paceShiftTimer -= Time.deltaTime;
        if (paceShiftTimer <= 0f)
        {
            currentPaceMultiplier = Random.Range(minPaceShiftMultiplier, maxPaceShiftMultiplier);
            _networkPaceMultiplier.Value = currentPaceMultiplier;
            paceShiftDurationTimer = Random.Range(minPaceShiftDuration, maxPaceShiftDuration);
            paceShiftTimer = Random.Range(minPaceShiftInterval, maxPaceShiftInterval);
        }
    }

    [Server]
    public void StartRace()
    {
        if (delayedRaceStartCoroutine != null)
            StopCoroutine(delayedRaceStartCoroutine);

        if (raceStartDelay <= 0.01f)
        {
            raceStarted = true;
            return;
        }

        delayedRaceStartCoroutine = StartCoroutine(StartRaceDelayedRoutine());
    }

    [Server]
    public void StopRace()
    {
        if (delayedRaceStartCoroutine != null)
        {
            StopCoroutine(delayedRaceStartCoroutine);
            delayedRaceStartCoroutine = null;
        }

        raceStarted = false;
    }

    private IEnumerator StartRaceDelayedRoutine()
    {
        raceStarted = false;
        yield return new WaitForSeconds(raceStartDelay);
        raceStarted = true;
        delayedRaceStartCoroutine = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!IsServerInitialized) return;

        if (other.CompareTag("Water"))
        {
            if (waterExitCoroutine != null)
            {
                StopCoroutine(waterExitCoroutine);
                waterExitCoroutine = null;
            }
            if (waterEntryCoroutine != null)
                StopCoroutine(waterEntryCoroutine);

            isInWater = true;
            waterEntryCoroutine = StartCoroutine(EnterWaterDelayed(other.bounds.max.y));
        }
    }

    private IEnumerator EnterWaterDelayed(float surfaceY)
    {
        yield return new WaitForSeconds(waterEntryDelay);
        isFullyInWater = true;
        waterSurfaceY = surfaceY;

        velocity = Vector3.zero;

        float targetY = (waterSurfaceY + waterLevelOffset - bodyDepthOffset) - playerHeightOffset;
        if (transform.position.y < targetY)
        {
            characterController.enabled = false;
            transform.position = new Vector3(transform.position.x, targetY, transform.position.z);
            characterController.enabled = true;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (!IsServerInitialized) return;

        if (other.CompareTag("Water"))
        {
            if (waterEntryCoroutine != null)
            {
                StopCoroutine(waterEntryCoroutine);
                waterEntryCoroutine = null;
            }
            if (waterExitCoroutine != null)
                StopCoroutine(waterExitCoroutine);
            waterExitCoroutine = StartCoroutine(ExitWaterDelayed());
        }
    }

    private IEnumerator ExitWaterDelayed()
    {
        yield return new WaitForSeconds(0.5f);
        isInWater = false;
        isFullyInWater = false;

        velocity = Vector3.zero;
    }

    private void OnTriggerStay(Collider other)
    {
        if (!IsServerInitialized) return;

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

    private bool isInWater
    {
        get => _isInWater.Value;
        set => _isInWater.Value = value;
    }

    private bool isFullyInWater
    {
        get => _isFullyInWater.Value;
        set => _isFullyInWater.Value = value;
    }

    [Server]
    public void ApplyCollisionPush(Vector3 direction, float force, float slowdown, float duration)
    {
        pushVelocity = direction * force;
        pushSlowdownAmount = slowdown;
        pushSlowdownTimer = duration;
    }
}
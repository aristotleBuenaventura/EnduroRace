using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using Unity.Cinemachine;

public class PlayerControllerTutorial : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float runSpeedMultiplier = 2f;
    [SerializeField] private float lookSensitivity = 10f;
    [SerializeField] private float jumpHeight = 0.5f;
    [SerializeField] private float gravity = -9.81f;

    [Header("Water Settings")]
    [SerializeField] private float swimSpeed = 2f;
    [SerializeField] private float swimSpeedMultiplier = 2f;
    [SerializeField] private float floatStrength = 2f;
    [SerializeField] private float waterLevelOffset = 0.3f;
    [SerializeField] private float bodyDepthOffset = 0.3f;
    [SerializeField] private float playerHeightOffset = 1.0f;
    [SerializeField] private float treadOffset = 0.2f;

    [Header("Log Settings")]
    [SerializeField] private float tripDuration = 1f;
    [SerializeField] private float stumbleSlowMultiplier = 0.3f;
    [SerializeField] private float tripPushBackDistance = 0.2f;

    [Header("Mud Settings")]
    [SerializeField] private float mudSlowMultiplier = 0.3f;
    [SerializeField] private MudSplatterEffect mudSplatterEffect;
    private bool isInMud;

    [Header("Water Obstacle (Buoy) Settings")]
    [SerializeField] private float buoyStunDuration = 1.5f;
    [SerializeField] private float buoyKnockbackForce = 2.5f;
    [SerializeField] private GameObject stunStars;
    private bool isStunned;

    [Header("Whirlpool Settings")]
    public bool isInWhirlpool;
    public Transform whirlpoolCenter;
    [SerializeField] private float whirlpoolPullStrength = 10f;
    [SerializeField] private float whirlpoolSlowMultiplier = 0.4f;
    [SerializeField] private float whirlpoolMaxPullDistance = 7f;
    private bool isBeingSucked = false;

    [Header("Stamina Settings")]
    [SerializeField] private float maxStamina = 100f;
    [SerializeField] private float staminaDecreaseRate = 20f;
    [SerializeField] private float staminaRegenRate = 15f;
    [SerializeField] private StaminaUITutorial staminaUI;
    [SerializeField] private float obstacleStaminaPenalty = 10f;
    private float currentStamina;

    [Header("Speed Boost Settings")]
    private bool isSpeedBoosted;

    [Header("Camera Shake Settings")]
    [SerializeField] private CinemachineImpulseSource impulseSource;
    [SerializeField] private float buoyShakeIntensity = 1.5f;
    [SerializeField] private float logShakeIntensity = 1f;

    [Header("Footstep Sound Settings")]
    public AudioClip runningFootstepClip;
    public AudioClip joggingFootstepClip;
    public AudioClip swimSoundClip;
    private AudioSource footstepSource;
    public float runningFootstepInterval = 0.5f;
    public float joggingFootstepInterval = 0.7f;
    public float swimFootstepInterval = 0.6f;
    private float footstepTimer = 0f;

    [Header("Obstacle Sound Settings")]
    public AudioClip logTripClip;
    public AudioClip buoyHitClip;
    public AudioClip whirlpoolLoopClip;
    private AudioSource obstacleAudioSource;
    
    private CharacterController characterController;
    private Animator animator;
    private Vector2 moveVector;
    private Vector3 velocity;
    private Vector3 rotation;
    private bool isRunning;
    private bool isInWater;
    private bool isTripping;
    public bool inputLocked = false;
    private float waterSurfaceY;
    private bool canRotate = true;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        footstepSource = GetComponent<AudioSource>();
        currentStamina = maxStamina;

        // Set up a second AudioSource for obstacle sounds
        // so they don't interrupt footsteps
        obstacleAudioSource = gameObject.AddComponent<AudioSource>();
        obstacleAudioSource.playOnAwake = false;
        obstacleAudioSource.loop = false;

        if (impulseSource == null)
            impulseSource = GetComponent<CinemachineImpulseSource>();

        if (staminaUI != null)
            staminaUI.SetStamina(currentStamina, maxStamina);

        rotation = transform.localEulerAngles;
    }

    public void InitializeStartingRotation(float yRotation)
    {
        rotation.y = yRotation;
        transform.localEulerAngles = rotation;
        canRotate = false;
    }

    public void EnableRotation()
    {
        canRotate = true;
    }

    private void HandleStamina()
    {
        if (isRunning && currentStamina > 0f)
        {
            currentStamina -= staminaDecreaseRate * Time.deltaTime;
            if (currentStamina < 0f) currentStamina = 0f;
        }
        else
        {
            currentStamina += staminaRegenRate * Time.deltaTime;
            if (currentStamina > maxStamina) currentStamina = maxStamina;
        }

        if (currentStamina <= 0f) isRunning = false;

        if (staminaUI != null)
            staminaUI.SetStamina(currentStamina, maxStamina);
    }

    void Update()
    {
        if (inputLocked || isBeingSucked)
        {
            moveVector = Vector2.zero;
            velocity = Vector3.zero;

            animator.SetBool("isJogging", false);
            animator.SetBool("isRunning", false);
            animator.SetBool("isTreading", false);
            animator.SetBool("isSwimming", false);

            return;
        }

        HandleStamina();

        if (isStunned)
            return;

        if (isInWater)
            SwimMovement();
        else
            GroundMovement();

        Rotate();
    }

    // --- INPUT HANDLERS ---
    public void OnMove(InputAction.CallbackContext context) => moveVector = context.ReadValue<Vector2>();
    
    public void OnJump(InputAction.CallbackContext context)
    {
        if (!isInWater && characterController.isGrounded && context.performed)
        {
            animator.Play("Jump");
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.performed && currentStamina > 0f) isRunning = true;
        else isRunning = false;
    }

    // --- MOVEMENT ---
    private void GroundMovement()
    {
        if (characterController.isGrounded && velocity.y < 0) velocity.y = -2f;

        float currentSpeed = moveSpeed;
        if (isRunning && currentStamina > 0f) currentSpeed *= runSpeedMultiplier;
        if (isInWhirlpool) currentSpeed *= whirlpoolSlowMultiplier;
        if (isTripping) currentSpeed *= stumbleSlowMultiplier;
        else if (isInMud) currentSpeed *= mudSlowMultiplier;

        currentSpeed = Mathf.Max(currentSpeed, 0.5f);

        Vector3 move = (transform.right * moveVector.x + transform.forward * moveVector.y) * currentSpeed;
        velocity.y += gravity * Time.deltaTime;
        characterController.Move((move + velocity) * Time.deltaTime);

        bool isMoving = moveVector.magnitude > 0.1f;

        if (isRunning && isMoving) PlayFootstep(runningFootstepClip, runningFootstepInterval);
        else if (!isRunning && isMoving) PlayFootstep(joggingFootstepClip, joggingFootstepInterval);

        animator.SetBool("isJogging", isMoving && !isRunning);
        animator.SetBool("isRunning", isRunning && isMoving && !isTripping);

        if (!isInWater)
        {
            animator.SetBool("isTreading", false);
            animator.SetBool("isSwimming", false);
        }
    }
    
    private void SwimMovement()
    {
        Vector3 move = transform.right * moveVector.x + transform.forward * moveVector.y;
        bool isMoving = moveVector.magnitude > 0.1f;

        float targetY = isMoving
            ? (waterSurfaceY + waterLevelOffset - bodyDepthOffset) - playerHeightOffset
            : (waterSurfaceY + waterLevelOffset - bodyDepthOffset) - playerHeightOffset - treadOffset;

        velocity.y = Mathf.Lerp(velocity.y, (targetY - transform.position.y) * floatStrength, Time.deltaTime * 5f);

        float currentSwimSpeed = swimSpeed;

        if (isRunning && currentStamina > 0f)
            currentSwimSpeed *= swimSpeedMultiplier;

        if (isInWhirlpool)
        {
            currentSwimSpeed *= whirlpoolSlowMultiplier;
            if (whirlpoolCenter != null)
                ApplyWhirlpoolPull();
        }

        Vector3 movement =
            (move * (isMoving ? currentSwimSpeed : 0f) + Vector3.up * velocity.y)
            * Time.deltaTime;
        characterController.Move(movement);

        animator.SetBool("isSwimming", isMoving);
        animator.SetBool("isTreading", !isMoving);
        animator.SetBool("isRunning", false);
        animator.SetBool("isJogging", false);

        if (isMoving) PlayFootstep(swimSoundClip, swimFootstepInterval);
    }

    private void Rotate()
    {
        if (!canRotate || inputLocked) return;

        float turnInput = moveVector.x;

        rotation.y += turnInput * lookSensitivity * Time.deltaTime;
        transform.localEulerAngles = rotation;
    }

    private void PlayFootstep(AudioClip clip, float interval)
    {
        footstepTimer += Time.deltaTime;
        if (footstepTimer >= interval)
        {
            footstepSource.PlayOneShot(clip);
            footstepTimer = 0f;
        }
    }

    private void PlayObstacleSound(AudioClip clip)
    {
        if (obstacleAudioSource == null || clip == null) return;
        obstacleAudioSource.PlayOneShot(clip);
    }

    // --- CAMERA SHAKE ---
    private void TriggerCameraShake(float intensity)
    {
        if (impulseSource != null)
        {
            impulseSource.GenerateImpulse(intensity);
        }
        else
        {
        }
    }

    // --- TRIGGERS ---
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            waterSurfaceY = other.bounds.max.y;
            isInWater = true;
            velocity.y = 0f;

            animator.SetBool("isSwimming", moveVector.magnitude > 0.1f);
            animator.SetBool("isTreading", moveVector.magnitude <= 0.1f);
        }
        else if (other.CompareTag("Buoy"))
        {
            Vector3 hitDirection = transform.position - other.transform.position;
            hitDirection.y = 0f;

            HitByBuoy(hitDirection);
        }
        else if (!isTripping && other.CompareTag("Log")) 
        {
            StartCoroutine(TripPlayer());
        }
        else if (other.CompareTag("Mud")) 
        {
            isInMud = true;
            
            if (!isInWater && mudSplatterEffect != null)
                mudSplatterEffect.StartEffect();
        }
        else if (other.CompareTag("Whirlpool"))
        {
            isInWhirlpool = true;
            whirlpoolCenter = other.transform;
            if (!isBeingSucked)
                StartCoroutine(SuckIntoWhirlpool());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            isInWater = false;
            velocity.y = 0f;
            animator.SetBool("isSwimming", false);
            animator.SetBool("isTreading", false);
        }
        else if (other.CompareTag("Mud")) 
        {
            isInMud = false;
            
            if (mudSplatterEffect != null)
                mudSplatterEffect.StopEffect();
        }
        else if (other.CompareTag("Whirlpool"))
        {
            isInWhirlpool = false;

            // Stop whirlpool loop sound when player exits
            if (obstacleAudioSource != null && obstacleAudioSource.clip == whirlpoolLoopClip)
                obstacleAudioSource.Stop();
        }
    }

    private IEnumerator TripPlayer()
    {
        isTripping = true;
        animator.SetTrigger("Trip");

        ReduceStamina(obstacleStaminaPenalty);
        TriggerCameraShake(logShakeIntensity);

        // Play log trip sound
        PlayObstacleSound(logTripClip);

        Vector3 pushBack = -transform.forward * tripPushBackDistance;
        characterController.Move(pushBack);

        yield return new WaitForSeconds(tripDuration);
        isTripping = false;
    }

    private IEnumerator BuoyStunRoutine(Vector3 hitDirection)
    {
        isStunned = true;
        canRotate = false;

        animator.SetTrigger("Stunned");
        stunStars.SetActive(true);

        Vector3 knockback = hitDirection.normalized * buoyKnockbackForce;
        characterController.Move(knockback);

        yield return new WaitForSeconds(buoyStunDuration);

        isStunned = false;
        stunStars.SetActive(false);
        canRotate = true;
    }

    public void HitByBuoy(Vector3 hitDirection)
    {
        if (!isInWater || isStunned) return;

        ReduceStamina(obstacleStaminaPenalty);
        TriggerCameraShake(buoyShakeIntensity);

        // Play buoy hit sound
        PlayObstacleSound(buoyHitClip);

        StartCoroutine(BuoyStunRoutine(hitDirection));
    }

    private void ReduceStamina(float amount)
    {   
        currentStamina -= amount;
        if (currentStamina < 0f)
            currentStamina = 0f;

        if (staminaUI != null)
            staminaUI.SetStamina(currentStamina, maxStamina);
    }

    // --- TUTORIAL CONTROLS ---
    public void EnableSprint(bool enabled)
    {
        isRunning = false;
        runSpeedMultiplier = enabled ? runSpeedMultiplier : 1f;
    }

    public bool HasUsedSprint()
    {
        return isRunning;
    }

    public bool HasMoved()
    {
        return moveVector.magnitude > 0.1f;
    }

    private void ApplyWhirlpoolPull()
    {
        if (!isInWhirlpool || whirlpoolCenter == null || !isInWater) return;

        Vector3 direction = whirlpoolCenter.position - transform.position;
        direction.y = 0f;

        float distance = direction.magnitude;
        if (distance < 0.1f) return;

        float pullFactor = 1f - Mathf.Clamp01(distance / whirlpoolMaxPullDistance);
        pullFactor = pullFactor * pullFactor;

        float pullStrengthFinal = Mathf.Max(whirlpoolPullStrength * pullFactor, 1f);

        Vector3 pull = direction.normalized * pullStrengthFinal * Time.deltaTime;

        Vector3 tangent = Vector3.Cross(Vector3.up, direction.normalized);
        pull += tangent * pullStrengthFinal * 0.5f * Time.deltaTime;

        characterController.Move(pull);
    }

    private IEnumerator SuckIntoWhirlpool()
    {
        isBeingSucked = true;

        animator.SetTrigger("SuckedIntoWhirlpool");

        // Play whirlpool loop sound when player gets sucked in
        if (obstacleAudioSource != null && whirlpoolLoopClip != null)
        {
            obstacleAudioSource.clip = whirlpoolLoopClip;
            obstacleAudioSource.loop = true;
            obstacleAudioSource.Play();
        }

        while (whirlpoolCenter != null &&
                Vector3.Distance(
                    new Vector3(transform.position.x, 0, transform.position.z),
                    new Vector3(whirlpoolCenter.position.x, 0, whirlpoolCenter.position.z)
                ) > 0.1f)
        {
            Vector3 horizontalDir = whirlpoolCenter.position - transform.position;
            horizontalDir.y = 0f;
            horizontalDir.Normalize();

            Vector3 tangent = Vector3.Cross(Vector3.up, horizontalDir);

            Vector3 horizontalPull = (horizontalDir * whirlpoolPullStrength + tangent * whirlpoolPullStrength * 0.5f) * Time.deltaTime;

            float verticalPull = Mathf.Min(whirlpoolCenter.position.y - transform.position.y, 0f);

            Vector3 pull = horizontalPull + Vector3.up * verticalPull;

            characterController.Move(pull);

            yield return null;
        }

        // Stop whirlpool loop sound when suction ends
        if (obstacleAudioSource != null && obstacleAudioSource.clip == whirlpoolLoopClip)
        {
            obstacleAudioSource.loop = false;
            obstacleAudioSource.Stop();
        }

        isBeingSucked = false;
        isInWhirlpool = false;
        whirlpoolCenter = null;
    }
    
    public void RestoreStamina(float amount)
    {
        currentStamina += amount;
        if (currentStamina > maxStamina)
            currentStamina = maxStamina;

        if (staminaUI != null)
            staminaUI.SetStamina(currentStamina, maxStamina);
    }

    public void ActivateSpeedBoost(float multiplier, float duration)
    {
        if (isSpeedBoosted) return;
        StartCoroutine(SpeedBoostRoutine(multiplier, duration));
    }

    private IEnumerator SpeedBoostRoutine(float multiplier, float duration)
    {
        isSpeedBoosted = true;
        moveSpeed *= multiplier;
        swimSpeed *= multiplier;

        yield return new WaitForSeconds(duration);

        moveSpeed /= multiplier;
        swimSpeed /= multiplier;
        isSpeedBoosted = false;
    }

    public void ForceSetRotation(Quaternion newRotation)
    {
        transform.rotation = newRotation;
        rotation = transform.localEulerAngles;
    }
}
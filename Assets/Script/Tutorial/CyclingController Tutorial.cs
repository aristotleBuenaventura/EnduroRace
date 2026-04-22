using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using Unity.Cinemachine;

[RequireComponent(typeof(CharacterController))]
public class CyclingControllerTutorial : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float accelMultiplier = 2f;
    [SerializeField] private float turnSpeed = 50f;
    [SerializeField] private float gravity = -9.81f;

    [Header("Animation")]
    [SerializeField] private Animator animator;

    [Header("Stamina Settings")]
    [SerializeField] private float maxStamina = 100f;
    [SerializeField] private float staminaDecreaseRate = 20f;
    [SerializeField] private float staminaRegenRate = 10f;
    [SerializeField] private StaminaUITutorial staminaUI; // Battery-style UI

    [Header("Cycling Sound Settings")]
    public AudioClip cyclingSoundClip;
    public float cyclingInterval = 0.5f; // seconds
    private AudioSource cyclingSource;
    private float cycleTimer = 0f;

    [Header("Visuals")]
    [SerializeField] private Transform bikeModel; // assign bike model child
    [SerializeField] private float maxLeanAngle = 10f;
    [SerializeField] private float leanSmooth = 5f;

    [Header("Speed Bump Settings")]
    [SerializeField] private float speedBumpDuration = 1f;
    [SerializeField] private float speedBumpSlowMultiplier = 0.4f;
    [SerializeField] private float speedBumpStaminaPenalty = 10f;

    [Header("Pothole Settings")]
    [SerializeField] private float stumbleDuration = 1f;
    [SerializeField] private float dipDepth = 0.2f;
    [SerializeField] private float potholeStaminaPenalty = 5f;
    [SerializeField] private float potholeShakeIntensity = 1.5f;

    [Header("Camera Shake Settings")]
    [SerializeField] private CinemachineImpulseSource impulseSource;
    [SerializeField] private float speedBumpShakeIntensity = 1.2f;

    [Header("Speed Boost Settings")]
    private bool isSpeedBoosted;
    private bool isOnSpeedBump;
    private bool isInPothole;
    private CharacterController characterController;
    private Vector2 moveInput;
    private bool isAccelerating;
    private float currentStamina;
    private Vector3 velocity;
    [HideInInspector] public bool inputLocked = false;
    
    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        cyclingSource = GetComponent<AudioSource>();
        currentStamina = maxStamina;

        // Get the impulse source if not assigned
        if (impulseSource == null)
            impulseSource = GetComponent<CinemachineImpulseSource>();

        if (cyclingSource != null)
            cyclingSource.playOnAwake = false;

        // Initialize stamina UI
        if (staminaUI != null)
            staminaUI.SetStamina(currentStamina, maxStamina);
    }

    private void Update()
    {
        if (inputLocked)
        {
            // Stop residual movement
            velocity = Vector3.zero;
            moveInput = Vector2.zero;
            isAccelerating = false;

            HandleAnimations();
            return;
        }

        HandleStamina();
        HandleMovement();
        HandleAnimations();

        if (staminaUI != null)
            staminaUI.SetStamina(currentStamina, maxStamina);
    }

    // --- Input Callbacks ---
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnAccel(InputAction.CallbackContext context)
    {
        isAccelerating = context.performed;
    }

    // --- Stamina ---
    private void HandleStamina()
    {
        if (isAccelerating && moveInput.y > 0f && currentStamina > 0f)
        {
            currentStamina -= staminaDecreaseRate * Time.deltaTime;
            if (currentStamina < 0f)
                currentStamina = 0f;
        }
        else
        {
            currentStamina += staminaRegenRate * Time.deltaTime;
            if (currentStamina > maxStamina)
                currentStamina = maxStamina;
        }

        if (currentStamina <= 0f)
            isAccelerating = false;
    }

    // --- Movement + Steering + Lean + Sound ---
    private void HandleMovement()
    {
        // --- Gravity ---
        if (characterController.isGrounded && velocity.y < 0f)
            velocity.y = -2f;
        else
            velocity.y += gravity * Time.deltaTime;

        // --- Base Speed ---
        float speed = moveSpeed;

        // Acceleration (stamina-based)
        if (isAccelerating && currentStamina > 0f)
            speed *= accelMultiplier;

        // Speed bump slowdown
        if (isOnSpeedBump)
            speed *= speedBumpSlowMultiplier;

        // Prevent negative or zero speed
        speed = Mathf.Max(speed, 0.1f);

        // --- Forward Movement ---
        Vector3 move = transform.forward * -moveInput.y * speed;
        characterController.Move((move + velocity) * Time.deltaTime);

        // --- Steering / Yaw Rotation ---
        float turnAmount = moveInput.x * turnSpeed * Time.deltaTime;
        transform.Rotate(0f, turnAmount, 0f);

        // --- Visual Lean (Bike Model Only) ---
        if (bikeModel != null)
        {
            float targetLean = -moveInput.x * maxLeanAngle;
            Quaternion leanRotation = Quaternion.Euler(0f, 0f, targetLean);
            bikeModel.localRotation = Quaternion.Lerp(
                bikeModel.localRotation,
                leanRotation,
                Time.deltaTime * leanSmooth
            );
        }

        // --- Cycling Sound ---
        bool isMoving = characterController.velocity.magnitude > 0.1f;

        if (isMoving)
        {
            cycleTimer += Time.deltaTime;
            if (cycleTimer >= cyclingInterval && cyclingSoundClip != null && cyclingSource != null)
            {
                cyclingSource.PlayOneShot(cyclingSoundClip);
                cycleTimer = 0f;
            }
        }
        else
        {
            cycleTimer = 0f;
        }
    }

    // --- Animations ---
    private void HandleAnimations()
    {
        if (moveInput.y == 0f)
        {
            animator.SetBool("isIdle", true);
            animator.SetBool("isMove", false);
            animator.SetBool("isAccel", false);
        }
        else if (!isAccelerating)
        {
            animator.SetBool("isIdle", false);
            animator.SetBool("isMove", true);
            animator.SetBool("isAccel", false);
        }
        else
        {
            animator.SetBool("isIdle", false);
            animator.SetBool("isMove", false);
            animator.SetBool("isAccel", true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SpeedBump") && !isOnSpeedBump)
        {
            StartCoroutine(SpeedBumpRoutine());
        }
        else if (other.CompareTag("Pothole") && !isInPothole) // ADD THIS
        {
            StartCoroutine(PotholeStumble(stumbleDuration, dipDepth));
        }
    }

    private IEnumerator SpeedBumpRoutine()
    {
        isOnSpeedBump = true;

        // Play bump animation
        animator.SetTrigger("Bump");

        // Camera shake on speed bump impact
        TriggerCameraShake(speedBumpShakeIntensity);

        // Reduce stamina
        currentStamina -= speedBumpStaminaPenalty;
        if (currentStamina < 0f)
            currentStamina = 0f;

        yield return new WaitForSeconds(speedBumpDuration);

        isOnSpeedBump = false;
    }

    public IEnumerator PotholeStumble(float duration, float dipDepth)
    {
        isInPothole = true;

        // Camera shake on pothole impact
        TriggerCameraShake(potholeShakeIntensity);

        // Dip the bike model down to simulate dropping into the hole
        Vector3 originalPos = bikeModel != null ? bikeModel.localPosition : Vector3.zero;
        if (bikeModel != null)
            bikeModel.localPosition = originalPos + Vector3.down * dipDepth;

        // Reduce stamina
        currentStamina -= potholeStaminaPenalty;
        if (currentStamina < 0f)
            currentStamina = 0f;

        // Hold the dip briefly, then recover
        yield return new WaitForSeconds(duration * 0.3f);

        if (bikeModel != null)
            bikeModel.localPosition = originalPos;

        yield return new WaitForSeconds(duration * 0.7f);

        isInPothole = false;
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
            Debug.LogWarning("CinemachineImpulseSource is not assigned! Add it to the Bike/Cyclist GameObject.");
        }
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

        yield return new WaitForSeconds(duration);

        moveSpeed /= multiplier;
        isSpeedBoosted = false;
    }
}
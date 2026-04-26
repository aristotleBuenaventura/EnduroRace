using UnityEngine;
using UnityEngine.InputSystem;
using FishNet.Object;
using FishNet.Object.Synchronizing;
using System.Collections;
using Unity.Cinemachine;

[RequireComponent(typeof(CharacterController))]
public class CyclingController : NetworkBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float accelMultiplier = 2f;
    [SerializeField] private float turnSpeed = 50f;
    [SerializeField] private float gravity = -9.81f;

    [Header("Animation / Visuals")]
    [SerializeField] public Animator animator;
    [SerializeField] private Transform bikeModel;
    [SerializeField] private float maxLeanAngle = 10f;
    [SerializeField] private float leanSmooth = 5f;

    [Header("Stamina Settings")]
    public float maxStamina = 100f;
    [SerializeField] private float staminaDecreaseRate = 20f;
    [SerializeField] private float staminaRegenRate = 10f;
    [SerializeField] public StaminaUI staminaUI;

    private float tierStaminaDecreaseRate;
    private float tierStaminaRegenRate;
    private bool  tierRegenEnabled = true;

    private bool isExhausted = false;
    private const float exhaustionSpeedMultiplier = 0.85f;

    [Header("Exhaustion Audio")]
    public AudioClip heavyBreathingClip;
    private AudioSource exhaustionAudioSource;
    private bool isPlayingHeavyBreathing = false;

    [Header("Audio Settings")]
    public AudioClip cyclingSoundClip;
    public float cyclingInterval = 0.5f;

    [Header("Speed Bump Settings")]
    [SerializeField] private float speedBumpDuration = 1f;
    [SerializeField] private float speedBumpSlowMultiplier = 0.4f;
    [SerializeField] private float speedBumpStaminaPenalty = 10f;

    [Header("Pothole / Bounce Settings")]
    [SerializeField] private float potholeSlowMultiplier = 0.5f;

    [Header("Camera Shake Settings")]
    [SerializeField] private CinemachineImpulseSource impulseSource;
    [SerializeField] private float speedBumpShakeIntensity = 1.2f;
    [SerializeField] private float potholeShakeIntensity = 1f;
    [SerializeField] private float stunShakeIntensity = 1.5f;

    [Header("Stun Effects")]
    [SerializeField] private GameObject stunStarPrefab;
    [SerializeField] private Transform stunStarSpawnPoint;
    private GameObject activeStunStar;

    private CharacterController characterController;
    private AudioSource cyclingSource;

    public Vector2 moveInput;
    public bool isAccelerating;
    private Vector3 velocity;
    public float currentStamina;
    private float cycleTimer;
    private bool isOnSpeedBump;
    private bool isOnPothole;

    [Header("References")]
    public bool isActiveModel = false;
    private Transform rootTransform;
    private float targetYRotation;
    private NetworkPlayer netPlayer;

    public override void OnStartClient()
    {
        base.OnStartClient();

        if (animator == null)
            animator = GetComponent<Animator>();

        if (!IsOwner) return;

        characterController = GetComponent<CharacterController>();
        cyclingSource       = GetComponent<AudioSource>();
        currentStamina      = maxStamina;

        if (impulseSource == null)
            impulseSource = GetComponent<CinemachineImpulseSource>();

        if (staminaUI == null)
            staminaUI = SceneReference.GetStaminaUI();

        staminaUI?.SetStamina(currentStamina, maxStamina);

        if (cyclingSource != null)
            cyclingSource.playOnAwake = false;

        exhaustionAudioSource           = gameObject.AddComponent<AudioSource>();
        exhaustionAudioSource.playOnAwake = false;
        exhaustionAudioSource.loop       = true;

        rootTransform   = GetComponentInParent<NetworkPlayer>()?.transform ?? transform;
        netPlayer       = GetComponentInParent<NetworkPlayer>();
        targetYRotation = rootTransform.eulerAngles.y;

        ApplyTierStaminaSettings();
    }

    private void ApplyTierStaminaSettings()
    {
        string tier = LobbyDataTransfer.Instance?.GetLocalPlayerData()?.tier ?? "Beginner";

        switch (tier)
        {
            case "Beginner":
                tierStaminaDecreaseRate = staminaDecreaseRate * 0.5f;
                tierStaminaRegenRate    = staminaRegenRate;
                tierRegenEnabled        = true;
                break;
            case "Intermediate":
                tierStaminaDecreaseRate = staminaDecreaseRate;
                tierStaminaRegenRate    = staminaRegenRate * 0.6f;
                tierRegenEnabled        = true;
                break;
            case "Pro":
                tierStaminaDecreaseRate = staminaDecreaseRate * 1.8f;
                tierStaminaRegenRate    = 0f;
                tierRegenEnabled        = false;
                break;
            default:
                tierStaminaDecreaseRate = staminaDecreaseRate;
                tierStaminaRegenRate    = staminaRegenRate;
                tierRegenEnabled        = true;
                break;
        }

        // ✅ Equipment: reduce stamina drain
        float equipReduction = EquipmentManager.Instance?.GetStaminaDecreaseReduction() ?? 0f;
        tierStaminaDecreaseRate = Mathf.Max(0f, tierStaminaDecreaseRate - equipReduction);

        Debug.Log($"[CyclingController] Tier '{tier}' — DrainRate: {tierStaminaDecreaseRate} " +
                  $"(equip reduction: {equipReduction}), RegenRate: {tierStaminaRegenRate}, RegenEnabled: {tierRegenEnabled}");
    }

    private void Update()
    {
        if (!IsOwner || !isActiveModel || characterController == null)
            return;

        if (netPlayer != null && netPlayer.IsStunned.Value)
        {
            moveInput = Vector2.zero;
            isAccelerating = false;
            HandleAnimations();
            return;
        }

        HandleStamina();
        UpdateExhaustionState();
        HandleMovement();
        HandleAnimations();
        staminaUI?.SetStamina(currentStamina, maxStamina);
        SmoothRotate();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (!IsOwner || !isActiveModel) return;
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnAccel(InputAction.CallbackContext context)
    {
        if (!IsOwner || !isActiveModel) return;
        isAccelerating = context.performed;
    }

    private void HandleStamina()
    {
        if (isAccelerating && moveInput.y > 0f && currentStamina > 0f)
        {
            currentStamina -= tierStaminaDecreaseRate * Time.deltaTime;
            if (currentStamina < 0f) currentStamina = 0f;
        }
        else
        {
            if (tierRegenEnabled)
            {
                currentStamina += tierStaminaRegenRate * Time.deltaTime;
                if (currentStamina > maxStamina) currentStamina = maxStamina;
            }
        }

        if (currentStamina <= 0f)
            isAccelerating = false;
    }

    private void UpdateExhaustionState()
    {
        if (!isExhausted && currentStamina <= 0f)
        {
            isExhausted = true;
            Debug.Log("[CyclingController] Exhaustion state ENTERED");
            PlayHeavyBreathing(true);
        }
        else if (isExhausted && currentStamina > maxStamina * 0.1f)
        {
            isExhausted = false;
            Debug.Log("[CyclingController] Exhaustion state EXITED");
            PlayHeavyBreathing(false);
        }
    }

    private void PlayHeavyBreathing(bool play)
    {
        if (exhaustionAudioSource == null || heavyBreathingClip == null) return;

        if (play && !isPlayingHeavyBreathing)
        {
            exhaustionAudioSource.clip = heavyBreathingClip;
            exhaustionAudioSource.Play();
            isPlayingHeavyBreathing = true;
        }
        else if (!play && isPlayingHeavyBreathing)
        {
            exhaustionAudioSource.Stop();
            isPlayingHeavyBreathing = false;
        }
    }

    public bool IsExhausted => isExhausted;

    private void HandleMovement()
    {
        Vector3 slopeDirection = Vector3.zero;
        float slopeAngle = 0f;
        bool onSlope = false;

        RaycastHit slopeHit;
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, 1.5f))
        {
            slopeAngle = Vector3.Angle(slopeHit.normal, Vector3.up);
            if (slopeAngle > 1f)
            {
                onSlope        = true;
                slopeDirection = Vector3.ProjectOnPlane(Vector3.down, slopeHit.normal).normalized;
            }
        }

        if (characterController.isGrounded) { if (velocity.y < 0f) velocity.y = -2f; }
        else velocity.y += gravity * Time.deltaTime;

        // ✅ Equipment cycle speed + sprint multiplier bonuses
        float equipCycleBonus  = EquipmentManager.Instance?.GetCycleSpeedBonus() ?? 0f;
        float equipSprintBonus = EquipmentManager.Instance?.GetMoveSpeedMultiplierBonus() ?? 0f;

        float speed = moveSpeed + equipCycleBonus;
        if (isAccelerating && currentStamina > 0f) speed *= (accelMultiplier + equipSprintBonus);
        if (isOnSpeedBump)  speed *= speedBumpSlowMultiplier;
        if (isOnPothole)    speed *= potholeSlowMultiplier;
        if (isExhausted)    speed *= exhaustionSpeedMultiplier;

        if (onSlope && moveInput.y != 0f)
        {
            float dot = Vector3.Dot(rootTransform.forward * -moveInput.y, slopeDirection);
            if (dot > 0.1f)       speed *= 1f + (slopeAngle / 45f) * 0.5f;
            else if (dot < -0.1f) speed *= Mathf.Clamp(1f - (slopeAngle / 45f) * 0.7f, 0.15f, 1f);
        }

        Vector2 effectiveMoveInput = isExhausted
            ? Vector2.Lerp(Vector2.zero, moveInput, 0.6f)
            : moveInput;

        Vector3 horizontalMove = rootTransform.forward * (-effectiveMoveInput.y) * speed;

        Vector3 motion = (onSlope && characterController.isGrounded)
            ? horizontalMove + slopeDirection * slopeAngle * 0.1f + Vector3.up * velocity.y
            : horizontalMove + Vector3.up * velocity.y;

        characterController.Move(motion * Time.deltaTime);

        rootTransform.position  = transform.position;
        transform.localPosition = Vector3.zero;

        targetYRotation += effectiveMoveInput.x * turnSpeed * Time.deltaTime;

        if (bikeModel != null)
        {
            float targetLean = -effectiveMoveInput.x * maxLeanAngle;
            bikeModel.localRotation = Quaternion.Lerp(
                bikeModel.localRotation,
                Quaternion.Euler(0f, 0f, targetLean),
                Time.deltaTime * leanSmooth);
        }

        bool isMoving = characterController.velocity.magnitude > 0.1f;
        if (isMoving && cyclingSource && cyclingSoundClip)
        {
            cycleTimer += Time.deltaTime;
            if (cycleTimer >= cyclingInterval)
            {
                cyclingSource.PlayOneShot(cyclingSoundClip);
                cycleTimer = 0f;
            }
        }
        else cycleTimer = 0f;
    }

    private void HandleAnimations()
    {
        byte newState = 0;
        if (moveInput.y == 0f)      newState = 0;
        else if (!isAccelerating)   newState = 1;
        else                        newState = 2;
        UpdateAnimationState(newState);
    }

    private void UpdateAnimationState(byte newState)
    {
        NetworkPlayer netPlayer = GetComponentInParent<NetworkPlayer>();
        if (netPlayer == null) return;
        if (IsOwner)
        {
            ApplyAnimationState(newState);
            netPlayer.ServerSetCyclistAnimState(newState);
        }
    }

    public void ApplyAnimationStateFromNetwork(byte state) => ApplyAnimationState(state);

    private void ApplyAnimationState(byte state)
    {
        if (animator == null) { Debug.LogError("[CyclingController] Animator is NULL!"); return; }
        animator.SetBool("isIdle",  state == 0);
        animator.SetBool("isMove",  state == 1);
        animator.SetBool("isAccel", state == 2);
    }

    private void SmoothRotate()
    {
        if (rootTransform == null) return;

        float slopeTilt = 0f;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1.5f))
        {
            Vector3 forward = Quaternion.Euler(0f, targetYRotation, 0f) * Vector3.forward;
            slopeTilt = Vector3.SignedAngle(Vector3.up, hit.normal,
                        Vector3.Cross(forward, Vector3.up));
            slopeTilt = -slopeTilt;
        }

        Quaternion targetRotation = Quaternion.Euler(slopeTilt, targetYRotation, 0f);
        rootTransform.rotation = Quaternion.Lerp(
            rootTransform.rotation, targetRotation, Time.deltaTime * 5f);
    }

    private void TriggerCameraShake(float intensity)
    {
        if (impulseSource != null) impulseSource.GenerateImpulse(intensity);
        else Debug.LogWarning("[CyclingController] CinemachineImpulseSource not assigned!");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!IsOwner) return;
        if (other.CompareTag("SpeedBump") && !isOnSpeedBump)
            StartCoroutine(SpeedBumpRoutine());
    }

    private IEnumerator SpeedBumpRoutine()
    {
        isOnSpeedBump  = true;
        currentStamina = Mathf.Max(0f, currentStamina - speedBumpStaminaPenalty);
        yield return StartCoroutine(SpeedBumpEffect(0.2f, speedBumpDuration, speedBumpSlowMultiplier));
        isOnSpeedBump  = false;
    }

    public IEnumerator SpeedBumpEffect(float height, float duration, float speedMultiplier)
    {
        animator?.SetTrigger("Bump");
        TriggerCameraShake(speedBumpShakeIntensity);
        float originalSpeed = moveSpeed;
        moveSpeed *= speedMultiplier;
        yield return StartCoroutine(SpeedBumpBounce(height, duration));
        moveSpeed = originalSpeed;
    }

    private IEnumerator SpeedBumpBounce(float height, float duration)
    {
        if (bikeModel == null) yield break;
        Vector3 start = bikeModel.localPosition;
        Vector3 up    = start + Vector3.up * height;
        float half    = duration / 2f;
        for (float t = 0; t < half; t += Time.deltaTime) { bikeModel.localPosition = Vector3.Lerp(start, up, t / half); yield return null; }
        for (float t = 0; t < half; t += Time.deltaTime) { bikeModel.localPosition = Vector3.Lerp(up, start, t / half); yield return null; }
        bikeModel.localPosition = start;
    }

    public IEnumerator PotholeStumble(float duration, float dipDepth)
    {
        if (impulseSource != null) impulseSource.GenerateImpulse(Vector3.right * potholeShakeIntensity);
        isOnPothole = true;

        if (bikeModel == null) { yield return new WaitForSeconds(duration); isOnPothole = false; yield break; }

        Vector3 start = bikeModel.localPosition;
        Vector3 dip   = start - Vector3.up * dipDepth;
        float half    = duration / 2f;
        for (float t = 0; t < half; t += Time.deltaTime) { bikeModel.localPosition = Vector3.Lerp(start, dip, t / half); yield return null; }
        for (float t = 0; t < half; t += Time.deltaTime) { bikeModel.localPosition = Vector3.Lerp(dip, start, t / half); yield return null; }
        bikeModel.localPosition = start;
        isOnPothole = false;
    }

    public IEnumerator StunCyclist(float duration)
    {
        if (netPlayer == null || netPlayer.IsStunned.Value) yield break;

        netPlayer.ServerSetStunned(true);
        ShowStunStars(true);
        animator?.SetTrigger("Bump");
        TriggerCameraShake(stunShakeIntensity);

        yield return new WaitForSeconds(duration);

        ShowStunStars(false);
        netPlayer.ServerSetStunned(false);
    }

    private void ShowStunStars(bool show)
    {
        if (stunStarPrefab == null) return;

        if (show)
        {
            if (activeStunStar == null)
            {
                Vector3 spawnPos = stunStarSpawnPoint != null
                    ? stunStarSpawnPoint.position
                    : transform.position + Vector3.up * 2f;
                activeStunStar = Instantiate(stunStarPrefab, spawnPos, Quaternion.identity, transform);
            }
            else
            {
                activeStunStar.SetActive(true);
            }
        }
        else
        {
            if (activeStunStar != null)
                activeStunStar.SetActive(false);
        }
    }

    public void ApplySpeedBoost(float multiplier, float duration)
        => StartCoroutine(SpeedBoostRoutine(multiplier, duration));

    private IEnumerator SpeedBoostRoutine(float multiplier, float duration)
    {
        moveSpeed *= multiplier;
        yield return new WaitForSeconds(duration);
        moveSpeed /= multiplier;
    }

    public void RestoreStamina(float amount)
    {
        currentStamina = Mathf.Clamp(currentStamina + amount, 0f, maxStamina);
        staminaUI?.SetStamina(currentStamina, maxStamina);
        Debug.Log($"[CyclingController] Stamina restored by {amount}. Current: {currentStamina}");
    }
}
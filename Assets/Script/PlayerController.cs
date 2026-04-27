using UnityEngine;
using UnityEngine.InputSystem;
using FishNet.Object;
using FishNet.Object.Synchronizing;
using System.Collections;
using Unity.Cinemachine;

[RequireComponent(typeof(AudioSource))]
public class PlayerController : NetworkBehaviour
{
    [Header("Movement Settings")]
    public bool isActiveModel = true;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float runSpeedMultiplier = 2f;
    [SerializeField] private float gravity = -9.81f;

    [Header("Rotation Settings")]
    [SerializeField] private float rotationSpeed = 10f;

    [Header("Water Settings")]
    [SerializeField] private float swimSpeed = 2f;
    [SerializeField] private float swimSprintMultiplier = 1.5f;
    [SerializeField] private float floatStrength = 2f;
    [SerializeField] private float waterLevelOffset = 0.38f;
    [SerializeField] private float bodyDepthOffset = 0.3f;
    [SerializeField] private float playerHeightOffset = 0.8f;

    [Header("Obstacle Settings")]
    [SerializeField] private float tripDuration = 1f;
    [SerializeField] private float stumbleSlowMultiplier = 0.3f;
    public float tripStaminaCost = 10f;

    [Header("Mud Settings")]
    public float mudStaminaDrainRate = 5f;

    [Header("Whirlpool Settings")]
    [SerializeField] private float whirlpoolPullStrength = 10f;
    [SerializeField] private float whirlpoolSlowMultiplier = 0.4f;
    [SerializeField] private float whirlpoolMaxPullDistance = 7f;
    [SerializeField] private float whirlpoolStaminaDrainRate = 15f;
    private bool isInWhirlpool;
    private Transform whirlpoolCenter;
    private bool isBeingSucked;

    [Header("Stun Settings")]
    [SerializeField] private float defaultStunDuration = 1.5f;

    [Header("Stun Knockback")]
    public float stunKnockbackDistance = 0.5f;
    public float stunKnockbackDuration = 0.2f;

    [Header("Stamina Settings")]
    [SerializeField] public float maxStamina = 100f;
    [SerializeField] private float staminaDecreaseRate = 20f;
    [SerializeField] private float staminaRegenRate = 15f;
    public StaminaUI staminaUI;

    private float tierStaminaDecreaseRate;
    private float tierStaminaRegenRate;
    private bool tierRegenEnabled = true;

    private bool isExhausted = false;
    private const float exhaustionSpeedMultiplier = 0.85f;

    [Header("Exhaustion Audio")]
    public AudioClip heavyBreathingClip;
    private AudioSource exhaustionAudioSource;
    private bool isPlayingHeavyBreathing = false;

    [Header("Footstep Sound Settings")]
    public AudioClip runningFootstepClip;
    public AudioClip joggingFootstepClip;
    public AudioClip swimSoundClip;
    private AudioSource footstepSource;
    public float runningFootstepInterval = 0.5f;
    public float joggingFootstepInterval = 0.7f;
    public float swimFootstepInterval = 0.6f;
    private float footstepTimer;

    [Header("Obstacle Sound Settings")]
    public AudioClip logTripClip;
    public AudioClip buoyHitClip;
    public AudioClip whirlpoolLoopClip;
    private AudioSource obstacleAudioSource;

    [Header("Camera Shake Settings")]
    [SerializeField] private CinemachineImpulseSource impulseSource;
    [SerializeField] private float logShakeIntensity = 1f;
    [SerializeField] private float buoyShakeIntensity = 1.5f;

    [Header("Effects")]
    [SerializeField] private GameObject stunStarPrefab;
    [SerializeField] private Transform stunStarSpawnPoint;
    private GameObject activeStunStar;

    public CharacterController characterController;
    private Animator animator;
    [SerializeField] private Transform runnerModel;
    private Transform rootTransform;

    public Vector2 moveVector;
    private Vector3 velocity;
    public bool isRunning;
    public float waterSurfaceY;
    private bool canRotate = true;
    private NetworkPlayer netPlayer;
    private bool playedTripAnim;
    private bool wasStunnedLastFrame;
    public float currentStamina;
    private float targetYRotation;
    private bool localIsInWater = false;

    void Awake()
    {
        characterController = GetComponent<CharacterController>();
        if (characterController == null)
            ;

        animator       = GetComponent<Animator>();
        footstepSource = GetComponent<AudioSource>();
        netPlayer      = GetComponentInParent<NetworkPlayer>();
        currentStamina = maxStamina;

        rootTransform = netPlayer != null ? netPlayer.transform : transform.parent;

        if (rootTransform != null)
            targetYRotation = rootTransform.eulerAngles.y;

        if (impulseSource == null)
            impulseSource = GetComponent<CinemachineImpulseSource>();

        obstacleAudioSource           = gameObject.AddComponent<AudioSource>();
        obstacleAudioSource.playOnAwake = false;
        obstacleAudioSource.loop       = false;

        exhaustionAudioSource           = gameObject.AddComponent<AudioSource>();
        exhaustionAudioSource.playOnAwake = false;
        exhaustionAudioSource.loop       = true;

        ;
    }

    public override void OnStartClient()
    {
        base.OnStartClient();

        if (!IsOwner)
        {
            if (animator == null)
                animator = GetComponent<Animator>();
            return;
        }

        var cam = SceneReference.GetVirtualCamera();
        if (cam != null)
        {
            cam.Follow = transform;
            cam.LookAt = transform;
        }

        staminaUI = SceneReference.GetStaminaUI();
        staminaUI?.SetStamina(currentStamina, maxStamina);

        ApplyTierStaminaSettings();
        StartCoroutine(InitializeAfterSpawn());
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

        ;

        ;
    }

    private IEnumerator InitializeAfterSpawn()
    {
        yield return new WaitForFixedUpdate();
        yield return new WaitForFixedUpdate();

        if (rootTransform != null && characterController != null && !characterController.isGrounded)
        {
            RaycastHit hit;
            if (Physics.Raycast(rootTransform.position, Vector3.down, out hit, 1000f))
            {
                if (hit.distance > 5f || hit.collider.isTrigger)
                {
                    Vector3 groundPos = hit.point;
                    groundPos.y += 1.5f;

                    characterController.enabled = false;
                    rootTransform.position = groundPos;
                    transform.position     = groundPos;
                    characterController.enabled = true;
                    velocity.y = 0f;

                    ;
                }
            }
        }
    }

    void Update()
    {
        if (!IsOwner || !isActiveModel || netPlayer == null)
            return;

        if (!netPlayer.CanMove.Value)
        {
            StopMovement();
            velocity = Vector3.zero;
            UpdateAnimationState(0);
            if (characterController.enabled && characterController.isGrounded)
                characterController.Move(Vector3.down * 0.01f);
            return;
        }

        Vector3 checkPos = rootTransform != null ? rootTransform.position : transform.position;

        if (checkPos.y < -100f)
        {
            characterController.enabled = false;
            Vector3 fixPos = checkPos;
            fixPos.y = 1f;
            if (rootTransform != null) rootTransform.position = fixPos;
            transform.position = fixPos;
            characterController.enabled = true;
            velocity.y = 0f;
            return;
        }

        if (isBeingSucked)
        {
            StopMovement();
            UpdateAnimationState(0);
            return;
        }

        if (netPlayer.IsTripping.Value)
        {
            StopMovement();
            if (!playedTripAnim)
            {
                playedTripAnim = true;
                StartCoroutine(PlayTripAnimation());
            }
            return;
        }
        playedTripAnim = false;

        if (netPlayer.IsStunned.Value)
        {
            StopMovement();
            UpdateAnimationState(0);
            wasStunnedLastFrame = true;
            return;
        }

        if (wasStunnedLastFrame)
        {
            RecoverFromStunAnimation();
            wasStunnedLastFrame = false;
        }

        HandleStamina();
        UpdateExhaustionState();

        if (localIsInWater)
            SwimMovement();
        else
            GroundMovement();

        SmoothRotate();
    }

    private void HandleStamina()
    {
        if (isInWhirlpool)
        {
            currentStamina -= whirlpoolStaminaDrainRate * Time.deltaTime;
        }
        else if (netPlayer.IsInMud.Value)
        {
            currentStamina -= netPlayer.MudMultiplier.Value * mudStaminaDrainRate * Time.deltaTime;
        }
        else if (isRunning && moveVector.sqrMagnitude > 0.01f)
        {
            currentStamina -= tierStaminaDecreaseRate * Time.deltaTime;
        }
        else
        {
            if (tierRegenEnabled)
                currentStamina += tierStaminaRegenRate * Time.deltaTime;
        }

        currentStamina = Mathf.Clamp(currentStamina, 0f, maxStamina);
        staminaUI?.SetStamina(currentStamina, maxStamina);
    }

    private void UpdateExhaustionState()
    {
        if (!isExhausted && currentStamina <= 0f)
        {
            isExhausted = true;
            ;
            PlayHeavyBreathing(true);
        }
        else if (isExhausted && currentStamina > maxStamina * 0.1f)
        {
            isExhausted = false;
            ;
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

    private void UpdateAnimationState(byte newState)
    {
        if (netPlayer == null) return;
        if (IsOwner)
        {
            ApplyAnimationState(newState);
            netPlayer.ServerSetRunnerAnimState(newState);
        }
    }

    public void ApplyAnimationStateFromNetwork(byte state) => ApplyAnimationState(state);

    private void ApplyAnimationState(byte state)
    {
        if (animator == null) { ; return; }
        animator.SetBool("isJogging",  state == 1);
        animator.SetBool("isRunning",  state == 2);
        animator.SetBool("isSwimming", state == 3);
        animator.SetBool("isTreading", state == 4);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (!IsOwner || !isActiveModel) return;
        moveVector = context.ReadValue<Vector2>();
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        if (!IsOwner || !isActiveModel) return;
        isRunning = context.performed;
    }

    private void StopMovement()
    {
        moveVector = Vector2.zero;
        velocity   = Vector3.zero;
    }

    private void PlayObstacleSound(AudioClip clip)
    {
        if (obstacleAudioSource == null || clip == null) return;
        obstacleAudioSource.PlayOneShot(clip);
    }

    private void GroundMovement()
    {
        if (characterController.isGrounded && velocity.y < 0f)
            velocity.y = -2f;

        // ✅ Equipment bonuses applied here
        float equipMoveBonus   = EquipmentManager.Instance?.GetMoveSpeedBonus() ?? 0f;
        float equipSprintBonus = EquipmentManager.Instance?.GetMoveSpeedMultiplierBonus() ?? 0f;

        float speed = moveSpeed + equipMoveBonus;
        if (isRunning) speed *= (runSpeedMultiplier + equipSprintBonus);
        if (netPlayer.IsInMud.Value)     speed *= netPlayer.MudMultiplier.Value;
        if (netPlayer.IsTripping.Value)  speed *= stumbleSlowMultiplier;
        if (isExhausted)                 speed *= exhaustionSpeedMultiplier;

        speed = Mathf.Clamp(speed, 0.5f, 20f);

        bool moving = moveVector.magnitude > 0.1f;

        Vector2 effectiveMoveVector = isExhausted
            ? Vector2.Lerp(Vector2.zero, moveVector, 0.6f)
            : moveVector;

        Vector3 moveDir = (rootTransform.forward * effectiveMoveVector.y +
                           rootTransform.right   * effectiveMoveVector.x).normalized;
        Vector3 move = moving ? moveDir * speed : Vector3.zero;

        velocity.y += gravity * Time.deltaTime;
        characterController.Move((move + velocity) * Time.deltaTime);

        if (moving)
            PlayFootstep(
                isRunning ? runningFootstepClip : joggingFootstepClip,
                isRunning ? runningFootstepInterval : joggingFootstepInterval);

        if (moving && isRunning) UpdateAnimationState(2);
        else if (moving)         UpdateAnimationState(1);
        else                     UpdateAnimationState(0);

        if (canRotate && !netPlayer.IsStunned.Value)
            targetYRotation += effectiveMoveVector.x * rotationSpeed * Time.deltaTime;

        rootTransform.position  = transform.position;
        transform.localPosition = Vector3.zero;
    }

    private void SwimMovement()
    {
        bool moving = moveVector.magnitude > 0.1f;

        Vector2 effectiveMoveVector = isExhausted
            ? Vector2.Lerp(Vector2.zero, moveVector, 0.6f)
            : moveVector;

        Vector3 move = rootTransform.right   * effectiveMoveVector.x +
                       rootTransform.forward * effectiveMoveVector.y;

        float targetY = (waterSurfaceY + waterLevelOffset - bodyDepthOffset) - playerHeightOffset;

        velocity.y = Mathf.Lerp(
            velocity.y,
            (targetY - rootTransform.position.y) * floatStrength,
            Time.deltaTime * 5f);

        // ✅ Equipment swim bonus
        float equipSwimBonus   = EquipmentManager.Instance?.GetSwimSpeedBonus() ?? 0f;
        float currentSwimSpeed = swimSpeed + equipSwimBonus;

        if (isExhausted) currentSwimSpeed *= exhaustionSpeedMultiplier;
        if (isRunning && currentStamina > 0f) currentSwimSpeed *= swimSprintMultiplier;

        if (isInWhirlpool)
        {
            currentSwimSpeed *= whirlpoolSlowMultiplier;
            if (whirlpoolCenter != null) ApplyWhirlpoolPull();
        }

        characterController.Move(
            (move * (moving ? currentSwimSpeed : 0f) + Vector3.up * velocity.y) * Time.deltaTime);

        if (moving) UpdateAnimationState(3);
        else        UpdateAnimationState(4);

        if (moving) PlayFootstep(swimSoundClip, swimFootstepInterval);

        if (canRotate && !netPlayer.IsStunned.Value)
            targetYRotation += effectiveMoveVector.x * rotationSpeed * Time.deltaTime;

        rootTransform.position  = transform.position;
        transform.localPosition = Vector3.zero;
    }

    private void SmoothRotate()
    {
        if (rootTransform == null) return;
        rootTransform.rotation = Quaternion.Euler(0f, targetYRotation, 0f);
    }

    private void PlayFootstep(AudioClip clip, float interval)
    {
        footstepTimer += Time.deltaTime;
        if (footstepTimer >= interval && clip != null)
        {
            footstepSource.PlayOneShot(clip);
            footstepTimer = 0f;
        }
    }

    private void TriggerCameraShake(float intensity)
    {
        if (impulseSource != null) impulseSource.GenerateImpulse(intensity);
        else ;
    }

    private IEnumerator PlayTripAnimation()
    {
        animator?.SetTrigger("Trip");
        currentStamina = Mathf.Max(0f, currentStamina - tripStaminaCost);
        staminaUI?.SetStamina(currentStamina, maxStamina);
        TriggerCameraShake(logShakeIntensity);
        PlayObstacleSound(logTripClip);
        yield return new WaitForSeconds(tripDuration);
    }

    public IEnumerator StunPlayer(float duration = -1f)
    {
        if (netPlayer == null || netPlayer.IsStunned.Value) yield break;

        netPlayer.ServerSetStunned(true);
        ShowStunStars(true);
        animator?.SetTrigger("Stunned");
        TriggerCameraShake(buoyShakeIntensity);
        PlayObstacleSound(buoyHitClip);

        yield return new WaitForSeconds(duration > 0 ? duration : defaultStunDuration);

        ShowStunStars(false);
        animator?.ResetTrigger("Stunned");
        netPlayer.ServerSetStunned(false);
    }

    private void RecoverFromStunAnimation()
    {
        if (animator == null) return;

        // Force-reset to base locomotion state so we don't get stuck in stun animation.
        animator.Rebind();
        animator.Update(0f);
        UpdateAnimationState(0);
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
            else activeStunStar.SetActive(true);
        }
        else
        {
            if (activeStunStar != null) activeStunStar.SetActive(false);
        }
    }

    public void EnterMud(float multiplier)  { if (netPlayer != null) netPlayer.ServerEnterMud(multiplier); }
    public void ExitMud()                   { if (netPlayer != null) netPlayer.ServerExitMud(); }

    public void EnterWhirlpool(Transform center, float pullStrength, float slowMultiplier,
                                float maxDist, float drainRate)
    {
        isInWhirlpool            = true;
        whirlpoolCenter          = center;
        whirlpoolPullStrength    = pullStrength;
        whirlpoolSlowMultiplier  = slowMultiplier;
        whirlpoolMaxPullDistance = maxDist;
        whirlpoolStaminaDrainRate = drainRate;
        if (!isBeingSucked) StartCoroutine(SuckIntoWhirlpool());
    }

    public void ExitWhirlpool()
    {
        isInWhirlpool  = false;
        whirlpoolCenter = null;
        StopCoroutine(nameof(SuckIntoWhirlpool));
        isBeingSucked = false;
        if (obstacleAudioSource != null && obstacleAudioSource.clip == whirlpoolLoopClip)
        {
            obstacleAudioSource.loop = false;
            obstacleAudioSource.Stop();
        }
        ;
    }

    private void ApplyWhirlpoolPull()
    {
        if (!isInWhirlpool || whirlpoolCenter == null) return;

        Vector3 direction = whirlpoolCenter.position - transform.position;
        direction.y = 0f;
        float distance = direction.magnitude;
        if (distance < 0.1f) return;

        float pullFactor = 1f - Mathf.Clamp01(distance / whirlpoolMaxPullDistance);
        pullFactor *= pullFactor;
        float pullStrengthFinal = Mathf.Max(whirlpoolPullStrength * pullFactor, 1f);

        Vector3 pull    = direction.normalized * pullStrengthFinal * Time.deltaTime;
        Vector3 tangent = Vector3.Cross(Vector3.up, direction.normalized);
        pull += tangent * pullStrengthFinal * 0.5f * Time.deltaTime;

        characterController.Move(pull);
    }

    private IEnumerator SuckIntoWhirlpool()
    {
        isBeingSucked = true;

        if (animator != null) animator.SetTrigger("SuckedIntoWhirlpool");

        if (obstacleAudioSource != null && whirlpoolLoopClip != null)
        {
            obstacleAudioSource.clip = whirlpoolLoopClip;
            obstacleAudioSource.loop = true;
            obstacleAudioSource.Play();
        }

        float maxSuckTime = 5f;
        float elapsed     = 0f;

        while (isInWhirlpool && whirlpoolCenter != null && elapsed < maxSuckTime)
        {
            Vector3 horizontalDir = whirlpoolCenter.position - transform.position;
            horizontalDir.y = 0f;

            if (horizontalDir.magnitude < 0.1f)
            {
                characterController.Move(transform.forward * -2f);
                ;
                break;
            }

            horizontalDir.Normalize();
            Vector3 tangent      = Vector3.Cross(Vector3.up, horizontalDir);
            Vector3 horizontalPull = (horizontalDir * whirlpoolPullStrength +
                                      tangent * whirlpoolPullStrength * 0.5f) * Time.deltaTime;
            float verticalPull   = Mathf.Min(whirlpoolCenter.position.y - transform.position.y, 0f);
            characterController.Move(horizontalPull + Vector3.up * verticalPull);

            elapsed += Time.deltaTime;
            yield return null;
        }

        if (obstacleAudioSource != null && obstacleAudioSource.clip == whirlpoolLoopClip)
        {
            obstacleAudioSource.loop = false;
            obstacleAudioSource.Stop();
        }

        isBeingSucked  = false;
        isInWhirlpool  = false;
        whirlpoolCenter = null;

        if (elapsed >= maxSuckTime)
            ;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!IsOwner) return;

        if (other.CompareTag("Water"))
        {
            waterSurfaceY  = other.bounds.max.y;
            velocity.y     = 0f;
            localIsInWater = true;
            netPlayer.ServerSetInWater(true, waterSurfaceY);

            if (rootTransform.position.y < waterSurfaceY - 0.5f)
            {
                characterController.enabled = false;
                Vector3 snapPos = rootTransform.position;
                snapPos.y = waterSurfaceY;
                rootTransform.position = snapPos;
                transform.position     = snapPos;
                characterController.enabled = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!IsOwner) return;

        if (other.CompareTag("Water"))
        {
            localIsInWater = false;
            netPlayer.ServerSetInWater(false);
            velocity.y = 0f;
            if (animator != null)
            {
                animator.SetBool("isSwimming", false);
                animator.SetBool("isTreading", false);
            }
        }
    }

    public void ApplySpeedBoost(float multiplier, float duration)
        => StartCoroutine(SpeedBoostRoutine(multiplier, duration));

    private IEnumerator SpeedBoostRoutine(float multiplier, float duration)
    {
        moveSpeed *= multiplier;
        swimSpeed *= multiplier;
        yield return new WaitForSeconds(duration);
        moveSpeed /= multiplier;
        swimSpeed /= multiplier;
    }

    public void RestoreStamina(float amount)
    {
        currentStamina = Mathf.Clamp(currentStamina + amount, 0f, maxStamina);
        staminaUI?.SetStamina(currentStamina, maxStamina);
        ;
    }

    public void ResetForDismount(Vector3 worldPos, Quaternion worldRot)
    {
        velocity      = Vector3.zero;
        moveVector    = Vector2.zero;
        isRunning     = false;
        footstepTimer = 0f;

        localIsInWater = false;
        netPlayer?.ServerSetInWater(false);

        if (animator != null)
        {
            animator.SetBool("isSwimming", false);
            animator.SetBool("isTreading", false);
        }

        targetYRotation = worldRot.eulerAngles.y;
        if (rootTransform != null)
            rootTransform.rotation = Quaternion.Euler(0f, targetYRotation, 0f);

        if (characterController != null)
        {
            characterController.enabled = false;
            if (rootTransform != null) rootTransform.position = worldPos;
            transform.position = worldPos;
            characterController.enabled = true;
        }

        if (characterController != null && characterController.enabled)
            characterController.Move(Vector3.down * 0.01f);

        if (isPlayingHeavyBreathing) PlayHeavyBreathing(false);

        isInWhirlpool  = false;
        isBeingSucked  = false;
        whirlpoolCenter = null;

        ;
    }
}
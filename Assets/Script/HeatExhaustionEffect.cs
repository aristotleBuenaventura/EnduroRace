using UnityEngine;
using UnityEngine.UI;

public class HeatExhaustionEffect : MonoBehaviour
{
    [Header("References")]
    public Image heatOverlay;
    public PlayerController playerController;
    public CyclingController cyclingController;
    public SegmentSwitcher segmentSwitcher;

    [Header("Settings")]
    [Range(0f, 1f)] public float effectStartThreshold = 0.3f;
    public float fadeSpeed = 2f;
    [Range(0f, 1f)] public float maxAlpha = 0.5f;

    [Header("Pulse Settings")]
    public bool pulseAtEmpty = true;
    public float pulseSpeed = 1.5f;

    // ✅ Intermediate gets a heat warning tint when stamina is LOW (not just at 0).
    //    All tiers get the exhaustion tint when stamina actually hits 0.
    private bool isIntermediateTier = false;
    private bool effectEnabled = false;

    private Color overlayColor;
    private float pulseTimer = 0f;

    private void Start()
    {
        string tier = LobbyDataTransfer.Instance?.GetLocalPlayerData()?.tier ?? "Beginner";

        isIntermediateTier = (tier == "Intermediate");
        effectEnabled = true;

        ;

        // ✅ Only look up the HeatOverlay on the Intermediate map — it doesn't exist on other maps
        if (isIntermediateTier)
        {
            if (heatOverlay == null)
                heatOverlay = SceneReference.GetHeatOverlay();

            // Fallback: find by name if SceneReference slot wasn't filled
            if (heatOverlay == null)
            {
                GameObject overlayObj = GameObject.Find("HeatOverlay");
                if (overlayObj != null)
                    heatOverlay = overlayObj.GetComponent<Image>();
                else
                    ;
            }
        }

        // Auto find controllers if not assigned
        if (segmentSwitcher == null)
            segmentSwitcher = FindFirstObjectByType<SegmentSwitcher>();

        if (segmentSwitcher != null)
        {
            if (playerController == null)
                playerController = segmentSwitcher.runnerController;

            if (cyclingController == null)
                cyclingController = segmentSwitcher.cyclistController;
        }

        if (playerController == null)
            playerController = FindFirstObjectByType<PlayerController>();

        if (cyclingController == null)
            cyclingController = FindFirstObjectByType<CyclingController>();

        ;

        if (heatOverlay == null) return;

        overlayColor = heatOverlay.color;
        overlayColor.a = 0f;
        heatOverlay.color = overlayColor;
        heatOverlay.raycastTarget = false;
    }

    private void Update()
    {
        if (!effectEnabled || heatOverlay == null) return;

        float currentStamina = GetCurrentStamina();
        float maxStamina     = GetMaxStamina();
        bool  isExhausted    = GetIsExhausted();

        if (maxStamina <= 0f) return;

        float staminaRatio = currentStamina / maxStamina;
        float targetAlpha  = 0f;

        if (isExhausted)
        {
            // ✅ All tiers: pulsing tint when in full exhaustion state (stamina = 0)
            pulseTimer += Time.deltaTime * pulseSpeed;
            float pulse = (Mathf.Sin(pulseTimer) + 1f) / 2f;
            targetAlpha = Mathf.Lerp(maxAlpha * 0.5f, maxAlpha, pulse);
        }
        else if (isIntermediateTier && staminaRatio <= effectStartThreshold)
        {
            // ✅ Intermediate only: heat warning tint as stamina approaches empty (30% threshold)
            pulseTimer  = 0f;
            float t     = 1f - (staminaRatio / effectStartThreshold);
            targetAlpha = Mathf.Lerp(0f, maxAlpha * 0.7f, t); // slightly softer than full exhaustion
        }
        else
        {
            pulseTimer  = 0f;
            targetAlpha = 0f;
        }

        overlayColor.a   = Mathf.Lerp(overlayColor.a, targetAlpha, Time.deltaTime * fadeSpeed);
        heatOverlay.color = overlayColor;
    }

    private float GetCurrentStamina()
    {
        if (segmentSwitcher != null)
        {
            if (segmentSwitcher.IsOnBike && cyclingController != null)
                return cyclingController.currentStamina;
            if (playerController != null)
                return playerController.currentStamina;
        }

        if (cyclingController != null && cyclingController.isActiveModel)
            return cyclingController.currentStamina;
        if (playerController != null && playerController.isActiveModel)
            return playerController.currentStamina;

        return 100f;
    }

    private float GetMaxStamina()
    {
        if (segmentSwitcher != null)
        {
            if (segmentSwitcher.IsOnBike && cyclingController != null)
                return cyclingController.maxStamina;
        }

        if (cyclingController != null && cyclingController.isActiveModel)
            return cyclingController.maxStamina;

        return 100f;
    }

    // ✅ Reads exhaustion state from whichever controller is currently active
    private bool GetIsExhausted()
    {
        if (segmentSwitcher != null)
        {
            if (segmentSwitcher.IsOnBike && cyclingController != null)
                return cyclingController.IsExhausted;
            if (playerController != null)
                return playerController.IsExhausted;
        }

        if (cyclingController != null && cyclingController.isActiveModel)
            return cyclingController.IsExhausted;
        if (playerController != null && playerController.isActiveModel)
            return playerController.IsExhausted;

        return false;
    }
}
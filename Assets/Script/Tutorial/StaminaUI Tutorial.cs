using UnityEngine;
using UnityEngine.UI;

public class StaminaUITutorial : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private Image fillImage;     // Assign the battery fill image
    [SerializeField] private Color fullColor = Color.green;
    [SerializeField] private Color lowColor = Color.red;
    [SerializeField] private float lowThreshold = 0.25f; // below 25% is low

    [Header("Animation Settings")]
    [SerializeField] private float fillSmoothSpeed = 5f;

    private float targetFill = 1f;
    private float currentFill = 1f;

    void Update()
    {
        // Smoothly interpolate the fill amount
        if (fillImage != null)
        {
            currentFill = Mathf.Lerp(currentFill, targetFill, Time.deltaTime * fillSmoothSpeed);
            fillImage.fillAmount = currentFill;

            // Change color based on fill
            fillImage.color = currentFill <= lowThreshold ? lowColor : fullColor;
        }
    }

    /// <summary>
    /// Sets the stamina UI fill based on current and max stamina
    /// </summary>
    /// <param name="current">Current stamina value</param>
    /// <param name="max">Maximum stamina value</param>
    public void SetStamina(float current, float max)
    {
        if (max <= 0) return;

        targetFill = Mathf.Clamp01(current / max); // normalized between 0-1
    }
}

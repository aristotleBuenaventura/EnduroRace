using UnityEngine;
using UnityEngine.InputSystem; // Required for new Input System
using System.Collections;

public class CutsceneManagerTutorial : MonoBehaviour
{
    [Header("Camera Settings")]
    public Camera cutsceneCamera;
    public Camera mainCamera;
    public Transform[] waypoints;

    [Header("UI Settings")]
    public GameObject staminaCanvas;

    [Header("Cutscene Settings")]
    public float duration = 10f;

    [Header("Audio Settings")]
    public AudioSource backgroundMusic;

    private Coroutine cutsceneCoroutine;

    void Start()
    {
        if (cutsceneCamera == null || mainCamera == null || waypoints.Length == 0)
        {
            return;
        }

        // Disable UI and main camera, enable cutscene camera
        if (staminaCanvas != null)
            staminaCanvas.SetActive(false);

        mainCamera.enabled = false;
        cutsceneCamera.enabled = true;

        // Disable player movement
        PlayerControllerTutorial player = Object.FindFirstObjectByType<PlayerControllerTutorial>();
        if (player != null)
            player.enabled = false;

        // Start the cutscene
        cutsceneCoroutine = StartCoroutine(PlayCutscene());
    }

    void Update()
    {
        // Check Esc key with new Input System
        if (cutsceneCamera.enabled && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            SkipCutscene();
        }
    }

    private IEnumerator PlayCutscene()
    {
        float elapsed = 0f;
        int currentSegment = 0;

        while (elapsed < duration)
        {
            if (currentSegment >= waypoints.Length - 1)
                break;

            float segmentDuration = duration / (waypoints.Length - 1);
            float t = (elapsed - segmentDuration * currentSegment) / segmentDuration;
            t = Mathf.Clamp01(t);

            // Move and rotate camera smoothly
            cutsceneCamera.transform.position = Vector3.Lerp(
                waypoints[currentSegment].position,
                waypoints[currentSegment + 1].position,
                t
            );

            cutsceneCamera.transform.rotation = Quaternion.Slerp(
                waypoints[currentSegment].rotation,
                waypoints[currentSegment + 1].rotation,
                t
            );

            if (t >= 1f)
                currentSegment++;

            elapsed += Time.deltaTime;
            yield return null;
        }

        EndCutscene();
    }

    private void SkipCutscene()
    {
        if (cutsceneCoroutine != null)
            StopCoroutine(cutsceneCoroutine);

        EndCutscene();
    }

    private void EndCutscene()
    {
        // Switch cameras
        cutsceneCamera.enabled = false;
        mainCamera.enabled = true;

        // Show stamina UI
        if (staminaCanvas != null)
            staminaCanvas.SetActive(true);

        // Re-enable player movement
        PlayerControllerTutorial playerCtrl = Object.FindFirstObjectByType<PlayerControllerTutorial>();
        if (playerCtrl != null)
            playerCtrl.enabled = true;

        // Play background music if assigned
        if (backgroundMusic != null)
            backgroundMusic.Play();
    }
}

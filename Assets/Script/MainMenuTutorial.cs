using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
using System.Collections;
using Firebase.Firestore;
using Firebase.Extensions;

public class MainMenuTutorial : MonoBehaviour
{
    [System.Serializable]
    public class TutorialStep
    {
        public Button targetButton;
        [TextArea(2, 4)]
        public string tooltipText;
        public string sceneToLoad;
        public RectTransform spotlightRect;
        public AudioClip voiceClip;
    }

    [Header("Tutorial Steps")]
    public TutorialStep[] steps;

    [Header("UI References")]
    public GameObject overlay;
    public GameObject tooltipPanel;
    public TextMeshProUGUI tooltipText;
    public GameObject tapToContinueText;

    [Header("Audio")]
    public AudioSource audioSource;

    private int currentStep = 0;
    private bool waitingForTap = false;
    private bool stepReady = false;

    private const string TUTORIAL_ACTIVE_KEY = "TutorialActive";
    private const string MAIN_MENU_STEP_KEY = "MainMenuTutorialStep";

    private void Start()
    {
        HideOverlay();

        // ── Case 1: Same-session scene handoff (e.g. returning from ProfileManager) ──
        if (TutorialSignal.PendingStep >= 0)
        {
            currentStep = TutorialSignal.PendingStep;
            TutorialSignal.Clear();
            ;
            AdvancePastSceneStepIfNeeded();
            if (currentStep < steps.Length)
                ShowStep(currentStep);
            else
                EndTutorial();
            return;
        }

        // ── Case 2: Mid-tutorial resume from PlayerPrefs (savedStep > 0) ──
        bool tutorialActive = PlayerPrefs.GetInt(TUTORIAL_ACTIVE_KEY, 0) == 1;
        int savedStep = PlayerPrefs.GetInt(MAIN_MENU_STEP_KEY, 0);

        ;

        if (tutorialActive && savedStep > 0)
        {
            currentStep = savedStep;
            AdvancePastSceneStepIfNeeded();
            if (currentStep < steps.Length)
                ShowStep(currentStep);
            else
                EndTutorial();
            return;
        }

        // ── Case 3: PlayerPrefs says active at step 0 — show immediately ──
        if (tutorialActive && savedStep == 0)
        {
            currentStep = 0;
            ShowStep(currentStep);
            return;
        }

        // ── Case 4: No PlayerPrefs signal — ask Firebase directly ──
        // This handles players who exist in Firestore but have hasSeenTutorial=false
        // and whose PlayerPrefs were cleared (reinstall, new device, etc.).
        if (FirebaseManager.Instance.IsFirebaseReady)
            CheckFirestoreForTutorial();
        else
            FirebaseManager.Instance.OnFirebaseReady += CheckFirestoreForTutorial;
    }

    private void OnDestroy()
    {
        TutorialSignal.OnTutorialReady -= OnTutorialReadySignal;
        FirebaseManager.Instance.OnFirebaseReady -= CheckFirestoreForTutorial;
    }

    private void CheckFirestoreForTutorial()
    {
        FirebaseManager.Instance.OnFirebaseReady -= CheckFirestoreForTutorial;

        var db = FirebaseManager.Instance.Db;
        var playerId = FirebaseManager.Instance.PlayerId;

        ;

        db.Collection("players").Document(playerId).GetSnapshotAsync()
          .ContinueWithOnMainThread(task =>
          {
              if (task.IsFaulted || task.IsCanceled)
              {
                  ;
                  return;
              }

              var snap = task.Result;

              if (!snap.Exists)
              {
                  // Brand new player — ProfileManager will handle creation + dispatch.
                  // Subscribe and wait.
                  TutorialSignal.OnTutorialReady += OnTutorialReadySignal;
                  ;
                  return;
              }

              bool hasSeenTutorial = snap.ContainsField("hasSeenTutorial")
                                     && snap.GetValue<bool>("hasSeenTutorial");

              ;

              if (!hasSeenTutorial)
              {
                  // Player exists but hasn't finished the tutorial.
                  // Restore PlayerPrefs and start from step 0.
                  StartTutorial();
                  currentStep = 0;
                  ShowStep(currentStep);
              }
              // else: tutorial done — do nothing, overlay stays hidden.
          });
    }

    private void OnTutorialReadySignal(int step)
    {
        TutorialSignal.OnTutorialReady -= OnTutorialReadySignal;
        currentStep = step;
        ;
        ShowStep(currentStep);
    }

    private void AdvancePastSceneStepIfNeeded()
    {
        if (currentStep < steps.Length && !string.IsNullOrEmpty(steps[currentStep].sceneToLoad))
        {
            currentStep++;
            PlayerPrefs.SetInt(MAIN_MENU_STEP_KEY, currentStep);
            PlayerPrefs.Save();
            ;
        }
    }

    private void Update()
    {
        if (!stepReady || !waitingForTap) return;

        bool tapped = false;

        if (Touchscreen.current != null)
        {
            if (Touchscreen.current.primaryTouch.phase.ReadValue() == UnityEngine.InputSystem.TouchPhase.Began)
                tapped = true;
        }
        else if (Mouse.current != null)
        {
            if (Mouse.current.leftButton.wasPressedThisFrame)
                tapped = true;
        }

        if (tapped)
        {
            waitingForTap = false;
            AdvanceStep();
        }
    }

    private void ShowStep(int index)
    {
        if (index >= steps.Length)
        {
            EndTutorial();
            return;
        }

        currentStep = index;
        stepReady = false;
        waitingForTap = false;

        StartCoroutine(RevealStep(steps[index]));
    }

    private IEnumerator RevealStep(TutorialStep step)
    {
        yield return new WaitForSeconds(0.1f);
        ;

        foreach (var s in steps)
            if (s.spotlightRect != null)
                s.spotlightRect.gameObject.SetActive(false);

        if (overlay != null) overlay.SetActive(true);
        if (tooltipPanel != null) tooltipPanel.SetActive(true);
        if (tapToContinueText != null) tapToContinueText.SetActive(true);

        if (overlay != null)
        {
            var img = overlay.GetComponent<UnityEngine.UI.Image>();
            if (img != null) img.raycastTarget = false;
        }

        if (tooltipPanel != null)
        {
            var img = tooltipPanel.GetComponent<UnityEngine.UI.Image>();
            if (img != null) img.raycastTarget = false;
        }

        if (tooltipText != null)
            tooltipText.text = step.tooltipText;

        if (audioSource != null && step.voiceClip != null)
            audioSource.PlayOneShot(step.voiceClip);

        if (step.spotlightRect != null)
        {
            step.spotlightRect.gameObject.SetActive(true);
            foreach (var graphic in step.spotlightRect.GetComponentsInChildren<UnityEngine.UI.Graphic>(true))
                graphic.raycastTarget = false;
        }

        stepReady = true;
        waitingForTap = true;
    }

    private void AdvanceStep()
    {
        TutorialStep step = steps[currentStep];
        ;

        if (!string.IsNullOrEmpty(step.sceneToLoad))
        {
            PlayerPrefs.SetInt(MAIN_MENU_STEP_KEY, currentStep);
            PlayerPrefs.Save();
            HideOverlay();
            UnityEngine.SceneManagement.SceneManager.LoadScene(step.sceneToLoad);
        }
        else
        {
            int nextStep = currentStep + 1;
            PlayerPrefs.SetInt(MAIN_MENU_STEP_KEY, nextStep);
            PlayerPrefs.Save();
            ShowStep(nextStep);
        }
    }

    private void HideOverlay()
    {
        if (overlay != null) overlay.SetActive(false);
        if (tooltipPanel != null) tooltipPanel.SetActive(false);
        if (tapToContinueText != null) tapToContinueText.SetActive(false);

        foreach (var s in steps)
            if (s.spotlightRect != null)
                s.spotlightRect.gameObject.SetActive(false);

        if (audioSource != null)
            audioSource.Stop();

        stepReady = false;
        waitingForTap = false;
    }

    private void EndTutorial()
    {
        ClearTutorial();
        HideOverlay();
    }

    public static void StartTutorial()
    {
        PlayerPrefs.SetInt(TUTORIAL_ACTIVE_KEY, 1);
        PlayerPrefs.SetInt(MAIN_MENU_STEP_KEY, 0);
        PlayerPrefs.Save();
    }

    public static void ClearTutorial()
    {
        PlayerPrefs.DeleteKey(TUTORIAL_ACTIVE_KEY);
        PlayerPrefs.DeleteKey(MAIN_MENU_STEP_KEY);
        PlayerPrefs.Save();
    }
}
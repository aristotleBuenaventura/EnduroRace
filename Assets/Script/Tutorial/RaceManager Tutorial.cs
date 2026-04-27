using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class RaceManagerTutorial : MonoBehaviour
{
    [Header("Segment & Models")]
    public SegmentSwitcherTutorial segmentSwitcher;

    [Header("UI")]
    public TMP_Text raceTimerText;
    public TMP_Text segmentText;
    public TMP_Text countdownText;

    [Header("Race Settings")]
    public bool raceStarted = false;
    private float raceTime = 0f;

    public enum Segment { Swim, Bike, Run, Finished }
    public Segment currentSegment = Segment.Swim;

    [Header("Countdown Settings")]
    public float countdownTime = 3f;
    public AudioSource countdownAudioSource;
    public AudioClip countdownClip;

    [Header("Cutscene Reference")]
    public Camera cutsceneCamera;

    [Header("Tutorial / Intro")]
    public TMP_Text introText;
    public GameObject introPanel;

    [Header("Gameplay UI")]
    public GameObject gameplayUIParent;

    [Header("Input")]
    public UnityEngine.InputSystem.InputAction tapAction;

    [Header("Tutorial State")]
    public TutorialState tutorialState = TutorialState.None;

    [Header("Quest UI")]
    public GameObject questPanel;
    public TMP_Text questDescriptionText;
    public TMP_Text questStatusText;

    [Header("Tutorial Images")]
    public UnityEngine.UI.Image tutorialImage;
    public UnityEngine.UI.Image tutorialImageStamina;
    public UnityEngine.UI.Image swimObstacleImage;
    public Sprite movementSprite;
    public Sprite sprintSprite;
    public Sprite staminaSprite;
    public Sprite swimObstacleSprite;
    public Sprite swimObstacleSprite2;
    public Sprite swimObstacleSprite3;
    public Sprite swimObstacleSprite4;
    public Sprite mountSprite;

    [Header("Powerup Sprites")]
    public Sprite waterBottleSprite;
    public Sprite energyDrinkSprite;

    [Header("Settings")]
    public InRaceSettingsPanel settingsPanel;
    public Button settingsButton;

    [Header("Ranking")]
    public RaceRankingSystem rankingSystem;
    public static System.Action OnRaceStarted;

    private bool isCutscenePlaying = false;
    private AIOpponentManager aiManager;

    [Header("Swim Obstacle Cutscene")]
    public Camera swimTutorialCamera;
    public Camera swimTutorialCamera2;
    public GameObject cutsceneSubtitlePanel;
    public TMP_Text cutsceneSubtitleText;

    [Header("Powerup Cutscene")]
    public Camera powerupCamera;
    public Camera powerupCamera2;

    [Header("Bike Mount Cutscene")]
    public Camera bikeMountCamera1;
    public Camera bikeMountCamera2;

    [Header("Bike Obstacle Cutscene")]
    public Camera bikeObstacleCamera1;
    public Camera bikeObstacleCamera2;

    [Header("Run Segment Cutscene")]
    public Camera runSegmentCamera1;
    public Camera runSegmentCamera2;

    [Header("Podium Cutscene")]
    public Camera podiumCamera;
    public GameObject podiumCutsceneObject;
    public Transform[] podiumPositions;
    public float podiumCutsceneDuration = 5f;
    public GameObject podiumUI;
    public TMP_Text podiumRankText;
    public TMP_Text podiumTimeText;
    public ParticleSystem confettiEffect;
    public AudioSource podiumMusic;

    [Header("Segment Barriers")]
    [Tooltip("Barrier at the start line — disabled once player reaches the start area")]
    public GameObject startLineBarrier;

    [Header("Scene Settings")]
    [Tooltip("Exact name of your Main Menu scene as shown in Build Settings")]
    public string mainMenuSceneName = "MainMenu";

    // -------------------------------------------------------
    // TTS FIELDS
    // -------------------------------------------------------

    [Header("TTS – Audio Source")]
    [Tooltip("A dedicated AudioSource on this GameObject for all TTS playback")]
    public AudioSource ttsAudioSource;

    [Header("TTS – Tutorial Panel Audio")]
    [Tooltip("Index 0=Welcome, 1=Movement, 2=Sprint, 3=Stamina")]
    public AudioClip[] introPanelClips;
    public AudioClip goToStartClip;
    public AudioClip raceExplanationClip;
    public AudioClip swimSegmentClip;
    public AudioClip bikeSegmentClip;
    public AudioClip runSegmentClip;
    public AudioClip bikeMountClip;

    [Header("TTS – Swim Obstacle Cutscene Audio")]
    [Tooltip("One clip per shot in order: careful/buoys/stun/whirlpool/stamina")]
    public AudioClip[] swimCutsceneClips;

    [Header("TTS – Powerup Cutscene Audio")]
    [Tooltip("One clip per shot in order: powerups/waterbottle/energydrink/grab")]
    public AudioClip[] powerupCutsceneClips;

    [Header("TTS – Bike Mount Cutscene Audio")]
    [Tooltip("One clip per shot in order: timetoride/mount/hazards/crash")]
    public AudioClip[] bikeMountCutsceneClips;

    [Header("TTS – Bike Obstacle Cutscene Audio")]
    [Tooltip("One clip per shot in order: hazards/rocks/crash/slowdown")]
    public AudioClip[] bikeObstacleCutsceneClips;

    [Header("TTS – Run Segment Cutscene Audio")]
    [Tooltip("One clip per shot in order: laststretch/sprint/logs/mud")]
    public AudioClip[] runCutsceneClips;

    // -------------------------------------------------------

    public enum TutorialState
    {
        None,
        IntroPanels,
        GoToStartArea,
        WaitingAtStart,
        Countdown,
        RaceExplanation,
        SwimQuest,
        BikeRideTutorial,
        Finished
    }

    public enum QuestStatus
    {
        InProgress,
        Completed
    }

    public enum PowerupType
    {
        WaterBottle,
        EnergyDrink
    }

    // -------------------------------------------------------
    // LIFECYCLE
    // -------------------------------------------------------

    void Start()
    {
        aiManager = Object.FindFirstObjectByType<AIOpponentManager>();

        if (questPanel != null)
            questPanel.SetActive(false);

        if (cutsceneSubtitlePanel != null)
            cutsceneSubtitlePanel.SetActive(false);
        
        if (settingsButton != null && settingsPanel != null)
            settingsButton.onClick.AddListener(() => settingsPanel.Toggle());

        StartCoroutine(WaitForCutsceneThenTutorial());
    }

    void Update()
    {
        if (!raceStarted || currentSegment == Segment.Finished) return;

        raceTime += Time.deltaTime;
        int minutes = Mathf.FloorToInt(raceTime / 60);
        int seconds = Mathf.FloorToInt(raceTime % 60);
        raceTimerText.text = $"{minutes:00}:{seconds:00}";
    }
    private void OnDestroy()
    {
        if (settingsButton != null)
            settingsButton.onClick.RemoveAllListeners();
    }
    
    // -------------------------------------------------------
    // CUTSCENE PAUSE / RESUME HELPERS
    // -------------------------------------------------------

    private void PauseCutscene()
    {
        Time.timeScale = 0f;
        if (aiManager != null)
            aiManager.SetOpponentsPaused(true);
    }

    private void ResumeCutscene()
    {
        Time.timeScale = 1f;
        if (aiManager != null)
            aiManager.SetOpponentsPaused(false);
    }

    // -------------------------------------------------------
    // TTS HELPERS
    // -------------------------------------------------------

    /// <summary>
    /// Plays a TTS clip and waits for it to finish.
    /// Use this in cutscenes where audio must complete before moving on.
    /// </summary>
    private IEnumerator PlayTTSAndWait(AudioClip clip)
    {
        if (ttsAudioSource == null || clip == null)
            yield break;

        ttsAudioSource.Stop();
        ttsAudioSource.clip = clip;
        ttsAudioSource.Play();

        yield return new WaitForSecondsRealtime(clip.length);
    }

    /// <summary>
    /// Plays a TTS clip without waiting.
    /// Use this for tutorial panels where the player taps to advance.
    /// </summary>
    private void PlayTTS(AudioClip clip)
    {
        if (ttsAudioSource == null || clip == null) return;
        ttsAudioSource.Stop();
        ttsAudioSource.clip = clip;
        ttsAudioSource.Play();
    }

    /// <summary>
    /// Stops TTS playback immediately (e.g. when player taps to skip ahead).
    /// </summary>
    private void StopTTS()
    {
        if (ttsAudioSource != null && ttsAudioSource.isPlaying)
            ttsAudioSource.Stop();
    }

    // -------------------------------------------------------
    // INTRO / TUTORIAL FLOW
    // -------------------------------------------------------

    private IEnumerator WaitForCutsceneThenTutorial()
    {
        while (cutsceneCamera != null && cutsceneCamera.enabled)
            yield return null;

        tutorialState = TutorialState.IntroPanels;

        segmentSwitcher.LockPlayerInput(true);

        // Panel 0 — Welcome
        if (introPanelClips != null && introPanelClips.Length > 0)
            PlayTTS(introPanelClips[0]);
        yield return ShowPanel(
            "Welcome to the Endurorace!\nLearn the controls, Finish each segment, and reach the goal.\n\nTap to continue"
        );

        // Panel 1 — Movement
        if (introPanelClips != null && introPanelClips.Length > 1)
            PlayTTS(introPanelClips[1]);
        yield return ShowPanel(
            "MOVEMENT\n\n\n\n\n\n\nUse D-Pad to move around\nTap to continue",
            "Movement"
        );

        // Panel 2 — Sprint
        if (introPanelClips != null && introPanelClips.Length > 2)
            PlayTTS(introPanelClips[2]);
        yield return ShowPanel(
            "SPRINT\n\n\n\n\n\n\nPress the Sprint Button to sprint\nTap to continue",
            "Sprint"
        );

        segmentSwitcher.runnerInput.GetComponent<PlayerControllerTutorial>()
            .EnableSprint(true);

        // Panel 3 — Stamina
        if (introPanelClips != null && introPanelClips.Length > 3)
            PlayTTS(introPanelClips[3]);
        yield return ShowPanel(
            "STAMINA\n\n\n\n\nSprinting use stamina.\nWhen stamina is empty you slow down\n\nTap to continue",
            "Stamina"
        );

        segmentSwitcher.LockPlayerInput(false);

        StartGoToStartQuest();
    }

    private void StartGoToStartQuest()
    {
        tutorialState = TutorialState.GoToStartArea;

        introPanel.SetActive(true);
        HideGameplayUI();

        PlayTTS(goToStartClip);
        introText.text = "OBJECTIVE:\nGo to the starting area to start the race\n\nTap to continue";

        StartCoroutine(HideObjectiveOnClick());

        if (questPanel != null)
            questPanel.SetActive(true);

        UpdateQuestUI(
            "Go to the starting area",
            QuestStatus.InProgress
        );

        segmentSwitcher.EnableRunnerInput();
    }

    public void OnPlayerReachedStartArea()
    {
        if (tutorialState != TutorialState.GoToStartArea)
            return;

        if (startLineBarrier != null)
            startLineBarrier.SetActive(false);

        StopTTS();

        UpdateQuestUI(
            "Go to the starting area",
            QuestStatus.Completed
        );

        tutorialState = TutorialState.RaceExplanation;

        introPanel.SetActive(false);
        ShowGameplayUI();

        StartCoroutine(ShowRaceExplanation());
    }

    // -------------------------------------------------------
    // PANELS
    // -------------------------------------------------------

    public IEnumerator ShowPanel(string message, string step = "")
    {
        if (introPanel == null || introText == null) yield break;

        introPanel.SetActive(true);
        HideGameplayUI();
        introText.text = message;

        HideAllTutorialImages();

        switch (step)
        {
            case "Movement":
                ShowImage(movementSprite);
                break;
            case "Sprint":
                ShowImage(sprintSprite);
                break;
            case "Stamina":
                ShowStaminaImage(staminaSprite);
                break;
            case "SwimObstacle":
                ShowSwimObstacleImage(swimObstacleSprite);
                break;
            case "SwimObstacle2":
                ShowSwimObstacleImage(swimObstacleSprite2);
                break;
            case "SwimObstacle3":
                ShowSwimObstacleImage(swimObstacleSprite3);
                break;
            case "SwimObstacle4":
                ShowSwimObstacleImage(swimObstacleSprite4);
                break;
            case "WaterBottle":
                ShowImage(waterBottleSprite);
                break;
            case "EnergyDrink":
                ShowImage(energyDrinkSprite);
                break;
            case "Mount":
                ShowImage(mountSprite);
                break;
        }

        yield return null;

        while (true)
        {
            if (!isCutscenePlaying)
            {
                if (UnityEngine.InputSystem.Keyboard.current.anyKey.wasPressedThisFrame ||
                    UnityEngine.InputSystem.Mouse.current.leftButton.wasPressedThisFrame)
                    break;

                if (UnityEngine.InputSystem.Touchscreen.current != null &&
                    UnityEngine.InputSystem.Touchscreen.current.primaryTouch.press.isPressed)
                    break;
            }

            yield return null;
        }

        StopTTS();
        HideAllTutorialImages();
        introPanel.SetActive(false);
        ShowGameplayUI();
    }

    private IEnumerator HideObjectiveOnClick()
    {
        yield return null;

        while (true)
        {
            if (!isCutscenePlaying)
            {
                if (UnityEngine.InputSystem.Keyboard.current.anyKey.wasPressedThisFrame ||
                    UnityEngine.InputSystem.Mouse.current.leftButton.wasPressedThisFrame)
                    break;

                if (UnityEngine.InputSystem.Touchscreen.current != null &&
                    UnityEngine.InputSystem.Touchscreen.current.primaryTouch.press.isPressed)
                    break;
            }

            yield return null;
        }

        StopTTS();
        introPanel.SetActive(false);
        ShowGameplayUI();
    }

    // -------------------------------------------------------
    // RACE EXPLANATION + COUNTDOWN
    // -------------------------------------------------------

    private IEnumerator ShowRaceExplanation()
    {
        segmentSwitcher.LockPlayerInput(true);

        PlayTTS(raceExplanationClip);
        introPanel.SetActive(true);
        HideGameplayUI();
        introText.text =
            "RACE START!\n\n" +
            "• Complete all segments in order\n" +
            "• Manage your stamina wisely\n" +
            "• Reach the finish as fast as possible\n\n" +
            "Tap to Continue";
        yield return StartCoroutine(HideObjectiveOnClick());

        PlayTTS(swimSegmentClip);
        introPanel.SetActive(true);
        HideGameplayUI();
        introText.text =
            "SWIM SEGMENT\n\n" +
            "• Swim through the water to reach the next checkpoint\n\n" +
            "Tap to Continue";
        yield return StartCoroutine(HideObjectiveOnClick());

        yield return StartCoroutine(ShowObstaclesAndPowerupsCutscenes());
        yield return StartCoroutine(CountdownRoutine());
    }

    private IEnumerator CountdownRoutine()
    {
        UpdateQuestUI(
            "Go to the starting area",
            QuestStatus.Completed
        );

        ShowGameplayUI();
        countdownText.gameObject.SetActive(true);
        float timer = countdownTime;

        segmentSwitcher.LockPlayerInput(true);

        if (countdownAudioSource != null && countdownClip != null)
        {
            countdownAudioSource.clip = countdownClip;
            countdownAudioSource.loop = false;
            countdownAudioSource.Play();
        }

        while (timer > 0)
        {
            countdownText.text = Mathf.CeilToInt(timer).ToString();
            timer -= Time.deltaTime;
            yield return null;
        }

        countdownText.text = "GO!";
        yield return new WaitForSeconds(1f);
        countdownText.gameObject.SetActive(false);

        if (countdownAudioSource != null && countdownAudioSource.isPlaying)
            countdownAudioSource.Stop();

        segmentSwitcher.LockPlayerInput(false);

        StartRace();
        StartSwimQuest();
        OnRaceStarted?.Invoke();
    }

    // -------------------------------------------------------
    // SWIM SEGMENT
    // -------------------------------------------------------

    private void StartSwimQuest()
    {
        tutorialState = TutorialState.SwimQuest;

        UpdateQuestUI(
            "Finish the swim segment and reach the bike area",
            QuestStatus.InProgress
        );
    }

    private IEnumerator ShowObstaclesAndPowerupsCutscenes()
    {
        yield return StartCoroutine(ShowSwimObstacleCutscene());
        yield return StartCoroutine(ShowPowerupCutscene());
    }

    private IEnumerator ShowSwimObstacleCutscene()
    {
        if (swimTutorialCamera == null || swimTutorialCamera2 == null)
        {
            cutsceneSubtitlePanel?.SetActive(false);
            ResumeCutscene();
            ShowGameplayUI();
            segmentSwitcher.LockPlayerInput(false);
            yield break;
        }

        isCutscenePlaying = true;
        segmentSwitcher.LockPlayerInput(true);
        PauseCutscene();

        Camera mainCam = Camera.main;
        if (mainCam != null) mainCam.enabled = false;

        HideGameplayUI();
        introPanel.SetActive(false);
        cutsceneSubtitlePanel?.SetActive(true);

        var shots = new (Camera cam, string subtitle, float duration)[]
        {
            (swimTutorialCamera,  "Careful... the water is full of dangers.",            2.5f),
            (swimTutorialCamera,  "BUOYS block your path...",                            2.5f),
            (swimTutorialCamera,  "Hitting them STUNS and pushes you back!",             2.5f),
            (swimTutorialCamera2, "WHIRLPOOLS are dangerous...",                         2.5f),
            (swimTutorialCamera2, "They pull you in, drain stamina, and slow you down.", 3.0f),
        };

        try
        {
            Camera activeCam = null;

            for (int i = 0; i < shots.Length; i++)
            {
                var shot = shots[i];

                if (activeCam != shot.cam)
                {
                    if (activeCam != null) activeCam.enabled = false;
                    shot.cam.enabled = true;
                    activeCam = shot.cam;
                }

                if (cutsceneSubtitleText != null)
                    cutsceneSubtitleText.text = shot.subtitle;

                // Play TTS clip for this shot if one is assigned
                AudioClip clip = (swimCutsceneClips != null && i < swimCutsceneClips.Length)
                    ? swimCutsceneClips[i] : null;

                if (clip != null)
                {
                    PlayTTS(clip);
                    // Wait for whichever is longer: the clip or the shot duration
                    float waitTime = Mathf.Max(clip.length, shot.duration);
                    yield return new WaitForSecondsRealtime(waitTime);
                }
                else
                {
                    yield return new WaitForSecondsRealtime(shot.duration);
                }
            }

            if (activeCam != null) activeCam.enabled = false;
        }
        finally
        {
            isCutscenePlaying = false;
            StopTTS();
            cutsceneSubtitlePanel?.SetActive(false);

            swimTutorialCamera.enabled = false;
            swimTutorialCamera2.enabled = false;
            if (mainCam != null) mainCam.enabled = true;

            ResumeCutscene();
            ShowGameplayUI();
            segmentSwitcher.LockPlayerInput(false);

        }
    }

    private IEnumerator ShowPowerupCutscene()
    {
        if (powerupCamera == null)
        {
            yield break;
        }

        isCutscenePlaying = true;
        segmentSwitcher.LockPlayerInput(true);
        PauseCutscene();

        Camera mainCam = Camera.main;
        if (mainCam != null) mainCam.enabled = false;

        HideGameplayUI();
        introPanel.SetActive(false);
        cutsceneSubtitlePanel?.SetActive(true);

        Camera cam2 = powerupCamera2 != null ? powerupCamera2 : powerupCamera;

        var shots = new (Camera cam, string subtitle, float duration)[]
        {
            (powerupCamera, "Keep an eye out for POWERUPS!",                2.5f),
            (powerupCamera, "WATER BOTTLE restores your stamina instantly.", 2.5f),
            (cam2,          "ENERGY DRINK boosts your speed temporarily.",   2.5f),
            (cam2,          "Grab them whenever you can!",                   2.0f),
        };

        try
        {
            Camera activeCam = null;

            for (int i = 0; i < shots.Length; i++)
            {
                var shot = shots[i];

                if (activeCam != shot.cam)
                {
                    if (activeCam != null) activeCam.enabled = false;
                    shot.cam.enabled = true;
                    activeCam = shot.cam;
                }

                if (cutsceneSubtitleText != null)
                    cutsceneSubtitleText.text = shot.subtitle;

                AudioClip clip = (powerupCutsceneClips != null && i < powerupCutsceneClips.Length)
                    ? powerupCutsceneClips[i] : null;

                if (clip != null)
                {
                    PlayTTS(clip);
                    float waitTime = Mathf.Max(clip.length, shot.duration);
                    yield return new WaitForSecondsRealtime(waitTime);
                }
                else
                {
                    yield return new WaitForSecondsRealtime(shot.duration);
                }
            }

            if (activeCam != null) activeCam.enabled = false;
        }
        finally
        {
            isCutscenePlaying = false;
            StopTTS();
            cutsceneSubtitlePanel?.SetActive(false);

            if (powerupCamera != null) powerupCamera.enabled = false;
            if (powerupCamera2 != null) powerupCamera2.enabled = false;
            if (mainCam != null) mainCam.enabled = true;

            ResumeCutscene();
            ShowGameplayUI();
            segmentSwitcher.LockPlayerInput(false);

        }
    }

    public void ReachSwimToBikePoint()
    {
        if (currentSegment != Segment.Swim) return;

        if (tutorialState == TutorialState.SwimQuest)
        {
            UpdateQuestUI(
                "Finish the swim segment and reach the bike area",
                QuestStatus.Completed
            );

            currentSegment = Segment.Bike;
            RefreshSegmentUI();

            StartCoroutine(ShowBikeSegmentExplanation());
        }
    }

    // -------------------------------------------------------
    // BIKE SEGMENT
    // -------------------------------------------------------

    private IEnumerator ShowBikeSegmentExplanation()
    {
        segmentSwitcher.LockPlayerInput(true);

        PlayTTS(bikeSegmentClip);
        introPanel.SetActive(true);
        HideGameplayUI();
        introText.text =
            "BIKE SEGMENT\n\n" +
            "• Ride your bike to the next checkpoint\n" +
            "• Maintain speed and control\n\n" +
            "Tap to Continue";
        yield return StartCoroutine(HideObjectiveOnClick());

        segmentSwitcher.LockPlayerInput(false);

        yield return StartCoroutine(ShowBikeMountCutscene());
        yield return StartCoroutine(ShowBikeObstacleCutscene());
        yield return StartCoroutine(ShowBikeRideTutorial());
    }

    private IEnumerator ShowBikeMountCutscene()
    {
        if (bikeMountCamera1 == null)
        {
            yield break;
        }

        isCutscenePlaying = true;
        segmentSwitcher.LockPlayerInput(true);

        Camera mainCam = Camera.main;
        if (mainCam != null) mainCam.enabled = false;

        HideGameplayUI();
        introPanel.SetActive(false);
        cutsceneSubtitlePanel?.SetActive(true);

        Camera cam2 = bikeMountCamera2 != null ? bikeMountCamera2 : bikeMountCamera1;

        var shots = new (Camera cam, string subtitle, float duration)[]
        {
            (bikeMountCamera1, "Time to ride!",                                        2.5f),
            (bikeMountCamera1, "To mount the bicycle,",                                2.5f),
            (bikeMountCamera1, "Press the Mount button when near your bike!",          2.5f),
            (bikeMountCamera1, "Ride your bike and pedal to the next zone.",          2.5f),
        };

        try
        {
            Camera activeCam = null;

            for (int i = 0; i < shots.Length; i++)
            {
                var shot = shots[i];

                if (activeCam != shot.cam)
                {
                    if (activeCam != null) activeCam.enabled = false;
                    shot.cam.enabled = true;
                    activeCam = shot.cam;
                }

                if (cutsceneSubtitleText != null)
                    cutsceneSubtitleText.text = shot.subtitle;

                AudioClip clip = (bikeMountCutsceneClips != null && i < bikeMountCutsceneClips.Length)
                    ? bikeMountCutsceneClips[i] : null;

                if (clip != null)
                {
                    PlayTTS(clip);
                    float waitTime = Mathf.Max(clip.length, shot.duration);
                    yield return new WaitForSecondsRealtime(waitTime);
                }
                else
                {
                    yield return new WaitForSecondsRealtime(shot.duration);
                }
            }

            if (activeCam != null) activeCam.enabled = false;
        }
        finally
        {
            isCutscenePlaying = false;
            StopTTS();
            cutsceneSubtitlePanel?.SetActive(false);

            if (bikeMountCamera1 != null) bikeMountCamera1.enabled = false;
            if (bikeMountCamera2 != null) bikeMountCamera2.enabled = false;
            if (mainCam != null) mainCam.enabled = true;

            ResumeCutscene();
            ShowGameplayUI();
            segmentSwitcher.LockPlayerInput(false);

        }
    }

    private IEnumerator ShowBikeObstacleCutscene()
    {
        if (bikeObstacleCamera1 == null)
        {
            yield break;
        }

        isCutscenePlaying = true;
        segmentSwitcher.LockPlayerInput(true);
        PauseCutscene();

        Camera mainCam = Camera.main;
        if (mainCam != null) mainCam.enabled = false;

        HideGameplayUI();
        introPanel.SetActive(false);
        cutsceneSubtitlePanel?.SetActive(true);

        Camera cam2 = bikeObstacleCamera2 != null ? bikeObstacleCamera2 : bikeObstacleCamera1;

        var shots = new (Camera cam, string subtitle, float duration)[]
        {
            (bikeObstacleCamera1, "Watch out for speed bumps on the road!",          2.5f),
            (bikeObstacleCamera1, "Hitting one slows you down and drains stamina!",  2.5f),
            (cam2,                "Potholes are hidden dangers...",                   2.5f),
            (cam2,                "They shake your bike and drain your stamina!",     2.5f),
        };

        try
        {
            Camera activeCam = null;

            for (int i = 0; i < shots.Length; i++)
            {
                var shot = shots[i];

                if (activeCam != shot.cam)
                {
                    if (activeCam != null) activeCam.enabled = false;
                    shot.cam.enabled = true;
                    activeCam = shot.cam;
                }

                if (cutsceneSubtitleText != null)
                    cutsceneSubtitleText.text = shot.subtitle;

                AudioClip clip = (bikeObstacleCutsceneClips != null && i < bikeObstacleCutsceneClips.Length)
                    ? bikeObstacleCutsceneClips[i] : null;

                if (clip != null)
                {
                    PlayTTS(clip);
                    float waitTime = Mathf.Max(clip.length, shot.duration);
                    yield return new WaitForSecondsRealtime(waitTime);
                }
                else
                {
                    yield return new WaitForSecondsRealtime(shot.duration);
                }
            }

            if (activeCam != null) activeCam.enabled = false;
        }
        finally
        {
            isCutscenePlaying = false;
            StopTTS();
            cutsceneSubtitlePanel?.SetActive(false);

            if (bikeObstacleCamera1 != null) bikeObstacleCamera1.enabled = false;
            if (bikeObstacleCamera2 != null) bikeObstacleCamera2.enabled = false;
            if (mainCam != null) mainCam.enabled = true;

            ResumeCutscene();
            ShowGameplayUI();
            segmentSwitcher.LockPlayerInput(false);

        }
    }

    private IEnumerator ShowBikeRideTutorial()
    {
        tutorialState = TutorialState.BikeRideTutorial;
        segmentSwitcher.LockPlayerInput(true);

        PlayTTS(bikeMountClip);
        yield return ShowPanel(
            "MOUNT BUTTON\n\n\n\n\n\n" +
            "Use the Mount Button to ride a bike\nTap to continue",
            "Mount"
        );

        segmentSwitcher.LockPlayerInput(false);

        StartBikeQuest();
    }

    private void StartBikeQuest()
    {
        UpdateQuestUI(
            "Finish the bike segment and reach the run area",
            QuestStatus.InProgress
        );
    }

    public void ReachBikeToRunPoint()
    {
        if (currentSegment != Segment.Bike)
            return;

        UpdateQuestUI(
            "Finish the bike segment and reach the run area",
            QuestStatus.Completed
        );

        currentSegment = Segment.Run;
        RefreshSegmentUI();

        StartCoroutine(ShowRunSegmentExplanation());
    }

    // -------------------------------------------------------
    // RUN SEGMENT
    // -------------------------------------------------------

    private IEnumerator ShowRunSegmentExplanation()
    {
        segmentSwitcher.LockPlayerInput(true);

        PlayTTS(runSegmentClip);
        introPanel.SetActive(true);
        HideGameplayUI();
        introText.text =
            "RUN SEGMENT\n\n" +
            "• Sprint to the finish line\n" +
            "• Manage your stamina carefully\n\n" +
            "Tap to Continue";
        yield return StartCoroutine(HideObjectiveOnClick());

        segmentSwitcher.LockPlayerInput(false);

        yield return StartCoroutine(ShowRunSegmentCutscene());

        StartRunQuest();
    }

    private IEnumerator ShowRunSegmentCutscene()
    {
        if (runSegmentCamera1 == null)
        {
            yield break;
        }

        isCutscenePlaying = true;
        segmentSwitcher.LockPlayerInput(true);
        PauseCutscene();

        Camera mainCam = Camera.main;
        if (mainCam != null) mainCam.enabled = false;

        HideGameplayUI();
        introPanel.SetActive(false);
        cutsceneSubtitlePanel?.SetActive(true);

        Camera cam2 = runSegmentCamera2 != null ? runSegmentCamera2 : runSegmentCamera1;

        var shots = new (Camera cam, string subtitle, float duration)[]
        {
            (runSegmentCamera1, "Last stretch... time to run!",        2.5f),
            (runSegmentCamera1, "Sprint to the finish line!",          2.5f),
            (runSegmentCamera1, "Watch out for logs on the ground...", 2.5f),
            (cam2,              "And mud puddles will slow you down!", 2.5f),
        };

        try
        {
            Camera activeCam = null;

            for (int i = 0; i < shots.Length; i++)
            {
                var shot = shots[i];

                if (activeCam != shot.cam)
                {
                    if (activeCam != null) activeCam.enabled = false;
                    shot.cam.enabled = true;
                    activeCam = shot.cam;
                }

                if (cutsceneSubtitleText != null)
                    cutsceneSubtitleText.text = shot.subtitle;

                AudioClip clip = (runCutsceneClips != null && i < runCutsceneClips.Length)
                    ? runCutsceneClips[i] : null;

                if (clip != null)
                {
                    PlayTTS(clip);
                    float waitTime = Mathf.Max(clip.length, shot.duration);
                    yield return new WaitForSecondsRealtime(waitTime);
                }
                else
                {
                    yield return new WaitForSecondsRealtime(shot.duration);
                }
            }

            if (activeCam != null) activeCam.enabled = false;
        }
        finally
        {
            isCutscenePlaying = false;
            StopTTS();
            cutsceneSubtitlePanel?.SetActive(false);

            if (runSegmentCamera1 != null) runSegmentCamera1.enabled = false;
            if (runSegmentCamera2 != null) runSegmentCamera2.enabled = false;
            if (mainCam != null) mainCam.enabled = true;

            ResumeCutscene();
            ShowGameplayUI();
            segmentSwitcher.LockPlayerInput(false);

        }
    }

    private void StartRunQuest()
    {
        UpdateQuestUI(
            "Finish the run segment and reach the finish line",
            QuestStatus.InProgress
        );
    }

    // -------------------------------------------------------
    // RACE CORE
    // -------------------------------------------------------

    public void StartRace()
    {
        raceStarted = true;
        raceTime = 0f;
        currentSegment = Segment.Swim;
        RefreshSegmentUI();

        if (rankingSystem != null)
            rankingSystem.RefreshRacers();
    }

    public void RefreshSegmentUI()
    {
        if (segmentText == null) return;

        switch (currentSegment)
        {
            case Segment.Swim:
                segmentText.text = "Segment: SWIM";
                break;
            case Segment.Bike:
                segmentText.text = "Segment: BIKE";
                break;
            case Segment.Run:
                segmentText.text = "Segment: RUN";
                break;
            case Segment.Finished:
                segmentText.text = "FINISHED!";
                break;
        }
    }

    public void FinishRace()
    {
        currentSegment = Segment.Finished;
        raceStarted = false;
        RefreshSegmentUI();

        UpdateQuestUI(
            "Finish the run segment and reach the finish line",
            QuestStatus.Completed
        );

        StartCoroutine(ShowPodiumCutscene());
    }

    // -------------------------------------------------------
    // PODIUM CUTSCENE
    // -------------------------------------------------------

    private IEnumerator ShowPodiumCutscene()
    {

        yield return new WaitForSeconds(1f);

        HideGameplayUI();
        if (questPanel != null)
            questPanel.SetActive(false);

        if (segmentSwitcher != null)
            segmentSwitcher.LockPlayerInput(true);
        else

        Camera mainCam = Camera.main;

        if (podiumCamera != null)
        {
            podiumCamera.enabled = true;
            if (mainCam != null)
                mainCam.enabled = false;
            else
        }
        else
        {
        }

        if (podiumCutsceneObject != null)
            podiumCutsceneObject.SetActive(true);
        else

        if (rankingSystem != null && podiumPositions != null && podiumPositions.Length >= 3)
        {
            var rankings = rankingSystem.GetFinalRankings();

            if (rankings.Count > 0 && podiumPositions[0] != null)
                PositionRacerOnPodium(rankings[0].transform, podiumPositions[0], 1);

            if (rankings.Count > 1 && podiumPositions[1] != null)
                PositionRacerOnPodium(rankings[1].transform, podiumPositions[1], 2);

            if (rankings.Count > 2 && podiumPositions[2] != null)
                PositionRacerOnPodium(rankings[2].transform, podiumPositions[2], 3);
        }
        else
        {
        }

        if (podiumUI != null)
            podiumUI.SetActive(true);
        else

        if (podiumRankText != null && rankingSystem != null)
        {
            int playerRank = rankingSystem.GetPlayerRank();
            podiumRankText.text = $"{GetOrdinal(playerRank)} Place";
        }

        if (podiumTimeText != null)
            podiumTimeText.text = $"Time: {GetFormattedTime()}";

        if (confettiEffect != null)
            confettiEffect.Play();

        if (podiumMusic != null)
            podiumMusic.Play();

        yield return new WaitForSeconds(podiumCutsceneDuration);

        yield return ShowCompletionPanel();

        // --- Cleanup ---
        if (podiumCamera != null)
            podiumCamera.enabled = false;

        if (podiumCutsceneObject != null)
            podiumCutsceneObject.SetActive(false);

        if (podiumUI != null)
            podiumUI.SetActive(false);

        if (confettiEffect != null)
            confettiEffect.Stop();

        if (podiumMusic != null)
            podiumMusic.Stop();

        if (mainCam != null)
            mainCam.enabled = true;

        yield return new WaitForSeconds(0.1f);


        LoadMainMenu();
    }

    private IEnumerator ShowCompletionPanel()
    {
        if (podiumUI != null)
            podiumUI.SetActive(false);

        if (podiumRankText != null)
            podiumRankText.gameObject.SetActive(false);

        if (podiumTimeText != null)
            podiumTimeText.gameObject.SetActive(false);

        introPanel.SetActive(true);

        int playerRank = rankingSystem != null ? rankingSystem.GetPlayerRank() : 1;

        introText.text =
            $"RACE COMPLETE!\n\n" +
            $"You finished in {GetOrdinal(playerRank)} place!\n" +
            $"Final Time: {GetFormattedTime()}\n\n" +
            "Tap to continue";

        yield return StartCoroutine(HideObjectiveOnClick());
    }

    private void PositionRacerOnPodium(Transform racerTransform, Transform podiumPosition, int rank)
    {
        if (racerTransform == null || podiumPosition == null)
            return;


        racerTransform.position = podiumPosition.position;
        racerTransform.rotation = podiumPosition.rotation;

        Transform footModel = racerTransform.Find("PlayeronFoot");
        Transform bikeModel = racerTransform.Find("PlayeronBike");

        if (footModel != null)
        {
            footModel.gameObject.SetActive(true);
            footModel.localPosition = Vector3.zero;
            footModel.localRotation = Quaternion.identity;
        }

        if (bikeModel != null)
            bikeModel.gameObject.SetActive(false);

        Animator[] animators = racerTransform.GetComponentsInChildren<Animator>(true);
        foreach (Animator anim in animators)
        {
            if (anim != null)
                anim.applyRootMotion = false;
        }

        MonoBehaviour[] scripts = racerTransform.GetComponentsInChildren<MonoBehaviour>(true);
        foreach (MonoBehaviour script in scripts)
        {
            if (script == null) continue;
            if (script.GetType() == typeof(Animator)) continue;
            if (script == this) continue;
            script.enabled = false;
        }

        Rigidbody[] rbs = racerTransform.GetComponentsInChildren<Rigidbody>(true);
        foreach (Rigidbody rb in rbs)
        {
            if (rb == null) continue;
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.isKinematic = true;
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }

        CharacterController[] controllers = racerTransform.GetComponentsInChildren<CharacterController>(true);
        foreach (CharacterController cc in controllers)
        {
            if (cc == null) continue;
            cc.enabled = false;
        }

        PlayerControllerTutorial pc = racerTransform.GetComponent<PlayerControllerTutorial>();
        if (pc != null)
            pc.enabled = false;

        racerTransform.rotation = podiumPosition.rotation;

        if (footModel != null)
        {
            PlayerControllerTutorial footController = footModel.GetComponent<PlayerControllerTutorial>();
            if (footController != null)
                footController.ForceSetRotation(podiumPosition.rotation);
        }

        Animator animator = racerTransform.GetComponentInChildren<Animator>();
        if (animator != null)
        {
            animator.applyRootMotion = false;
            animator.Rebind();
            animator.Update(0f);

            if (rank == 1)
            {
                animator.SetBool("Victory", true);
                animator.SetBool("Clap", false);
            }
            else
            {
                animator.SetBool("Clap", true);
                animator.SetBool("Victory", false);
            }
        }

        StartCoroutine(LockPositionAndRotationOnPodium(racerTransform, podiumPosition.position, podiumPosition.rotation));
    }

    private IEnumerator LockPositionAndRotationOnPodium(Transform racer, Vector3 lockPosition, Quaternion lockRotation)
    {
        float elapsed = 0f;

        while (elapsed < podiumCutsceneDuration)
        {
            if (racer != null)
            {
                racer.position = lockPosition;
                racer.rotation = lockRotation;
            }
            elapsed += Time.deltaTime;
            yield return null;
        }
    }

    // -------------------------------------------------------
    // SCENE LOADING
    // -------------------------------------------------------

    private void LoadMainMenu()
    {
        SceneManager.LoadScene(mainMenuSceneName);
    }

    // -------------------------------------------------------
    // QUEST UI
    // -------------------------------------------------------

    private void UpdateQuestUI(string description, QuestStatus status)
    {
        if (questPanel == null) return;

        questPanel.SetActive(true);
        questDescriptionText.text = description;

        switch (status)
        {
            case QuestStatus.InProgress:
                questStatusText.text = "[ IN PROGRESS ]";
                questStatusText.color = Color.yellow;
                break;
            case QuestStatus.Completed:
                questStatusText.text = "[ COMPLETED ]";
                questStatusText.color = Color.green;
                break;
        }
    }

    // -------------------------------------------------------
    // UI HELPERS
    // -------------------------------------------------------

    private void HideGameplayUI()
    {
        if (gameplayUIParent != null)
            gameplayUIParent.SetActive(false);
    }

    private void ShowGameplayUI()
    {
        if (gameplayUIParent != null)
            gameplayUIParent.SetActive(true);
    }

    private void HideAllTutorialImages()
    {
        if (tutorialImage != null)
            tutorialImage.gameObject.SetActive(false);

        if (tutorialImageStamina != null)
            tutorialImageStamina.gameObject.SetActive(false);

        if (swimObstacleImage != null)
            swimObstacleImage.gameObject.SetActive(false);
    }

    private void ShowImage(Sprite sprite)
    {
        if (tutorialImage == null || sprite == null) return;
        tutorialImage.sprite = sprite;
        tutorialImage.gameObject.SetActive(true);
    }

    private void ShowStaminaImage(Sprite sprite)
    {
        if (tutorialImageStamina == null || sprite == null) return;
        tutorialImageStamina.sprite = sprite;
        tutorialImageStamina.gameObject.SetActive(true);
    }

    private void ShowSwimObstacleImage(Sprite sprite)
    {
        if (swimObstacleImage == null || sprite == null) return;
        swimObstacleImage.sprite = sprite;
        swimObstacleImage.gameObject.SetActive(true);
    }

    public void ShowTutorialPanel(string message, string step = "")
    {
        StartCoroutine(ShowPanel(message, step));
    }

    // -------------------------------------------------------
    // UTILITY
    // -------------------------------------------------------

    private string GetOrdinal(int number)
    {
        switch (number)
        {
            case 1: return "1st";
            case 2: return "2nd";
            case 3: return "3rd";
            default: return $"{number}th";
        }
    }

    private string GetFormattedTime()
    {
        int minutes = Mathf.FloorToInt(raceTime / 60);
        int seconds = Mathf.FloorToInt(raceTime % 60);
        return $"{minutes:00}:{seconds:00}";
    }
}
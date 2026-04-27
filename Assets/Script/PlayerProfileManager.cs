using UnityEngine;
using UnityEngine.InputSystem;
using Firebase.Firestore;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using Firebase.Extensions;
using System.Collections;

public class PlayerProfileManager : MonoBehaviour
{
    private FirebaseFirestore db;
    private string playerId;

    [Header("UI")]
    public TextMeshProUGUI nameText;
    public Image tierBadgeImage;
    public TextMeshProUGUI racesText;
    public TextMeshProUGUI winsText;
    public Image avatarImage;

    [Header("Edit Name UI")]
    public GameObject profilePanel;
    public GameObject editNamePanel;
    public TMP_InputField nameInputField;
    public Button saveNameButton;
    public Button cancelNameButton;

    [Header("Avatar Selection UI")]
    public GameObject avatarSelectionPanel;

    [Header("Model Selection UI")]
    public GameObject modelSelectionPanel;
    public GameObject characterPreviewRoot;
    public TextMeshProUGUI currentModelText;
    public CharacterPreviewManager characterPreview;

    [Header("Customize UI")]
    public CustomizePanel customizePanel;
    public Button customizeButton;

    [Header("Profile Buttons")]
    public Button editNameButton;
    public Button changeAvatarButton;
    public Button changeModelButton;

    [Header("Tutorial")]
    public GameObject tutorialOverlay;
    public GameObject tooltipPanel;
    public TextMeshProUGUI tooltipText;
    public RectTransform editNameSpotlight;
    public RectTransform changeAvatarSpotlight;
    public RectTransform changeModelSpotlight;

    private int profileTutorialStep = 0;
    private bool tutorialActive = false;
    private bool waitingForButtonTap = false;
    private bool isEditingName = false;

    private const string TUTORIAL_ACTIVE_KEY   = "TutorialActive";
    private const string MAIN_MENU_STEP_KEY    = "MainMenuTutorialStep";
    private const string HAS_SEEN_TUTORIAL_KEY = "HasSeenTutorial";
    private const int    PROFILE_STEP_INDEX    = 1;

    private void Start()
    {
        editNamePanel.SetActive(false);
        avatarSelectionPanel.SetActive(false);
        modelSelectionPanel?.SetActive(false);

        SetSpotlightRaycast(editNameSpotlight, false);
        SetSpotlightRaycast(changeAvatarSpotlight, false);
        SetSpotlightRaycast(changeModelSpotlight, false);

        ForceHideTutorial();
        SetupNormalButtons();

        if (FirebaseManager.Instance.IsFirebaseReady)
            Init();
        else
            FirebaseManager.Instance.OnFirebaseReady += Init;
    }

    private void Update()
    {
        if (!tutorialActive) return;
        if (waitingForButtonTap) return;
        if (isEditingName) return;

        if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
            OnTapToContinue();
        else if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
            OnTapToContinue();
    }

    private void SetupNormalButtons()
    {
        saveNameButton.onClick.RemoveAllListeners();
        saveNameButton.onClick.AddListener(SaveName);

        cancelNameButton.onClick.RemoveAllListeners();
        cancelNameButton.onClick.AddListener(CancelEdit);

        editNameButton.onClick.RemoveAllListeners();
        editNameButton.onClick.AddListener(OpenEditNamePanel);

        changeAvatarButton.onClick.RemoveAllListeners();
        changeAvatarButton.onClick.AddListener(OpenAvatarSelection);

        changeModelButton.onClick.RemoveAllListeners();
        changeModelButton.onClick.AddListener(OpenModelSelection);

        customizeButton?.onClick.RemoveAllListeners();
        customizeButton?.onClick.AddListener(OpenCustomizePanel);
    }

    private void SetSpotlightRaycast(RectTransform spotlight, bool enabled)
    {
        if (spotlight == null) return;
        foreach (var graphic in spotlight.GetComponentsInChildren<Graphic>(true))
            graphic.raycastTarget = enabled;
    }

    // ── TIER BADGE ─────────────────────────────────────────

    private void SetTierBadge(string tier)
    {
        if (tierBadgeImage == null) return;

        string spritePath = tier switch
        {
            "Beginner"     => "Badges/BadgeBeginner",
            "Intermediate" => "Badges/BadgeIntermediate",
            "Pro"          => "Badges/BadgePro",
            _              => "Badges/BadgeBeginner"
        };

        Sprite badge = Resources.Load<Sprite>(spritePath);
        if (badge != null)
            tierBadgeImage.sprite = badge;
    }

    // ── TUTORIAL ─────────────────────────────────────────

    private void HideAllSpotlights()
    {
        if (editNameSpotlight     != null) editNameSpotlight.gameObject.SetActive(false);
        if (changeAvatarSpotlight != null) changeAvatarSpotlight.gameObject.SetActive(false);
        if (changeModelSpotlight  != null) changeModelSpotlight.gameObject.SetActive(false);
    }

    private void HideTutorialUI()
    {
        tutorialOverlay?.SetActive(false);
        tooltipPanel?.SetActive(false);
        HideAllSpotlights();
    }

    private void ShowTutorialUI()
    {
        tutorialOverlay?.SetActive(true);
        tooltipPanel?.SetActive(true);

        var overlayImage = tutorialOverlay?.GetComponent<Image>();
        if (overlayImage != null) overlayImage.raycastTarget = false;

        var tooltipImage = tooltipPanel?.GetComponent<Image>();
        if (tooltipImage != null) tooltipImage.raycastTarget = false;
    }

    private void ForceHideTutorial()
    {
        tutorialActive      = false;
        waitingForButtonTap = false;
        isEditingName       = false;
        HideTutorialUI();
    }

    private IEnumerator StartProfileTutorial()
    {
        yield return new WaitForSeconds(0.3f);
        profileTutorialStep = 0;
        ShowProfileTutorialStep(0);
    }

    public void OnTapToContinue()
    {
        if (!tutorialActive) return;
        profileTutorialStep++;
        ShowProfileTutorialStep(profileTutorialStep);
    }

    private void ShowProfileTutorialStep(int step)
    {
        HideAllSpotlights();
        ShowTutorialUI();

        switch (step)
        {
            case 0:
                waitingForButtonTap = true;
                editNameSpotlight?.gameObject.SetActive(true);
                tooltipText.text = "Tap 'Edit Player Name' to set your name.";

                editNameButton.onClick.RemoveAllListeners();
                editNameButton.onClick.AddListener(() =>
                {
                    waitingForButtonTap = false;
                    HideTutorialUI();
                    OpenEditNamePanel();

                    saveNameButton.onClick.RemoveAllListeners();
                    saveNameButton.onClick.AddListener(SaveNameAndContinue);

                    cancelNameButton.onClick.RemoveAllListeners();
                    cancelNameButton.onClick.AddListener(() =>
                    {
                        CancelEdit();
                        profileTutorialStep = 1;
                        ShowProfileTutorialStep(1);
                    });
                });
                break;

            case 1:
                waitingForButtonTap = true;
                changeAvatarSpotlight?.gameObject.SetActive(true);
                tooltipText.text = "Tap 'Change Avatar'.";

                changeAvatarButton.onClick.RemoveAllListeners();
                changeAvatarButton.onClick.AddListener(() =>
                {
                    waitingForButtonTap = false;
                    HideTutorialUI();
                    OpenAvatarSelection();
                });
                break;

            case 2:
                waitingForButtonTap = true;
                changeModelSpotlight?.gameObject.SetActive(true);
                tooltipText.text = "Tap 'Change Model'.";

                changeModelButton.onClick.RemoveAllListeners();
                changeModelButton.onClick.AddListener(() =>
                {
                    waitingForButtonTap = false;
                    HideTutorialUI();
                    OpenModelSelection();
                });
                break;

            default:
                FinishProfileTutorial();
                break;
        }
    }

    private IEnumerator ShowNextStepAfterDelay(int step)
    {
        yield return new WaitForSeconds(0.5f);
        profileTutorialStep = step;
        ShowProfileTutorialStep(step);
    }

    private void SaveNameAndContinue()
    {
        SaveName();
        StartCoroutine(ShowNextStepAfterDelay(1));
    }

    private void FinishProfileTutorial()
    {
        PlayerPrefs.SetInt(HAS_SEEN_TUTORIAL_KEY, 1);
        int nextMainMenuStep = PROFILE_STEP_INDEX + 1;
        PlayerPrefs.SetInt(MAIN_MENU_STEP_KEY, nextMainMenuStep);
        PlayerPrefs.Save();
        TutorialSignal.PendingStep = nextMainMenuStep;

        ForceHideTutorial();
        SetupNormalButtons();

        if (db != null && playerId != null)
        {
            db.Collection("players").Document(playerId)
                .UpdateAsync("hasSeenTutorial", true)
                .ContinueWithOnMainThread(_ =>
                {
                    SceneManager.LoadScene("MainMenu");
                });
        }
        else
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    // ── NAME ─────────────────────────────────────────

    private void Init()
    {
        db       = FirebaseManager.Instance.Db;
        playerId = FirebaseManager.Instance.PlayerId;
        LoadOrCreateProfile();
    }

    private void LoadOrCreateProfile()
    {
        db.Collection("players").Document(playerId).GetSnapshotAsync()
            .ContinueWithOnMainThread(task =>
            {
                if (task.IsFaulted || task.IsCanceled) return;

                var snap = task.Result;

                if (snap.Exists)
                {
                    nameText.text  = snap.GetValue<string>("displayName");
                    racesText.text = snap.GetValue<int>("racesCompleted").ToString();
                    winsText.text  = snap.GetValue<int>("wins").ToString();
                    SetAvatar(snap.GetValue<string>("avatar"));
                    SetTierBadge(snap.GetValue<string>("tier"));

                    bool hasSeenTutorialLocal     = PlayerPrefs.GetInt(HAS_SEEN_TUTORIAL_KEY, 0) == 1;
                    bool hasSeenTutorialFirestore  = snap.ContainsField("hasSeenTutorial")
                                                    && snap.GetValue<bool>("hasSeenTutorial");
                    bool hasSeenTutorial = hasSeenTutorialLocal || hasSeenTutorialFirestore;

                    if (!hasSeenTutorial)
                    {
                        bool isTutorialActive = PlayerPrefs.GetInt(TUTORIAL_ACTIVE_KEY, 0) == 1;
                        int  mainMenuStep     = PlayerPrefs.GetInt(MAIN_MENU_STEP_KEY, 0);

                        ;

                        if (isTutorialActive && mainMenuStep == PROFILE_STEP_INDEX)
                        {
                            tutorialActive = true;
                            StartCoroutine(StartProfileTutorial());
                        }
                        else if (!isTutorialActive)
                        {
                            MainMenuTutorial.StartTutorial();
                            TutorialSignal.PendingStep = 0;
                            TutorialSignal.Dispatch(0);

                            tutorialActive = true;
                            StartCoroutine(StartProfileTutorial());
                        }
                    }
                }
                else
                {
                    CreateDefaultProfile();
                }
            });
    }

    private void CreateDefaultProfile()
    {
        var data = new Dictionary<string, object>
        {
            { "displayName",     "Player"        },
            { "tier",            "Beginner"      },
            { "racesCompleted",  0               },
            { "wins",            0               },
            { "avatar",          "DefaultAvatar" },
            { "selectedModel",   "Male"          },
            { "hasSeenTutorial", false           },
        };

        db.Collection("players").Document(playerId).SetAsync(data)
            .ContinueWithOnMainThread(_ =>
            {
                MainMenuTutorial.StartTutorial();
                TutorialSignal.PendingStep = 0;
                TutorialSignal.Dispatch(0);
            });
    }

    public void OpenEditNamePanel()
    {
        isEditingName       = true;
        nameInputField.text = nameText.text;
        editNamePanel.SetActive(true);
        profilePanel?.SetActive(false);
    }

    private void SaveName()
    {
        string newName = nameInputField.text.Trim();
        if (string.IsNullOrEmpty(newName)) return;

        db.Collection("players").Document(playerId)
            .UpdateAsync("displayName", newName);

        nameText.text = newName;
        isEditingName = false;
        editNamePanel.SetActive(false);
        profilePanel?.SetActive(true);
    }

    private void CancelEdit()
    {
        isEditingName = false;
        editNamePanel.SetActive(false);
        profilePanel?.SetActive(true);
    }

    // ── AVATAR ─────────────────────────────────────────

    public void OpenAvatarSelection()
    {
        avatarSelectionPanel.SetActive(true);
        profilePanel?.SetActive(false);
    }

    public void SelectAvatar(string avatarName)
    {
        SetAvatar(avatarName);

        if (db != null && playerId != null)
            db.Collection("players").Document(playerId)
                .UpdateAsync("avatar", avatarName);

        avatarSelectionPanel.SetActive(false);
        profilePanel?.SetActive(true);

        if (tutorialActive)
        {
            profileTutorialStep = 2;
            ShowProfileTutorialStep(2);
        }
    }

    public void CancelAvatarSelection()
    {
        avatarSelectionPanel.SetActive(false);
        profilePanel?.SetActive(true);

        if (tutorialActive)
        {
            profileTutorialStep = 2;
            ShowProfileTutorialStep(2);
        }
    }

    private void SetAvatar(string avatarName)
    {
        Sprite avatarSprite = Resources.Load<Sprite>("Avatars/" + avatarName);
        if (avatarSprite != null) avatarImage.sprite = avatarSprite;
    }

    // ── MODEL ─────────────────────────────────────────

    public void OpenModelSelection()
    {
        characterPreviewRoot?.SetActive(true);
        modelSelectionPanel?.SetActive(true);
        profilePanel?.SetActive(false);

        characterPreview?.InitPreview();
    }

    public void SelectModel(string modelName)
    {
        if (db != null && playerId != null)
            db.Collection("players").Document(playerId)
                .UpdateAsync("selectedModel", modelName);

        PlayerPrefs.SetString("SelectedModel", modelName);
        PlayerPrefs.Save();

        if (currentModelText != null)
            currentModelText.text = modelName;

        characterPreviewRoot?.SetActive(false);
        modelSelectionPanel?.SetActive(false);
        profilePanel?.SetActive(true);

        if (tutorialActive)
            FinishProfileTutorial();
    }

    public void CancelModelSelection()
    {
        characterPreviewRoot?.SetActive(false);
        modelSelectionPanel?.SetActive(false);
        profilePanel?.SetActive(true);

        if (tutorialActive)
            ShowProfileTutorialStep(2);
    }

    public void ConfirmModelSelection()
    {
        if (characterPreview == null) return;
        SelectModel(characterPreview.GetCurrentSelection());
    }

    // ── CUSTOMIZE ─────────────────────────────────────────

    public void OpenCustomizePanel()
    {
        customizePanel?.Open();
        profilePanel?.SetActive(false);
    }

    public void CloseCustomizePanel()
    {
        customizePanel?.Close();
        profilePanel?.SetActive(true);
    }

    // ── BACK ─────────────────────────────────────────

    public void BackToMainMenu() => SceneManager.LoadScene("MainMenu");
}
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class InRaceSettingsPanel : MonoBehaviour
{
    [Header("Panel Root")]
    public GameObject panelRoot;

    [Header("Options Sub-Panel")]
    public GameObject optionsPanel;
    public Button optionsButton;
    public Button optionsBackButton;

    [Header("Confirmation Panel")]
    public GameObject confirmationPanel;
    public Button confirmYesButton;
    public Button confirmNoButton;

    [Header("Audio")]
    public AudioMixer audioMixer;
    public Slider masterVolumeSlider;

    [Header("Crowd")]
    public CrowdLineSpawner crowdLineSpawner;
    public Toggle crowdToggle;

    [Header("Buttons")]
    public Button resumeButton;
    public Button mainMenuButton;

    private const string CrowdPrefKey = "CrowdEnabled";

    private void Start()
    {
        panelRoot.SetActive(false);
        optionsPanel.SetActive(false);
        confirmationPanel.SetActive(false);

        resumeButton.onClick.AddListener(Resume);
        optionsButton.onClick.AddListener(ShowOptions);
        optionsBackButton.onClick.AddListener(HideOptions);
        mainMenuButton.onClick.AddListener(ShowConfirmation);

        confirmYesButton.onClick.AddListener(GoToMainMenu);
        confirmNoButton.onClick.AddListener(HideConfirmation);

        masterVolumeSlider.onValueChanged.AddListener(SetMasterVolume);
        crowdToggle.onValueChanged.AddListener(SetCrowdEnabled);

        LoadSettings();
    }

    private void OnDestroy()
    {
        masterVolumeSlider.onValueChanged.RemoveListener(SetMasterVolume);
        crowdToggle.onValueChanged.RemoveListener(SetCrowdEnabled);
    }

    private void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame) // ← Replace Input.GetKeyDown
        {
            if (confirmationPanel.activeSelf)
                HideConfirmation();
            else if (optionsPanel.activeSelf)
                HideOptions();
            else
                Toggle();
        }
    }

    // ── Panel Control ────────────────────────────────────────────

    public void Toggle() =>
        panelRoot.SetActive(!panelRoot.activeSelf);

    public void Resume()
    {
        HideConfirmation();
        HideOptions();
        panelRoot.SetActive(false);
    }

    // ── Options Sub-Panel ────────────────────────────────────────

    private void ShowOptions()
    {
        optionsPanel.SetActive(true);
        resumeButton.gameObject.SetActive(false);
        optionsButton.gameObject.SetActive(false);
        mainMenuButton.gameObject.SetActive(false);
    }

    private void HideOptions()
    {
        optionsPanel.SetActive(false);
        resumeButton.gameObject.SetActive(true);
        optionsButton.gameObject.SetActive(true);
        mainMenuButton.gameObject.SetActive(true);
    }

    // ── Confirmation Panel ───────────────────────────────────────

    private void ShowConfirmation() =>
        confirmationPanel.SetActive(true);

    private void HideConfirmation() =>
        confirmationPanel.SetActive(false);

    // ── Volume ───────────────────────────────────────────────────

    private void ApplyMasterVolume(float value)
    {
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(Mathf.Max(value, 0.0001f)) * 20f);
    }

    public void SetMasterVolume(float value)
    {
        ApplyMasterVolume(value);
        PlayerPrefs.SetFloat("MasterVolume", value);
    }

    // ── Crowd ────────────────────────────────────────────────────

    private void SetCrowdEnabled(bool value)
    {
        if (crowdLineSpawner != null)
            crowdLineSpawner.gameObject.SetActive(value);

        PlayerPrefs.SetInt(CrowdPrefKey, value ? 1 : 0);
    }

    // ── Load / Save ──────────────────────────────────────────────

    private void LoadSettings()
    {
        // Volume — set slider silently then apply mixer directly
        float masterVolume = PlayerPrefs.GetFloat("MasterVolume", 1f);
        masterVolumeSlider.SetValueWithoutNotify(masterVolume);
        ApplyMasterVolume(masterVolume);

        // Crowd
        bool crowdEnabled = PlayerPrefs.GetInt(CrowdPrefKey, 1) == 1;
        crowdToggle.isOn = crowdEnabled;
        if (crowdLineSpawner != null)
            crowdLineSpawner.gameObject.SetActive(crowdEnabled);
    }
    // ── Main Menu ────────────────────────────────────────────────

    private void GoToMainMenu()
    {
        PlayerPrefs.Save();
        FishNet.InstanceFinder.ClientManager?.StopConnection();
        SceneManager.LoadScene("MainMenu");
    }
}
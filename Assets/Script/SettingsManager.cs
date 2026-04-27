using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class SettingsManager : MonoBehaviour
{
    [Header("Audio")]
    public AudioMixer audioMixer;
    public Slider masterVolumeSlider;
    public Slider musicVolumeSlider;
    public Slider sfxVolumeSlider;

    [Header("Graphics")]
    public TMP_Dropdown resolutionDropdown;

    private readonly (int w, int h, string label)[] resolutions =
    {
        (800,  480,  "800x480  (Low)"),
        (1280, 720,  "1280x720 (Medium)"),
        (1920, 1080, "1920x1080 (High)"),
    };

    void Start()
    {
        masterVolumeSlider.onValueChanged.AddListener(SetMasterVolume);
        musicVolumeSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxVolumeSlider.onValueChanged.AddListener(SetSFXVolume);
        resolutionDropdown.onValueChanged.AddListener(SetResolutionScale);

        PopulateResolutionDropdown();
        LoadSettings();
    }

    void OnDestroy()
    {
        masterVolumeSlider.onValueChanged.RemoveListener(SetMasterVolume);
        musicVolumeSlider.onValueChanged.RemoveListener(SetMusicVolume);
        sfxVolumeSlider.onValueChanged.RemoveListener(SetSFXVolume);
        resolutionDropdown.onValueChanged.RemoveListener(SetResolutionScale);
    }

    // ── RESOLUTION ──────────────────────────────────────

    void PopulateResolutionDropdown()
    {
        resolutionDropdown.ClearOptions();
        var labels = new System.Collections.Generic.List<string>();
        foreach (var r in resolutions) labels.Add(r.label);
        resolutionDropdown.AddOptions(labels);
    }

    public void SetResolutionScale(int index)
    {
        var r = resolutions[index];
        Screen.SetResolution(r.w, r.h, Screen.fullScreenMode);
        PlayerPrefs.SetInt("ResolutionScale", index);
    }

    // ── VOLUME ──────────────────────────────────────────

    public void SetMasterVolume(float value)
    {
        float db = Mathf.Log10(Mathf.Max(value, 0.0001f)) * 20f;
        audioMixer.SetFloat("MasterVolume", db);
        PlayerPrefs.SetFloat("MasterVolume", value);
    }

    public void SetMusicVolume(float value)
    {
        float db = Mathf.Log10(Mathf.Max(value, 0.0001f)) * 20f;
        audioMixer.SetFloat("MusicVolume", db);
        PlayerPrefs.SetFloat("MusicVolume", value);
    }

    public void SetSFXVolume(float value)
    {
        float db = Mathf.Log10(Mathf.Max(value, 0.0001f)) * 20f;
        audioMixer.SetFloat("SFXVolume", db);
        PlayerPrefs.SetFloat("SFXVolume", value);
    }

    // ── SAVE / LOAD ─────────────────────────────────────

    void LoadSettings()
    {
        masterVolumeSlider.value = PlayerPrefs.GetFloat("MasterVolume", 1f);
        musicVolumeSlider.value  = PlayerPrefs.GetFloat("MusicVolume",  1f);
        sfxVolumeSlider.value    = PlayerPrefs.GetFloat("SFXVolume",    1f);

        int resIndex = PlayerPrefs.GetInt("ResolutionScale", 2);
        resolutionDropdown.value = resIndex;
        SetResolutionScale(resIndex);
    }

    public void SaveSettings()
    {
        PlayerPrefs.Save();
    }

    public void BackToMainMenu()
    {
        SaveSettings();
        SceneManager.LoadScene("MainMenu");
    }
}
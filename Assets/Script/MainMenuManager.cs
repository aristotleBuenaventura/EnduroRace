using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [Header("Exit Confirmation")]
    public GameObject exitConfirmPanel;

    [Header("Credits")]
    public GameObject creditsPanel;

    void Awake()
    {
        Screen.SetResolution(1280, 720, FullScreenMode.FullScreenWindow);
    }
    void Start()
    {
        if (exitConfirmPanel != null)
            exitConfirmPanel.SetActive(false);

        if (creditsPanel != null)
            creditsPanel.SetActive(false);
    }

    public void Tutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void StartRace()
    {
        SceneManager.LoadScene("PreRaceOverview");
    }

    public void OpenProfile()
    {
        SceneManager.LoadScene("Profile");
    }

    public void OpenShop()
    {
        SceneManager.LoadScene("Shop");
    }

    public void OpenLeaderboardsAchievements()
    {
        SceneManager.LoadScene("LeaderboardsAchievementsScene");
    }

    public void OpenSettings()
    {
        SceneManager.LoadScene("Settings");
    }

    public void ShowExitConfirm()
    {
        if (exitConfirmPanel != null)
            exitConfirmPanel.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    public void CancelExit()
    {
        if (exitConfirmPanel != null)
            exitConfirmPanel.SetActive(false);
    }

    // --- Credits ---
    public void ShowCredits()
    {
        if (creditsPanel != null)
            creditsPanel.SetActive(true);
    }

    public void CloseCredits()
    {
        if (creditsPanel != null)
            creditsPanel.SetActive(false);
    }
}
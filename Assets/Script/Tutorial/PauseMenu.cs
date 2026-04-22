using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem; // For the new input system

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI; // Assign the Canvas or Panel
    public GameObject gameplayUI; // Drag GameplayUI here
    private bool isPaused = false;
    public GameObject settingsUI;

    void Start()
    {
        // Always hidden at start
        pauseMenuUI.SetActive(false);
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void Update()
    {
        // Toggle pause when pressing ESC (new Input System)
        if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (isPaused)
                Resume();
            else
                Pause();
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Hide menu whenever a new scene loads
        pauseMenuUI.SetActive(false);
        gameplayUI.SetActive(true);
        isPaused = false;
        Time.timeScale = 1f; // Only for single-player
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        gameplayUI.SetActive(true);
        isPaused = false;
        Time.timeScale = 1f; // Only for single-player
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        gameplayUI.SetActive(false);
        isPaused = true;
        Time.timeScale = 0f;
    }

    public void LoadMainMenu()
    {
        // Reset time scale and load scene
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu"); // Make sure your main menu scene is in Build Settings
    }

    public void ExitGame()
    {
                Application.Quit();
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false; // Stop play mode in editor
        #endif
    }

    public void OpenSettings()
    {
        pauseMenuUI.SetActive(false);
        settingsUI.SetActive(true);
    }

    public void CloseSettings()
    {
        settingsUI.SetActive(false);
        pauseMenuUI.SetActive(true);
    }
}

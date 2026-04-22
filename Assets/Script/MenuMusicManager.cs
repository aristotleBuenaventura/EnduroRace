    using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class MenuMusicManager : MonoBehaviour
{
    public AudioSource menuMusic;

    public List<string> allowedScenes = new List<string>()
    {
        "MainMenu",
        "Settings",
        "Shop",
        "Profile",
        "Lobby",
        "LeaderboardsAchievementsScene"
    };

    static MenuMusicManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;

            if (menuMusic != null)
            {
                menuMusic.loop = true;
                menuMusic.Play();
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (menuMusic == null) return;

        if (allowedScenes.Contains(scene.name))
        {
            if (!menuMusic.isPlaying)
                menuMusic.Play(); // Use Play() not UnPause() — safer
        }
        else
        {
            menuMusic.Pause();
        }
    }
}
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Firebase.Firestore;
using Firebase.Extensions;
using System.Collections;

public class PreRaceOverview : MonoBehaviour
{
    [Header("Map Art")]
    public Image mapArtImage;

    [Header("Map Info")]
    public TextMeshProUGUI mapNameText;

    [Header("Player Info")]
    public TextMeshProUGUI tierText;
    public TextMeshProUGUI qualificationText;
    public TextMeshProUGUI pointsProgressText;
    public Slider pointsProgressBar;

    [Header("Button")]
    public Button startRaceButton;

    [Header("Map Arts")]
    public Sprite sunnyShoresArt;
    public Sprite jungleRapidsArt;
    public Sprite stormbreakerCoveArt;

    private string playerTier = "Beginner";
    private int playerPoints = 0;

    private void Start()
    {
        if (startRaceButton != null)
            startRaceButton.onClick.AddListener(OnStartRace);

        StartCoroutine(LoadPlayerData());
    }

    private IEnumerator LoadPlayerData()
    {
        yield return new WaitUntil(() =>
            FirebaseManager.Instance != null &&
            FirebaseManager.Instance.IsFirebaseReady);

        string playerId = FirebaseManager.Instance.PlayerId;

        bool done = false;
        FirebaseFirestore.DefaultInstance
            .Collection("players").Document(playerId)
            .GetSnapshotAsync(Source.Server)
            .ContinueWithOnMainThread(task =>
            {
                if (!task.IsFaulted && task.Result.Exists)
                {
                    playerTier = task.Result.ContainsField("tier")
                        ? task.Result.GetValue<string>("tier") : "Beginner";
                    playerPoints = task.Result.ContainsField("points")
                        ? (int)task.Result.GetValue<long>("points") : 0;
                }
                done = true;
            });

        yield return new WaitUntil(() => done);

        PopulateUI();
    }

    private void PopulateUI()
    {
        switch (playerTier)
        {
            case "Beginner":
                SetMapInfo(
                    sunnyShoresArt,
                    "Sunny Shores",
                    "Beat the target time of 6:00 to advance",
                    0, 0
                );
                break;

            case "Intermediate":
                SetMapInfo(
                    jungleRapidsArt,
                    "Jungle Rapids",
                    "Finish within 15% of 1st place time to advance to Pro",
                    0, 0
                );
                break;

            case "Pro":
                SetMapInfo(
                    stormbreakerCoveArt,
                    "Stormbreaker Cove",
                    "Finish 1st to complete Pro Tier",
                    1, 1
                );
                break;
        }

        if (tierText != null)
            tierText.text = $"Tier: {playerTier}";
    }

    private void SetMapInfo(
        Sprite art, string mapName,
        string qualification,
        int currentPoints, int maxPoints)
    {
        if (mapArtImage != null && art != null)
            mapArtImage.sprite = art;

        if (mapNameText != null)
            mapNameText.text = mapName;

        if (qualificationText != null)
            qualificationText.text = qualification;

        if (pointsProgressText != null)
            pointsProgressText.text = $"Points: {currentPoints} / {maxPoints}";

        if (pointsProgressBar != null)
            pointsProgressBar.gameObject.SetActive(maxPoints > 0);

        if (pointsProgressText != null)
            pointsProgressText.gameObject.SetActive(maxPoints > 0);

        if (pointsProgressBar != null)
        {
            pointsProgressBar.minValue = 0;
            pointsProgressBar.maxValue = maxPoints;
            pointsProgressBar.value = Mathf.Min(currentPoints, maxPoints);
            
        }
    }

    private void OnStartRace()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Lobby");
    }
}
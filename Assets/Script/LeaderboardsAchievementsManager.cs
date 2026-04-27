using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Firebase.Firestore;
using Firebase.Extensions;
using System.Collections;
using System.Collections.Generic;

public class LeaderboardsAchievementsManager : MonoBehaviour
{
    [Header("Tabs")]
    public Button leaderboardsTabButton;
    public Button achievementsTabButton;
    public GameObject leaderboardsPanel;
    public GameObject achievementsPanel;

    [Header("Leaderboard UI")]
    public TMP_Dropdown tierDropdown;
    public Transform leaderboardContent;
    public GameObject leaderboardEntryPrefab;
    public TextMeshProUGUI personalBestText;
    public TextMeshProUGUI personalRankText;

    [Header("Achievements UI")]
    public Transform achievementsContent;
    public GameObject achievementCardPrefab;

    [Header("Leaderboard UI")]
    public Sprite goldEntrySprite;
    public Sprite silverEntrySprite;
    public Sprite bronzeEntrySprite;
    public Sprite defaultEntrySprite;

    [Header("Back Button")]
    public Button backButton;

    private FirebaseFirestore db;
    private string localPlayerId;
    private string localPlayerTier;
    private List<string> earnedAchievements = new List<string>();

    private readonly string[] tiers = { "All", "Beginner", "Intermediate", "Pro" };

    private void Start()
    {
        db = FirebaseFirestore.DefaultInstance;

        leaderboardsTabButton.onClick.AddListener(() => ShowTab(true));
        achievementsTabButton.onClick.AddListener(() => ShowTab(false));

        tierDropdown.ClearOptions();
        tierDropdown.AddOptions(new List<string>(tiers));
        tierDropdown.onValueChanged.AddListener(_ => LoadLeaderboard());

        if (backButton != null)
            backButton.onClick.AddListener(() =>
                UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu"));

        ShowTab(true);
        StartCoroutine(InitializeData());
    }

    private IEnumerator InitializeData()
    {
        yield return new WaitUntil(() =>
            FirebaseManager.Instance != null &&
            FirebaseManager.Instance.IsFirebaseReady);

        localPlayerId = FirebaseManager.Instance.PlayerId;

        bool done = false;
        db.Collection("players").Document(localPlayerId)
            .GetSnapshotAsync()
            .ContinueWithOnMainThread(task =>
            {
                if (!task.IsFaulted && task.Result.Exists)
                {
                    localPlayerTier = task.Result.ContainsField("tier")
                        ? task.Result.GetValue<string>("tier") : "Beginner";

                    if (task.Result.ContainsField("achievements"))
                        earnedAchievements = new List<string>(
                            task.Result.GetValue<List<string>>("achievements"));
                }
                done = true;
            });

        yield return new WaitUntil(() => done);

        int tierIndex = System.Array.IndexOf(tiers, localPlayerTier);
        if (tierIndex >= 0)
            tierDropdown.value = tierIndex;
        else
            tierDropdown.value = 0;

        LoadLeaderboard();
        LoadAchievements();
    }

    private void ShowTab(bool showLeaderboard)
    {
        leaderboardsPanel.SetActive(showLeaderboard);
        achievementsPanel.SetActive(!showLeaderboard);
    }

    // ==================== LEADERBOARD ====================

    private string GetTimeField(string tier)
    {
        switch (tier)
        {
            case "Beginner": return "fastestTimeBeginner";
            case "Intermediate": return "fastestTimeIntermediate";
            case "Pro": return "fastestTimePro";
            default: return "fastestTimeBeginner";
        }
    }

    private string FormatTime(float seconds)
    {
        if (seconds <= 0) return "No Record";
        int m = Mathf.FloorToInt(seconds / 60f);
        int s = Mathf.FloorToInt(seconds % 60f);
        return string.Format("{0:00}:{1:00}", m, s);
    }

    private void LoadLeaderboard()
    {
        foreach (Transform child in leaderboardContent)
            Destroy(child.gameObject);

        string selectedTier = tiers[tierDropdown.value];

        Query query = db.Collection("players");
        string timeField = GetTimeField(selectedTier);

        if (selectedTier != "All")
        {
            query = query.WhereEqualTo("tier", selectedTier)
                         .OrderBy(timeField)
                         .Limit(50);
        }
        else
        {
            // For "All", just order by beginner time as fallback
            query = query.OrderBy("fastestTimeBeginner").Limit(50);
        }

        query.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted)
            {
                return;
            }

            int rank = 1;
            int localRank = 0;
            float localFastestTime = 0f;

            foreach (var doc in task.Result.Documents)
            {
                string name = doc.ContainsField("displayName")
                    ? doc.GetValue<string>("displayName") : "Player";
                float fastestTime = doc.ContainsField(timeField)
                    ? (float)doc.GetValue<double>(timeField) : 0f;
                bool isLocal = doc.Id == localPlayerId;

                if (isLocal)
                {
                    localRank = rank;
                    localFastestTime = fastestTime;
                }

                CreateLeaderboardEntry(rank, name, fastestTime, isLocal);
                rank++;
            }

            if (personalRankText != null)
                personalRankText.text = localRank > 0
                    ? $"Your Rank: #{localRank}"
                    : "Your Rank: Unranked";

            if (personalBestText != null)
                personalBestText.text = $"Your Best: {FormatTime(localFastestTime)}";
        });
    }

    private void CreateLeaderboardEntry(int rank, string name,
    float fastestTime, bool isLocalPlayer)
    {
        GameObject entry = Instantiate(leaderboardEntryPrefab, leaderboardContent);

        // ── Background sprite based on rank ──────────────────────────────────
        var bg = entry.GetComponent<Image>();
        if (bg != null)
        {
            switch (rank)
            {
                case 1:
                    bg.sprite = goldEntrySprite;
                    bg.color = isLocalPlayer ? new Color(1f, 1f, 0.6f, 1f) : Color.white;
                    break;
                case 2:
                    bg.sprite = silverEntrySprite;
                    bg.color = Color.white;
                    break;
                case 3:
                    bg.sprite = bronzeEntrySprite;
                    bg.color = Color.white;
                    break;
                default:
                    bg.sprite = defaultEntrySprite;
                    bg.color = isLocalPlayer ? new Color(1f, 1f, 0f, 0.25f) : Color.white;
                    break;
            }
        }

        // ── Rank text ─────────────────────────────────────────────────────────
        var rankText = entry.transform.Find("RankText")?.GetComponent<TextMeshProUGUI>();
        if (rankText != null)
        {
            rankText.text = $"#{rank}";
            rankText.color = rank == 1 ? new Color(1f, 0.85f, 0.3f)   // gold
                        : rank == 2 ? new Color(0.85f, 0.85f, 0.9f) // silver
                        : rank == 3 ? new Color(0.8f, 0.55f, 0.35f) // bronze
                        :             Color.white;
        }

        // ── Name text ─────────────────────────────────────────────────────────
        var nameText = entry.transform.Find("NameText")?.GetComponent<TextMeshProUGUI>();
        if (nameText != null)
        {
            nameText.text = isLocalPlayer ? $"{name} (You)" : name;
            nameText.color = isLocalPlayer ? Color.yellow : Color.white;
        }

        // ── Time text ─────────────────────────────────────────────────────────
        var timeText = entry.transform.Find("TimeText")?.GetComponent<TextMeshProUGUI>();
        if (timeText != null)
        {
            timeText.text = FormatTime(fastestTime);
            timeText.color = Color.white;
        }
    }

    // ==================== ACHIEVEMENTS ====================

    private void LoadAchievements()
    {
        foreach (Transform child in achievementsContent)
            Destroy(child.gameObject);

        foreach (var achievement in AchievementData.All)
        {
            bool earned = earnedAchievements.Contains(achievement.id.ToString());
            CreateAchievementCard(achievement, earned);
        }
    }

    private void CreateAchievementCard(
        AchievementData.Achievement achievement, bool earned)
    {
        GameObject card = Instantiate(achievementCardPrefab, achievementsContent);

        var badgeIcon = card.transform.Find("BadgeIcon")?.GetComponent<Image>();
        if (badgeIcon != null)
        {
            Sprite sprite = Resources.Load<Sprite>("UI/" + achievement.badgeIcon);
            if (sprite != null) badgeIcon.sprite = sprite;
            badgeIcon.color = Color.white;
        }

        var titleText = card.transform.Find("TitleText")?.GetComponent<TextMeshProUGUI>();
        if (titleText != null)
        {
            titleText.text = achievement.title;
            titleText.color = Color.white;
        }

        var descText = card.transform.Find("DescriptionText")
            ?.GetComponent<TextMeshProUGUI>();
        if (descText != null)
        {
            descText.text = achievement.description;
            descText.color = Color.white;
        }

        var statusText = card.transform.Find("StatusText")
            ?.GetComponent<TextMeshProUGUI>();
        if (statusText != null)
        {
            statusText.text = earned ? "EARNED" : "LOCKED";
            statusText.color = earned ? Color.green : Color.red;
        }

        var bg = card.GetComponent<Image>();
        if (bg != null)
            bg.color = earned ? Color.white : Color.black;
    }
}
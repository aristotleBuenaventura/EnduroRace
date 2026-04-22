// PodiumManager.cs
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Firebase.Firestore;
using Firebase.Extensions;
using FishNet.Object;
using System.Collections;
using System.Collections.Generic;

public class PodiumManager : NetworkBehaviour
{
    [Header("Leaderboard Screen")]
    public GameObject leaderboardPanel;
    public Transform leaderboardContainer;
    public GameObject leaderboardEntryPrefab;
    public float leaderboardDisplayDuration = 5f;
    public float entryRevealDelay = 0.15f;

    [Header("Podium UI")]
    public GameObject podiumPanel;
    public TextMeshProUGUI placementText;
    public TextMeshProUGUI raceTimeText;
    public TextMeshProUGUI pointsEarnedText;
    public TextMeshProUGUI tierStatusText;
    public TextMeshProUGUI totalPointsText;
    public Button continueButton;

    [Header("Podium Positions")]
    public Transform[] podiumPositions;
    public GameObject podiumCutsceneObject;

    [Header("Effects")]
    public ParticleSystem confettiEffect;
    public AudioSource podiumMusic;

    [Header("Cameras")]
    public Camera podiumCamera;
    public Camera mainGameCamera;

    [Header("Colors")]
    public Color localPlayerColor = Color.yellow;
    public Color otherPlayerColor = Color.white;
    public Color aiColor = Color.gray;
    public Color topThreeColor = new Color(1f, 0.85f, 0f);

    [Header("References")]
    public MultiplayerRaceRanking rankingSystem;
    public RaceManager raceManager;

    [Header("Coins")]
    public TextMeshProUGUI coinsEarnedText;

    [Header("Progress Bar")]
    public Slider pointsProgressBar;

    [Header("Trophy")]
    public GameObject trophyPrefab;

    private FirebaseFirestore db;
    private bool podiumShown = false;
    private float finalRaceTime = 0f;
    private int finalPlacement = 0;
    private string currentTierAtRaceEnd = "Beginner";

    private const int POINTS_1ST   = 100;
    private const int POINTS_2ND   = 50;
    private const int POINTS_3RD   = 50;
    private const int POINTS_OTHER = 35;

    private const int INTERMEDIATE_TO_PRO_THRESHOLD = 100;
    private float firstPlaceTime = 0f;
    private const float BEGINNER_TARGET_TIME = 600f;

    private readonly List<NetworkPlayer> frozenPlayers = new List<NetworkPlayer>();

    private void Start()
    {
        db = FirebaseFirestore.DefaultInstance;

        if (leaderboardPanel != null) leaderboardPanel.SetActive(false);
        if (podiumPanel      != null) podiumPanel.SetActive(false);
        if (continueButton   != null) continueButton.onClick.AddListener(OnContinuePressed);
    }

    // ─────────────────────────────────────────────
    // PUBLIC SERVER ENTRY POINTS
    // ─────────────────────────────────────────────

    // Called by RaceManager on the server once all players have finished.
    // Packs rankings into RPC-safe parallel arrays and broadcasts to all clients.
    public void ShowPodium(int placement, float raceTime)
    {
        if (!IsServerStarted) return;
        if (podiumShown) return;
        podiumShown = true;

        var rankings = rankingSystem != null
            ? rankingSystem.GetFinalRankings()
            : new List<MultiplayerRaceRanking.RankSnapshot>();

        float p1Time = rankings.Count > 0 ? rankings[0].finishTime : 0f;

        int      count    = rankings.Count;
        int[]    ownerIds = new int[count];
        string[] names    = new string[count];
        int[]    ranks    = new int[count];
        float[]  times    = new float[count];
        bool[]   isAI     = new bool[count];

        for (int i = 0; i < count; i++)
        {
            ownerIds[i] = rankings[i].ownerId;
            names[i]    = rankings[i].name;
            ranks[i]    = rankings[i].rank;
            times[i]    = rankings[i].finishTime;
            isAI[i]     = rankings[i].racerType == MultiplayerRaceRanking.RacerType.AI;
        }

        RpcShowPodiumAllClients(ownerIds, names, ranks, times, isAI, p1Time);
    }

    // Called by RaceManager on the server when a specific player finishes.
    // Sends that player their personal placement + time via TargetRpc.
    [Server]
    public void NotifyPlayerFinished(NetworkPlayer player, int placement, float raceTime)
    {
        RpcNotifyPlayerResult(player.Owner, placement, raceTime);
    }

    // ─────────────────────────────────────────────
    // TARGET RPC — personal result to one client
    // ─────────────────────────────────────────────

    [TargetRpc]
    private void RpcNotifyPlayerResult(FishNet.Connection.NetworkConnection conn,
                                        int placement, float raceTime)
    {
        finalPlacement = placement;
        finalRaceTime  = raceTime;
        Debug.Log($"[PodiumManager] My result received — placement: {placement}, time: {FormatTime(raceTime)}");
    }

    // ─────────────────────────────────────────────
    // OBSERVERS RPC — broadcast to all clients
    // ─────────────────────────────────────────────

    [ObserversRpc]
    private void RpcShowPodiumAllClients(int[] ownerIds, string[] names,
                                          int[] ranks,    float[] times,
                                          bool[] isAI,    float p1Time)
    {
        firstPlaceTime = p1Time;

        // Rebuild server-authoritative snapshot locally
        var serverRankings = new List<MultiplayerRaceRanking.RankSnapshot>();
        for (int i = 0; i < ownerIds.Length; i++)
        {
            serverRankings.Add(new MultiplayerRaceRanking.RankSnapshot
            {
                ownerId    = ownerIds[i],
                name       = names[i],
                rank       = ranks[i],
                finishTime = times[i],
                racerType  = isAI[i]
                    ? MultiplayerRaceRanking.RacerType.AI
                    : MultiplayerRaceRanking.RacerType.NetworkPlayer,
            });
        }

        StartCoroutine(FullPodiumSequence(serverRankings));
    }

    // ─────────────────────────────────────────────
    // FULL SEQUENCE — runs on ALL clients
    // ─────────────────────────────────────────────

    private IEnumerator FullPodiumSequence(
        List<MultiplayerRaceRanking.RankSnapshot> serverRankings)
    {
        // Resolve local player once
        NetworkPlayer localPlayer = null;
        NetworkPlayer[] allNetPlayers = FindObjectsByType<NetworkPlayer>(FindObjectsSortMode.None);
        foreach (var np in allNetPlayers)
            if (np.IsOwner) { localPlayer = np; break; }

        int localOwnerId = localPlayer != null ? localPlayer.OwnerId : -1;

        // Derive placement/time from server list in case TargetRpc hasn't arrived yet
        if (finalPlacement == 0 && localOwnerId >= 0)
        {
            foreach (var snap in serverRankings)
            {
                if (snap.ownerId == localOwnerId)
                {
                    finalPlacement = snap.rank;
                    if (finalRaceTime <= 0f)
                        finalRaceTime = snap.finishTime;
                    break;
                }
            }
        }

        // Stop local player movement
        if (localPlayer != null)
            localPlayer.ServerSetCanMove(false);

        // Hide live ranking HUD so it doesn't bleed through
        if (rankingSystem != null && rankingSystem.rankingPanel != null)
            rankingSystem.rankingPanel.SetActive(false);

        yield return StartCoroutine(ShowLeaderboard(serverRankings, localOwnerId));

        // Switch cameras
        Camera mainCam = mainGameCamera != null ? mainGameCamera : Camera.main;
        if (mainCam != null) mainCam.enabled = false;
        if (podiumCamera         != null) podiumCamera.enabled = true;
        if (podiumCutsceneObject != null) podiumCutsceneObject.SetActive(true);

        // Place top 3 on podium using server-authoritative order
        if (podiumPositions != null && podiumPositions.Length >= 3)
        {
            NetworkedAIOpponent[] allAI = FindObjectsByType<NetworkedAIOpponent>(FindObjectsSortMode.None);

            int podiumSlot = 0;
            foreach (var snap in serverRankings)
            {
                if (podiumSlot >= 3) break;
                if (podiumPositions[podiumSlot] == null) { podiumSlot++; continue; }

                if (snap.racerType == MultiplayerRaceRanking.RacerType.AI)
                {
                    NetworkedAIOpponent matched = null;
                    foreach (var ai in allAI)
                    {
                        if (ai.opponentName == snap.name || ai.gameObject.name == snap.name)
                        { matched = ai; break; }
                    }

                    if (matched != null)
                        PositionAIOnPodium(matched, podiumPositions[podiumSlot], podiumSlot + 1);
                    else
                        Debug.LogWarning($"[PodiumManager] AI '{snap.name}' not found on this client");
                }
                else
                {
                    foreach (var np in allNetPlayers)
                    {
                        if (np.OwnerId == snap.ownerId)
                        {
                            PositionPlayerOnPodium(np, podiumPositions[podiumSlot], podiumSlot + 1);
                            break;
                        }
                    }
                }

                podiumSlot++;
            }
        }

        if (confettiEffect != null && finalPlacement <= 3) confettiEffect.Play();
        if (podiumMusic    != null) podiumMusic.Play();

        yield return new WaitForSeconds(2f);

        int pointsEarned = GetPointsForPlacement(finalPlacement);
        yield return StartCoroutine(FetchAndShowResults(finalPlacement, finalRaceTime, pointsEarned));
    }

    // ─────────────────────────────────────────────
    // PLAYER PODIUM POSITIONING
    // ─────────────────────────────────────────────

    private void PositionPlayerOnPodium(NetworkPlayer networkPlayer, Transform podiumPoint, int rank)
    {
        Transform root = networkPlayer.transform;

        networkPlayer.enabled = false;
        frozenPlayers.Add(networkPlayer);

        PlayerController pc = root.GetComponentInChildren<PlayerController>(true);
        if (pc != null) pc.enabled = false;

        CyclingController cc = root.GetComponentInChildren<CyclingController>(true);
        if (cc != null) cc.enabled = false;

        foreach (var controller in root.GetComponentsInChildren<CharacterController>(true))
            if (controller != null) controller.enabled = false;

        foreach (var rb in root.GetComponentsInChildren<Rigidbody>(true))
        {
            if (rb == null) continue;
            rb.linearVelocity  = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.isKinematic     = true;
        }

        var netTransform = root.GetComponent<FishNet.Component.Transforming.NetworkTransform>();
        if (netTransform != null) netTransform.enabled = false;

        root.position = podiumPoint.position;
        root.rotation = podiumPoint.rotation;

        string[] footNames = { "PlayeronFoot", "PlayereronFoot" };
        string[] bikeNames = { "PlayeronBike", "BikeModel", "CycleModel", "VehicleModel" };

        foreach (string n in bikeNames)
        {
            Transform t = root.Find(n);
            if (t != null) t.gameObject.SetActive(false);
        }

        Transform footModel = null;
        foreach (string n in footNames)
        {
            Transform t = root.Find(n);
            if (t != null)
            {
                t.gameObject.SetActive(true);
                t.localPosition = Vector3.zero;
                t.localRotation = Quaternion.identity;
                footModel = t;
                break;
            }
        }

        Animator animator = footModel != null
            ? footModel.GetComponent<Animator>()
            : root.GetComponentInChildren<Animator>(true);

        if (animator != null)
        {
            animator.applyRootMotion = false;
            string trigger = rank == 1 ? "Victory" : "Clap";
            animator.SetTrigger(trigger);

            if (rank == 1 && trophyPrefab != null)
            {
                Transform rightHand = animator.GetBoneTransform(HumanBodyBones.RightHand);
                if (rightHand != null)
                {
                    // Check for pre-existing trophy child first, otherwise instantiate
                    Transform trophy = rightHand.Find("trophy");
                    if (trophy != null)
                    {
                        trophy.gameObject.SetActive(true);
                    }
                    else
                    {
                        GameObject trophyGO = Instantiate(trophyPrefab, rightHand);
                        trophyGO.name = "trophy";
                        trophyGO.transform.localPosition = Vector3.zero;
                        trophyGO.transform.localRotation = Quaternion.identity;
                    }
                }
            }
        }
        else
        {
            Debug.LogWarning($"[PodiumManager] No Animator found on {root.name}!");
        }

        StartCoroutine(LockPodiumPosition(root, podiumPoint.position, podiumPoint.rotation));
    }

    // ─────────────────────────────────────────────
    // AI PODIUM POSITIONING
    // ─────────────────────────────────────────────

    private void PositionAIOnPodium(NetworkedAIOpponent ai, Transform podiumPoint, int rank)
    {
        if (ai == null || podiumPoint == null) return;

        Transform aiRoot = ai.transform;
        ai.enabled = false;

        var navAgent = aiRoot.GetComponent<UnityEngine.AI.NavMeshAgent>();
        if (navAgent != null)
        {
            navAgent.ResetPath();
            navAgent.isStopped = true;
            navAgent.enabled   = false;
        }

        foreach (var rb in aiRoot.GetComponentsInChildren<Rigidbody>(true))
        {
            if (rb == null) continue;
            rb.linearVelocity  = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.isKinematic     = true;
        }

        aiRoot.position = podiumPoint.position;
        aiRoot.rotation = podiumPoint.rotation;

        string[] footNames = { "PlayeronFoot", "PlayereronFoot", "FootModel", "CharacterModel" };
        string[] bikeNames = { "PlayeronBike", "BikeModel", "CycleModel", "VehicleModel" };

        foreach (string n in bikeNames)
        {
            Transform t = aiRoot.Find(n);
            if (t != null) t.gameObject.SetActive(false);
        }

        Transform footTransform = null;
        foreach (string n in footNames)
        {
            Transform t = aiRoot.Find(n);
            if (t != null)
            {
                t.gameObject.SetActive(true);
                t.localPosition = Vector3.zero;
                t.localRotation = Quaternion.identity;
                footTransform = t;
                break;
            }
        }

        Animator animator = footTransform != null
            ? footTransform.GetComponent<Animator>()
            : aiRoot.GetComponentInChildren<Animator>(true);

        if (animator != null)
        {
            animator.applyRootMotion = false;
            string trigger = rank == 1 ? "Victory" : "Clap";
            animator.SetTrigger(trigger);
            Debug.Log($"[PodiumManager] AI '{ai.opponentName}' → rank {rank} → trigger '{trigger}'");
        }
        else
        {
            Debug.LogWarning($"[PodiumManager] No Animator found on AI '{ai.opponentName}'");
        }

        StartCoroutine(LockPodiumPosition(aiRoot, podiumPoint.position, podiumPoint.rotation));
    }

    private IEnumerator LockPodiumPosition(Transform root, Vector3 pos, Quaternion rot)
    {
        float elapsed = 0f;
        while (elapsed < 30f)
        {
            if (root == null) yield break;
            if (Vector3.Distance(root.position, pos) > 0.01f)
            {
                root.position = pos;
                root.rotation = rot;
            }
            elapsed += Time.deltaTime;
            yield return null;
        }
    }

    // ─────────────────────────────────────────────
    // LEADERBOARD
    // ─────────────────────────────────────────────

    private IEnumerator ShowLeaderboard(
        List<MultiplayerRaceRanking.RankSnapshot> rankings, int localOwnerId)
    {
        if (leaderboardPanel == null || leaderboardContainer == null || leaderboardEntryPrefab == null)
        {
            Debug.LogWarning("[PodiumManager] Leaderboard references not assigned — skipping.");
            yield break;
        }

        foreach (Transform child in leaderboardContainer)
            Destroy(child.gameObject);

        leaderboardPanel.SetActive(true);

        for (int i = 0; i < rankings.Count; i++)
        {
            var snap = rankings[i];
            GameObject entry = Instantiate(leaderboardEntryPrefab, leaderboardContainer);

            // FIX: identify local player by ownerId — isLocalPlayer unreliable here
            bool isLocal = snap.ownerId == localOwnerId;

            TMP_Text rankText = entry.transform.Find("RankText")?.GetComponent<TMP_Text>();
            if (rankText != null) rankText.text = GetOrdinal(snap.rank);

            TMP_Text nameText = entry.transform.Find("NameText")?.GetComponent<TMP_Text>();
            if (nameText != null) nameText.text = snap.name;

            TMP_Text timeText = entry.transform.Find("TimeText")?.GetComponent<TMP_Text>();
            if (timeText != null)
            {
                // Local player uses locally-tracked finalRaceTime (most accurate)
                if (isLocal)
                    timeText.text = FormatTime(finalRaceTime);
                else if (snap.finishTime > 0f)
                    timeText.text = FormatTime(snap.finishTime);
                else
                    timeText.text = "---";
            }

            // FIX: use ownerId-based isLocal for color — fixes all-yellow bug
            Color rowColor = isLocal ? localPlayerColor
                           : snap.racerType == MultiplayerRaceRanking.RacerType.AI ? aiColor
                           : otherPlayerColor;

            Image rowBg = entry.GetComponent<Image>();
            if (rowBg != null && snap.rank <= 3)
                rowBg.color = new Color(topThreeColor.r, topThreeColor.g, topThreeColor.b, rowBg.color.a);

            if (rankText != null) rankText.color = rowColor;
            if (nameText != null) nameText.color = rowColor;
            if (timeText != null) timeText.color = rowColor;

            if (nameText != null && isLocal)
                nameText.fontStyle = FontStyles.Bold;

            yield return new WaitForSeconds(entryRevealDelay);
        }

        yield return new WaitForSeconds(leaderboardDisplayDuration);
        leaderboardPanel.SetActive(false);
    }

    // ─────────────────────────────────────────────
    // RESULTS UI + FIREBASE
    // Each client only reads/writes its own Firebase document
    // ─────────────────────────────────────────────

    private IEnumerator FetchAndShowResults(int placement, float raceTime, int pointsEarned)
    {
        string playerId = FirebaseManager.Instance?.PlayerId;
        if (string.IsNullOrEmpty(playerId))
        {
            ShowPodiumUI(placement, raceTime, pointsEarned, 0, "Beginner", "");
            yield break;
        }

        bool done = false;
        int currentPoints = 0;
        string currentTier = "Beginner";

        db.Collection("players").Document(playerId)
            .GetSnapshotAsync()
            .ContinueWithOnMainThread(task =>
            {
                if (!task.IsFaulted && task.Result.Exists)
                {
                    currentPoints = task.Result.ContainsField("points")
                        ? (int)task.Result.GetValue<long>("points") : 0;
                    currentTier = task.Result.ContainsField("tier")
                        ? task.Result.GetValue<string>("tier") : "Beginner";
                }
                done = true;
            });

        yield return new WaitUntil(() => done);

        int newPoints = currentPoints + pointsEarned;
        bool advancing = IsAdvancing(currentTier, placement, raceTime, firstPlaceTime);
        string newTier = advancing ? GetNextTier(currentTier) : currentTier;
        int savedPoints = advancing ? 0 : newPoints;
        string tierStatus = GetTierStatus(currentTier, placement, raceTime, firstPlaceTime, advancing);

        currentTierAtRaceEnd = currentTier;

        yield return StartCoroutine(UpdatePlayerData(playerId, savedPoints, newTier));

        RaceAchievementTracker tracker = RaceAchievementTracker.LocalInstance;
        if (tracker != null)
            tracker.OnRaceFinished(placement, currentTier, newTier);

        ShowPodiumUI(placement, raceTime, pointsEarned, newPoints, currentTier, tierStatus);
    }

    private IEnumerator UpdatePlayerData(string playerId, int newPoints, string newTier)
    {
        bool done = false;

        if (CoinManager.Instance != null)
            CoinManager.Instance.SaveCoinsToFirebase();

        string timeField = currentTierAtRaceEnd switch
        {
            "Beginner"     => "fastestTimeBeginner",
            "Intermediate" => "fastestTimeIntermediate",
            "Pro"          => "fastestTimePro",
            _              => "fastestTimeBeginner"
        };

        var updateData = new Dictionary<string, object>
        {
            { "points",         newPoints },
            { "tier",           newTier   },
            { "racesCompleted", FieldValue.Increment(1) }
        };

        if (finalPlacement == 1)
            updateData["wins"] = FieldValue.Increment(1);

        bool timeDone = false;
        db.Collection("players").Document(playerId)
            .GetSnapshotAsync()
            .ContinueWithOnMainThread(task =>
            {
                if (!task.IsFaulted && task.Result.Exists)
                {
                    float existingBest = task.Result.ContainsField(timeField)
                        ? (float)task.Result.GetValue<double>(timeField) : 0f;

                    if (existingBest <= 0 || finalRaceTime < existingBest)
                        updateData[timeField] = (double)finalRaceTime;
                }
                timeDone = true;
            });

        yield return new WaitUntil(() => timeDone);

        db.Collection("players").Document(playerId)
            .UpdateAsync(updateData)
            .ContinueWithOnMainThread(task =>
            {
                if (task.IsFaulted)
                    Debug.LogError("[PodiumManager] Failed to update player data: " + task.Exception);
                else
                    Debug.Log($"[PodiumManager] Updated — Points: {newPoints}, Tier: {newTier}");
                done = true;
            });

        yield return new WaitUntil(() => done);
    }

    private void ShowPodiumUI(int placement, float raceTime, int pointsEarned,
        int totalPoints, string currentTier, string tierStatus)
    {
        podiumPanel.SetActive(true);

        if (placementText    != null) placementText.text    = GetOrdinal(placement) + " Place";
        if (raceTimeText     != null) raceTimeText.text     = $"Time: {FormatTime(raceTime)}";
        if (pointsEarnedText != null) pointsEarnedText.text = $"+{pointsEarned} Points";
        if (totalPointsText  != null) totalPointsText.text  = $"Total: {totalPoints} Points";
        if (tierStatusText   != null) tierStatusText.text   = tierStatus;

        if (coinsEarnedText != null && CoinManager.Instance != null)
            coinsEarnedText.text = $"+{CoinManager.Instance.GetSessionCoins()} Coins";

        if (pointsProgressBar != null)
        {
            if (currentTier == "Beginner")
            {
                pointsProgressBar.minValue = 0;
                pointsProgressBar.maxValue = BEGINNER_TARGET_TIME;
                pointsProgressBar.value    = Mathf.Min(finalRaceTime, BEGINNER_TARGET_TIME);
            }
            else
            {
                int maxPoints = GetProgressBarMax(currentTier);
                pointsProgressBar.minValue = 0;
                pointsProgressBar.maxValue = maxPoints;
                pointsProgressBar.value    = Mathf.Min(totalPoints, maxPoints);
            }
        }
    }

    // ─────────────────────────────────────────────
    // TIER LOGIC
    // ─────────────────────────────────────────────

    private int GetPointsForPlacement(int placement)
    {
        switch (placement)
        {
            case 1:  return POINTS_1ST;
            case 2:  return POINTS_2ND;
            case 3:  return POINTS_3RD;
            default: return POINTS_OTHER;
        }
    }

    private bool IsAdvancing(string tier, int placement, float playerTime, float p1Time)
    {
        switch (tier)
        {
            case "Beginner":
                return playerTime <= BEGINNER_TARGET_TIME;
            case "Intermediate":
                if (p1Time <= 0f) return false;
                return playerTime <= p1Time * 1.15f;
            case "Pro":
                return placement == 1;
            default:
                return false;
        }
    }

    private string GetNextTier(string currentTier)
    {
        switch (currentTier)
        {
            case "Beginner":     return "Intermediate";
            case "Intermediate": return "Pro";
            default:             return currentTier;
        }
    }

    private string GetTierStatus(string tier, int placement, float playerTime,
        float p1Time, bool advancing)
    {
        switch (tier)
        {
            case "Beginner":
                if (advancing) return "🎉 Advancing to Intermediate!";
                float diff = playerTime - BEGINNER_TARGET_TIME;
                return $"Beat the target time to advance — you were {diff:F0}s over";
            case "Intermediate":
                if (advancing) return "🎉 Advancing to Pro!";
                if (p1Time > 0f)
                {
                    float threshold = p1Time * 1.15f;
                    float gap = playerTime - threshold;
                    return $"Finish within 15% of 1st place — you were {gap:F0}s off";
                }
                return "Finish within 15% of 1st place time to advance to Pro";
            case "Pro":
                return advancing ? "🏆 Pro Tier Complete!" : "Finish 1st to complete Pro tier";
            default:
                return "";
        }
    }

    private int GetProgressBarMax(string tier)
    {
        switch (tier)
        {
            case "Intermediate": return INTERMEDIATE_TO_PRO_THRESHOLD;
            default:             return 1;
        }
    }

    // ─────────────────────────────────────────────
    // CONTINUE
    // ─────────────────────────────────────────────

    private void OnContinuePressed()
    {
        StartCoroutine(ReturnToLobby());
    }

    private IEnumerator ReturnToLobby()
    {
        if (podiumMusic    != null) podiumMusic.Stop();
        if (confettiEffect != null) confettiEffect.Stop();

        if (LobbyDataTransfer.Instance != null)
            LobbyDataTransfer.Instance.ClearData();

        if (EdgegapRelayManager.Instance != null)
            EdgegapRelayManager.Instance.DeleteRelaySession();

        yield return new WaitForSeconds(0.5f);
        UnityEngine.SceneManagement.SceneManager.LoadScene("Lobby");
    }

    // ─────────────────────────────────────────────
    // HELPERS
    // ─────────────────────────────────────────────

    private string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);
        return $"{minutes:00}:{seconds:00}";
    }

    private string GetOrdinal(int number)
    {
        switch (number)
        {
            case 1:  return "1st";
            case 2:  return "2nd";
            case 3:  return "3rd";
            default: return $"{number}th";
        }
    }
}
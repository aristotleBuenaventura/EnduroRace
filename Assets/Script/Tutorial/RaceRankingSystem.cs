using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Linq;
using System.Collections;

public class RaceRankingSystem : MonoBehaviour
{
    [Header("UI References")]
    public GameObject rankingPanel;
    public Transform rankingContainer;
    public GameObject rankEntryPrefab;
    
    [Header("Race References")]
    public Transform playerTransform;
    public AIOpponentManager aiManager;
    public Transform finishLine;
    
    [Header("Checkpoint System")]
    public Transform[] checkpoints;
    
    [Header("Update Settings")]
    public float updateInterval = 0.1f;
    private float updateTimer = 0f;
    
    [Header("Display Settings")]
    public int maxDisplayedRanks = 10;
    public bool showPlayerOnly = false;
    public Color playerColor = Color.yellow;
    public Color aiColor = Color.white;
    
    private List<RacerData> racers = new List<RacerData>();
    private Dictionary<Transform, RankEntryUI> rankEntries = new Dictionary<Transform, RankEntryUI>();
    
    // Make RacerData public so it can be accessed from RaceManagerTutorial
    public class RacerData
    {
        public string name;
        public Transform transform;
        public bool isPlayer;
        public int checkpointsPassed = 0;
        public float totalProgress = 0f;
        public int rank = 0;
        
        // Cache segment data
        public Vector3 segmentStartPos;
        public float segmentLength;
        public float lastKnownProgress = 0f;
    }
    
    private class RankEntryUI
    {
        public GameObject gameObject;
        public TMP_Text rankText;
        public TMP_Text nameText;
        public TMP_Text gapText;
    }
    
    IEnumerator Start()
    {
        if (rankingPanel != null)
            rankingPanel.SetActive(true);

        yield return new WaitUntil(() =>
            aiManager != null &&
            aiManager.GetActiveOpponents().Count > 0
        );

        InitializeRacers();
    }

    void LateUpdate()
    {
        updateTimer += Time.deltaTime;
        
        if (updateTimer >= updateInterval)
        {
            UpdateRankings();
            updateTimer = 0f;
        }
    }
    
    private void InitializeRacers()
    {
        racers.Clear();
        
        
        if (playerTransform != null)
        {
            var playerData = new RacerData
            {
                name = "YOU",
                transform = playerTransform,
                isPlayer = true,
                segmentStartPos = playerTransform.position,
                segmentLength = 0f
            };
            racers.Add(playerData);
        }
        
        if (aiManager != null)
        {
            var opponents = aiManager.GetActiveOpponents();
            
            foreach (var ai in opponents)
            {
                if (ai != null)
                {
                    var aiData = new RacerData
                    {
                        name = ai.opponentName,
                        transform = ai.transform,
                        isPlayer = false,
                        segmentStartPos = ai.transform.position,
                        segmentLength = 0f
                    };
                    racers.Add(aiData);
                }
            }
        }
        
    }
    
    private void UpdateRankings()
    {
        if (racers.Count == 0)
            return;

        foreach (var racer in racers)
        {
            if (racer.transform == null)
                continue;

            CalculateProgress(racer);
        }

        racers.Sort((a, b) => b.totalProgress.CompareTo(a.totalProgress));

        for (int i = 0; i < racers.Count; i++)
        {
            racers[i].rank = i + 1;
        }

        UpdateUI();
    }

    private void CalculateProgress(RacerData racer)
    {   
        if (checkpoints == null || checkpoints.Length == 0)
        {
            if (finishLine != null)
            {
                float distanceToFinish = Vector3.Distance(
                    GetActualPosition(racer),
                    finishLine.position
                );
                racer.totalProgress = 10000f - distanceToFinish;
            }
            return;
        }

        float checkpointProgress = racer.checkpointsPassed * 1000f;
        float segmentProgress = 0f;
        int nextCheckpoint = racer.checkpointsPassed;
        
        if (nextCheckpoint < checkpoints.Length)
        {
            Vector3 segmentStart = racer.segmentStartPos;
            Vector3 segmentEnd = checkpoints[nextCheckpoint].position;
            Vector3 racerPos = GetActualPosition(racer);
            
            Vector3 segmentDirection = (segmentEnd - segmentStart).normalized;
            Vector3 racerOffset = racerPos - segmentStart;
            
            float forwardDistance = Vector3.Dot(racerOffset, segmentDirection);
            forwardDistance = Mathf.Max(0f, forwardDistance);
            
            if (racer.segmentLength > 0)
            {
                segmentProgress = Mathf.Clamp((forwardDistance / racer.segmentLength) * 999f, 0f, 999f);
            }
        }
        
        float newProgress = checkpointProgress + segmentProgress;
        
        if (newProgress > racer.lastKnownProgress)
        {
            racer.totalProgress = newProgress;
            racer.lastKnownProgress = newProgress;
        }
        else
        {
            racer.totalProgress = racer.lastKnownProgress;
        }
    }

    private void UpdateUI()
    {
        if (rankingContainer == null || rankEntryPrefab == null)
            return;
        
        List<RacerData> racersToShow = showPlayerOnly
            ? racers.Where(r => r.isPlayer).ToList()
            : racers.Take(maxDisplayedRanks).ToList();
        
        var toRemove = new List<Transform>();
        foreach (var kvp in rankEntries)
        {
            if (!racersToShow.Any(r => r.transform == kvp.Key))
            {
                if (kvp.Value.gameObject != null)
                    Destroy(kvp.Value.gameObject);
                toRemove.Add(kvp.Key);
            }
        }
        foreach (var t in toRemove)
            rankEntries.Remove(t);
        
        for (int i = 0; i < racersToShow.Count; i++)
        {
            var racer = racersToShow[i];
            
            if (!rankEntries.ContainsKey(racer.transform))
            {
                CreateRankEntry(racer);
            }
            
            UpdateRankEntry(racer);
            
            if (rankEntries.ContainsKey(racer.transform))
            {
                rankEntries[racer.transform].gameObject.transform.SetSiblingIndex(i);
            }
        }
    }
    
    private void CreateRankEntry(RacerData racer)
    {
        GameObject entryGO = Instantiate(rankEntryPrefab, rankingContainer);
        
        RankEntryUI entry = new RankEntryUI
        {
            gameObject = entryGO,
            rankText = entryGO.transform.Find("RankText")?.GetComponent<TMP_Text>(),
            nameText = entryGO.transform.Find("NameText")?.GetComponent<TMP_Text>(),
            gapText = entryGO.transform.Find("GapText")?.GetComponent<TMP_Text>()
        };
        
        rankEntries[racer.transform] = entry;
    }
    
    private void UpdateRankEntry(RacerData racer)
    {
        if (!rankEntries.ContainsKey(racer.transform))
            return;
        
        var entry = rankEntries[racer.transform];
        
        if (entry.rankText != null)
        {
            entry.rankText.text = GetRankSuffix(racer.rank);
            entry.rankText.color = racer.isPlayer ? playerColor : aiColor;
        }
        
        if (entry.nameText != null)
        {
            entry.nameText.text = racer.name;
            entry.nameText.color = racer.isPlayer ? playerColor : aiColor;
        }
        
        if (entry.gapText != null)
        {
            if (racer.rank == 1)
            {
                entry.gapText.text = "LEAD";
                entry.gapText.color = Color.green;
            }
            else
            {
                var leader = racers.FirstOrDefault(r => r.rank == 1);
                if (leader != null)
                {
                    float gap = leader.totalProgress - racer.totalProgress;
                    float gapInCheckpoints = gap / 1000f;
                    entry.gapText.text = $"+{gapInCheckpoints:F2}";
                    entry.gapText.color = racer.isPlayer ? playerColor : aiColor;
                }
            }
        }
    }
    
    private string GetRankSuffix(int rank)
    {
        string suffix = "th";
        
        if (rank % 100 >= 11 && rank % 100 <= 13)
        {
            suffix = "th";
        }
        else
        {
            switch (rank % 10)
            {
                case 1: suffix = "st"; break;
                case 2: suffix = "nd"; break;
                case 3: suffix = "rd"; break;
            }
        }
        
        return $"{rank}{suffix}";
    }
    
    public void RefreshRacers()
    {
        InitializeRacers();
    }
    
    public int GetPlayerRank()
    {
        var playerData = racers.FirstOrDefault(r => r.isPlayer);
        return playerData?.rank ?? 0;
    }

    void OnEnable()
    {
        RaceManagerTutorial.OnRaceStarted += HandleRaceStarted;
    }

    void OnDisable()
    {
        RaceManagerTutorial.OnRaceStarted -= HandleRaceStarted;
    }

    void HandleRaceStarted()
    {
        InitializeRacers();
    }

    public void OnCheckpointPassed(Transform racerTransform, int checkpointIndex)
    {
        var racer = racers.FirstOrDefault(r => r.transform == racerTransform);
        
        if (racer == null)
        {
            return;
        }
        
        if (checkpointIndex >= racer.checkpointsPassed)
        {
            int oldProgress = racer.checkpointsPassed;
            racer.checkpointsPassed = checkpointIndex + 1;
            
            racer.segmentStartPos = GetActualPosition(racer);
            
            int nextCP = racer.checkpointsPassed;
            if (nextCP < checkpoints.Length)
            {
                racer.segmentLength = Vector3.Distance(
                    racer.segmentStartPos,
                    checkpoints[nextCP].position
                );
            }
            
        }
    }

    private Vector3 GetActualPosition(RacerData racer)
    {
        if (racer.isPlayer)
        {
            Transform footChild = racer.transform.Find("PlayeronFoot");
            Transform bikeChild = racer.transform.Find("PlayeronBike");
            
            if (footChild != null && footChild.gameObject.activeSelf)
            {
                return footChild.position;
            }
            if (bikeChild != null && bikeChild.gameObject.activeSelf)
            {
                return bikeChild.position;
            }
        }
        
        return racer.transform.position;
    }

    // ========== NEW METHODS FOR PODIUM CUTSCENE ==========
    
    /// <summary>
    /// Returns the final sorted list of racers (used by podium cutscene)
    /// </summary>
    public List<RacerData> GetFinalRankings()
    {
        return new List<RacerData>(racers);
    }

    /// <summary>
    /// Get the name of a racer by their rank
    /// </summary>
    public string GetRacerName(int rank)
    {
        var racer = racers.FirstOrDefault(r => r.rank == rank);
        return racer?.name ?? "Unknown";
    }

    /// <summary>
    /// Get the transform of a racer by their rank
    /// </summary>
    public Transform GetRacerTransform(int rank)
    {
        var racer = racers.FirstOrDefault(r => r.rank == rank);
        return racer?.transform;
    }

    /// <summary>
    /// Check if a specific rank belongs to the player
    /// </summary>
    public bool IsPlayerRank(int rank)
    {
        var racer = racers.FirstOrDefault(r => r.rank == rank);
        return racer?.isPlayer ?? false;
    }
}
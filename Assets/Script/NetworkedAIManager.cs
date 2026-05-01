using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using FishNet.Object;
using FishNet.Connection;

public class NetworkedAIManager : NetworkBehaviour
{
    [Header("AI Prefab")]
    public GameObject aiOpponentPrefab;
    public GameObject aiOpponentPrefab1;
    public bool randomizeGender = true;

    [Header("Spawn Settings")]
    public int numberOfOpponents = 3;
    public float spawnDelay = 0.5f;

    [Header("Swim Paths")]
    public Transform[] swimPath1;
    public Transform[] swimPath2;

    [Header("Bike Paths")]
    public Transform[] bikePath1;
    public Transform[] bikePath2;

    [Header("Run Paths")]
    public Transform[] runPath1;
    public Transform[] runPath2;

    [Header("Segment Transition Points")]
    public Transform swimToBikeTransition;
    public Transform bikeToRunTransition;

    [Header("AI Names")]
    public string[] aiNames = { "Alex", "Jordan", "Taylor", "Morgan", "Casey" };

    [Header("Difficulty Settings")]
    public float minSpeed = 4.5f;
    public float maxSpeed = 5.5f;
    public Vector2 speedMultiplierRange = new Vector2(0.92f, 1.1f);

    [Header("Movement Variation")]
    public float minLaneOffset = -1.1f;
    public float maxLaneOffset = 1.1f;
    public float laneJitter = 0.2f;
    public float minRotationSpeed = 4f;
    public float maxRotationSpeed = 7f;
    public float minWaypointReachDistance = 1.4f;
    public float maxWaypointReachDistance = 2.4f;

    [Header("Animation Variation")]
    public Vector2 swimAnimationSpeedRange = new Vector2(0.72f, 1.28f);
    public Vector2 bikeAnimationSpeedRange = new Vector2(0.75f, 1.25f);
    public Vector2 runAnimationSpeedRange = new Vector2(0.74f, 1.3f);
    public Vector2 animationCadenceFrequencyRange = new Vector2(0.42f, 1.35f);
    public Vector2 animationCadenceAmplitudeRange = new Vector2(0.08f, 0.2f);

    [Header("Dynamic Pace Shift")]
    public Vector2 paceShiftIntervalRange = new Vector2(1.5f, 4.2f);
    public Vector2 paceShiftDurationRange = new Vector2(0.8f, 2.4f);
    public Vector2 paceShiftMultiplierRange = new Vector2(0.72f, 1.28f);

    [Header("Segment Move Speed Randomness")]
    public Vector2 swimMoveSpeedRange = new Vector2(0.2f, 2.2f);
    public Vector2 bikeMoveSpeedRange = new Vector2(0.25f, 2.25f);
    public Vector2 runMoveSpeedRange = new Vector2(0.2f, 2.15f);
    [Range(0.05f, 1f)] public float swimMoveSpeedStdDev = 0.48f;
    [Range(0.05f, 1f)] public float bikeMoveSpeedStdDev = 0.45f;
    [Range(0.05f, 1f)] public float runMoveSpeedStdDev = 0.42f;

    [Header("Guaranteed Super Slow AI")]
    public bool forceOneSuperSlowAI = true;
    public Vector2 superSlowSwimRange = new Vector2(0.12f, 0.28f);
    public Vector2 superSlowBikeRange = new Vector2(0.16f, 0.34f);
    public Vector2 superSlowRunRange = new Vector2(0.12f, 0.3f);

    [Header("Race Start Spread")]
    public Vector2 raceStartDelayRange = new Vector2(0f, 1.25f);

    private List<NetworkedAIOpponent> activeOpponents = new List<NetworkedAIOpponent>();
    private Dictionary<NetworkedAIOpponent, SpawnPoint> usedSpawnPoints = new Dictionary<NetworkedAIOpponent, SpawnPoint>();
    private Dictionary<NetworkedAIOpponent, AIProfile> aiProfiles = new Dictionary<NetworkedAIOpponent, AIProfile>();
    private Dictionary<NetworkedAIOpponent, float> aiElapsedRaceTimes = new Dictionary<NetworkedAIOpponent, float>();
    private HashSet<NetworkedAIOpponent> aiFinishTimesReported = new HashSet<NetworkedAIOpponent>();
    private bool raceRunning = false;

    // Track each AI's current segment so we know when to transition
    private Dictionary<NetworkedAIOpponent, NetworkedAIOpponent.AISegment> aiSegments
        = new Dictionary<NetworkedAIOpponent, NetworkedAIOpponent.AISegment>();

    private class AIProfile
    {
        public Transform[] swimPath;
        public Transform[] bikePath;
        public Transform[] runPath;
        public float speedMultiplier;
        public float lateralOffset;
        public float turnSpeed;
        public float waypointReachDistance;
        public float swimAnimSpeed;
        public float bikeAnimSpeed;
        public float runAnimSpeed;
        public float swimMoveSpeedMultiplier;
        public float bikeMoveSpeedMultiplier;
        public float runMoveSpeedMultiplier;
        public float swimAnimationPhaseOffset;
        public float bikeAnimationPhaseOffset;
        public float runAnimationPhaseOffset;
        public float animationCadenceFrequency;
        public float animationCadenceAmplitude;
        public float animationCadencePhase;
        public float paceShiftMinInterval;
        public float paceShiftMaxInterval;
        public float paceShiftMinDuration;
        public float paceShiftMaxDuration;
        public float paceShiftMinMultiplier;
        public float paceShiftMaxMultiplier;
        public float startDelay;
    }

    public override void OnStartServer()
    {
        base.OnStartServer();
        ;
    }

    private void Update()
    {
        // Only server monitors AI progress and triggers transitions
        if (!IsServerInitialized) return;

        foreach (var opponent in activeOpponents)
        {
            if (opponent == null) continue;
            if (!aiSegments.ContainsKey(opponent)) continue;

            NetworkedAIOpponent.AISegment currentSeg = aiSegments[opponent];
            bool hasFinished = currentSeg == (NetworkedAIOpponent.AISegment)99;

            if (raceRunning && !hasFinished)
            {
                if (!aiElapsedRaceTimes.ContainsKey(opponent))
                    aiElapsedRaceTimes[opponent] = 0f;

                aiElapsedRaceTimes[opponent] += Time.deltaTime;
            }

            // AI has exhausted its current waypoints — trigger next segment
            if (opponent.currentWaypointIndex >= opponent.waypointPath?.Length)
            {
                if (currentSeg == NetworkedAIOpponent.AISegment.Swim)
                {
                    ;
                    TransitionOpponent(opponent, NetworkedAIOpponent.AISegment.Bike);
                }
                else if (currentSeg == NetworkedAIOpponent.AISegment.Bike)
                {
                    ;
                    TransitionOpponent(opponent, NetworkedAIOpponent.AISegment.Run);
                }
                else if (currentSeg == NetworkedAIOpponent.AISegment.Run)
                {
                    // AI finished the race — mark done so we stop checking
                    if (!aiFinishTimesReported.Contains(opponent))
                    {
                        float finishTime = aiElapsedRaceTimes.TryGetValue(opponent, out float elapsedTime)
                            ? elapsedTime
                            : 0f;
                        ReportAIFinishTime(opponent, finishTime);
                        aiFinishTimesReported.Add(opponent);
                        Debug.Log($"[NetworkedAIManager] AI finished: {opponent.opponentName} | time={finishTime:F2}s");
                    }
                    aiSegments[opponent] = (NetworkedAIOpponent.AISegment)99; // sentinel: finished
                }
            }
        }
    }

    [Server]
    private void ReportAIFinishTime(NetworkedAIOpponent opponent, float finishTime)
    {
        if (opponent == null) return;

        MultiplayerRaceRanking ranking = FindFirstObjectByType<MultiplayerRaceRanking>();
        if (ranking != null)
        {
            ranking.ReportAIFinish(opponent.transform, finishTime);
            Debug.Log($"[NetworkedAIManager] Reported AI finish time to ranking: {opponent.opponentName} => {finishTime:F2}s");
        }
        else
        {
            Debug.LogWarning($"[NetworkedAIManager] Could not report AI finish time. MultiplayerRaceRanking not found for {opponent.opponentName}.");
        }
    }

    [Server]
    private void TransitionOpponent(NetworkedAIOpponent opponent, NetworkedAIOpponent.AISegment newSegment)
    {
        Transform[] newPath = null;
        Vector3 transitionPos = opponent.transform.position;

        switch (newSegment)
        {
            case NetworkedAIOpponent.AISegment.Bike:
                if (aiProfiles.TryGetValue(opponent, out AIProfile bikeProfile))
                    newPath = bikeProfile.bikePath;
                if (newPath == null || newPath.Length == 0)
                    newPath = GetRandomBikePath();
                if (swimToBikeTransition != null)
                    transitionPos = swimToBikeTransition.position;
                break;

            case NetworkedAIOpponent.AISegment.Run:
                if (aiProfiles.TryGetValue(opponent, out AIProfile runProfile))
                    newPath = runProfile.runPath;
                if (newPath == null || newPath.Length == 0)
                    newPath = GetRandomRunPath();
                if (bikeToRunTransition != null)
                    transitionPos = bikeToRunTransition.position;
                break;
        }

        if (newPath == null || newPath.Length == 0)
        {
            ;
            return;
        }

        // Update tracked segment BEFORE calling transition so Update() doesn't re-trigger
        aiSegments[opponent] = newSegment;

        ;

        // FIX: Use the overload that takes the path so index resets correctly
        opponent.TransitionToSegment(newSegment, transitionPos, newPath);
    }

    [Server]
    public void SpawnOpponents()
    {
        ;

        if (aiOpponentPrefab == null && aiOpponentPrefab1 == null)
        {
            ;
            return;
        }

        List<SpawnPoint> reserved = new List<SpawnPoint>();
        for (int i = 0; i < numberOfOpponents; i++)
        {
            SpawnPoint sp = SpawnPoint.GetRandomSpawnPoint();
            if (sp != null)
            {
                reserved.Add(sp);
                ;
            }
            else
            {
                ;
            }
        }

        List<Transform[]> swimAssignments = BuildPathAssignments(GetAvailablePaths(swimPath1, swimPath2), reserved.Count);
        List<Transform[]> bikeAssignments = BuildPathAssignments(GetAvailablePaths(bikePath1, bikePath2), reserved.Count);
        List<Transform[]> runAssignments = BuildPathAssignments(GetAvailablePaths(runPath1, runPath2), reserved.Count);
        int superSlowIndex = forceOneSuperSlowAI && reserved.Count > 0 ? Random.Range(0, reserved.Count) : -1;

        for (int i = 0; i < reserved.Count; i++)
            SpawnOpponent(
                i,
                reserved.Count,
                reserved[i],
                swimAssignments[i],
                bikeAssignments[i],
                runAssignments[i],
                i == superSlowIndex
            );

        ;
    }

    [Server]
    private void SpawnOpponent(
        int index,
        int totalOpponents,
        SpawnPoint spawnPoint,
        Transform[] assignedSwimPath,
        Transform[] assignedBikePath,
        Transform[] assignedRunPath,
        bool forceSuperSlow)
    {
        GameObject selectedPrefab;
        if (randomizeGender)
        {
            bool pickFemale = Random.value > 0.5f;
            if (pickFemale && aiOpponentPrefab1 != null)
                selectedPrefab = aiOpponentPrefab1;
            else if (aiOpponentPrefab != null)
                selectedPrefab = aiOpponentPrefab;
            else
                selectedPrefab = aiOpponentPrefab1;
        }
        else
        {
            selectedPrefab = aiOpponentPrefab != null ? aiOpponentPrefab : aiOpponentPrefab1;
        }

        if (selectedPrefab == null)
        {
            ;
            spawnPoint.Release();
            return;
        }

        if (spawnPoint == null)
        {
            ;
            return;
        }

        spawnPoint.GetSpawnTransform(out Vector3 position, out Quaternion rotation);
        GameObject aiGO = Instantiate(selectedPrefab, position, rotation);

        NetworkedAIOpponent ai = aiGO.GetComponent<NetworkedAIOpponent>();
        if (ai == null)
        {
            ;
            Destroy(aiGO);
            spawnPoint.Release();
            return;
        }

        ai.opponentName = aiNames[index % aiNames.Length];
        aiGO.name = "AI_" + ai.opponentName;
        ai.baseSpeed = Random.Range(minSpeed, maxSpeed);

        AIProfile profile = CreateProfile(index, totalOpponents, assignedSwimPath, assignedBikePath, assignedRunPath, forceSuperSlow);
        ai.baseSpeed *= profile.speedMultiplier;
        ai.ConfigureMovementProfile(
            profile.lateralOffset,
            profile.turnSpeed,
            profile.waypointReachDistance,
            profile.swimAnimSpeed,
            profile.bikeAnimSpeed,
            profile.runAnimSpeed,
            profile.swimMoveSpeedMultiplier,
            profile.bikeMoveSpeedMultiplier,
            profile.runMoveSpeedMultiplier,
            profile.swimAnimationPhaseOffset,
            profile.bikeAnimationPhaseOffset,
            profile.runAnimationPhaseOffset,
            profile.animationCadenceFrequency,
            profile.animationCadenceAmplitude,
            profile.animationCadencePhase,
            profile.paceShiftMinInterval,
            profile.paceShiftMaxInterval,
            profile.paceShiftMinDuration,
            profile.paceShiftMaxDuration,
            profile.paceShiftMinMultiplier,
            profile.paceShiftMaxMultiplier,
            profile.startDelay
        );

        if (ai.runnerModel != null) ai.runnerModel.SetActive(true);
        if (ai.cyclistModel != null) ai.cyclistModel.SetActive(false);

        ServerManager.Spawn(aiGO);

        usedSpawnPoints[ai] = spawnPoint;
        aiProfiles[ai] = profile;
        aiElapsedRaceTimes[ai] = 0f;
        aiFinishTimesReported.Remove(ai);

        Transform[] chosenSwimPath = profile.swimPath ?? GetRandomSwimPath();
        if (chosenSwimPath != null && chosenSwimPath.Length > 0)
        {
            ai.SetWaypointPath(chosenSwimPath);
            ;
        }
        else
        {
            ;
        }

        // Track starting segment
        aiSegments[ai] = NetworkedAIOpponent.AISegment.Swim;

        activeOpponents.Add(ai);
        ;
    }

    private AIProfile CreateProfile(
        int slotIndex,
        int totalOpponents,
        Transform[] swimPath,
        Transform[] bikePath,
        Transform[] runPath,
        bool forceSuperSlow)
    {
        float normalizedLane = totalOpponents <= 1
            ? 0.5f
            : (float)slotIndex / (totalOpponents - 1f);
        int tierIndex = GetAnimationTierIndex(slotIndex);
        float animationTier = GetAnimationTierMultiplier(slotIndex);

        float laneCenter = Mathf.Lerp(minLaneOffset, maxLaneOffset, normalizedLane);
        float laneOffset = laneCenter + Random.Range(-Mathf.Abs(laneJitter), Mathf.Abs(laneJitter));
        float paceLow;
        float paceHigh;
        GetPaceTierRange(tierIndex, out paceLow, out paceHigh);
        float swimTierBase = GetSwimTierBase(tierIndex);

        float swimMoveMultiplier = forceSuperSlow
            ? RandomRange(superSlowSwimRange)
            : RandomGaussianAroundOne(swimMoveSpeedRange, swimMoveSpeedStdDev);
        float bikeMoveMultiplier = forceSuperSlow
            ? RandomRange(superSlowBikeRange)
            : RandomGaussianAroundOne(bikeMoveSpeedRange, bikeMoveSpeedStdDev);
        float runMoveMultiplier = forceSuperSlow
            ? RandomRange(superSlowRunRange)
            : RandomGaussianAroundOne(runMoveSpeedRange, runMoveSpeedStdDev);

        return new AIProfile
        {
            swimPath = swimPath,
            bikePath = bikePath,
            runPath = runPath,
            speedMultiplier = RandomRange(speedMultiplierRange),
            lateralOffset = Mathf.Clamp(laneOffset, minLaneOffset, maxLaneOffset),
            turnSpeed = Random.Range(minRotationSpeed, maxRotationSpeed),
            waypointReachDistance = Random.Range(minWaypointReachDistance, maxWaypointReachDistance),
            swimAnimSpeed = Mathf.Clamp(swimTierBase + Random.Range(-0.08f, 0.08f), 0.5f, 1.55f),
            bikeAnimSpeed = Mathf.Clamp(RandomRange(bikeAnimationSpeedRange) * animationTier, 0.65f, 1.45f),
            runAnimSpeed = Mathf.Clamp(RandomRange(runAnimationSpeedRange) * animationTier, 0.65f, 1.45f),
            swimMoveSpeedMultiplier = swimMoveMultiplier,
            bikeMoveSpeedMultiplier = bikeMoveMultiplier,
            runMoveSpeedMultiplier = runMoveMultiplier,
            swimAnimationPhaseOffset = Random.Range(0f, 1f),
            bikeAnimationPhaseOffset = Random.Range(0f, 1f),
            runAnimationPhaseOffset = Random.Range(0f, 1f),
            animationCadenceFrequency = RandomRange(animationCadenceFrequencyRange),
            animationCadenceAmplitude = RandomRange(animationCadenceAmplitudeRange),
            animationCadencePhase = Random.Range(0f, Mathf.PI * 2f),
            paceShiftMinInterval = RandomRange(paceShiftIntervalRange) * Random.Range(0.8f, 1f),
            paceShiftMaxInterval = RandomRange(paceShiftIntervalRange) * Random.Range(1f, 1.35f),
            paceShiftMinDuration = RandomRange(paceShiftDurationRange) * Random.Range(0.8f, 1f),
            paceShiftMaxDuration = RandomRange(paceShiftDurationRange) * Random.Range(1f, 1.35f),
            paceShiftMinMultiplier = Mathf.Clamp(paceLow, 0.55f, 1.1f),
            paceShiftMaxMultiplier = Mathf.Clamp(paceHigh, 0.9f, 1.6f),
            startDelay = RandomRange(raceStartDelayRange),
        };
    }

    private int GetAnimationTierIndex(int slotIndex) => Mathf.Abs(slotIndex) % 3;

    private float GetAnimationTierMultiplier(int slotIndex)
    {
        int tierIndex = GetAnimationTierIndex(slotIndex);
        if (tierIndex == 0) return 0.72f; // visibly slower
        if (tierIndex == 1) return 1f;    // normal
        return 1.28f;                     // visibly faster
    }

    private void GetPaceTierRange(int tierIndex, out float min, out float max)
    {
        if (tierIndex == 0)
        {
            min = 0.62f;
            max = 0.95f;
            return;
        }

        if (tierIndex == 1)
        {
            min = 0.9f;
            max = 1.12f;
            return;
        }

        min = 1.05f;
        max = 1.36f;
    }

    private float GetSwimTierBase(int tierIndex)
    {
        if (tierIndex == 0) return 0.62f; // slow swimmer cadence
        if (tierIndex == 1) return 0.95f; // normal swimmer cadence
        return 1.32f;                      // fast swimmer cadence
    }

    private float RandomRange(Vector2 range)
    {
        float min = Mathf.Min(range.x, range.y);
        float max = Mathf.Max(range.x, range.y);
        return Random.Range(min, max);
    }

    private float RandomGaussianAroundOne(Vector2 range, float stdDev)
    {
        float min = Mathf.Min(range.x, range.y);
        float max = Mathf.Max(range.x, range.y);
        float sigma = Mathf.Max(0.0001f, stdDev);

        float u1 = Mathf.Clamp(Random.value, 0.0001f, 0.9999f);
        float u2 = Random.value;
        float z0 = Mathf.Sqrt(-2f * Mathf.Log(u1)) * Mathf.Cos(2f * Mathf.PI * u2);

        float sample = 1f + z0 * sigma;
        return Mathf.Clamp(sample, min, max);
    }

    private List<Transform[]> GetAvailablePaths(params Transform[][] candidates)
    {
        List<Transform[]> paths = new List<Transform[]>();
        foreach (Transform[] path in candidates)
        {
            if (path != null && path.Length > 0)
                paths.Add(path);
        }

        return paths;
    }

    private List<Transform[]> BuildPathAssignments(List<Transform[]> availablePaths, int aiCount)
    {
        List<Transform[]> assignments = new List<Transform[]>(aiCount);
        if (aiCount <= 0)
            return assignments;

        if (availablePaths.Count == 0)
        {
            for (int i = 0; i < aiCount; i++)
                assignments.Add(null);
            return assignments;
        }

        List<Transform[]> shuffled = new List<Transform[]>(availablePaths);
        ShufflePathList(shuffled);

        for (int i = 0; i < aiCount; i++)
            assignments.Add(shuffled[i % shuffled.Count]);

        return assignments;
    }

    private void ShufflePathList(List<Transform[]> paths)
    {
        for (int i = paths.Count - 1; i > 0; i--)
        {
            int swapIndex = Random.Range(0, i + 1);
            (paths[i], paths[swapIndex]) = (paths[swapIndex], paths[i]);
        }
    }

    private Transform[] GetRandomFromList(List<Transform[]> paths)
    {
        List<Transform[]> validPaths = new List<Transform[]>();
        foreach (var path in paths)
        {
            if (path != null && path.Length > 0)
                validPaths.Add(path);
        }

        if (validPaths.Count == 0)
        {
            ;
            return null;
        }

        return validPaths[Random.Range(0, validPaths.Count)];
    }

    private Transform[] GetRandomSwimPath() => GetRandomFromList(new List<Transform[]> { swimPath1, swimPath2 });
    private Transform[] GetRandomBikePath() => GetRandomFromList(new List<Transform[]> { bikePath1, bikePath2 });
    private Transform[] GetRandomRunPath()  => GetRandomFromList(new List<Transform[]> { runPath1,  runPath2  });

    public List<NetworkedAIOpponent> GetActiveOpponents() => activeOpponents;

    [Server]
    public void StartRace()
    {
        raceRunning = true;

        foreach (var opponent in activeOpponents)
        {
            if (opponent != null)
                opponent.StartRace();
        }
        ;
    }

    [Server]
    public void StopRace()
    {
        raceRunning = false;

        foreach (var opponent in activeOpponents)
        {
            if (opponent != null)
                opponent.StopRace();
        }
        ;
    }

    [Server]
    public void RemoveAllOpponents()
    {
        foreach (var opponent in activeOpponents)
        {
            if (opponent == null) continue;

            if (usedSpawnPoints.ContainsKey(opponent))
            {
                usedSpawnPoints[opponent].Release();
                usedSpawnPoints.Remove(opponent);
            }

            if (opponent.NetworkObject != null)
                ServerManager.Despawn(opponent.gameObject);
        }

        activeOpponents.Clear();
        usedSpawnPoints.Clear();
        aiProfiles.Clear();
        aiSegments.Clear();
        aiElapsedRaceTimes.Clear();
        aiFinishTimesReported.Clear();
        raceRunning = false;
        ;
    }
}
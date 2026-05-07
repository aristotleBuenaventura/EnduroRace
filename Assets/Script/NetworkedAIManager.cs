using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using FishNet.Object;
using FishNet.Connection;

public class NetworkedAIManager : NetworkBehaviour
{
    private class BikeTransitionAssignment
    {
        public Transform[] bikeApproachPath;
        public Collider swimToBikeTrigger;
        public int bikeSlotIndex = -1;
        public bool bikeHidden = false;
    }

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

    [Header("Swim To Bike Trigger Slots (Attach 20)")]
    [Tooltip("Attach dito yung 20 swimtobike colliders. Imamatch ito by index sa Bike Approach Paths.")]
    public List<Collider> bikeTransitionSlots = new List<Collider>();

    [Header("Bike Approach Paths (Separate, Attach 20)")]
    [Tooltip("Attach dito yung 20 transforms (bike targets) na pupuntahan muna ng AI bago mag bike transition.")]
    public List<Transform> bikeApproachPaths = new List<Transform>();

    [Header("Bike Slots (Attach 20)")]
    [Tooltip("Attach dito yung 20 bike GameObjects. Matatago ang assigned bike kapag naabot ng AI ang transition slot.")]
    public List<GameObject> bikeSlots = new List<GameObject>();

    [Header("Bike Slot Recovery (Anti-Stuck)")]
    [Tooltip("Unused for normal pickup. Keep at 0 para strict collider-only bike pickup.")]
    public float bikeSlotTriggerPadding = 0f;
    [Tooltip("Kapag lumampas dito (seconds) habang papunta sa bike slot, auto pickup na.")]
    public float bikeSlotAutoPickupTimeout = 10f;
    [Tooltip("Kapag walang meaningful movement nang ganitong katagal, auto pickup na.")]
    public float bikeSlotStuckTimeout = 4f;
    [Tooltip("Minimum movement (meters) para ma-consider na may progress sa bike slot.")]
    public float bikeSlotMinProgressDistance = 0.15f;

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
    private Dictionary<NetworkedAIOpponent, BikeTransitionAssignment> aiBikeSlots = new Dictionary<NetworkedAIOpponent, BikeTransitionAssignment>();
    private HashSet<NetworkedAIOpponent> aiHeadingToBikeSlot = new HashSet<NetworkedAIOpponent>();
    private Dictionary<NetworkedAIOpponent, BikeApproachProgress> aiBikeApproachProgress = new Dictionary<NetworkedAIOpponent, BikeApproachProgress>();
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

    private class BikeApproachProgress
    {
        public Vector3 lastPosition;
        public float stuckTimer;
        public float totalApproachTime;
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
                    if (aiHeadingToBikeSlot.Contains(opponent))
                    {
                        TryTransitionToBikeIfReachedSlot(opponent);
                    }
                    else if (!TrySendToAssignedBikeSlot(opponent))
                    {
                        TransitionOpponent(opponent, NetworkedAIOpponent.AISegment.Bike);
                    }
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

            if (currentSeg == NetworkedAIOpponent.AISegment.Swim && aiHeadingToBikeSlot.Contains(opponent))
            {
                TryTransitionToBikeIfReachedSlot(opponent);
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
        bool keepCurrentPosition = false;

        switch (newSegment)
        {
            case NetworkedAIOpponent.AISegment.Bike:
                if (aiProfiles.TryGetValue(opponent, out AIProfile bikeProfile))
                    newPath = bikeProfile.bikePath;
                if (newPath == null || newPath.Length == 0)
                    newPath = GetRandomBikePath();
                if (TryGetBikeSlotTransitionPosition(opponent, out Vector3 bikeSlotPosition))
                {
                    transitionPos = bikeSlotPosition;
                }
                else if (swimToBikeTransition != null)
                {
                    transitionPos = swimToBikeTransition.position;
                }
                else
                {
                    keepCurrentPosition = true;
                }
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

        if (keepCurrentPosition)
            transitionPos = opponent.transform.position;

        // Update tracked segment BEFORE calling transition so Update() doesn't re-trigger
        aiSegments[opponent] = newSegment;
        aiHeadingToBikeSlot.Remove(opponent);
        aiBikeApproachProgress.Remove(opponent);

        ;

        // FIX: Use the overload that takes the path so index resets correctly
        opponent.TransitionToSegment(newSegment, transitionPos, newPath);
    }

    [Server]
    public void SpawnOpponents()
    {
        ;

        SetAllBikeSlotsActive(true);

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
        List<BikeTransitionAssignment> bikeSlotAssignments = BuildBikeSlotAssignments(reserved.Count);
        List<byte> swimStrokeAssignments = BuildSwimStrokeAssignments(reserved.Count);
        int superSlowIndex = forceOneSuperSlowAI && reserved.Count > 0 ? Random.Range(0, reserved.Count) : -1;

        for (int i = 0; i < reserved.Count; i++)
            SpawnOpponent(
                i,
                reserved.Count,
                reserved[i],
                swimAssignments[i],
                bikeAssignments[i],
                runAssignments[i],
                bikeSlotAssignments[i],
                swimStrokeAssignments[i],
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
        BikeTransitionAssignment assignedBikeSlot,
        byte swimStrokeStyle,
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

        // Stroke variant (isSwimming / isSwimming2 / isSwimming3) — applied in OnStartServer after Spawn.
        ai.PresetSwimStrokeStyleForSpawn(swimStrokeStyle);

        if (ai.runnerModel != null) ai.runnerModel.SetActive(true);
        if (ai.cyclistModel != null) ai.cyclistModel.SetActive(false);

        ServerManager.Spawn(aiGO);
        ai.SetColorPaletteIndex(index % AIColors.PaletteCount);

        usedSpawnPoints[ai] = spawnPoint;
        aiProfiles[ai] = profile;
        aiBikeSlots[ai] = assignedBikeSlot;
        if (assignedBikeSlot != null)
        {
            Debug.Log($"[NetworkedAIManager] {ai.opponentName} assigned bike slot index {assignedBikeSlot.bikeSlotIndex} (display #{assignedBikeSlot.bikeSlotIndex + 1}).");
        }
        else
        {
            Debug.LogWarning($"[NetworkedAIManager] {ai.opponentName} has no assigned bike slot. Using default swim-to-bike behavior.");
        }
        aiElapsedRaceTimes[ai] = 0f;
        aiFinishTimesReported.Remove(ai);
        aiHeadingToBikeSlot.Remove(ai);
        aiBikeApproachProgress.Remove(ai);

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

    /// <summary>Each AI gets 0/1/2/3 cycled then shuffled so a field of 4+ usually shows all four swim styles.</summary>
    private List<byte> BuildSwimStrokeAssignments(int aiCount)
    {
        var list = new List<byte>(aiCount);
        for (int i = 0; i < aiCount; i++)
            list.Add((byte)(i % 4));

        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            (list[i], list[j]) = (list[j], list[i]);
        }

        return list;
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

    private List<BikeTransitionAssignment> BuildBikeSlotAssignments(int aiCount)
    {
        List<BikeTransitionAssignment> assignments = new List<BikeTransitionAssignment>(aiCount);
        if (aiCount <= 0)
            return assignments;

        List<BikeTransitionAssignment> validSlots = GetValidBikeTransitionSlots();
        if (validSlots.Count == 0)
        {
            for (int i = 0; i < aiCount; i++)
                assignments.Add(null);
            return assignments;
        }

        ShuffleBikeSlotList(validSlots);

        for (int i = 0; i < aiCount; i++)
        {
            if (i < validSlots.Count)
                assignments.Add(validSlots[i]);
            else
                assignments.Add(null);
        }

        if (aiCount > validSlots.Count)
        {
            Debug.LogWarning($"[NetworkedAIManager] AI count ({aiCount}) exceeds bike transition slots ({validSlots.Count}). No duplicates assigned; extra AIs will use default swim-to-bike transition.");
        }

        return assignments;
    }

    private List<BikeTransitionAssignment> GetValidBikeTransitionSlots()
    {
        List<BikeTransitionAssignment> validSlots = new List<BikeTransitionAssignment>();

        int pairedCount = Mathf.Min(bikeApproachPaths.Count, Mathf.Min(bikeTransitionSlots.Count, bikeSlots.Count));
        if (pairedCount > 0)
        {
            for (int i = 0; i < pairedCount; i++)
            {
                Transform approachTarget = bikeApproachPaths[i];
                Collider triggerCollider = bikeTransitionSlots[i];
                GameObject bikeSlotObject = bikeSlots[i];
                if (approachTarget == null)
                    continue;
                if (triggerCollider == null)
                    continue;
                if (bikeSlotObject == null)
                    continue;

                validSlots.Add(new BikeTransitionAssignment
                {
                    bikeApproachPath = new Transform[] { approachTarget },
                    swimToBikeTrigger = triggerCollider,
                    bikeSlotIndex = i
                });
            }

            if (bikeApproachPaths.Count != bikeTransitionSlots.Count || bikeApproachPaths.Count != bikeSlots.Count)
            {
                Debug.LogWarning($"[NetworkedAIManager] Bike Approach Paths ({bikeApproachPaths.Count}), Swim To Bike Trigger Slots ({bikeTransitionSlots.Count}), and Bike Slots ({bikeSlots.Count}) count mismatch. Using matched index pairs only.");
            }
        }

        return validSlots;
    }

    private void ShuffleBikeSlotList(List<BikeTransitionAssignment> slots)
    {
        for (int i = slots.Count - 1; i > 0; i--)
        {
            int swapIndex = Random.Range(0, i + 1);
            (slots[i], slots[swapIndex]) = (slots[swapIndex], slots[i]);
        }
    }

    [Server]
    private bool TrySendToAssignedBikeSlot(NetworkedAIOpponent opponent)
    {
        if (!aiBikeSlots.TryGetValue(opponent, out BikeTransitionAssignment slot) || slot == null)
            return false;
        if (slot.bikeApproachPath == null || slot.bikeApproachPath.Length == 0)
            return false;

        aiHeadingToBikeSlot.Add(opponent);
        aiBikeApproachProgress[opponent] = new BikeApproachProgress
        {
            lastPosition = opponent.transform.position,
            stuckTimer = 0f,
            totalApproachTime = 0f
        };
        opponent.SetWaypointPath(slot.bikeApproachPath);
        return true;
    }

    [Server]
    private void TryTransitionToBikeIfReachedSlot(NetworkedAIOpponent opponent)
    {
        if (!aiBikeSlots.TryGetValue(opponent, out BikeTransitionAssignment slot) || slot == null)
            return;
        if (slot.swimToBikeTrigger == null)
            return;

        bool reachedTrigger = slot.swimToBikeTrigger.bounds.Contains(opponent.transform.position);
        bool shouldForcePickup = false;
        string forceReason = string.Empty;

        if (!reachedTrigger)
            shouldForcePickup = ShouldForceBikePickup(opponent, out forceReason);

        if (!reachedTrigger && !shouldForcePickup)
            return;

        HideBikeSlotForAssignment(slot);
        if (shouldForcePickup)
        {
            Debug.LogWarning($"[NetworkedAIManager] Forced bike pickup for {opponent.opponentName}. Reason: {forceReason}");
        }
        TransitionOpponent(opponent, NetworkedAIOpponent.AISegment.Bike);
    }

    [Server]
    private bool ShouldForceBikePickup(NetworkedAIOpponent opponent, out string reason)
    {
        reason = string.Empty;
        if (opponent == null)
            return false;

        if (!aiBikeApproachProgress.TryGetValue(opponent, out BikeApproachProgress progress) || progress == null)
            return false;

        Vector3 currentPosition = opponent.transform.position;
        float delta = Vector3.Distance(currentPosition, progress.lastPosition);
        progress.totalApproachTime += Time.deltaTime;

        if (delta >= Mathf.Max(0.01f, bikeSlotMinProgressDistance))
        {
            progress.stuckTimer = 0f;
        }
        else
        {
            progress.stuckTimer += Time.deltaTime;
        }

        progress.lastPosition = currentPosition;

        if (progress.stuckTimer >= Mathf.Max(0.1f, bikeSlotStuckTimeout))
        {
            reason = $"stuck for {progress.stuckTimer:F2}s";
            return true;
        }

        if (progress.totalApproachTime >= Mathf.Max(0.1f, bikeSlotAutoPickupTimeout))
        {
            reason = $"timeout {progress.totalApproachTime:F2}s";
            return true;
        }

        return false;
    }

    private bool TryGetBikeSlotTransitionPosition(NetworkedAIOpponent opponent, out Vector3 position)
    {
        position = opponent.transform.position;

        if (!aiBikeSlots.TryGetValue(opponent, out BikeTransitionAssignment slot) || slot == null)
            return false;
        if (slot.swimToBikeTrigger == null)
            return false;

        position = slot.swimToBikeTrigger.bounds.center;
        return true;
    }

    [Server]
    private void HideBikeSlotForAssignment(BikeTransitionAssignment slot)
    {
        if (slot == null || slot.bikeHidden)
            return;

        if (slot.bikeSlotIndex < 0 || slot.bikeSlotIndex >= bikeSlots.Count)
            return;

        slot.bikeHidden = true;
        SetBikeSlotActive(slot.bikeSlotIndex, false);
    }

    [Server]
    private void SetAllBikeSlotsActive(bool isActive)
    {
        for (int i = 0; i < bikeSlots.Count; i++)
            SetBikeSlotActive(i, isActive);
    }

    [Server]
    private void SetBikeSlotActive(int slotIndex, bool isActive)
    {
        if (slotIndex < 0 || slotIndex >= bikeSlots.Count)
            return;

        GameObject bike = bikeSlots[slotIndex];
        if (bike != null)
            bike.SetActive(isActive);

        RpcSetBikeSlotActive(slotIndex, isActive);
    }

    [ObserversRpc]
    private void RpcSetBikeSlotActive(int slotIndex, bool isActive)
    {
        if (slotIndex < 0 || slotIndex >= bikeSlots.Count)
            return;

        GameObject bike = bikeSlots[slotIndex];
        if (bike != null)
            bike.SetActive(isActive);
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
        SetAllBikeSlotsActive(true);

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
        aiBikeSlots.Clear();
        aiHeadingToBikeSlot.Clear();
        aiBikeApproachProgress.Clear();
        aiSegments.Clear();
        aiElapsedRaceTimes.Clear();
        aiFinishTimesReported.Clear();
        raceRunning = false;
        ;
    }
}
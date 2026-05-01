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
    public Vector2 swimAnimationSpeedRange = new Vector2(0.88f, 1.15f);
    public Vector2 bikeAnimationSpeedRange = new Vector2(0.9f, 1.12f);
    public Vector2 runAnimationSpeedRange = new Vector2(0.9f, 1.18f);

    [Header("Race Start Spread")]
    public Vector2 raceStartDelayRange = new Vector2(0f, 1.25f);

    private List<NetworkedAIOpponent> activeOpponents = new List<NetworkedAIOpponent>();
    private Dictionary<NetworkedAIOpponent, SpawnPoint> usedSpawnPoints = new Dictionary<NetworkedAIOpponent, SpawnPoint>();
    private Dictionary<NetworkedAIOpponent, AIProfile> aiProfiles = new Dictionary<NetworkedAIOpponent, AIProfile>();

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
                    ;
                    aiSegments[opponent] = (NetworkedAIOpponent.AISegment)99; // sentinel: finished
                }
            }
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

        for (int i = 0; i < reserved.Count; i++)
            SpawnOpponent(i, reserved.Count, reserved[i], swimAssignments[i], bikeAssignments[i], runAssignments[i]);

        ;
    }

    [Server]
    private void SpawnOpponent(
        int index,
        int totalOpponents,
        SpawnPoint spawnPoint,
        Transform[] assignedSwimPath,
        Transform[] assignedBikePath,
        Transform[] assignedRunPath)
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

        AIProfile profile = CreateProfile(index, totalOpponents, assignedSwimPath, assignedBikePath, assignedRunPath);
        ai.baseSpeed *= profile.speedMultiplier;
        ai.ConfigureMovementProfile(
            profile.lateralOffset,
            profile.turnSpeed,
            profile.waypointReachDistance,
            profile.swimAnimSpeed,
            profile.bikeAnimSpeed,
            profile.runAnimSpeed,
            profile.startDelay
        );

        if (ai.runnerModel != null) ai.runnerModel.SetActive(true);
        if (ai.cyclistModel != null) ai.cyclistModel.SetActive(false);

        ServerManager.Spawn(aiGO);

        usedSpawnPoints[ai] = spawnPoint;
        aiProfiles[ai] = profile;

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

    private AIProfile CreateProfile(int slotIndex, int totalOpponents, Transform[] swimPath, Transform[] bikePath, Transform[] runPath)
    {
        float normalizedLane = totalOpponents <= 1
            ? 0.5f
            : (float)slotIndex / (totalOpponents - 1f);

        float laneCenter = Mathf.Lerp(minLaneOffset, maxLaneOffset, normalizedLane);
        float laneOffset = laneCenter + Random.Range(-Mathf.Abs(laneJitter), Mathf.Abs(laneJitter));

        return new AIProfile
        {
            swimPath = swimPath,
            bikePath = bikePath,
            runPath = runPath,
            speedMultiplier = RandomRange(speedMultiplierRange),
            lateralOffset = Mathf.Clamp(laneOffset, minLaneOffset, maxLaneOffset),
            turnSpeed = Random.Range(minRotationSpeed, maxRotationSpeed),
            waypointReachDistance = Random.Range(minWaypointReachDistance, maxWaypointReachDistance),
            swimAnimSpeed = RandomRange(swimAnimationSpeedRange),
            bikeAnimSpeed = RandomRange(bikeAnimationSpeedRange),
            runAnimSpeed = RandomRange(runAnimationSpeedRange),
            startDelay = RandomRange(raceStartDelayRange),
        };
    }

    private float RandomRange(Vector2 range)
    {
        float min = Mathf.Min(range.x, range.y);
        float max = Mathf.Max(range.x, range.y);
        return Random.Range(min, max);
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
        ;
    }
}
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

    private List<NetworkedAIOpponent> activeOpponents = new List<NetworkedAIOpponent>();
    private Dictionary<NetworkedAIOpponent, SpawnPoint> usedSpawnPoints = new Dictionary<NetworkedAIOpponent, SpawnPoint>();

    // Track each AI's current segment so we know when to transition
    private Dictionary<NetworkedAIOpponent, NetworkedAIOpponent.AISegment> aiSegments
        = new Dictionary<NetworkedAIOpponent, NetworkedAIOpponent.AISegment>();

    public override void OnStartServer()
    {
        base.OnStartServer();
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
                    TransitionOpponent(opponent, NetworkedAIOpponent.AISegment.Bike);
                }
                else if (currentSeg == NetworkedAIOpponent.AISegment.Bike)
                {
                    TransitionOpponent(opponent, NetworkedAIOpponent.AISegment.Run);
                }
                else if (currentSeg == NetworkedAIOpponent.AISegment.Run)
                {
                    // AI finished the race — mark done so we stop checking
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
                newPath = GetRandomBikePath();
                if (swimToBikeTransition != null)
                    transitionPos = swimToBikeTransition.position;
                break;

            case NetworkedAIOpponent.AISegment.Run:
                newPath = GetRandomRunPath();
                if (bikeToRunTransition != null)
                    transitionPos = bikeToRunTransition.position;
                break;
        }

        if (newPath == null || newPath.Length == 0)
        {
            return;
        }

        // Update tracked segment BEFORE calling transition so Update() doesn't re-trigger
        aiSegments[opponent] = newSegment;


        // FIX: Use the overload that takes the path so index resets correctly
        opponent.TransitionToSegment(newSegment, transitionPos, newPath);
    }

    [Server]
    public void SpawnOpponents()
    {

        if (aiOpponentPrefab == null && aiOpponentPrefab1 == null)
        {
            return;
        }

        List<SpawnPoint> reserved = new List<SpawnPoint>();
        for (int i = 0; i < numberOfOpponents; i++)
        {
            SpawnPoint sp = SpawnPoint.GetRandomSpawnPoint();
            if (sp != null)
            {
                reserved.Add(sp);
            }
            else
            {
            }
        }

        for (int i = 0; i < reserved.Count; i++)
            SpawnOpponent(i, reserved[i]);

    }

    [Server]
    private void SpawnOpponent(int index, SpawnPoint spawnPoint)
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
            spawnPoint.Release();
            return;
        }

        if (spawnPoint == null)
        {
            return;
        }

        spawnPoint.GetSpawnTransform(out Vector3 position, out Quaternion rotation);
        GameObject aiGO = Instantiate(selectedPrefab, position, rotation);

        NetworkedAIOpponent ai = aiGO.GetComponent<NetworkedAIOpponent>();
        if (ai == null)
        {
            Destroy(aiGO);
            spawnPoint.Release();
            return;
        }

        ai.opponentName = aiNames[index % aiNames.Length];
        aiGO.name = "AI_" + ai.opponentName;
        ai.baseSpeed = Random.Range(minSpeed, maxSpeed);

        if (ai.runnerModel != null) ai.runnerModel.SetActive(true);
        if (ai.cyclistModel != null) ai.cyclistModel.SetActive(false);

        ServerManager.Spawn(aiGO);

        usedSpawnPoints[ai] = spawnPoint;

        Transform[] chosenSwimPath = GetRandomSwimPath();
        if (chosenSwimPath != null && chosenSwimPath.Length > 0)
        {
            ai.SetWaypointPath(chosenSwimPath);
        }
        else
        {
        }

        // Track starting segment
        aiSegments[ai] = NetworkedAIOpponent.AISegment.Swim;

        activeOpponents.Add(ai);
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
    }

    [Server]
    public void StopRace()
    {
        foreach (var opponent in activeOpponents)
        {
            if (opponent != null)
                opponent.StopRace();
        }
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
        aiSegments.Clear();
    }
}
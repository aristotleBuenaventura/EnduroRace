using UnityEngine;
using System.Collections.Generic;

public class AIOpponentManager : MonoBehaviour
{
    [Header("AI Prefab")]
    public GameObject aiOpponentPrefab;   // Male
    public GameObject aiOpponentPrefab1;  // Female
    public bool randomizeGender = true;

    [Header("Spawn Settings")]
    public Transform[] spawnPoints;
    public int numberOfOpponents = 3;
    public float spawnDelay = 0.5f;

    [Header("Swim Paths (5 routes)")]
    public Transform[] swimPath1;
    public Transform[] swimPath2;

    [Header("Bike Paths (5 routes)")]
    public Transform[] bikePath1;
    public Transform[] bikePath2;

    [Header("Run Paths (5 routes)")]
    public Transform[] runPath1;
    public Transform[] runPath2;

    [Header("Segment Transition Points")]
    public Transform swimToBikeTransition;
    public Transform bikeToRunTransition;

    [Header("AI Names")]
    public string[] aiNames = { "Alex", "Jordan", "Taylor", "Morgan", "Casey" };

    [Header("Difficulty Settings")]
    public float minSpeed = 3.5f;
    public float maxSpeed = 5.5f;

    private List<AIOpponentController> activeOpponents = new List<AIOpponentController>();
    private RaceManagerTutorial raceManager;

    private void Start()
    {
        raceManager = Object.FindFirstObjectByType<RaceManagerTutorial>();
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
            Debug.LogWarning("No valid paths found! Make sure you assigned waypoints in the Inspector.");
            return null;
        }

        return validPaths[Random.Range(0, validPaths.Count)];
    }

    private Transform[] GetRandomSwimPath()
    {
        return GetRandomFromList(new List<Transform[]> { swimPath1, swimPath2 });
    }

    private Transform[] GetRandomBikePath()
    {
        return GetRandomFromList(new List<Transform[]> { bikePath1, bikePath2 });
    }

    private Transform[] GetRandomRunPath()
    {
        return GetRandomFromList(new List<Transform[]> { runPath1, runPath2 });
    }

    public void SpawnOpponents()
    {
        if (this == null)
        {
            Debug.LogError("AIOpponentManager instance is NULL!");
            return;
        }

        Debug.Log("=== SpawnOpponents called ===");

        if (aiOpponentPrefab == null && aiOpponentPrefab1 == null)
        {
            Debug.LogError("Cannot spawn AI: both prefabs are NULL!");
            return;
        }

        if (spawnPoints == null || spawnPoints.Length == 0)
        {
            Debug.LogError("Cannot spawn AI: No spawn points assigned!");
            return;
        }

        for (int i = 0; i < numberOfOpponents; i++)
        {
            SpawnOpponent(i);
        }

        Debug.Log($"=== Spawn complete. Active opponents: {activeOpponents.Count} ===");
    }

    private void SpawnOpponent(int index)
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
            Debug.LogError("Selected AI prefab is NULL!");
            return;
        }

        Transform spawnPoint = spawnPoints[index % spawnPoints.Length];
        GameObject aiGO = Instantiate(selectedPrefab, spawnPoint.position, spawnPoint.rotation);
        aiGO.SetActive(true);

        AIOpponentController ai = aiGO.GetComponent<AIOpponentController>();
        if (ai != null)
        {
            ai.opponentName = aiNames[index % aiNames.Length];
            aiGO.name = "AI_" + ai.opponentName;
            ai.baseSpeed = Random.Range(minSpeed, maxSpeed);

            if (ai.nameTagText != null)
                ai.nameTagText.text = ai.opponentName;
            else
                Debug.LogWarning($"  ⚠ {ai.opponentName} has no nameTagText assigned on AIOpponentController.");

            if (ai.runnerModel != null) ai.runnerModel.SetActive(true);
            if (ai.cyclistModel != null) ai.cyclistModel.SetActive(false);

            Transform[] chosenSwimPath = GetRandomSwimPath();
            if (chosenSwimPath != null)
            {
                ai.SetWaypointPath(chosenSwimPath);
                Debug.Log($"  ✓ {ai.opponentName} randomly assigned swim path with {chosenSwimPath.Length} waypoints");
            }
            else
            {
                Debug.LogError($"  ✗ Could not assign swim path to {ai.opponentName}!");
            }

            activeOpponents.Add(ai);
        }
        else
        {
            Debug.LogError("AIOpponentController not found on spawned prefab!");
        }
    }

    public void TransitionOpponentsToSegment(AIOpponentController.AISegment segment)
    {
        Debug.Log($"=== TransitionOpponentsToSegment called for {segment} ===");
        foreach (var opponent in activeOpponents)
        {
            if (opponent == null) continue;

            Transform[] newPath = null;
            Vector3 transitionPos = opponent.transform.position;

            switch (segment)
            {
                case AIOpponentController.AISegment.Bike:
                    newPath = GetRandomBikePath();
                    if (swimToBikeTransition != null)
                        transitionPos = swimToBikeTransition.position;
                    break;

                case AIOpponentController.AISegment.Run:
                    newPath = GetRandomRunPath();
                    if (bikeToRunTransition != null)
                        transitionPos = bikeToRunTransition.position;
                    break;
            }

            if (newPath != null)
            {
                Debug.Log($"  ✓ {opponent.opponentName} randomly assigned {segment} path with {newPath.Length} waypoints");
                opponent.SetWaypointPath(newPath);
                opponent.TransitionToSegment(segment, transitionPos);
            }
        }
    }

    public List<AIOpponentController> GetActiveOpponents() => activeOpponents;

    public void RemoveAllOpponents()
    {
        foreach (var opponent in activeOpponents)
            if (opponent != null) Destroy(opponent.gameObject);
        activeOpponents.Clear();
    }

    /// <summary>
    /// Pauses/resumes AI movement using a flag instead of disabling the component.
    /// Disabling the component kills coroutines (like TransitionRoutine) which causes
    /// AI to get permanently stuck with currentSpeed = 0 after segment transitions.
    /// </summary>
    public void SetOpponentsPaused(bool paused)
    {
        foreach (var opponent in activeOpponents)
        {
            if (opponent == null) continue;
            opponent.isPaused = paused;
        }
    }
}
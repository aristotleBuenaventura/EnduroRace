using UnityEngine;
using System.Collections.Generic;

public class ObstacleSpawner : MonoBehaviour
{
    [Header("References")]
    public GameObject[] obstaclePrefabs;   // Obstacle choices
    public Transform[] roadNodes;          // Node positions

    [Header("Spawn Settings")]
    public float roadHalfWidth = 2.5f;     // Left-right offset range
    public float spawnY = 1.3f;            // Height for obstacles

    void Start()
    {
        SpawnAllObstacles();
    }

    void SpawnAllObstacles()
    {
        foreach (Transform node in roadNodes)
        {
            RoadNode rn = node.GetComponent<RoadNode>();
            if (rn != null && !rn.isOccupied)
            {
                SpawnObstacleAtNode(node);
                rn.isOccupied = true; // Mark node as used
            }
        }
    }

    void SpawnObstacleAtNode(Transform node)
    {
        // Pick a random obstacle
        int index = Random.Range(0, obstaclePrefabs.Length);
        GameObject prefab = obstaclePrefabs[index];

        // Lateral o ffset (side-to-side randomness)
        float lateralOffset = Random.Range(-roadHalfWidth, roadHalfWidth);

        // Final position
        Vector3 spawnPos = node.position + node.right * lateralOffset;
        spawnPos.y = spawnY;

        // Spawn object
        Instantiate(prefab, spawnPos, prefab.transform.rotation);
    }
}

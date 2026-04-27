using UnityEngine;
using System.Collections.Generic;

public class SpawnPoint : MonoBehaviour
{
    [Header("Spawn Point Settings")]
    public bool isActive = true;
    public bool showGizmo = true;
    public Color gizmoColor = Color.green;

    private bool isOccupied = false;

    private static List<SpawnPoint> allSpawnPoints = new List<SpawnPoint>();

    private void OnEnable()
    {
        if (!allSpawnPoints.Contains(this))
            allSpawnPoints.Add(this);
    }

    private void OnDisable()
    {
        allSpawnPoints.Remove(this);
    }

    public void Release()
    {
        isOccupied = false;
    }

    public static SpawnPoint GetRandomSpawnPoint()
    {
        List<SpawnPoint> available = allSpawnPoints.FindAll(sp => sp.isActive && !sp.isOccupied);

        if (available.Count == 0)
        {
            available = allSpawnPoints.FindAll(sp => sp.isActive);
        }

        if (available.Count == 0)
        {
            return null;
        }

        SpawnPoint chosen = available[Random.Range(0, available.Count)];
        chosen.isOccupied = true;
        return chosen;
    }

    public static SpawnPoint GetNextSpawnPoint()
    {
        return GetRandomSpawnPoint();
    }

    public void GetSpawnTransform(out Vector3 position, out Quaternion rotation)
    {
        position = transform.position;
        rotation = transform.rotation;
    }

    public static List<SpawnPoint> GetAllActiveSpawnPoints()
    {
        return allSpawnPoints.FindAll(sp => sp.isActive);
    }

    private void OnDrawGizmos()
    {
        if (!showGizmo) return;

        Gizmos.color = isOccupied ? Color.red : Color.green;
        Gizmos.DrawWireSphere(transform.position, 0.5f);
        Gizmos.DrawRay(transform.position, transform.forward * 1f);
        Gizmos.DrawCube(transform.position + Vector3.up * 0.1f, Vector3.one * 0.2f);
    }

    private void OnDrawGizmosSelected()
    {
        if (!showGizmo) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 1f);
    }
}
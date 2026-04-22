using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public GameObject wavePrefab;
    public float spawnInterval = 10f;
    public Vector3 waveDirection = Vector3.forward;

    private float timer;

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            timer = 0f;
            SpawnWave();
        }
    }

    private void SpawnWave()
    {
        if (wavePrefab == null) return;
        Quaternion waveRotation = Quaternion.Euler(-90f, 0f, -90f);
        GameObject wave = Instantiate(wavePrefab, transform.position, waveRotation);
        WaveObstacle wo = wave.GetComponent<WaveObstacle>();
        if (wo != null)
            wo.moveDirection = waveDirection;
    }
}
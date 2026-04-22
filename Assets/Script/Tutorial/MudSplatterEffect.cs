using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MudSplatterEffect : MonoBehaviour
{
    [Header("Droplet Settings")]
    [SerializeField] private GameObject dropletPrefab; // Your water droplet 3D model
    [SerializeField] private Transform spawnPoint; // Where droplets spawn (player's head)
    
    [Header("Spawn Settings")]
    [SerializeField] private float spawnInterval = 0.3f; // How often to spawn droplets
    [SerializeField] private int dropletsPerBurst = 1; // Usually 1 droplet at a time for sweat
    [SerializeField] private float spawnRadius = 0.15f; // Small random offset around head
    
    [Header("Physics Settings")]
    [SerializeField] private float initialForceMin = 0.3f; // Small sideways push
    [SerializeField] private float initialForceMax = 0.8f; // Small sideways push
    [SerializeField] private float upwardForce = 0.2f; // Tiny initial pop before falling
    [SerializeField] private float gravity = 12f; // Stronger gravity for faster fall
    [SerializeField] private float dropletLifetime = 1.5f; // Shorter lifetime
    
    [Header("Visual Settings")]
    [SerializeField] private Vector3 dropletScale = new Vector3(0.05f, 0.08f, 0.05f); // Small tear-drop shape
    [SerializeField] private Color sweatColor = new Color(0.7f, 0.85f, 1f, 0.9f); // Light blue for sweat
    
    [Header("Performance")]
    [SerializeField] private int maxDroplets = 20; // Fewer droplets needed
    
    private bool isActive = false;
    private float spawnTimer = 0f;
    private List<GameObject> activeDroplets = new List<GameObject>();
    private Queue<GameObject> dropletPool = new Queue<GameObject>();

    void Start()
    {
        if (dropletPrefab == null)
        {
            Debug.LogError("MudSplatterEffect: Droplet Prefab is not assigned!");
            return;
        }

        if (spawnPoint == null)
        {
            Debug.LogWarning("MudSplatterEffect: Spawn Point is not assigned! Using transform.position instead.");
        }

        // Pre-instantiate pool of droplets for performance
        for (int i = 0; i < maxDroplets; i++)
        {
            GameObject droplet = Instantiate(dropletPrefab);
            droplet.SetActive(false);
            dropletPool.Enqueue(droplet);
            
            // Apply sweat color if the droplet has a renderer
            Renderer renderer = droplet.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.color = sweatColor;
            }
        }
    }

    void Update()
    {
        if (isActive)
        {
            spawnTimer += Time.deltaTime;
            
            if (spawnTimer >= spawnInterval)
            {
                SpawnDropletBurst();
                spawnTimer = 0f;
            }
        }
        
        // Update all active droplets
        UpdateDroplets();
    }

    public void StartEffect()
    {
        Debug.Log("MudSplatterEffect: StartEffect() called!");
        
        if (dropletPrefab == null)
        {
            Debug.LogError("Cannot start effect - Droplet Prefab is null!");
            return;
        }
        
        isActive = true;
        spawnTimer = 0f;
    }

    public void StopEffect()
    {
        Debug.Log("MudSplatterEffect: StopEffect() called!");
        isActive = false;
    }

    private void SpawnDropletBurst()
    {
        for (int i = 0; i < dropletsPerBurst; i++)
        {
            SpawnDroplet();
        }
    }

    private void SpawnDroplet()
    {
        if (dropletPool.Count == 0)
        {
            Debug.LogWarning("Droplet pool is empty!");
            return;
        }

        Debug.Log("Spawning droplet!"); // ADD THIS
        
        GameObject droplet = dropletPool.Dequeue();
        
        // Random position around spawn point (near head)
        Vector3 randomOffset = new Vector3(
            Random.Range(-spawnRadius, spawnRadius),
            Random.Range(-0.1f, 0.1f), // Small vertical offset
            Random.Range(-spawnRadius, spawnRadius)
        );
        
        Vector3 spawnPos = spawnPoint != null ? spawnPoint.position : transform.position;
        droplet.transform.position = spawnPos + randomOffset;
        droplet.transform.localScale = dropletScale;
        droplet.SetActive(true);

        // Sweat droplets fall DOWN with slight sideways motion
        Vector3 randomDirection = new Vector3(
            Random.Range(-1f, 1f),      // Left or right
            Random.Range(-0.8f, -0.3f), // Mostly downward
            Random.Range(-0.5f, 0.5f)   // Front or back
        ).normalized;

        float force = Random.Range(initialForceMin, initialForceMax);
        Vector3 initialVelocity = randomDirection * force + Vector3.up * upwardForce;

        // Attach or get droplet physics component
        MudDroplet mudDropletScript = droplet.GetComponent<MudDroplet>();
        if (mudDropletScript == null)
        {
            mudDropletScript = droplet.AddComponent<MudDroplet>();
        }
        
        mudDropletScript.Initialize(initialVelocity, gravity, dropletLifetime, this);
        
        activeDroplets.Add(droplet);
    }

    private void UpdateDroplets()
    {
        for (int i = activeDroplets.Count - 1; i >= 0; i--)
        {
            if (activeDroplets[i] == null || !activeDroplets[i].activeInHierarchy)
            {
                activeDroplets.RemoveAt(i);
            }
        }
    }

    public void ReturnDropletToPool(GameObject droplet)
    {
        droplet.SetActive(false);
        activeDroplets.Remove(droplet);
        dropletPool.Enqueue(droplet);
    }

    void OnDestroy()
    {
        // Clean up pooled droplets
        foreach (GameObject droplet in dropletPool)
        {
            if (droplet != null)
                Destroy(droplet);
        }
        
        foreach (GameObject droplet in activeDroplets)
        {
            if (droplet != null)
                Destroy(droplet);
        }
    }
}
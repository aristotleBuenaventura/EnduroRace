using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FoamTrailEffect : MonoBehaviour
{
    public static FoamTrailEffect Instance { get; private set; }

    [Header("Foam Settings")]
    [SerializeField] private GameObject foamDecalPrefab;
    [SerializeField] private float spawnInterval    = 0.1f;
    [SerializeField] private float foamLifetime     = 1.5f;
    [SerializeField] private float foamStartScale   = 0.3f;
    [SerializeField] private float foamEndScale     = 1.2f;
    [SerializeField] private float foamStartAlpha   = 0.8f;
    [SerializeField] private Color foamColor        = new Color(1f, 1f, 1f, 0.8f);

    [Header("Sprint Foam")]
    [SerializeField] private float sprintSpawnInterval = 0.05f;
    [SerializeField] private float sprintFoamScale     = 1.8f;

    private Transform  playerFeet;
    private NetworkPlayer networkPlayer;
    private float      spawnTimer  = 0f;
    private bool       isMoving    = false;
    private bool       isSprinting = false;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else { Destroy(gameObject); return; }
    }

    public void SetPlayerFeet(Transform feet)
    {
        playerFeet    = feet;
        networkPlayer = feet.GetComponentInParent<NetworkPlayer>();
    }

    public void UpdateMovement(bool moving, bool sprinting)
    {
        isMoving    = moving;
        isSprinting = sprinting;
    }

    private void Update()
    {
        // Only show foam when in water and moving
        if (playerFeet == null || networkPlayer == null) return;
        if (!networkPlayer.IsInWater.Value)
        {
            spawnTimer = 0f;
            return;
        }

        if (!isMoving)
        {
            spawnTimer = 0f;
            return;
        }

        float interval = isSprinting ? sprintSpawnInterval : spawnInterval;
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= interval)
        {
            spawnTimer = 0f;
            SpawnFoam();
        }
    }

    private void SpawnFoam()
    {
        if (foamDecalPrefab == null) return;

        // Spawn at water surface level
        Vector3 spawnPos  = playerFeet.position;
        spawnPos.y        = networkPlayer.WaterSurfaceY.Value;

        float scale       = isSprinting ? sprintFoamScale : foamStartScale;
        GameObject foam   = Instantiate(foamDecalPrefab, spawnPos,
                            Quaternion.Euler(90f, 0f, 0f));
        foam.transform.localScale = Vector3.one * scale;

        StartCoroutine(AnimateFoam(foam));
    }

    private IEnumerator AnimateFoam(GameObject foam)
    {
        if (foam == null) yield break;

        float elapsed  = 0f;
        float endScale = isSprinting ? sprintFoamScale * 1.5f : foamEndScale;

        Renderer rend  = foam.GetComponent<Renderer>();

        while (elapsed < foamLifetime && foam != null)
        {
            float t = elapsed / foamLifetime;

            // Scale up over time
            float currentScale = Mathf.Lerp(foamStartScale, endScale, t);
            foam.transform.localScale = Vector3.one * currentScale;

            // Fade out over time
            if (rend != null)
            {
                Color c = foamColor;
                c.a = Mathf.Lerp(foamStartAlpha, 0f, t);
                rend.material.color = c;
            }

            elapsed += Time.deltaTime;
            yield return null;
        }

        if (foam != null)
            Destroy(foam);
    }
}
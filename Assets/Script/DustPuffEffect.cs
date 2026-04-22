using UnityEngine;

public class DustPuffEffect : MonoBehaviour
{
    public static DustPuffEffect Instance { get; private set; }

    [Header("Dust Settings")]
    [SerializeField] private GameObject dustPuffPrefab;
    [SerializeField] private float walkPuffInterval = 0.5f;
    [SerializeField] private float runPuffInterval  = 0.25f;
    [SerializeField] private float puffScale        = 1f;
    [SerializeField] private float runPuffScale     = 1.5f;

    private Transform playerFeet;
    private NetworkPlayer networkPlayer;
    private float puffTimer  = 0f;
    private bool  isMoving   = false;
    private bool  isSprinting = false;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else { Destroy(gameObject); return; }
    }

    public void SetPlayerFeet(Transform feet)
    {
        playerFeet    = feet;
        // Grab NetworkPlayer from the feet's parent hierarchy
        networkPlayer = feet.GetComponentInParent<NetworkPlayer>();
    }

    public void UpdateMovement(bool moving, bool sprinting)
    {
        isMoving   = moving;
        isSprinting = sprinting;
    }

    private void Update()
    {
        // Don't puff if not moving, no feet, or player is in water
        if (!isMoving || playerFeet == null)
        {
            puffTimer = 0f;
            return;
        }

        // ── Water check ──
        if (networkPlayer != null && networkPlayer.IsInWater.Value)
        {
            puffTimer = 0f;
            return;
        }

        float interval = isSprinting ? runPuffInterval : walkPuffInterval;
        puffTimer += Time.deltaTime;

        if (puffTimer >= interval)
        {
            puffTimer = 0f;
            SpawnPuff();
        }
    }

    private void SpawnPuff()
    {
        if (dustPuffPrefab == null) return;

        // Raycast down to find exact ground position
        Vector3 spawnPos = playerFeet.position;
        if (Physics.Raycast(playerFeet.position + Vector3.up * 0.5f,
            Vector3.down, out RaycastHit hit, 2f))
        {
            spawnPos = hit.point;
        }

        float scale      = isSprinting ? runPuffScale : puffScale;
        GameObject puff  = Instantiate(dustPuffPrefab, spawnPos,
                           Quaternion.Euler(-90f, 0f, 0f));
        puff.transform.localScale = Vector3.one * scale;

        // Auto destroy after particle finishes
        ParticleSystem ps = puff.GetComponent<ParticleSystem>();
        float lifetime   = ps != null 
            ? ps.main.duration + ps.main.startLifetime.constantMax 
            : 2f;
        Destroy(puff, lifetime);
    }
}
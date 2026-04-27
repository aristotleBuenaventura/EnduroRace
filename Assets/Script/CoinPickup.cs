using UnityEngine;
using FishNet.Object;
using FishNet.Object.Synchronizing;
using System.Collections;

public class CoinPickup : NetworkBehaviour
{
    [Header("Settings")]
    public int coinValue    = 1;
    private float respawnTime = 5f;

    [Header("Visuals")]
    public GameObject coinVisual;
    public GameObject pickupEffect;
    public AudioClip  pickupSound;
    public float rotationSpeed = 90f;
    public float bobSpeed      = 2f;
    public float bobHeight     = 0.2f;

    private readonly SyncVar<bool> isActive = new SyncVar<bool>(true);
    private Vector3     startPosition;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource   = GetComponent<AudioSource>();
        startPosition = transform.position;
    }

    public override void OnStartClient()
    {
        base.OnStartClient();
        isActive.OnChange += OnActiveChanged;
        UpdateVisual(isActive.Value);
    }

    private void OnDestroy()
    {
        isActive.OnChange -= OnActiveChanged;
    }

    private void Update()
    {
        if (!isActive.Value || coinVisual == null) return;

        coinVisual.transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        float newY = startPosition.y + Mathf.Sin(Time.time * bobSpeed) * bobHeight;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Client-side detection — send to server
        if (!IsServerInitialized)
        {
            NetworkPlayer networkPlayer = other.GetComponentInParent<NetworkPlayer>();
            if (networkPlayer == null) return;
            if (!networkPlayer.IsOwner) return; // only local player triggers this
            if (!isActive.Value) return;

            ServerRequestPickup(networkPlayer.OwnerId);
            return;
        }

        // Server-side detection (host)
        if (!isActive.Value) return;
        NetworkPlayer np = other.GetComponentInParent<NetworkPlayer>();
        if (np == null) return;
        ServerProcessPickup(np.OwnerId);
    }

    [ServerRpc(RequireOwnership = false)]
    private void ServerRequestPickup(int requestingOwnerId)
    {
        if (!isActive.Value) return;
        ServerProcessPickup(requestingOwnerId);
    }

    [Server]
    private void ServerProcessPickup(int ownerId)
    {
        if (!isActive.Value) return;
        isActive.Value = false;

        // Find the connection by ownerId
        foreach (var conn in ServerManager.Clients.Values)
        {
            if (conn.ClientId == ownerId)
            {
                GiveCoinRpc(conn, coinValue);
                break;
            }
        }

        PlayPickupEffectRpc();
        StartCoroutine(RespawnRoutine());
    }

    [TargetRpc]
    private void GiveCoinRpc(FishNet.Connection.NetworkConnection conn, int amount)
    {
        CoinManager.Instance?.AddCoins(amount);
        ;
    }

    [ObserversRpc]
    private void PlayPickupEffectRpc()
    {
        if (pickupEffect != null)
            Instantiate(pickupEffect, transform.position, Quaternion.identity);

        if (pickupSound != null && audioSource != null)
            audioSource.PlayOneShot(pickupSound);
    }

    private void OnActiveChanged(bool prev, bool next, bool asServer)
        => UpdateVisual(next);

    private void UpdateVisual(bool active)
    {
        if (coinVisual != null)
            coinVisual.SetActive(active);

        var col = GetComponent<Collider>();
        if (col != null) col.enabled = active;
    }

    [Server]
    private IEnumerator RespawnRoutine()
    {
        yield return new WaitForSeconds(respawnTime);
        isActive.Value    = true;
        transform.position = startPosition;
    }
}
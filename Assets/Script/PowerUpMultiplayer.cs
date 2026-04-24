using UnityEngine;
using System.Collections;
using FishNet.Object;
using FishNet.Object.Synchronizing;

public class PowerUpMultiplayer : NetworkBehaviour
{
    [Header("Stamina Restore")]
    [SerializeField] private float staminaRestoreAmount = 30f;

    [Header("Respawn Settings")]
    private float respawnTime = 5f;

    [Header("Feedback")]
    [SerializeField] private AudioClip pickupSound;
    [SerializeField] private GameObject pickupEffect;

    [Header("Speed Boost")]
    [SerializeField] private float boostMultiplier = 1.5f;
    [SerializeField] private float boostDuration = 3f;

    [Header("PowerUp Type")]
    [SerializeField] private PowerUpType powerUpType;

    public enum PowerUpType
    {
        Water,
        EnergyDrink
    }

    // Synced so all clients see pickup/respawn
    private readonly SyncVar<bool> isActive = new SyncVar<bool>(true);

    private Collider powerupCollider;
    private Renderer[] renderers;

    private void Awake()
    {
        powerupCollider = GetComponent<Collider>();
        renderers = GetComponentsInChildren<Renderer>();
    }

    public override void OnStartClient()
    {
        base.OnStartClient();

        // Listen for active state changes to update visuals on all clients
        isActive.OnChange += OnActiveStateChanged;

        // Apply initial state
        SetVisualState(isActive.Value);
    }

    private void OnDestroy()
    {
        isActive.OnChange -= OnActiveStateChanged;
    }

    private void OnActiveStateChanged(bool prev, bool next, bool asServer)
    {
        SetVisualState(next);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Client-side detection
        if (!IsServerInitialized)
        {
            NetworkPlayer networkPlayer = other.GetComponentInParent<NetworkPlayer>();
            if (networkPlayer == null) return;
            if (!networkPlayer.IsOwner) return;
            if (!isActive.Value) return;

            ServerRequestPickup(networkPlayer.OwnerId);
            return;
        }

        // Server-side detection (host)
        if (!isActive.Value) return;
        NetworkPlayer np = other.GetComponentInParent<NetworkPlayer>();
        if (np == null) return;
        ServerApplyPowerUpById(np.OwnerId);
    }

    [ServerRpc(RequireOwnership = false)]
    private void ServerRequestPickup(int requestingOwnerId)
    {
        if (!isActive.Value) return;
        ServerApplyPowerUpById(requestingOwnerId);
    }

    [Server]
    private void ServerApplyPowerUpById(int ownerId)
    {
        if (!isActive.Value) return;
        isActive.Value = false;
        powerupCollider.enabled = false;

        foreach (var conn in ServerManager.Clients.Values)
        {
            if (conn.ClientId == ownerId)
            {
                ApplyEffectRpc(conn, powerUpType, staminaRestoreAmount, boostMultiplier, boostDuration);
                break;
            }
        }

        PlayPickupEffectsRpc();
        StartCoroutine(RespawnRoutine());
    }

    [Server]
    private void ServerApplyPowerUp(NetworkPlayer networkPlayer)
    {
        // Deactivate immediately on server
        isActive.Value = false;
        powerupCollider.enabled = false;

        // Tell the owning client to apply the effect
        ApplyEffectRpc(networkPlayer.Owner, powerUpType, staminaRestoreAmount, boostMultiplier, boostDuration);

        // Play pickup effects on all clients
        PlayPickupEffectsRpc();

        // Start respawn timer
        StartCoroutine(RespawnRoutine());
    }

    [TargetRpc]
    private void ApplyEffectRpc(FishNet.Connection.NetworkConnection conn,
        PowerUpType type, float stamina, float multiplier, float duration)
    {
        // Find local player components
        PlayerController runner = FindLocalPlayerController();
        CyclingController cyclist = FindLocalCyclingController();

        switch (type)
        {
            case PowerUpType.Water:
                if (runner != null && runner.isActiveModel)
                    runner.currentStamina = Mathf.Min(
                        runner.currentStamina + stamina,
                        runner.staminaUI != null ? 100f : 100f
                    );
                if (cyclist != null && cyclist.isActiveModel)
                    cyclist.currentStamina = Mathf.Min(
                        cyclist.currentStamina + stamina,
                        cyclist.maxStamina
                    );
                break;

            case PowerUpType.EnergyDrink:
                if (runner != null && runner.isActiveModel)
                    StartCoroutine(SpeedBoostRunner(runner, multiplier, duration));
                if (cyclist != null && cyclist.isActiveModel)
                    StartCoroutine(SpeedBoostCyclist(cyclist, multiplier, duration));
                break;
        }

        Debug.Log($"[PowerUp] Applied {type} to local player");
    }

    [ObserversRpc]
    private void PlayPickupEffectsRpc()
    {
        if (pickupEffect != null)
            Instantiate(pickupEffect, transform.position, Quaternion.identity);

        if (pickupSound != null)
            AudioSource.PlayClipAtPoint(pickupSound, transform.position);
    }

    private IEnumerator SpeedBoostRunner(PlayerController runner, float multiplier, float duration)
    {
        // Access moveSpeed via reflection since it's private
        // Instead add a public method to PlayerController
        Debug.Log($"[PowerUp] Speed boost applied to runner for {duration}s");

        // You need to add ApplySpeedBoost to PlayerController
        runner.ApplySpeedBoost(multiplier, duration);
        yield return null;
    }

    private IEnumerator SpeedBoostCyclist(CyclingController cyclist, float multiplier, float duration)
    {
        Debug.Log($"[PowerUp] Speed boost applied to cyclist for {duration}s");

        // You need to add ApplySpeedBoost to CyclingController
        cyclist.ApplySpeedBoost(multiplier, duration);
        yield return null;
    }

    [Server]
    private IEnumerator RespawnRoutine()
    {
        yield return new WaitForSeconds(respawnTime);

        isActive.Value = true;
        powerupCollider.enabled = true;
        Debug.Log("[PowerUp] Respawned");
    }

    private void SetVisualState(bool state)
    {
        if (powerupCollider != null)
            powerupCollider.enabled = state;

        foreach (Renderer r in renderers)
            r.enabled = state;
    }

    private PlayerController FindLocalPlayerController()
    {
        NetworkPlayer[] players = FindObjectsByType<NetworkPlayer>(FindObjectsSortMode.None);
        foreach (var p in players)
        {
            if (p.IsOwner)
                return p.GetComponentInChildren<PlayerController>();
        }
        return null;
    }

    private CyclingController FindLocalCyclingController()
    {
        NetworkPlayer[] players = FindObjectsByType<NetworkPlayer>(FindObjectsSortMode.None);
        foreach (var p in players)
        {
            if (p.IsOwner)
                return p.GetComponentInChildren<CyclingController>();
        }
        return null;
    }
}
using UnityEngine;
using System.Collections;

public class PowerUpsTutorial : MonoBehaviour
{
    [Header("Stamina Restore")]
    [SerializeField] private float staminaRestoreAmount = 30f;

    [Header("Respawn Settings")]
    [SerializeField] private float respawnTime = 15f;

    [Header("Feedback")]
    [SerializeField] private AudioClip pickupSound;
    [SerializeField] private GameObject pickupEffect;

    [Header("Speed Boost")]
    [SerializeField] private float boostMultiplier = 1.5f;
    [SerializeField] private float boostDuration = 3f;
    private bool isSpeedBoosted;
    private Collider bottleCollider;
    private Renderer[] renderers;

    [Header("PowerUp Type")]
    [SerializeField] private PowerUpType powerUpType;

    public enum PowerUpType
    {
        Water,        // restores stamina
        EnergyDrink   // speed boost
    }

    private void Awake()
    {
        bottleCollider = GetComponent<Collider>();
        renderers = GetComponentsInChildren<Renderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerControllerTutorial runner = other.GetComponent<PlayerControllerTutorial>();
        CyclingControllerTutorial cyclist = other.GetComponent<CyclingControllerTutorial>();

        if (runner != null)
        {
            ApplyPowerUp(runner);
            return;
        }

        if (cyclist != null)
        {
            ApplyPowerUp(cyclist);
            return;
        }
    }

    private void Pickup()
    {
        if (pickupEffect != null)
            Instantiate(pickupEffect, transform.position, Quaternion.identity);

        if (pickupSound != null)
            AudioSource.PlayClipAtPoint(pickupSound, transform.position);

        StartCoroutine(RespawnRoutine());
    }

    private void ApplyPowerUp(PlayerControllerTutorial player)
    {
        switch (powerUpType)
        {
            case PowerUpType.Water:
                player.RestoreStamina(staminaRestoreAmount);
                break;

            case PowerUpType.EnergyDrink:
                player.ActivateSpeedBoost(boostMultiplier, boostDuration);
                break;
        }

        Pickup();
    }

    private void ApplyPowerUp(CyclingControllerTutorial player)
    {
        switch (powerUpType)
        {
            case PowerUpType.Water:
                player.RestoreStamina(staminaRestoreAmount);
                break;

            case PowerUpType.EnergyDrink:
                player.ActivateSpeedBoost(boostMultiplier, boostDuration);
                break;
        }

        Pickup();
    }

    private IEnumerator RespawnRoutine()
    {
        SetActiveState(false);
        yield return new WaitForSeconds(respawnTime);
        SetActiveState(true);
    }

    private void SetActiveState(bool state)
    {
        bottleCollider.enabled = state;

        foreach (Renderer r in renderers)
            r.enabled = state;
    }
}

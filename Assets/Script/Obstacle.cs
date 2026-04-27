using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Collider))]
public class Obstacle : MonoBehaviour
{
    public enum ObstacleType { Log, Mud, Buoy, SpeedBump, Pothole, Whirlpool }

    [Header("Type")]
    public ObstacleType type = ObstacleType.Log;
    
    [Header("Cooldown - Prevents Vibration")]
    [Tooltip("Time before this obstacle can affect the same player again")]
    public float triggerCooldown = 2f;
    
    // Track cooldowns per player (using GetInstanceID as key)
    private Dictionary<int, float> playerCooldowns = new Dictionary<int, float>();

    [Header("Log Settings")]
    public float tripDistance = 0.2f;
    public float tripDuration = 1f;
    public float tripStaminaCost = 10f;
    public float cameraShakeDuration = 0.2f;
    public float cameraShakeMagnitude = 0.1f;

    [Header("Mud Settings")]
    public float slowMultiplier = 0.3f;
    public float mudStaminaDrainRate = 5f;

    [Header("Buoy Settings")]
    public float stunDuration = 1f;
    public float knockbackDistance = 0.5f;
    public float knockbackDuration = 0.2f;

    [Header("SpeedBump Settings")]
    public float bumpHeight = 0.2f;
    public float bumpDuration = 0.3f;
    public float bumpSpeedMultiplier = 0.7f;

    [Header("Pothole Settings")]
    public float stumbleDuration = 1f;
    public float dipDepth = 0.2f;
    public float cameraShakeDurationPothole = 0.2f;
    public float cameraShakeMagnitudePothole = 0.15f;

    [Header("Whirlpool Settings")]
    public float whirlpoolPullStrength = 10f;
    public float whirlpoolSlowMultiplier = 0.4f;
    public float whirlpoolMaxPullDistance = 7f;
    public float whirlpoolStaminaDrainRate = 15f;

    private void Reset()
    {
        GetComponent<Collider>().isTrigger = true;
    }
    
    private void Update()
    {
        // Clean up expired cooldowns
        List<int> expiredKeys = new List<int>();
        foreach (var kvp in playerCooldowns)
        {
            if (Time.time >= kvp.Value)
                expiredKeys.Add(kvp.Key);
        }
        
        foreach (var key in expiredKeys)
            playerCooldowns.Remove(key);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Get PlayerController (for running segment)
        PlayerController player = other.GetComponentInParent<PlayerController>();
        
        // Get CyclingController (for cycling segment)
        CyclingController cyclist = other.GetComponentInParent<CyclingController>();

        // Only trigger on LOCAL player (IsOwner in FishNet)
        if (player != null && !player.IsOwner)
            return;

        if (cyclist != null && !cyclist.IsOwner)
            return;
        
        // Check cooldown
        int playerID = player != null ? player.GetInstanceID() : (cyclist != null ? cyclist.GetInstanceID() : 0);
        
        if (playerID != 0 && playerCooldowns.ContainsKey(playerID))
        {
            if (Time.time < playerCooldowns[playerID])
            {
                return;
            }
        }

        if (player != null)

        switch (type)
        {
            // --- RUNNING OBSTACLES ---
            case ObstacleType.Log:
                if (player == null) return;
                HandleLog(player);
                SetCooldown(playerID);
                break;

            case ObstacleType.Mud:
                if (player == null) return;
                HandleMud(player);
                SetCooldown(playerID, 0.5f);
                break;

            case ObstacleType.Buoy:
                if (player == null) return;
                HandleBuoy(player);
                SetCooldown(playerID);
                break;

            // --- CYCLING OBSTACLES ---
            case ObstacleType.SpeedBump:
                if (cyclist == null) return;
                HandleSpeedBump(cyclist);
                SetCooldown(playerID);
                break;

            case ObstacleType.Pothole:
                if (cyclist == null) return;
                HandlePothole(cyclist);
                SetCooldown(playerID);
                break;

            // --- WATER OBSTACLES ---
            case ObstacleType.Whirlpool:
                if (player == null) return;
                HandleWhirlpool(player);
                SetCooldown(playerID, 5f);
                break;
        }
    }
    
    private void SetCooldown(int playerID, float customCooldown = -1f)
    {
        if (playerID == 0) return;
        
        float cooldownTime = customCooldown > 0 ? customCooldown : triggerCooldown;
        playerCooldowns[playerID] = Time.time + cooldownTime;
        
    }

     private void OnTriggerExit(Collider other)
    {
        PlayerController player = other.GetComponentInParent<PlayerController>();
        if (player == null || !player.IsOwner) return;
 
        if (type == ObstacleType.Mud)
        {
            player.ExitMud();
        }
        else if (type == ObstacleType.Whirlpool)
        {
            // ✅ FIX: ExitWhirlpool now also stops the SuckIntoWhirlpool coroutine
            // and clears isBeingSucked, so the player is never left frozen
            // after physically leaving the trigger volume.
            player.ExitWhirlpool();
        }
    }
 

    // ================= RUNNING OBSTACLES =================
    
    private void HandleLog(PlayerController player)
    {
        NetworkPlayer netPlayer = player.GetComponent<NetworkPlayer>();
        if (netPlayer == null)
            netPlayer = player.GetComponentInParent<NetworkPlayer>();
            
        if (netPlayer == null)
        {
            return;
        }

        if (netPlayer.IsInWater.Value) return;

        NotifyTracker(player, true);
        netPlayer.ServerTrip(tripDuration);
    }

    private void HandleMud(PlayerController player)
    {
        NetworkPlayer netPlayer = player.GetComponent<NetworkPlayer>();
        if (netPlayer == null)
            netPlayer = player.GetComponentInParent<NetworkPlayer>();
            
        if (netPlayer == null)
        {
            return;
        }

        if (netPlayer.IsInWater.Value) return;

        NotifyTracker(player, true);
        player.EnterMud(slowMultiplier);
        player.mudStaminaDrainRate = mudStaminaDrainRate;
    }

    private void HandleBuoy(PlayerController player)
    {
        NetworkPlayer netPlayer = player.GetComponent<NetworkPlayer>();
        if (netPlayer == null)
            netPlayer = player.GetComponentInParent<NetworkPlayer>();
            
        if (netPlayer == null)
        {
            return;
        }

        // Only affect players in water
        if (!netPlayer.IsInWater.Value) return;


        NotifyTracker(player, true);
        // Deduct stamina
        player.currentStamina = Mathf.Max(0f, player.currentStamina - tripStaminaCost);

        // ✅ Knockback: push away from buoy horizontally only (Y=0 prevents flying up)
        if (knockbackDistance > 0f)
        {
            Vector3 knockbackDir = player.transform.position - transform.position;
            knockbackDir.y = 0f;            // zero Y so player doesn't fly upward
            knockbackDir.Normalize();
            player.characterController.Move(knockbackDir * knockbackDistance);
        }

        player.StartCoroutine(player.StunPlayer(stunDuration));
    }

    private void HandleWhirlpool(PlayerController player)
    {
        NetworkPlayer netPlayer = player.GetComponent<NetworkPlayer>();
        if (netPlayer == null)
            netPlayer = player.GetComponentInParent<NetworkPlayer>();
            
        if (netPlayer == null)
        {
            return;
        }

        // Only affect players in water
        if (!netPlayer.IsInWater.Value) return;


        NotifyTracker(player, true);
        player.EnterWhirlpool(
            transform, 
            whirlpoolPullStrength, 
            whirlpoolSlowMultiplier, 
            whirlpoolMaxPullDistance,
            whirlpoolStaminaDrainRate
        );
    }

    // ================= CYCLING OBSTACLES =================

    private void HandleSpeedBump(CyclingController cyclist)
    {
        NotifyTrackerCyclist(cyclist, true);
        if (cyclist.animator != null)
            cyclist.animator.SetTrigger("HitSpeedBump");

        cyclist.StartCoroutine(cyclist.SpeedBumpEffect(bumpHeight, bumpDuration, bumpSpeedMultiplier));

        cyclist.currentStamina = Mathf.Max(0f, cyclist.currentStamina - 5f);
        if (cyclist.staminaUI != null)
            cyclist.staminaUI.SetStamina(cyclist.currentStamina, cyclist.maxStamina);
    }

    private void HandlePothole(CyclingController cyclist)
    {
        NotifyTrackerCyclist(cyclist, true);
        cyclist.StartCoroutine(cyclist.PotholeStumble(
            duration: stumbleDuration,
            dipDepth: dipDepth
        ));

        cyclist.currentStamina = Mathf.Max(0f, cyclist.currentStamina - 5f);
        if (cyclist.staminaUI != null)
            cyclist.staminaUI.SetStamina(cyclist.currentStamina, cyclist.maxStamina);
    }
    private void NotifyTracker(PlayerController player, bool isSpeedLoss)
    {
        RaceAchievementTracker tracker = RaceAchievementTracker.LocalInstance;
        if (tracker == null) return;

        tracker.RegisterCollision();
        if (isSpeedLoss)
            tracker.RegisterSpeedLoss();
    }

    private void NotifyTrackerCyclist(CyclingController cyclist, bool isSpeedLoss)
    {
        RaceAchievementTracker tracker = RaceAchievementTracker.LocalInstance;
        if (tracker == null) return;

        tracker.RegisterCollision();
        if (isSpeedLoss)
            tracker.RegisterSpeedLoss();
    }
}
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Collider))]
public class WaveObstacle : MonoBehaviour
{
    [Header("Wave Movement")]
    public float moveSpeed = 8f;
    public float lifetime = 6f;
    public Vector3 moveDirection = Vector3.forward;

    [Header("Knockback")]
    public float knockbackDistance = 4f;
    public float knockbackDuration = 0.4f;
    public float stunDuration = 0.6f;
    public float staminaCost = 15f;

    [Header("Camera Shake")]
    public float shakeDuration = 0.3f;
    public float shakeMagnitude = 0.2f;

    // Prevent hitting the same player twice per wave
    private HashSet<int> hitPlayers = new HashSet<int>();

    private void Start()
    {
        Collider col = GetComponent<Collider>();
        col.isTrigger = true;

        StartCoroutine(MoveAndDestroy());
    }

    private IEnumerator MoveAndDestroy()
    {
        float elapsed = 0f;
        while (elapsed < lifetime)
        {
            transform.position += moveDirection.normalized * moveSpeed * Time.deltaTime;
            elapsed += Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerController player = other.GetComponentInParent<PlayerController>();
        if (player == null || !player.IsOwner) return;

        // Only affect swimmers
        NetworkPlayer netPlayer = player.GetComponent<NetworkPlayer>()
            ?? player.GetComponentInParent<NetworkPlayer>();
        if (netPlayer == null || !netPlayer.IsInWater.Value) return;

        int id = player.GetInstanceID();
        if (hitPlayers.Contains(id)) return;
        hitPlayers.Add(id);

        // Notify achievement tracker
        RaceAchievementTracker tracker = RaceAchievementTracker.LocalInstance;
        if (tracker != null)
        {
            tracker.RegisterCollision();
            tracker.RegisterSpeedLoss();
        }

        StartCoroutine(ApplyKnockback(player));
    }

    private IEnumerator ApplyKnockback(PlayerController player)
    {
        // Drain stamina
        player.currentStamina = Mathf.Max(0f, player.currentStamina - staminaCost);

        // Knockback direction = away from wave origin, horizontal only
        Vector3 knockDir = player.transform.position - transform.position;
        knockDir.y = 0f;
        knockDir.Normalize();

        float elapsed = 0f;
        float distancePerFrame = knockbackDistance / knockbackDuration;

        while (elapsed < knockbackDuration)
        {
            if (player == null) yield break;
            player.characterController.Move(
                knockDir * distancePerFrame * Time.deltaTime);
            elapsed += Time.deltaTime;
            yield return null;
        }

        // Stun after knockback
        yield return player.StartCoroutine(player.StunPlayer(stunDuration));
    }
}
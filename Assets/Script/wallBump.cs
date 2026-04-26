using UnityEngine;

public class wallBump : MonoBehaviour
{
    [Header("Buoy-like Bump Settings")]
    public float tripStaminaCost = 10f;
    public float stunDuration = 1f;
    public float knockbackDistance = 0.5f;

    private void OnTriggerEnter(Collider other)
    {
        PlayerController hitPlayer = other.GetComponentInParent<PlayerController>();
        if (hitPlayer == null)
            return;

        if (!hitPlayer.IsOwner)
            return;

        NetworkPlayer netPlayer = hitPlayer.GetComponent<NetworkPlayer>();
        if (netPlayer == null)
            netPlayer = hitPlayer.GetComponentInParent<NetworkPlayer>();

        if (netPlayer == null)
        {
            Debug.LogError("[wallBump] NetworkPlayer not found on player or parent!");
            return;
        }

        // Match buoy logic: only apply when player is in water.
        if (!netPlayer.IsInWater.Value)
            return;

        Debug.Log("[wallBump] Buoy-style bump triggered");

        hitPlayer.currentStamina = Mathf.Max(0f, hitPlayer.currentStamina - tripStaminaCost);

        if (knockbackDistance > 0f)
        {
            Vector3 knockbackDir = hitPlayer.transform.position - transform.position;
            knockbackDir.y = 0f;
            knockbackDir.Normalize();
            hitPlayer.characterController.Move(knockbackDir * knockbackDistance);
        }

        hitPlayer.StartCoroutine(hitPlayer.StunPlayer(stunDuration));
    }
}
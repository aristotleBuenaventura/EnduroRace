using UnityEngine;

public class wallBump : MonoBehaviour
{
    [Header("Bump Settings")]
    public float tripStaminaCost = 10f;
    public float stunDuration = 1f;
    public float knockbackDistance = 0.5f;

    private void OnTriggerEnter(Collider other)
    {
        PlayerController hitPlayer = other.GetComponentInParent<PlayerController>();
        CyclingController hitCyclist = other.GetComponentInParent<CyclingController>();

        if (hitPlayer == null && hitCyclist == null)
            return;

        NetworkPlayer netPlayer = other.GetComponentInParent<NetworkPlayer>();
        if (netPlayer == null)
        {
            if (hitPlayer != null)
                netPlayer = hitPlayer.GetComponentInParent<NetworkPlayer>();
            else if (hitCyclist != null)
                netPlayer = hitCyclist.GetComponentInParent<NetworkPlayer>();
        }

        if (netPlayer == null)
        {
            Debug.LogError("[wallBump] NetworkPlayer not found on collider hierarchy!");
            return;
        }

        if (!netPlayer.IsOwner || netPlayer.IsStunned.Value)
            return;

        Debug.Log("[wallBump] Wall bump triggered");

        if (hitPlayer != null)
            hitPlayer.currentStamina = Mathf.Max(0f, hitPlayer.currentStamina - tripStaminaCost);

        if (hitCyclist != null)
            hitCyclist.currentStamina = Mathf.Max(0f, hitCyclist.currentStamina - tripStaminaCost);

        if (knockbackDistance > 0f)
        {
            Transform hitRoot = netPlayer.transform;
            Vector3 knockbackDir = hitRoot.position - transform.position;
            knockbackDir.y = 0f;
            knockbackDir.Normalize();

            if (hitPlayer != null && hitPlayer.characterController != null)
                hitPlayer.characterController.Move(knockbackDir * knockbackDistance);

            CharacterController cyclistController = hitCyclist != null
                ? hitCyclist.GetComponent<CharacterController>()
                : null;
            if (cyclistController != null)
                cyclistController.Move(knockbackDir * knockbackDistance);
        }

        if (hitPlayer != null)
            hitPlayer.StartCoroutine(hitPlayer.StunPlayer(stunDuration));
        else if (hitCyclist != null)
            hitCyclist.StartCoroutine(hitCyclist.StunCyclist(stunDuration));
    }
}
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
            ;
            return;
        }

        if (!netPlayer.IsOwner || netPlayer.IsStunned.Value)
            return;

        bool cyclistActive = hitCyclist != null && hitCyclist.isActiveModel;
        bool playerActive = hitPlayer != null && hitPlayer.isActiveModel;

        // Prefer whichever model is currently active to avoid triggering the wrong controller.
        bool useCyclist = cyclistActive || (hitCyclist != null && !playerActive);
        bool usePlayer = !useCyclist && hitPlayer != null;

        ;

        if (usePlayer && hitPlayer != null)
            hitPlayer.currentStamina = Mathf.Max(0f, hitPlayer.currentStamina - tripStaminaCost);

        if (useCyclist && hitCyclist != null)
            hitCyclist.currentStamina = Mathf.Max(0f, hitCyclist.currentStamina - tripStaminaCost);

        if (knockbackDistance > 0f)
        {
            Transform hitRoot = netPlayer.transform;
            Vector3 knockbackDir = hitRoot.position - transform.position;
            knockbackDir.y = 0f;
            knockbackDir.Normalize();

            if (usePlayer && hitPlayer != null && hitPlayer.characterController != null)
                hitPlayer.characterController.Move(knockbackDir * knockbackDistance);

            CharacterController cyclistController = (useCyclist && hitCyclist != null)
                ? hitCyclist.GetComponent<CharacterController>()
                : null;
            if (cyclistController != null)
                cyclistController.Move(knockbackDir * knockbackDistance);
        }

        if (usePlayer && hitPlayer != null)
            hitPlayer.StartCoroutine(hitPlayer.StunPlayer(stunDuration));
        else if (useCyclist && hitCyclist != null)
            hitCyclist.StartCoroutine(hitCyclist.StunCyclist(stunDuration));
    }
}
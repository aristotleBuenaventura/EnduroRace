using UnityEngine;

public class wallBumpMesh : MonoBehaviour
{
    [Header("Bump Settings")]
    public float tripStaminaCost = 10f;
    public float stunDuration = 1f;
    public float knockbackDistance = 0.5f;

    private void OnTriggerEnter(Collider other)
    {
        HandleWallHit(other);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision == null) return;
        HandleWallHit(collision.collider);
    }

    private void HandleWallHit(Collider other)
    {
        PlayerController hitPlayer = other.GetComponentInParent<PlayerController>();
        CyclingController hitCyclist = other.GetComponentInParent<CyclingController>();
        if (hitPlayer == null && hitCyclist == null) return;

        NetworkPlayer netPlayer = ResolveNetworkPlayer(other, hitPlayer, hitCyclist);
        if (netPlayer == null || !netPlayer.IsOwner || netPlayer.IsStunned.Value) return;

        bool cyclistActive = hitCyclist != null && hitCyclist.isActiveModel;
        bool playerActive = hitPlayer != null && hitPlayer.isActiveModel;
        bool useCyclist = cyclistActive || (hitCyclist != null && !playerActive);
        bool usePlayer = !useCyclist && hitPlayer != null;

        if (usePlayer)
            hitPlayer.currentStamina = Mathf.Max(0f, hitPlayer.currentStamina - tripStaminaCost);

        if (useCyclist)
            hitCyclist.currentStamina = Mathf.Max(0f, hitCyclist.currentStamina - tripStaminaCost);

        if (knockbackDistance > 0f)
        {
            Vector3 dir = netPlayer.transform.position - transform.position;
            dir.y = 0f;
            dir = dir.sqrMagnitude > 0.0001f ? dir.normalized : netPlayer.transform.forward;

            if (usePlayer && hitPlayer.characterController != null)
                hitPlayer.characterController.Move(dir * knockbackDistance);

            CharacterController cyclistController = useCyclist ? hitCyclist.GetComponent<CharacterController>() : null;
            if (cyclistController != null)
                cyclistController.Move(dir * knockbackDistance);
        }

        if (usePlayer)
            hitPlayer.StartCoroutine(hitPlayer.StunPlayer(stunDuration));
        else if (useCyclist)
            hitCyclist.StartCoroutine(hitCyclist.StunCyclist(stunDuration));
    }

    private NetworkPlayer ResolveNetworkPlayer(Collider other, PlayerController hitPlayer, CyclingController hitCyclist)
    {
        NetworkPlayer netPlayer = other.GetComponentInParent<NetworkPlayer>();
        if (netPlayer != null) return netPlayer;

        if (hitPlayer != null)
            netPlayer = hitPlayer.GetComponentInParent<NetworkPlayer>();
        else if (hitCyclist != null)
            netPlayer = hitCyclist.GetComponentInParent<NetworkPlayer>();

        return netPlayer;
    }
}
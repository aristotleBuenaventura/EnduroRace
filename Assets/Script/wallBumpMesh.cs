using UnityEngine;

public class wallBumpMesh : MonoBehaviour
{
    [Header("Bump Settings")]
    public float tripStaminaCost = 10f;
    public float stunDuration = 1f;
    public float knockbackDistance = 0.5f;
    
    [Header("Debug")]
    public bool enableDebugLogs = true;
    private bool hasLoggedStay;

    private void Awake()
    {
        if (!enableDebugLogs) return;

        Collider ownCollider = GetComponent<Collider>();
        Rigidbody ownRb = GetComponent<Rigidbody>();
        ;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (enableDebugLogs)
        {
            ;
        }

        PlayerController hitPlayer = other.GetComponentInParent<PlayerController>();
        CyclingController hitCyclist = other.GetComponentInParent<CyclingController>();

        if (hitPlayer == null && hitCyclist == null)
        {
            if (enableDebugLogs)
                ;
            return;
        }

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
        {
            if (enableDebugLogs)
                ;
            return;
        }

        bool cyclistActive = hitCyclist != null && hitCyclist.isActiveModel;
        bool playerActive = hitPlayer != null && hitPlayer.isActiveModel;

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
            Vector3 dir = hitRoot.position - transform.position;
            dir.y = 0f;
            dir.Normalize();

            if (usePlayer && hitPlayer != null && hitPlayer.characterController != null)
                hitPlayer.characterController.Move(dir * knockbackDistance);

            CharacterController cyclistController = (useCyclist && hitCyclist != null)
                ? hitCyclist.GetComponent<CharacterController>()
                : null;

            if (cyclistController != null)
                cyclistController.Move(dir * knockbackDistance);
        }

        if (usePlayer && hitPlayer != null)
            hitPlayer.StartCoroutine(hitPlayer.StunPlayer(stunDuration));
        else if (useCyclist && hitCyclist != null)
            hitCyclist.StartCoroutine(hitCyclist.StunCyclist(stunDuration));
    }

    private void OnTriggerStay(Collider other)
    {
        if (!enableDebugLogs || hasLoggedStay) return;
        hasLoggedStay = true;
        ;
    }
}
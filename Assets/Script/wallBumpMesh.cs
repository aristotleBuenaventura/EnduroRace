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
        Debug.Log($"[wallBumpMesh] Ready on {name} | layer={LayerMask.LayerToName(gameObject.layer)}({gameObject.layer}) | hasCollider={ownCollider != null} trigger={ownCollider != null && ownCollider.isTrigger} | hasRigidbody={ownRb != null}");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (enableDebugLogs)
        {
            Debug.Log($"[wallBumpMesh] OnTriggerEnter on {name} with {other.name} | otherLayer={LayerMask.LayerToName(other.gameObject.layer)}({other.gameObject.layer})");
        }

        PlayerController hitPlayer = other.GetComponentInParent<PlayerController>();
        CyclingController hitCyclist = other.GetComponentInParent<CyclingController>();

        if (hitPlayer == null && hitCyclist == null)
        {
            if (enableDebugLogs)
                Debug.Log($"[wallBumpMesh] Ignored trigger: no PlayerController/CyclingController in parent hierarchy of {other.name}");
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
            Debug.LogError("[wallBumpMesh] NetworkPlayer not found!");
            return;
        }

        if (!netPlayer.IsOwner || netPlayer.IsStunned.Value)
        {
            if (enableDebugLogs)
                Debug.Log($"[wallBumpMesh] Ignored trigger: IsOwner={netPlayer.IsOwner}, IsStunned={netPlayer.IsStunned.Value}");
            return;
        }

        bool cyclistActive = hitCyclist != null && hitCyclist.isActiveModel;
        bool playerActive = hitPlayer != null && hitPlayer.isActiveModel;

        bool useCyclist = cyclistActive || (hitCyclist != null && !playerActive);
        bool usePlayer = !useCyclist && hitPlayer != null;

        Debug.Log($"[wallBumpMesh] Triggered | player={usePlayer}, cyclist={useCyclist}");

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
        Debug.Log($"[wallBumpMesh] OnTriggerStay detected with {other.name}. Trigger system is active.");
    }
}
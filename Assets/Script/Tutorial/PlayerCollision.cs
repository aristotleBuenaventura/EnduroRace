using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [Header("Collision Settings")]
    public float bounceForce = 4f;
    public float slowdownAmount = 0.5f;
    public float slowdownDuration = 0.4f;
    public float collisionCooldown = 0.5f;

    private CharacterController characterController;
    private Vector3 bounceVelocity = Vector3.zero;
    private float cooldownTimer = 0f;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (!gameObject.activeSelf) return;

        if (cooldownTimer > 0f) cooldownTimer -= Time.deltaTime;

        if (bounceVelocity.magnitude > 0.1f)
        {
            characterController.Move(bounceVelocity * Time.deltaTime);
            bounceVelocity = Vector3.Lerp(bounceVelocity, Vector3.zero, Time.deltaTime * 5f);
        }
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (!gameObject.activeSelf) return;
        if (cooldownTimer > 0f) return;

        AIOpponentController ai = hit.collider.GetComponent<AIOpponentController>();
        if (ai == null) return;

        cooldownTimer = collisionCooldown;

        Vector3 bounceDir = (transform.position - hit.transform.position).normalized;
        bounceDir.y = 0f;
        bounceVelocity = bounceDir * bounceForce;

        ai.ApplyCollisionPush(-bounceDir, bounceForce, slowdownAmount, slowdownDuration);

        ;
    }

    public void ResetBounce()
    {
        bounceVelocity = Vector3.zero;
        cooldownTimer = 0f;
    }
}
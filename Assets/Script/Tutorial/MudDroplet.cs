using UnityEngine;

public class MudDroplet : MonoBehaviour
{
    private Vector3 velocity;
    private float gravity;
    private float lifetime;
    private float timer = 0f;
    private MudSplatterEffect parentEffect;
    
    [SerializeField] private float airResistance = 0.98f; // Slows horizontal movement over time

    public void Initialize(Vector3 initialVelocity, float gravityStrength, float lifeTime, MudSplatterEffect parent)
    {
        velocity = initialVelocity;
        gravity = gravityStrength;
        lifetime = lifeTime;
        timer = 0f;
        parentEffect = parent;
    }

    void Update()
    {
        // Apply gravity to vertical velocity (Y axis)
        velocity.y -= gravity * Time.deltaTime;
        
        // Apply air resistance to horizontal velocity (X and Z) - creates curved arc
        velocity.x *= airResistance;
        velocity.z *= airResistance;

        // Move the droplet along the curved path
        transform.position += velocity * Time.deltaTime;

        // Optional: Rotate droplet as it falls for more realism
        transform.Rotate(velocity * Time.deltaTime * 50f);

        // Update lifetime
        timer += Time.deltaTime;
        
        if (timer >= lifetime)
        {
            // Return to pool
            if (parentEffect != null)
            {
                parentEffect.ReturnDropletToPool(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
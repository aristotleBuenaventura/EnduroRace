using UnityEngine;

public class MiniMapCameraFollowTutorial : MonoBehaviour
{
    [Header("Follow Settings")]
    public Transform target;       // Active player model
    public float height = 50f;     // Height above player
    public float offsetY = 0f;     // Optional vertical offset
    public bool topDownRotation = true; // Lock rotation to top-down

    private void LateUpdate()
    {
        if (target == null) return;

        // Move camera above target
        Vector3 newPos = target.position + Vector3.up * height + Vector3.up * offsetY;
        transform.position = newPos;

        // Keep top-down rotation
        if (topDownRotation)
            transform.rotation = Quaternion.Euler(90f, 0f, 0f);
    }
}

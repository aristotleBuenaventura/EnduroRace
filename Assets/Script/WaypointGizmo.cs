using UnityEngine;

[ExecuteAlways] // This makes it visible even when not in play mode
public class WaypointGizmo : MonoBehaviour
{
    public float forwardLength = 2f; // Length of the arrow
    public Color arrowColor = Color.green;

    private void OnDrawGizmos()
    {
        Gizmos.color = arrowColor;
        // Draw a line along the forward (Z-axis)
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * forwardLength);

        // Draw a small arrowhead
        Vector3 right = Quaternion.LookRotation(transform.forward) * Quaternion.Euler(0, 150, 0) * Vector3.forward;
        Vector3 left = Quaternion.LookRotation(transform.forward) * Quaternion.Euler(0, -150, 0) * Vector3.forward;
        Gizmos.DrawLine(transform.position + transform.forward * forwardLength, transform.position + transform.forward * forwardLength - right * 0.5f);
        Gizmos.DrawLine(transform.position + transform.forward * forwardLength, transform.position + transform.forward * forwardLength - left * 0.5f);
    }
}

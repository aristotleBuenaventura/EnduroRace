using UnityEngine;

public class MiniMapCameraFollow : MonoBehaviour
{
    [Header("Follow Settings")]
    public float height = 50f;
    public float offsetY = 0f;
    public bool topDownRotation = true;

    private Transform target;

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
        Debug.Log($"[MiniMapCameraFollow] SetTarget called — target={newTarget?.name ?? "NULL"}");
    }

    private void LateUpdate()
    {
        if (target == null)
        {
            Debug.LogWarning("[MiniMapCameraFollow] LateUpdate — target is NULL, not moving");
            return;
        }

        transform.position = target.position + Vector3.up * (height + offsetY);

        if (topDownRotation)
            transform.rotation = Quaternion.Euler(90f, 0f, 0f);
    }
}
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
    }

    private void LateUpdate()
    {
        if (target == null)
        {
            return;
        }

        transform.position = target.position + Vector3.up * (height + offsetY);

        if (topDownRotation)
            transform.rotation = Quaternion.Euler(90f, 0f, 0f);
    }
}
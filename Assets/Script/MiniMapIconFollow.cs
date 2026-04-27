using UnityEngine;

public class MiniMapIconFollow : MonoBehaviour
{
    public RectTransform icon;
    public Camera minimapCamera;

    [Header("Rotation Fix")]
    public float rotationOffset = 0f;

    private Transform target;
    private RectTransform minimapRect;

    private void Awake()
    {
        minimapRect = GetComponent<RectTransform>();
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
        Debug.Log($"[MiniMapIconFollow] SetTarget called — target={newTarget?.name ?? "NULL"}");
    }

    private void LateUpdate()
    {
        if (target == null || icon == null || minimapCamera == null) return;

        Vector3 viewportPos = minimapCamera.WorldToViewportPoint(target.position);

        icon.anchoredPosition = new Vector2(
            (viewportPos.x - 0.5f) * minimapRect.rect.width,
            (viewportPos.y - 0.5f) * minimapRect.rect.height
        );

        icon.localEulerAngles = new Vector3(0, 0, -target.eulerAngles.y + rotationOffset);
    }
}
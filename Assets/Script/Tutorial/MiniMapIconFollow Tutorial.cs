using UnityEngine;

public class MiniMapIconFollowTutorial : MonoBehaviour
{
    public Transform target;              // Active character (runner/cyclist)
    public RectTransform icon;            // Arrow icon
    public Camera minimapCamera;          // Minimap camera

    [Header("Rotation Fix")]
    public float rotationOffset = 0f;     // Offset added when switching models

    private RectTransform minimapRect;

    private void Awake()
    {
        minimapRect = GetComponent<RectTransform>();
    }

    private void LateUpdate()
    {
        if (target == null || icon == null || minimapCamera == null)
            return;

        // Convert world position to minimap viewport space
        Vector3 viewportPos = minimapCamera.WorldToViewportPoint(target.position);

        // Move the icon inside the minimap rect
        icon.anchoredPosition = new Vector2(
            (viewportPos.x - 0.5f) * minimapRect.rect.width,
            (viewportPos.y - 0.5f) * minimapRect.rect.height
        );

        // Rotate icon with character + offset
        icon.localEulerAngles = new Vector3(
            0,
            0,
            -target.eulerAngles.y + rotationOffset
        );
    }
}

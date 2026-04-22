using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SimpleMobileJoystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    [Header("Joystick Settings")]
    public RectTransform joystickBackground;
    public RectTransform joystickHandle;
    public float handleRange = 75f;

    private Vector2 inputVector = Vector2.zero;
    private bool isInitialized = false;

    private void Start()
    {
        ValidateSetup();
    }

    private void ValidateSetup()
    {
        bool hasErrors = false;

        if (joystickBackground == null) hasErrors = true;
        if (joystickHandle == null) hasErrors = true;

        if (joystickBackground != null)
        {
            Image bgImage = joystickBackground.GetComponent<Image>();
            if (bgImage == null || !bgImage.raycastTarget) hasErrors = true;
        }

        EventSystem eventSystem = Object.FindFirstObjectByType<EventSystem>();
        if (eventSystem == null) hasErrors = true;

        Canvas canvas = GetComponentInParent<Canvas>();
        if (canvas == null)
        {
            hasErrors = true;
        }
        else
        {
            GraphicRaycaster raycaster = canvas.GetComponent<GraphicRaycaster>();
            if (raycaster == null) hasErrors = true;
        }

        if (!hasErrors) isInitialized = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!isInitialized || joystickBackground == null || joystickHandle == null)
            return;

        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            joystickBackground,
            eventData.position,
            eventData.pressEventCamera,
            out position
        );

        position.x = (position.x / joystickBackground.sizeDelta.x);
        position.y = (position.y / joystickBackground.sizeDelta.y);

        float x = position.x * 2;
        float y = position.y * 2;

        inputVector = new Vector2(x, y);
        inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;

        joystickHandle.anchoredPosition = new Vector2(
            inputVector.x * handleRange,
            inputVector.y * handleRange
        );
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        inputVector = Vector2.zero;

        if (joystickHandle != null)
            joystickHandle.anchoredPosition = Vector2.zero;
    }

    public Vector2 GetInput()
    {
        return inputVector;
    }

    private void OnDrawGizmos()
    {
        if (joystickBackground == null) return;

        Vector3 worldPos = joystickBackground.position;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(worldPos, handleRange * joystickBackground.lossyScale.x);
    }
}
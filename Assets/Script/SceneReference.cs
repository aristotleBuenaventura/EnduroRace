using UnityEngine;
using UnityEngine.UI;
using Unity.Cinemachine;

public class SceneReference : MonoBehaviour
{
    public static SceneReference Instance { get; private set; }

    [Header("Cameras")]
    public CinemachineCamera mainVirtualCamera;

    [Header("Minimap")]
    public MiniMapIconFollow minimapIconFollow;
    public MiniMapCameraFollow minimapCameraFollow;

    [Header("UI")]
    public StaminaUI staminaUI;

    // ✅ ADDED: Drag the HeatOverlay Image from your Canvas here in the Inspector
    public Image heatOverlay;

    [Header("Bike Triggers (Mount)")]
    public BikeTrigger[] bikeTriggers;

    [Header("Bike Dismount Triggers")]
    public BikeDismountTrigger[] bikeDismountTriggers;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    // ---- Helpers ----
    public static CinemachineCamera GetVirtualCamera()
        => Instance?.mainVirtualCamera;

    public static MiniMapIconFollow GetMinimapIconFollow()
        => Instance?.minimapIconFollow;

    public static MiniMapCameraFollow GetMinimapCameraFollow()
        => Instance?.minimapCameraFollow;

    public static StaminaUI GetStaminaUI()
        => Instance?.staminaUI;

    // ✅ ADDED: Getter for HeatOverlay Image
    public static Image GetHeatOverlay()
        => Instance?.heatOverlay;

    public static BikeTrigger[] GetBikeTriggers()
        => Instance?.bikeTriggers;

    public static BikeDismountTrigger[] GetBikeDismountTriggers()
        => Instance?.bikeDismountTriggers;
}
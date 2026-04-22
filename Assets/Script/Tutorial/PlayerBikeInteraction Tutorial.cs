using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBikeInteractionTutorial : MonoBehaviour
{
    public PlayerSegmentStateTutorial playerState;
    public SegmentSwitcherTutorial segmentSwitcher;
    private PlayerInput currentInput;
    private InputAction interactAction;

    private void Awake()
    {
        BindToCurrentPlayerInput();

        if (segmentSwitcher != null)
            segmentSwitcher.OnPlayerModelChanged += BindToCurrentPlayerInput;
    }

    private void OnDestroy()
    {
        UnbindInput();

        if (segmentSwitcher != null)
            segmentSwitcher.OnPlayerModelChanged -= BindToCurrentPlayerInput;
    }

    private void BindToCurrentPlayerInput()
    {
        UnbindInput();

        // Find ACTIVE PlayerInput
        PlayerInput[] inputs = GetComponentsInChildren<PlayerInput>(true);
        foreach (var input in inputs)
        {
            if (input.gameObject.activeInHierarchy)
            {
                currentInput = input;
                break;
            }
        }

        if (currentInput == null)
        {
            Debug.LogError("No active PlayerInput found");
            return;
        }

        interactAction = currentInput.actions["Interact"];
        interactAction.performed += OnInteract;

        Debug.Log("Interact rebound to: " + currentInput.gameObject.name);
    }

    private void UnbindInput()
    {
        if (interactAction != null)
            interactAction.performed -= OnInteract;
    }

    private void OnInteract(InputAction.CallbackContext ctx)
    {   
        if (playerState == null)
        {
            Debug.LogError("PlayerBikeInteraction: playerState is NULL");
            return;
        }

        if (segmentSwitcher == null)
        {
            Debug.LogError("PlayerBikeInteraction: segmentSwitcher is NULL");
            return;
        }
        
        Debug.Log(
            $"E pressed! isCycling={playerState.isCycling}, " +
            $"currentBikeStand={(playerState.currentBikeStand ? playerState.currentBikeStand.name : "null")}, " +
            $"canDismountBike={playerState.canDismountBike}"
        );

        if (!segmentSwitcher.runnerInput.enabled &&
        !segmentSwitcher.cyclistInput.enabled)
        {
            Debug.Log("Interact blocked during tutorial");
            return;
        }

        // ---------- DISMOUNT ----------
        if (playerState.isCycling && playerState.canDismountBike && playerState.currentBikeStand != null)
        {
            Debug.Log("Dismount logic running");

            BikeStandTutorial stand = playerState.currentBikeStand;

            if (stand.parkingMarker)
                stand.parkingMarker.SetActive(false);

            if (stand.parkedBike)
                stand.parkedBike.SetActive(true);

            stand.isOccupied = true;

            playerState.isCycling = false;
            playerState.canDismountBike = false;
            playerState.currentBikeStand = null;

            segmentSwitcher.SwitchToRunner(
                stand.dismountPoint.position,
                stand.dismountPoint.rotation
            );

            return;
        }

        // ---------- MOUNT ----------
        if (!playerState.isCycling && playerState.canMountBike && playerState.currentBike != null)
        {
            Debug.Log("Mount logic running");

            playerState.isCycling = true;

            playerState.currentBike.gameObject.SetActive(false);

            segmentSwitcher.SwitchToCyclist(
                playerState.currentBike.mountTransform.position,
                playerState.currentBike.mountTransform.rotation
            );

            return;
        }
    }
}

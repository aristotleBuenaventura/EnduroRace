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
            return;
        }

        interactAction = currentInput.actions["Interact"];
        interactAction.performed += OnInteract;

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
            return;
        }

        if (segmentSwitcher == null)
        {
            return;
        }
        
        if (!segmentSwitcher.runnerInput.enabled &&
        !segmentSwitcher.cyclistInput.enabled)
        {
            return;
        }

        // ---------- DISMOUNT ----------
        if (playerState.isCycling && playerState.canDismountBike && playerState.currentBikeStand != null)
        {

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

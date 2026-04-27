using UnityEngine;
using UnityEngine.InputSystem;
using FishNet.Object;
using System.Collections;

public class PlayerBikeInteraction : NetworkBehaviour
{
    public SegmentSwitcher segmentSwitcher;

    [Header("Optional")]
    public RaceManager raceManager;

    private InputAction interactAction;
    private RaceManager.Segment myCurrentSegment = RaceManager.Segment.Swim;

    public override void OnStartClient()
    {
        base.OnStartClient();

        if (!IsOwner) return;

        // Initial bind after one frame so action map is ready
        StartCoroutine(DelayedBind());

        if (raceManager == null)
            raceManager = Object.FindFirstObjectByType<RaceManager>();
    }

    private void OnDestroy()
    {
        if (interactAction != null)
            interactAction.performed -= OnInteract;
    }

    private IEnumerator DelayedBind()
    {
        yield return null;
        RebindInteract();
    }

    // ✅ Public so SegmentSwitcher can call it explicitly after action map switches
    public void RebindInteract()
    {
        if (interactAction != null)
            interactAction.performed -= OnInteract;

        if (segmentSwitcher?.playerInput == null)
        {
            ;
            return;
        }

        var currentMap = segmentSwitcher.playerInput.currentActionMap;
        ;

        if (currentMap == null)
        {
            ;
            return;
        }

        interactAction = currentMap.FindAction("Interact");
        ;

        if (interactAction == null)
        {
            ;
            return;
        }

        interactAction.performed += OnInteract;
        ;
    }

    private void OnInteract(InputAction.CallbackContext ctx)
    {
        ;
        if (!segmentSwitcher || !IsOwner) return;

        // =============================
        // MOUNT (Runner -> Bike)
        // =============================
        if (segmentSwitcher.runnerModel.activeSelf)
        {
            if (raceManager != null && myCurrentSegment != RaceManager.Segment.Swim)
            {
                ;
                return;
            }

            BikeTrigger closestBike = null;
            float closestDist = float.MaxValue;

            foreach (var bike in SceneReference.GetBikeTriggers())
            {
                if (!bike.isActiveAndEnabled) continue;

                float dist = Vector3.Distance(
                    segmentSwitcher.CurrentPlayerTransform.position,
                    bike.transform.position
                );

                if (dist < closestDist)
                {
                    closestDist = dist;
                    closestBike = bike;
                }
            }

            if (closestBike == null)
            {
                ;
                return;
            }

            if (closestDist > closestBike.triggerRadius)
            {
                ;
                return;
            }

            if (!closestBike.isPlayerNearby)
            {
                ;
                return;
            }

            Vector3 mountPos = closestBike.mountPoint.position;
            Quaternion mountRot = closestBike.mountPoint.rotation;

            ;
            segmentSwitcher.SwitchToCyclist(closestBike.gameObject, mountPos, mountRot);

            if (raceManager != null)
            {
                myCurrentSegment = RaceManager.Segment.Bike;
                raceManager.ProgressToSegment(RaceManager.Segment.Bike);
            }

            if (closestBike.mountPromptUI != null)
                closestBike.mountPromptUI.SetActive(false);

            foreach (var dismount in SceneReference.GetBikeDismountTriggers())
            {
                float dist = Vector3.Distance(
                    segmentSwitcher.CurrentPlayerTransform.position,
                    dismount.transform.position
                );
                if (dist <= dismount.triggerRadius && dismount.dismountPromptUI != null)
                    dismount.dismountPromptUI.SetActive(true);
            }
        }
        // =============================
        // DISMOUNT (Bike -> Runner)
        // =============================
        else
        {
            BikeDismountTrigger closestDismount = null;
            float closestDist = float.MaxValue;

            foreach (var dismount in SceneReference.GetBikeDismountTriggers())
            {
                float dist = Vector3.Distance(
                    segmentSwitcher.CurrentPlayerTransform.position,
                    dismount.transform.position
                );

                if (dist < closestDist)
                {
                    closestDist = dist;
                    closestDismount = dismount;
                }
            }

            if (closestDismount == null || closestDist > closestDismount.triggerRadius)
            {
                ;
                return;
            }

            if (raceManager != null && myCurrentSegment != RaceManager.Segment.Bike)
            {
                ;
                return;
            }

            closestDismount.GetDismountTransform(out Vector3 pos, out Quaternion rot);
            ;
            segmentSwitcher.SwitchToRunner(pos, rot);

            if (raceManager != null)
            {
                myCurrentSegment = RaceManager.Segment.Run;
                raceManager.ProgressToSegment(RaceManager.Segment.Run);
            }

            if (closestDismount.dismountPromptUI != null)
                closestDismount.dismountPromptUI.SetActive(false);
        }
    }

    public RaceManager.Segment GetCurrentSegment() => myCurrentSegment;
} 
using FishNet.Object;
using FishNet.Object.Synchronizing;
using FishNet.Component.Transforming;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
using System.Collections;

public class SegmentSwitcher : NetworkBehaviour
{
    [Header("Runner")]
    public GameObject runnerModel;
    public Transform runnerFollowPoint;
    public Transform runnerLookPoint;

    [Header("Cyclist")]
    public GameObject cyclistModel;
    public Animator cyclistAnimator;
    public Transform cyclistFollowPoint;
    public Transform cyclistLookPoint;

    [Header("Input")]
    public PlayerInput playerInput;

    [Header("Controllers")]
    public PlayerController runnerController;
    public CyclingController cyclistController;

    [Header("Bike Interaction")]
    public PlayerBikeInteraction bikeInteraction;

    [Header("Animation Timing")]
    public float mountAnimTime = 1.2f;
    public float dismountAnimTime = 1f;

    public event Action OnPlayerModelChanged;

    private NetworkPlayer networkPlayer;
    private bool isSwitching;
    private GameObject currentBike;

    public bool IsOnBike => networkPlayer != null && networkPlayer.IsOnBike.Value;
    public Transform CurrentPlayerTransform => runnerModel.activeSelf
        ? runnerModel.transform
        : cyclistModel.transform;

    void Awake()
    {
        networkPlayer = GetComponent<NetworkPlayer>();
    }

    public override void OnStartClient()
    {
        base.OnStartClient();

        if (runnerModel != null)
        {
            runnerModel.transform.localPosition = Vector3.zero;
            runnerModel.transform.localRotation = Quaternion.identity;
        }

        if (cyclistModel != null)
        {
            cyclistModel.transform.localPosition = Vector3.zero;
            cyclistModel.transform.localRotation = Quaternion.identity;
        }

        runnerModel.SetActive(true);
        cyclistModel.SetActive(false);

        if (IsOwner)
        {
            runnerController.isActiveModel = true;
            cyclistController.isActiveModel = false;
            AssignCamera(runnerFollowPoint, runnerLookPoint);
            StartCoroutine(InitInput());

            // ✅ Assign minimap to local player on spawn
            UpdateMinimapTargets();

            // ✅ Re-assign whenever model switches (mount/dismount)
            OnPlayerModelChanged += UpdateMinimapTargets;
        }
        else
        {
            runnerController.isActiveModel = false;
            cyclistController.isActiveModel = false;
        }

        networkPlayer.IsOnBike.OnChange += OnBikeStateChanged;

        Debug.Log($"[SegmentSwitcher] Initialized - IsOwner={IsOwner}");
    }

    private void OnDestroy()
    {
        if (networkPlayer != null)
            networkPlayer.IsOnBike.OnChange -= OnBikeStateChanged;

        // ✅ Clean up event subscription
        OnPlayerModelChanged -= UpdateMinimapTargets;
    }

    // ✅ Called on spawn and on every model switch
    private void UpdateMinimapTargets()
    {
        Transform t = CurrentPlayerTransform;
        SceneReference.GetMinimapCameraFollow()?.SetTarget(t);
        SceneReference.GetMinimapIconFollow()?.SetTarget(t);
        Debug.Log($"[SegmentSwitcher] Minimap targets updated to: {t?.name}");
    }

    private void OnBikeStateChanged(bool prev, bool next, bool asServer)
    {
        Debug.Log($"[SegmentSwitcher] Bike state changed: {prev} → {next} (IsOwner={IsOwner})");

        if (!isSwitching)
        {
            runnerModel.SetActive(!next);
            cyclistModel.SetActive(next);

            if (!next)
            {
                runnerModel.transform.localPosition = Vector3.zero;
                runnerModel.transform.localRotation = Quaternion.identity;
            }
            else
            {
                cyclistModel.transform.localPosition = Vector3.zero;
                cyclistModel.transform.localRotation = Quaternion.identity;
            }

            if (IsOwner)
            {
                runnerController.isActiveModel = !next;
                cyclistController.isActiveModel = next;
            }
        }
    }

    private IEnumerator InitInput()
    {
        yield return null;
        if (playerInput != null)
            playerInput.SwitchCurrentActionMap("Runner");
    }

    public void SwitchToCyclist(GameObject bike, Vector3 pos, Quaternion rot)
    {
        if (!IsOwner || isSwitching) return;
        currentBike = bike;
        StartCoroutine(MountRoutine(pos, rot));
    }

    public void SwitchToRunner(Vector3 pos, Quaternion rot)
    {
        Debug.Log($"[SegmentSwitcher] SwitchToRunner called - IsOwner={IsOwner} isSwitching={isSwitching}");
        if (!IsOwner || isSwitching) return;
        StartCoroutine(DismountRoutine(pos, rot));
    }

    private IEnumerator MountRoutine(Vector3 pos, Quaternion rot)
    {
        isSwitching = true;

        runnerController.isActiveModel = false;
        networkPlayer.SetCanMove(false);

        runnerModel.SetActive(false);

        cyclistModel.SetActive(true);
        cyclistModel.transform.localPosition = Vector3.zero;
        cyclistModel.transform.localRotation = Quaternion.identity;
        currentBike?.SetActive(false);

        cyclistAnimator?.SetTrigger("Mount");

        yield return new WaitForSeconds(mountAnimTime);

        cyclistController.isActiveModel = true;

        if (playerInput != null)
            playerInput.SwitchCurrentActionMap("Cyclist");

        networkPlayer.SetOnBike(true);
        networkPlayer.SetCanMove(true);

        AssignCamera(cyclistFollowPoint, cyclistLookPoint);
        OnPlayerModelChanged?.Invoke();

        bikeInteraction?.RebindInteract();

        Debug.Log("[SegmentSwitcher] Mounted bike");
        isSwitching = false;
    }

    private IEnumerator DismountRoutine(Vector3 pos, Quaternion rot)
    {
        isSwitching = true;

        cyclistController.isActiveModel = false;
        networkPlayer.SetCanMove(false);

        cyclistAnimator?.SetTrigger("Dismount");

        yield return new WaitForSeconds(dismountAnimTime);

        runnerController.ResetForDismount(pos, rot);

        runnerModel.SetActive(true);

        cyclistModel.SetActive(false);

        runnerController.isActiveModel = true;

        if (playerInput != null)
            playerInput.SwitchCurrentActionMap("Runner");

        networkPlayer.SetOnBike(false);
        networkPlayer.SetCanMove(true);

        AssignCamera(runnerFollowPoint, runnerLookPoint);
        OnPlayerModelChanged?.Invoke();

        Debug.Log($"[SegmentSwitcher] About to rebind - current map: {playerInput.currentActionMap?.name}");
        bikeInteraction?.RebindInteract();

        isSwitching = false;
    }

    private void AssignCamera(Transform follow, Transform look)
    {
        var cam = SceneReference.GetVirtualCamera();
        if (!cam) return;
        cam.Follow = follow;
        cam.LookAt = look;
    }
}
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Cinemachine;
using System;
using System.Collections;

public class SegmentSwitcherTutorial : MonoBehaviour
{
    [Header("Runner")]
    public GameObject runnerModel;
    public PlayerInput runnerInput;
    public Transform runnerFollowPoint;
    public Transform runnerLookPoint;
    public Animator runnerAnimator;

    [Header("Cyclist")]
    public GameObject cyclistModel;
    public PlayerInput cyclistInput;
    public Transform cyclistFollowPoint;
    public Transform cyclistLookPoint;
    public Animator cyclistAnimator;

    [Header("Cinemachine")]
    public CinemachineCamera virtualCamera;

    [Header("Minimap")]
    public MiniMapIconFollowTutorial minimapIconFollow;
    public MiniMapCameraFollowTutorial minimapCameraFollow;

    [Header("Animation Timing")]
    public float mountAnimTime = 1.2f;
    public float dismountAnimTime = 1.0f;

    public event Action OnPlayerModelChanged;

    public Transform CurrentPlayerTransform =>
        runnerModel.activeSelf ? runnerModel.transform : cyclistModel.transform;

    private bool isSwitching = false;
    private bool globalInputLocked = false;

    private void Start()
    {
        runnerModel.SetActive(true);
        runnerInput.enabled = false;
        runnerModel.transform.rotation = Quaternion.Euler(0f, -90f, 0f);

        cyclistModel.SetActive(false);
        cyclistInput.enabled = false;

        AssignCameraTargets(runnerFollowPoint, runnerLookPoint);

        if (minimapIconFollow)
            minimapIconFollow.target = runnerModel.transform;

        if (minimapCameraFollow)
            minimapCameraFollow.target = runnerModel.transform;
    }

    public void SwitchToCyclist(Vector3 targetPos, Quaternion targetRot)
    {
        if (isSwitching) return;
        StartCoroutine(MountRoutine(targetPos, targetRot));
    }

    public void SwitchToRunner(Vector3 targetPos, Quaternion targetRot)
    {
        if (isSwitching) return;
        StartCoroutine(DismountRoutine(targetPos, targetRot));
    }

    public void SwitchToRunnerModel()
    {
        cyclistModel.SetActive(false);
        runnerModel.SetActive(true);
        runnerInput.enabled = true;
        cyclistInput.enabled = false;
    }

    public void EnableRunnerInput() => runnerInput.enabled = true;
    public void EnableCyclistInput() => cyclistInput.enabled = true;

    private IEnumerator MountRoutine(Vector3 targetPos, Quaternion targetRot)
    {
        isSwitching = true;

        ResetCollisionOnSwitch();
        DeactivateRunner();

        cyclistModel.transform.SetPositionAndRotation(targetPos, targetRot);
        cyclistModel.SetActive(true);
        cyclistInput.enabled = false;

        AssignCameraTargets(cyclistFollowPoint, cyclistLookPoint);

        if (minimapIconFollow)
        {
            minimapIconFollow.target = cyclistModel.transform;
            minimapIconFollow.rotationOffset = 180f;
        }

        if (minimapCameraFollow)
            minimapCameraFollow.target = cyclistModel.transform;

        if (cyclistAnimator)
        {
            cyclistAnimator.ResetTrigger("Dismount");
            cyclistAnimator.SetTrigger("Mount");
        }

        yield return new WaitForSeconds(mountAnimTime);

        if (!globalInputLocked)
            cyclistInput.enabled = true;

        OnPlayerModelChanged?.Invoke();
        isSwitching = false;
    }

    private IEnumerator DismountRoutine(Vector3 targetPos, Quaternion targetRot)
    {
        isSwitching = true;

        ResetCollisionOnSwitch();
        cyclistInput.enabled = false;

        if (cyclistAnimator)
        {
            cyclistAnimator.ResetTrigger("Mount");
            cyclistAnimator.SetTrigger("Dismount");
        }

        yield return new WaitForSeconds(dismountAnimTime);

        DeactivateCyclist();

        runnerModel.transform.SetPositionAndRotation(targetPos, targetRot);
        runnerModel.SetActive(true);

        if (!globalInputLocked)
            runnerInput.enabled = true;

        AssignCameraTargets(runnerFollowPoint, runnerLookPoint);

        if (minimapIconFollow)
        {
            minimapIconFollow.target = runnerModel.transform;
            minimapIconFollow.rotationOffset = 0f;
        }

        if (minimapCameraFollow)
            minimapCameraFollow.target = runnerModel.transform;

        OnPlayerModelChanged?.Invoke();
        isSwitching = false;
    }

    private void DeactivateRunner()
    {
        runnerModel.SetActive(false);
        runnerInput.enabled = false;
    }

    private void DeactivateCyclist()
    {
        cyclistModel.SetActive(false);
        cyclistInput.enabled = false;
    }

    private void AssignCameraTargets(Transform follow, Transform look)
    {
        if (!virtualCamera) return;
        virtualCamera.Follow = follow;
        virtualCamera.LookAt = look;
    }

    private void ResetCollisionOnSwitch()
    {
        var runnerCollision = runnerModel.GetComponent<PlayerCollision>();
        if (runnerCollision != null) runnerCollision.ResetBounce();

        var cyclistCollision = cyclistModel.GetComponent<PlayerCollision>();
        if (cyclistCollision != null) cyclistCollision.ResetBounce();
    }

    public void SetInputEnabled(bool enabled)
    {
        if (runnerInput != null)
        {
            if (runnerModel.activeSelf)
            {
                runnerInput.enabled = enabled;
                if (enabled) runnerInput.actions.Enable();
                else runnerInput.actions.Disable();
            }
            else
            {
                runnerInput.enabled = false;
                runnerInput.actions.Disable();
            }
        }

        if (cyclistInput != null)
        {
            if (cyclistModel.activeSelf)
            {
                cyclistInput.enabled = enabled;
                if (enabled) cyclistInput.actions.Enable();
                else cyclistInput.actions.Disable();
            }
            else
            {
                cyclistInput.enabled = false;
                cyclistInput.actions.Disable();
            }
        }
    }

    public void LockPlayerInput(bool locked)
    {
        globalInputLocked = locked;

        if (runnerInput != null)
        {
            var runnerController = runnerInput.GetComponentInChildren<PlayerControllerTutorial>();
            if (runnerController != null)
                runnerController.inputLocked = locked;
        }

        if (cyclistInput != null)
        {
            var cyclistController = cyclistInput.GetComponentInChildren<PlayerControllerTutorial>();
            if (cyclistController != null)
                cyclistController.inputLocked = locked;
        }
    }
}
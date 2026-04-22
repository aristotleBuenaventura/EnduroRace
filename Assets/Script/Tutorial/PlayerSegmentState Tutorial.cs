using UnityEngine;

public class PlayerSegmentStateTutorial : MonoBehaviour
{
    [Header("Models")]
    public GameObject runnerModel;
    public GameObject cyclistModel;

    [Header("State")]
    public bool isCycling = false;           // true if player is on a bike
    public bool canMountBike = false;        // near a bike
    public bool canDismountBike = false;     // near a stand
    public BikeMountPointTutorial currentBike;
    public BikeStandTutorial currentBikeStand;

    [Header("Barrier")]
    public GameObject bikeSegmentBarrier;
    public GameObject runSegmentBarrier;

    public Transform CurrentPlayerTransform =>
        runnerModel.activeSelf ? runnerModel.transform : cyclistModel.transform;

    // Mount bike
    public void MountBike()
    {
        if (!canMountBike || currentBike == null) return;

        isCycling = true;
        runnerModel.SetActive(false);
        cyclistModel.SetActive(true);

        if (currentBike.mountTransform != null)
        {
            transform.position = currentBike.mountTransform.position;
            transform.rotation = currentBike.mountTransform.rotation;
        }

        // Remove barrier the moment player mounts the bike
        if (bikeSegmentBarrier != null)
        {
            bikeSegmentBarrier.SetActive(false);
            bikeSegmentBarrier = null;
        }

        currentBike = null;
    }

    // Dismount bike
    public void DismountBike(Vector3 dismountPosition)
    {
        isCycling = false;
        cyclistModel.SetActive(false);
        runnerModel.SetActive(true);

        transform.position = dismountPosition;
        currentBikeStand = null;

        // Remove run segment barrier the moment player dismounts
        if (runSegmentBarrier != null)
        {
            runSegmentBarrier.SetActive(false);
            runSegmentBarrier = null;
        }
    }
}

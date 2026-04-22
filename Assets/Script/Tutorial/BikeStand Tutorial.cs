using UnityEngine;

public class BikeStandTutorial : MonoBehaviour
{
    [Header("Dismount")]
    public Transform dismountPoint;
    public GameObject parkingMarker;

    [Header("Bike Info")]
    public bool isOccupied = false;
    public GameObject parkedBike;

    public float dismountTriggerRadius = 2f;
}

using UnityEngine;

public class BikeStandMarkerTutorial : MonoBehaviour
{
    public BikeStandTutorial bikeStand;           // assign the parent BikeStand
    public float triggerRadius = 2f;      // how close the player needs to be
    public GameObject dismountPromptUI;   // "Press E to dismount" UI

    private PlayerSegmentStateTutorial playerState;

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
            playerState = player.GetComponent<PlayerSegmentStateTutorial>();

        if (dismountPromptUI != null)
            dismountPromptUI.SetActive(false); // hide UI at start
    }

    private void Update()
    {
        if (playerState == null || !playerState.isCycling)
        {
            if (dismountPromptUI != null)
                dismountPromptUI.SetActive(false); // hide UI if not cycling
            return;
        }

        float distance = Vector3.Distance(playerState.CurrentPlayerTransform.position, transform.position);

        if (distance <= triggerRadius && !bikeStand.isOccupied)
        {
            playerState.canDismountBike = true;
            playerState.currentBikeStand = bikeStand;

            if (dismountPromptUI != null)
                dismountPromptUI.SetActive(true); // show UI
        }
        else
        {
            if (playerState.currentBikeStand == bikeStand)
            {
                playerState.canDismountBike = false;
                playerState.currentBikeStand = null;

                if (dismountPromptUI != null)
                    dismountPromptUI.SetActive(false); // hide UI
            }
        }
    }

    // Optional: visualize the trigger radius
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, triggerRadius);
    }
}

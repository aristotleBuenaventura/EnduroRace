using UnityEngine;

public class BikeTriggerTutorial : MonoBehaviour
{
    public BikeMountPointTutorial bikeMountPoint;
    public float triggerRadius = 2f;
    public GameObject mountPromptUI; // Assign your "Press E to mount" UI here

    private PlayerSegmentStateTutorial playerState;

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
            playerState = player.GetComponent<PlayerSegmentStateTutorial>();

        if (mountPromptUI != null)
            mountPromptUI.SetActive(false); // Hide UI at start
    }

    private void Update()
    {
        if (playerState == null || playerState.isCycling) 
        {
            if (mountPromptUI != null)
                mountPromptUI.SetActive(false); // Hide UI if cycling
            return;
        }

        float distance = Vector3.Distance(
            playerState.CurrentPlayerTransform.position,
            transform.position
        );

        if (distance <= triggerRadius)
        {
            playerState.canMountBike = true;
            playerState.currentBike = bikeMountPoint;

            if (mountPromptUI != null)
                mountPromptUI.SetActive(true); // Show UI
        }
        else
        {
            if (playerState.currentBike == bikeMountPoint)
            {
                playerState.currentBike = null;
                playerState.canMountBike = false;

                if (mountPromptUI != null)
                    mountPromptUI.SetActive(false); // Hide UI
            }
        }
    }

    // Optional: Draw the trigger radius in Scene view
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, triggerRadius);
    }
}

using UnityEngine;
using FishNet.Object;

public class BikeDismountTrigger : NetworkBehaviour
{
    [Header("References")]
    public Transform dismountPoint;
    public float triggerRadius = 2f;
    public GameObject dismountPromptUI;
    public GameObject textBG;

    private SegmentSwitcher localSegmentSwitcher;
    private bool isPlayerNearby;

    private void Start()
    {
        if (dismountPromptUI != null)
            dismountPromptUI.SetActive(false); // hide UI initially
            textBG.SetActive(false);
    }

    private void Update()
    {
        if (dismountPromptUI == null) return;

        // Find local player if not yet assigned
        if (localSegmentSwitcher == null)
        {
            foreach (var np in FindObjectsByType<NetworkPlayer>(FindObjectsSortMode.None))
            {
                if (np.IsOwner) // CHANGED: HasInputAuthority -> IsOwner
                {
                    localSegmentSwitcher = np.GetComponent<SegmentSwitcher>();
                    break;
                }
            }
            if (localSegmentSwitcher == null) return; // still not found
        }

        // Only show UI if the player is on the bike
        if (!localSegmentSwitcher.IsOnBike)
        {
            if (isPlayerNearby)
            {
                isPlayerNearby = false;
                dismountPromptUI.SetActive(false);
                textBG.SetActive(false);
                ;
            }
            return;
        }

        // Distance check
        float distance = Vector3.Distance(localSegmentSwitcher.CurrentPlayerTransform.position, transform.position);

        if (distance <= triggerRadius)
        {
            if (!isPlayerNearby)
            {
                isPlayerNearby = true;
                dismountPromptUI.SetActive(true);
                textBG.SetActive(true);
                ;
            }
        }
        else
        {
            if (isPlayerNearby)
            {
                isPlayerNearby = false;
                dismountPromptUI.SetActive(false);
                textBG.SetActive(true);
                ;
            }
        }
    }

    // Returns the dismount position & rotation
    public void GetDismountTransform(out Vector3 pos, out Quaternion rot)
    {
        pos = dismountPoint.position;
        rot = dismountPoint.rotation;
    }
    
    public void DismountPlayer()
    {
        if (localSegmentSwitcher != null)
        {
            // Get the dismount position & rotation
            Vector3 dismountPos;
            Quaternion dismountRot;
            GetDismountTransform(out dismountPos, out dismountRot);

            // Tell the SegmentSwitcher to switch back to runner
            localSegmentSwitcher.SwitchToRunner(dismountPos, dismountRot);

            ;
        }
    }
}
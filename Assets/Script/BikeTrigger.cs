using UnityEngine;
using FishNet.Object;

public class BikeTrigger : NetworkBehaviour
{
    [Header("References")]
    public Transform mountPoint;
    public float triggerRadius = 2f;
    public GameObject mountPromptUI;

    [HideInInspector] public bool isPlayerNearby = false;

    private SegmentSwitcher localSegmentSwitcher;

    private void Start()
    {
        if (mountPromptUI != null)
            mountPromptUI.SetActive(false);
    }

    private void Update()
    {
        if (mountPromptUI == null) return;

        if (localSegmentSwitcher == null)
        {
            foreach (var np in FindObjectsByType<NetworkPlayer>(FindObjectsSortMode.None))
            {
                if (np.IsOwner)
                {
                    localSegmentSwitcher = np.GetComponent<SegmentSwitcher>();
                    break;
                }
            }

            if (localSegmentSwitcher == null)
                return;
        }

        if (localSegmentSwitcher.IsOnBike)
        {
            if (mountPromptUI.activeSelf)
                mountPromptUI.SetActive(false);

            isPlayerNearby = false;
            return;
        }

        float distance = Vector3.Distance(
            localSegmentSwitcher.CurrentPlayerTransform.position,
            transform.position
        );

        if (distance <= triggerRadius)
        {
            if (!isPlayerNearby)
            {
                isPlayerNearby = true;
                mountPromptUI.SetActive(true);
                Debug.Log("[BikeTrigger] Player entered mount area!");
            }
        }
        else
        {
            if (isPlayerNearby)
            {
                isPlayerNearby = false;
                mountPromptUI.SetActive(false);
                Debug.Log("[BikeTrigger] Player left mount area!");
            }
        }
    }

    public void GetMountTransform(out Vector3 pos, out Quaternion rot)
    {
        pos = mountPoint.position;
        rot = mountPoint.rotation;
    }

    public void MountPlayer()
    {
        if (localSegmentSwitcher != null)
        {
            GetMountTransform(out Vector3 pos, out Quaternion rot);
            localSegmentSwitcher.SwitchToCyclist(this.gameObject, pos, rot);
            Debug.Log("[BikeTrigger] Player mounting this bike!");
        }
    }
}
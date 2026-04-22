using UnityEngine;

public class WhirlpoolTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Water")) return;

        if (other.TryGetComponent(out PlayerControllerTutorial player))
        {
            player.isInWhirlpool = true;
            player.whirlpoolCenter = transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Water")) return;

        if (other.TryGetComponent(out PlayerControllerTutorial player))
        {
            player.isInWhirlpool = false;
            player.whirlpoolCenter = null;
        }
    }
}

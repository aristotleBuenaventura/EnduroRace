using UnityEngine;

public class StartAreaTriggerTutorial : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        // Notify RaceManager
        RaceManagerTutorial raceManager = Object.FindFirstObjectByType<RaceManagerTutorial>();
        if (raceManager != null)
            raceManager.OnPlayerReachedStartArea();

        // Hide or disable the marker
        gameObject.SetActive(false);
    }
}

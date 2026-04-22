using UnityEngine;

public class RaceTrigger : MonoBehaviour
{
    public enum TriggerType { SwimToBike, BikeToRun, Finish }
    public TriggerType triggerType;

    public RaceManagerTutorial raceManager;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        switch (triggerType)
        {
            case TriggerType.SwimToBike:
                if (raceManager.currentSegment == RaceManagerTutorial.Segment.Swim)
                    raceManager.ReachSwimToBikePoint();
                break;

            case TriggerType.BikeToRun:
                if (raceManager.currentSegment == RaceManagerTutorial.Segment.Bike)
                    raceManager.ReachBikeToRunPoint();
                break;

            case TriggerType.Finish:
                raceManager.FinishRace();
                break;
        }
    }
}

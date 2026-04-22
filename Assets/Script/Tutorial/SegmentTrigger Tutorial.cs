using UnityEngine;

public class SegmentTriggerTutorial : MonoBehaviour
{
    public enum TriggerType { SwimToBike, BikeToRun }
    public TriggerType triggerType;

    public RaceManagerTutorial raceManager;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        PlayerSegmentStateTutorial playerState =
            other.GetComponentInParent<PlayerSegmentStateTutorial>();

        if (playerState == null) return;

        switch (triggerType)
        {
            case TriggerType.SwimToBike:
                if (raceManager.currentSegment == RaceManagerTutorial.Segment.Swim &&
                    playerState.isCycling)
                {
                    raceManager.currentSegment = RaceManagerTutorial.Segment.Bike;
                    raceManager.RefreshSegmentUI();
                }
                break;

            case TriggerType.BikeToRun:
                if (raceManager.currentSegment == RaceManagerTutorial.Segment.Bike &&
                    !playerState.isCycling)
                {
                    raceManager.currentSegment = RaceManagerTutorial.Segment.Run;
                    raceManager.RefreshSegmentUI();
                }
                break;
        }
    }
}

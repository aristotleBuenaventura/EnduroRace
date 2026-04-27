using UnityEngine;
using FishNet.Object;

public class RaceSegmentTrigger : MonoBehaviour
{
    public enum TriggerType
    {
        SwimToBike,
        BikeToRun,
        Finish
    }

    public TriggerType triggerType;
    public RaceManager raceManager;

    private void Start()
    {
        // ✅ Auto-find RaceManager if not assigned
        if (raceManager == null)
        {
            raceManager = Object.FindFirstObjectByType<RaceManager>();
            if (raceManager == null)
            {
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // ✅ Check if it's a player
        NetworkPlayer networkPlayer = other.GetComponentInParent<NetworkPlayer>();
        if (networkPlayer == null) return;
        
        // ✅ Only the owner of this player should trigger
        if (!networkPlayer.IsOwner) return;
        
        // ✅ Get the player's bike interaction to check their current segment
        PlayerBikeInteraction bikeInteraction = networkPlayer.GetComponent<PlayerBikeInteraction>();
        if (bikeInteraction == null)
        {
            return;
        }

        RaceManager.Segment currentSegment = bikeInteraction.GetCurrentSegment();

        switch (triggerType)
        {
            case TriggerType.SwimToBike:
                if (currentSegment != RaceManager.Segment.Swim)
                {
                    return;
                }
                raceManager?.ProgressToSegment(RaceManager.Segment.Bike);
                break;

            case TriggerType.BikeToRun:
                if (currentSegment != RaceManager.Segment.Bike)
                {
                    return;
                }
                raceManager?.ProgressToSegment(RaceManager.Segment.Run);
                break;

            case TriggerType.Finish:
                if (currentSegment != RaceManager.Segment.Run)
                {
                    return;
                }
                raceManager?.FinishRace();

                // Show podium for this player
                PodiumManager podiumManager = Object.FindFirstObjectByType<PodiumManager>();
                if (podiumManager != null)
                {
                    int placement = Object.FindFirstObjectByType<MultiplayerRaceRanking>()
                        ?.GetLocalPlayerRank() ?? 1;
                    float raceTime = Object.FindFirstObjectByType<RaceManager>()?.GetRaceTime() ?? 0f;
                    podiumManager.ShowPodium(placement, raceTime);
                }
                break;
        }

        // ✅ Disable this trigger after first use
        GetComponent<Collider>().enabled = false;
    }
}
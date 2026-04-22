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
                Debug.LogWarning("[RaceSegmentTrigger] No RaceManager found in scene!");
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
            Debug.LogWarning("[RaceSegmentTrigger] PlayerBikeInteraction not found on player!");
            return;
        }

        RaceManager.Segment currentSegment = bikeInteraction.GetCurrentSegment();

        switch (triggerType)
        {
            case TriggerType.SwimToBike:
                if (currentSegment != RaceManager.Segment.Swim)
                {
                    Debug.Log("[RaceSegmentTrigger] Player not in Swim segment, cannot progress to Bike");
                    return;
                }
                Debug.Log("[RaceSegmentTrigger] Player progressing to Bike segment");
                raceManager?.ProgressToSegment(RaceManager.Segment.Bike);
                break;

            case TriggerType.BikeToRun:
                if (currentSegment != RaceManager.Segment.Bike)
                {
                    Debug.Log("[RaceSegmentTrigger] Player not in Bike segment, cannot progress to Run");
                    return;
                }
                Debug.Log("[RaceSegmentTrigger] Player progressing to Run segment");
                raceManager?.ProgressToSegment(RaceManager.Segment.Run);
                break;

            case TriggerType.Finish:
                if (currentSegment != RaceManager.Segment.Run)
                {
                    Debug.Log("[RaceSegmentTrigger] Player not in Run segment, cannot finish");
                    return;
                }
                Debug.Log("[RaceSegmentTrigger] Player finished race!");
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
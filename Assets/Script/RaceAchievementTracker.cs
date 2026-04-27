using UnityEngine;
using System.Collections.Generic;
using Firebase.Firestore;
using Firebase.Extensions;
using FishNet.Object;

public class RaceAchievementTracker : NetworkBehaviour
{
    // Track collision state during race
    private bool hadCollision = false;
    private bool hadSpeedLoss = false;
    private bool raceFinished = false;

    public static RaceAchievementTracker LocalInstance { get; private set; }

    public override void OnStartClient()
    {
        base.OnStartClient();
        if (IsOwner)
            LocalInstance = this;
    }

    // Call this from Obstacle.cs when local player hits anything
    public void RegisterCollision()
    {
        if (!IsOwner || raceFinished) return;
        hadCollision = true;
    }

    // Call this from Obstacle.cs when local player loses speed
    public void RegisterSpeedLoss()
    {
        if (!IsOwner || raceFinished) return;
        hadSpeedLoss = true;
    }

    // Call this when race finishes
    public void OnRaceFinished(int placement, string currentTier, string newTier)
    {
        if (!IsOwner) return;
        raceFinished = true;

        List<AchievementData.AchievementID> earned = new List<AchievementData.AchievementID>();

        earned.Add(AchievementData.AchievementID.FirstFinish);

        if (newTier == "Pro")
            earned.Add(AchievementData.AchievementID.OutstandingWinner);

        if (placement == 1 && currentTier == "Pro")
            earned.Add(AchievementData.AchievementID.EnduroMaster);

        if (!hadCollision)
            earned.Add(AchievementData.AchievementID.CleanRun);

        SaveAchievementsToFirebase(earned);
    }

    private void SaveAchievementsToFirebase(
        List<AchievementData.AchievementID> newlyEarned)
    {
        string playerId = FirebaseManager.Instance?.PlayerId;
        if (string.IsNullOrEmpty(playerId)) return;

        var db = FirebaseFirestore.DefaultInstance;

        db.Collection("players").Document(playerId)
            .GetSnapshotAsync()
            .ContinueWithOnMainThread(task =>
            {
                if (task.IsFaulted) return;

                // Get existing achievements
                List<string> existing = new List<string>();
                if (task.Result.ContainsField("achievements"))
                {
                    existing = new List<string>(
                        task.Result.GetValue<List<string>>("achievements")
                    );
                }

                // Add newly earned ones
                foreach (var id in newlyEarned)
                {
                    string idStr = id.ToString();
                    if (!existing.Contains(idStr))
                    {
                        existing.Add(idStr);
                    }
                }

                // Save back
                db.Collection("players").Document(playerId)
                    .UpdateAsync("achievements", existing);
            });
    }
}
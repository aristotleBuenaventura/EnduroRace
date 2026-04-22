using System.Collections.Generic;

public static class AchievementData
{
    public enum AchievementID
    {
        FirstFinish,
        OutstandingWinner,
        EnduroMaster,
        CleanRun,
        NoStumbles
    }

    public class Achievement
    {
        public AchievementID id;
        public string title;
        public string description;
        public string badgeIcon; // emoji or sprite name
    }

    public static readonly List<Achievement> All = new List<Achievement>
    {
        new Achievement
        {
            id = AchievementID.FirstFinish,
            title = "First Finish",
            description = "Complete your first triathlon race.",
            badgeIcon = "badge1"
        },
        new Achievement
        {
            id = AchievementID.OutstandingWinner,
            title = "Outstanding Winner",
            description = "Reach Pro Tier.",
            badgeIcon = "badge2"
        },
        new Achievement
        {
            id = AchievementID.EnduroMaster,
            title = "Enduro Master",
            description = "Win a race in the Pro tier.",
            badgeIcon = "badge3"
        },
        new Achievement
        {
            id = AchievementID.CleanRun,
            title = "Clean Run",
            description = "Finish a race without any obstacle collision.",
            badgeIcon = "badge4"
        },
    };
}
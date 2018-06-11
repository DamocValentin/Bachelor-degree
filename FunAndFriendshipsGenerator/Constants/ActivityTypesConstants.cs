using System.Collections.Generic;

namespace Constants
{
    public static class ActivityTypesConstants
    {
        public const string FootballActivityName = "Football";
        public const string VaolleyBallActivityName = "VolleyBall";
        public const string TennisActivityName = "Tennis";
        public const string RugbyActivityName = "Rugby";
        public const string HandballActivityName = "Handball";
        public const string BasketBallActivityName = "BasketBall";

        private static readonly string[] ActivitiesNames = {
            FootballActivityName,
            VaolleyBallActivityName,
            TennisActivityName,
            RugbyActivityName,
            HandballActivityName,
            BasketBallActivityName};

        public static List<string> GetActivitiesNames()
        {
            return new List<string>(ActivitiesNames);
        }
    }
}

using UnityEngine;

namespace TennisTest.Struct
{
    public class GameStatisticsTemplate
    {
        public int GamesPlayed;
        public int Serves;
        public int Hits;
        public int Points;

        public void ResetStats()
        {
            GamesPlayed = Serves = Hits = Points = 0;
        }
    }
}

using PlayFab.ClientModels;
using PlayFab;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TennisTest.Struct;
using System;

namespace TennisTest.Statistics
{
    public class StatisticsManager : MonoBehaviour
    {
        public StatisticsSettings StatisticsSettings;
        public Action OnStatisticsSaved;

        public void SaveProfileData(UserProfileTemplate currentUserProfile)
        {
            var data = new Dictionary<string, string>
        {
            { "Country", currentUserProfile.Country },
            { "Level", currentUserProfile.Level }
        };

            PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest { Data = data },
                result => Debug.Log("Profile saved"),
                error => Debug.LogError("Failed to save profile: " + error.GenerateErrorReport()));
        }

        public void SaveStatisticsData(GameStatisticsTemplate currentStats)
        {
            var statistics = new List<StatisticUpdate>
        {
            new StatisticUpdate { StatisticName = "GamesPlayed", Value = currentStats.GamesPlayed },
            new StatisticUpdate { StatisticName = "Serves", Value = currentStats.Serves },
            new StatisticUpdate { StatisticName = "Hits", Value = currentStats.Hits },
            new StatisticUpdate { StatisticName = "Points", Value = currentStats.Points }
        };

            PlayFabClientAPI.UpdatePlayerStatistics(new UpdatePlayerStatisticsRequest { Statistics = statistics },
                result => Debug.Log("Statistics saved"),
                error => Debug.LogError("Failed to save stats: " + error.GenerateErrorReport()));
            OnStatisticsSaved?.Invoke();
        }

        public void LoadUserData(UserProfileTemplate userProfile, GameStatisticsTemplate stats, System.Action<UserProfileTemplate, GameStatisticsTemplate> onLoaded)
        {
            PlayFabClientAPI.GetUserData(new GetUserDataRequest(), result =>
            {
                if (result.Data != null)
                {
                    userProfile.Country = result.Data.ContainsKey("Country") ? result.Data["Country"].Value : "Unknown";
                    userProfile.Level = result.Data.ContainsKey("Level") ? result.Data["Level"].Value : "Beginner";
                }

                PlayFabClientAPI.GetPlayerStatistics(new GetPlayerStatisticsRequest(), statsResult =>
                {
                    if (statsResult.Statistics != null && statsResult.Statistics.Count > 0)
                    {
                        foreach (var stat in statsResult.Statistics)
                        {
                            switch (stat.StatisticName)
                            {
                                case "GamesPlayed": stats.GamesPlayed = stat.Value; break;
                                case "Serves": stats.Serves = stat.Value; break;
                                case "Hits": stats.Hits = stat.Value; break;
                                case "Points": stats.Points = stat.Value; break;
                            }
                        }
                    }
                    else
                    {
                        Debug.LogWarning("Player statistics are empty or null");
                        stats.ResetStats();
                    }

                    userProfile.GetLevel(stats.Serves, stats.Points);
                    onLoaded?.Invoke(userProfile, stats);

                }, error => Debug.LogError("Failed to get stats: " + error.GenerateErrorReport()));

            }, error => Debug.LogError("Failed to get user data: " + error.GenerateErrorReport()));
        }

        public (int,int) StatisticCalculation(int palyedGame, int hitedBallls)
        {
            int servedBalls = palyedGame * StatisticsSettings.ServesPerGame;
            int points = hitedBallls * StatisticsSettings.PointPerBall;
            return (servedBalls, points);
        }

    }

}

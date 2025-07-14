using System.Collections;
using System.Collections.Generic;
using TennisTest.Struct;
using UnityEngine;

namespace TennisTest.Struct
{
    [System.Serializable]
    public class PlayerPDFData
    {
        public GameStatisticsTemplate Stats;
        public UserProfileTemplate UserInfo;

        public PlayerPDFData(GameStatisticsTemplate stats, UserProfileTemplate userInfo)
        {
            Stats = stats;
            UserInfo = userInfo;
        }
    }
}


using System;
using UnityEngine;

namespace TennisTest.Struct
{
    [Serializable]
    public class UserProfileTemplate
    {
        public string Username;
        public string Country;
        public string Level = "Beginner";

        public void GetLevel(int numberOfBallls, int points)
        {
            if (points <= (numberOfBallls * 2) && points > numberOfBallls)
            {
                Level = "Master";
            }
            else if (points >= numberOfBallls)
            {
                Level = "Base";
            }
            else
            {
                Level = "Beginner";
            }
        }
    }
}
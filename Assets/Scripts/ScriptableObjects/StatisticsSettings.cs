using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "StatisticSettings", menuName = "Tennis/Statistics")]
public class StatisticsSettings : ScriptableObject
{
    public int ServesPerGame = 10;
    public int PointPerBall = 2;
}

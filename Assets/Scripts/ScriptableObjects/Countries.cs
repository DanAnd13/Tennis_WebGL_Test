using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Countries", menuName = "Tennis/Countries")]
public class Countries : ScriptableObject
{
    public List<countries> CountriesName;
    public enum countries
    {
        America,
        Germany,
        Ukraine,
        England,
        France,
        Spain,
        Italy,
        Canada,
        China,
        Japan,
        Mexico,
        Poland,
        Sweden,
        Turkey
    }
}

using System.Runtime.InteropServices;
using TennisTest.Struct;
using UnityEngine;


namespace TennisTest.PDF
{
    public class PDFExporter : MonoBehaviour
    {
#if UNITY_WEBGL && !UNITY_EDITOR
    [DllImport("__Internal")]
    private static extern void GeneratePDFWithStats(string jsonString);
#endif

        public static void ExportStats(GameStatisticsTemplate stats, UserProfileTemplate userInfo)
        {
            var data = new PlayerPDFData(stats, userInfo);
            string json = JsonUtility.ToJson(data);
            Debug.Log("ExportStats JSON: " + json);

#if UNITY_WEBGL && !UNITY_EDITOR
        GeneratePDFWithStats(json);
#else
            Debug.Log("PDF export is only supported in WebGL build.");
#endif
        }
    }
}

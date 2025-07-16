using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TennisTest.Diagrams
{
    public class TennisStatsChart : MonoBehaviour
    {
        public StatisticsSettings StatisticsSettings;
        public Slider ServedSlider;
        public Slider HitsSlider;
        public Slider PointsSlider;
        public TextMeshProUGUI GamesTMP;
        public TextMeshProUGUI ServedTMP;
        public TextMeshProUGUI HitsTMP;
        public TextMeshProUGUI PointsTMP;

        public void DrawDiagram(int numberOfGames, int servedBalls, int hitedBalls, int points)
        {
            int maxBalls = numberOfGames * StatisticsSettings.ServesPerGame;
            int maxPoints = maxBalls * StatisticsSettings.PointPerBall;

            float serveValue = Mathf.Clamp01((float)servedBalls / maxBalls);
            float hitValue = Mathf.Clamp01((float)hitedBalls / servedBalls);
            float pointValue = Mathf.Clamp01((float)points / maxPoints);

            ServedSlider.value = 0;
            HitsSlider.value = 0;
            PointsSlider.value = 0;

            // Анімація DOTween
            DOTweenMoveAnimation(serveValue, hitValue, pointValue);

            GamesTMP.text = "Number of played games: " + numberOfGames;
            ServedTMP.text = "Served\nballs:\n" + servedBalls.ToString("F2") + "/\n" + maxBalls;
            HitsTMP.text = "Hited\nballs:\n" + hitedBalls.ToString("F2") + "/\n" + servedBalls;
            PointsTMP.text = "Points:\n" + points.ToString("F2") + "/\n" + maxPoints;
        }

        public void DOTweenMoveAnimation(float serveValue, float hitValue, float pointValue)
        {
            ServedSlider.DOValue(serveValue, 1f).SetEase(Ease.OutCubic);
            HitsSlider.DOValue(hitValue, 1f).SetEase(Ease.OutCubic);
            PointsSlider.DOValue(pointValue, 1f).SetEase(Ease.OutCubic);
        }
    }
}

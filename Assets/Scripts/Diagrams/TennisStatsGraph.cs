using DG.Tweening;
using System.Collections.Generic;
using TennisTest.Diagrams;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TennisTest.Diagrams
{
    public class TennisStatsGraph : MonoBehaviour
    {
        [SerializeField] private Image _pointPrefab;
        [SerializeField] private UILineRenderer _lineRenderer;
        [SerializeField] private RectTransform _graphContainer;
        [SerializeField] private float _maxHeight = 100f;

        [Header("Вісь X (підписи)")]
        [SerializeField] private List<TextMeshProUGUI> _xAxisLabels; // Підписи "Матчі", "Подачі", ...

        [Header("Вісь Y (підписи значень)")]
        [SerializeField] private List<TextMeshProUGUI> _yAxisLabels; // Підписи "0", "25", "50", ...

        [Header("Колір точок і лінії")]
        [SerializeField] private Color _pointColor;

        private List<Transform> _spawnedPoints = new();

        public void ShowGraph(int matches, int serves, int hits, int points)
        {
            Clear();

            // Дані
            var values = new List<int> { matches, serves, hits, points };
            int yMax = Mathf.Max(values.ToArray());

            float graphHeight = _maxHeight; // ⚠️ використовуємо фіксовану висоту графіка
            float graphWidth = _graphContainer.rect.width;

            // 🔹 Рівномірний крок по X, враховуючи центрування
            float xStep = graphWidth / values.Count;
            float xOffset = xStep / 2f;

            // 1. Підписи осі Y
            float yStep = yMax / (float)(_yAxisLabels.Count - 1);
            for (int i = 0; i < _yAxisLabels.Count; i++)
            {
                float value = i * yStep;
                _yAxisLabels[i].text = Mathf.RoundToInt(value).ToString();

                // 📌 якщо потрібно розташувати по осі Y:
                var rt = _yAxisLabels[i].rectTransform;
                float yPos = (value / yMax) * graphHeight;
                rt.anchoredPosition = new Vector2(rt.anchoredPosition.x, yPos);
            }

            // 2. Побудова точок та X-підписів
            string[] xNames = { "Матчі", "Подачі", "Відбиті", "Очки" };

            for (int i = 0; i < values.Count; i++)
            {
                float normalizedY = (float)values[i] / yMax;
                float targetY = normalizedY * graphHeight;
                float targetX = i * xStep + xOffset;

                // Точка
                Image spot = Instantiate(_pointPrefab, _graphContainer);
                spot.color = _pointColor;
                RectTransform spotRT = spot.rectTransform;

                spotRT.anchoredPosition = new Vector2(targetX, 0);
                spotRT.DOAnchorPosY(targetY, 0.5f).SetEase(Ease.OutCubic);

                _lineRenderer.controlPointsObjects.Add(spotRT);
                _spawnedPoints.Add(spotRT);

                // Підпис X
                if (i < _xAxisLabels.Count)
                {
                    var xLabel = _xAxisLabels[i];
                    xLabel.text = xNames[i];
                    xLabel.rectTransform.anchoredPosition = new Vector2(targetX, xLabel.rectTransform.anchoredPosition.y);
                }
            }
        }


        private void Clear()
        {
            foreach (var point in _spawnedPoints)
            {
                if (point != null)
                    Destroy(point.gameObject);
            }
            _spawnedPoints.Clear();
            _lineRenderer.controlPointsObjects.Clear();
        }
    }
}

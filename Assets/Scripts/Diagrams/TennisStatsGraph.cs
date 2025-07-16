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
        [SerializeField] private GraphicsSpot _pointPrefab;
        [SerializeField] private UILineRenderer _lineRenderer;
        [SerializeField] private RectTransform _graphContainer;
        [SerializeField] private float _maxHeight = 600f;

        [Header("Вісь X (підписи)")]
        [SerializeField] private List<TextMeshProUGUI> _xAxisLabels;

        [Header("Колір точок і лінії")]
        [SerializeField] private Color _pointColor;

        private readonly List<Transform> _spawnedPoints = new();

        public void ShowGraph(int matches, int serves, int hits, int points)
        {
            Clear();

            var values = new List<int> { matches, serves, hits, points };
            int maxValue = Mathf.Max(values.ToArray());

            float heightStep = _maxHeight / maxValue;

            for (int i = 0; i < values.Count; i++)
            {
                if (i >= _xAxisLabels.Count) continue;

                var xLabel = _xAxisLabels[i];
                var xLabelRT = xLabel.rectTransform;

                Vector3 worldPos = xLabelRT.position;
                Vector3 localPos = _graphContainer.InverseTransformPoint(worldPos);

                float x = localPos.x;
                float y = values[i] * heightStep - (_maxHeight / 2f);

                var point = Instantiate(_pointPrefab, _graphContainer);
                //point.SpotImage.color = _pointColor;
                var pointRT = point.GetComponent<RectTransform>();
                pointRT.anchoredPosition = new Vector2(x, 0);
                pointRT.DOAnchorPosY(y, 0.5f).SetEase(Ease.OutCubic);
                point.ValueLabel.text = values[i].ToString();

                _lineRenderer.controlPointsObjects.Add(pointRT);
                _spawnedPoints.Add(pointRT);
            }

            string[] xNames = { "Матчі", "Подачі", "Відбиті", "Очки" };
            for (int i = 0; i < _xAxisLabels.Count && i < xNames.Length; i++)
            {
                _xAxisLabels[i].text = xNames[i];
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



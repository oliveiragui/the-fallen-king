using System;
using System.Globalization;
using TMPro;
using UnityEngine;

namespace _Game.Scripts.Services.ScoreSystem
{
    public class RankingElementView : MonoBehaviour
    {
        public TextMeshProUGUI rankPosition;
        public TextMeshProUGUI points;
        public TextMeshProUGUI time;
        public TextMeshProUGUI date;

        public Color currentColor = new Color(255, 255, 255, 0);
        public Color defaultColor = new Color(164, 150, 137, 0);
        public float currentSize = 24;
        public int defaultSize = 16;

        public void UpdateValue(RankingElement ranking, int position, bool current)
        {
            var seconds = TimeSpan.FromSeconds(ranking.score.time);
            
            rankPosition.text = position.ToString();
            points.text = ranking.score.points.ToString();
            time.text = string.Format("{0:D2}:{1:D2}:{2:D3}", seconds.Minutes, seconds.Seconds, seconds.Milliseconds);
            date.text = ranking.date;

            if (current)
            {
                rankPosition.color = currentColor;
                points.color = currentColor;
                time.color = currentColor;
                date.color = currentColor;

                rankPosition.fontSize = currentSize;
                points.fontSize = currentSize;
                time.fontSize = currentSize;
                date.fontSize = currentSize;
            }
            else
            {
                rankPosition.color = defaultColor;
                points.color = defaultColor;
                time.color = defaultColor;
                date.color = defaultColor;

                rankPosition.fontSize = defaultSize;
                points.fontSize = defaultSize;
                time.fontSize = defaultSize;
                date.fontSize = defaultSize;
            }
        }

        public void Clear()
        {
            rankPosition.text = "--";
            points.text = "--";
            time.text = "--";
            date.text = "--";

            rankPosition.color = defaultColor;
            points.color = defaultColor;
            time.color = defaultColor;
            date.color = defaultColor;

            rankPosition.fontSize = defaultSize;
            points.fontSize = defaultSize;
            time.fontSize = defaultSize;
            date.fontSize = defaultSize;
        }
    }
}
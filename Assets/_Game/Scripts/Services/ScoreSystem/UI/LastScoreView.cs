using System;
using TMPro;
using UnityEngine;

namespace _Game.Scripts.Services.ScoreSystem
{
    public class LastScoreView : MonoBehaviour
    {
        [SerializeField] Ranking ranking;
        public TextMeshProUGUI points;
        public TextMeshProUGUI time;
        public TextMeshProUGUI date;

        void Start()
        {
            ranking.rankingModified.AddListener(ranking => UpdateValue(ranking.lastScore));
            UpdateValue(ranking.lastScore);
        }

        public void UpdateValue(RankingElement rankingElement)
        {
            var seconds = TimeSpan.FromSeconds(rankingElement.score.time);

            points.text = rankingElement.score.points.ToString();
            time.text = string.Format("{0:D2}:{1:D2}:{2:D3}", seconds.Minutes, seconds.Seconds, seconds.Milliseconds);
            date.text = rankingElement.date;
        }

        public void Clear()
        {
            points.text = "--";
            time.text = "--";
            date.text = "--";
        }
    }
}
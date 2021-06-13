using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts.Services.ScoreSystem
{
    public class RankingView : MonoBehaviour
    {
        [SerializeField] RankingElementView[] elements;
        [SerializeField] Ranking ranking;

        void Start()
        {
            ranking.rankingModified.AddListener(UpdateRanking);
            UpdateRanking(ranking);
        }

        public void UpdateRanking(Ranking ranking)
        {
            int n = Mathf.Min(elements.Length, ranking.ranking.Count);
            ranking.ranking.Sort((a, b) => b.score.points - a.score.points);

            for (var i = 0; i < n; i++)
                elements[i].UpdateValue(ranking.ranking[i], i + 1, i == ranking.scoreIndex);

            if (elements.Length > ranking.ranking.Count)
                for (int i = n; i < elements.Length; i++)
                    elements[i].Clear();
        }
    }
}
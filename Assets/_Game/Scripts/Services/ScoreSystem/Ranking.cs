using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace _Game.Scripts.Services.ScoreSystem
{
    // [CreateAssetMenu(fileName = "Ranking", menuName = "GameContent/Ranking", order = 1)]
    public class Ranking : MonoBehaviour
    {
        public int scoreIndex;
        public List<RankingElement> ranking;
        public ScoreMarkEvent rankingModified;

        void Start()
        {
            LoadData();
            rankingModified.Invoke(this);
        }

        public void Score(Score score)
        {
            var newScore = new RankingElement
            {
                score = score,
                date = DateTime.Now.ToString(CultureInfo.CurrentCulture),
            };
            
            ranking.Add(newScore);
            if (ranking.Count > 10) ranking = ranking.GetRange(0, ranking.Count);
            ranking.Sort((a, b) => b.score.points - a.score.points);

            scoreIndex = ranking.IndexOf(newScore);
            SaveData();
            rankingModified.Invoke(this);
        }

        public void SaveData()
        {
            SaveManager.SaveData("Ranking", this);
        }

        public void LoadData()
        {
            SaveManager.LoadDataTo("Ranking", this);
        }
    }

    [Serializable]
    public class ScoreMarkEvent : UnityEvent<Ranking> { }
}
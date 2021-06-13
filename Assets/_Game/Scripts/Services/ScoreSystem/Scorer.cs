using System;
using UnityEngine;
using UnityEngine.Events;

namespace _Game.Scripts.Services.ScoreSystem
{
    public class Scorer : MonoBehaviour
    {
        Score _score;
        bool _canScore;
        [SerializeField] ScoreEvent scoreMarked;
        [SerializeField] ScoreEvent stopScoring;

        public void Mark(int value)
        {
            if (!_canScore) return;
            _score.points += value;
            scoreMarked.Invoke(_score);
        }

        public void StartScoring()
        {
            _score = new Score
            {
                time = Time.time
            };
            _canScore = true;
        }

        public void StopScoring()
        {
            _canScore = false;
            _score.time = Time.time - _score.time;
            stopScoring.Invoke(_score);
        }
    }

    [Serializable]
    public class ScoreEvent : UnityEvent<Score> { }
}
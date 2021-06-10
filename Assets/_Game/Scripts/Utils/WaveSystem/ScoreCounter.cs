using System;
using System.Collections;
using _Game.Scripts.Utils.MyBox.Attributes;
using UnityEngine;
using UnityEngine.Events;

namespace _Game.Scripts.Utils
{
    public class ScoreCounter : MonoBehaviour
    {
        [SerializeField] int scoreValue;
        [SerializeField] bool discountPerTime;
        [ConditionalField("discountPerTime")] [SerializeField] int discountRate;
        public PointScoredEvent pointScored;

        int _currentScore;

        Coroutine cr;

        IEnumerator Discount()
        {
            _currentScore = scoreValue;
            if (!discountPerTime) yield break;
            while (enabled)
            {
                yield return new WaitForSeconds(1);
                _currentScore -= discountRate;
                if (_currentScore < 0) _currentScore = 0;
            }
        }

        public void StartCount()
        {
            _currentScore = scoreValue;
            if (cr != null) StopCoroutine(cr);
            cr = StartCoroutine(Discount());
        }

        public void Score()
        {
            if (cr != null) StopCoroutine(cr);
            pointScored.Invoke(_currentScore);
        }
    }

    [Serializable]
    public class PointScoredEvent : UnityEvent<int> { }
}
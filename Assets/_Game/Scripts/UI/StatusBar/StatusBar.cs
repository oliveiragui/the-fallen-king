using System.Collections;
using UnityEngine;

namespace UI.StatusBar
{
    public class StatusBar : MonoBehaviour
    {
        [SerializeField] bool smoothIncrease;
        [SerializeField] bool smoothDecrease;
        [SerializeField] float time;

        Coroutine _coroutine;

        public void Reset()
        {
            transform.localScale = Vector3.one;
        }

        Vector3 TransformScale(float newValue)
        {
            var scale = transform.localScale;
            scale.x = newValue;
            return scale;
        }

        IEnumerator SmoothVariation(float initial, float final)
        {
            var scale = transform.localScale;
            float runningTime = 0;
            while (runningTime < time)
            {
                scale.x = Mathf.SmoothStep(initial, final, runningTime);
                transform.localScale = scale;
                runningTime += Time.deltaTime;
                yield return null;
            }
        }

        public void ApplyVariation(float variation)
        {
            if (_coroutine != null) StopCoroutine(_coroutine);
            bool isPositive = variation > transform.localScale.x;
            if (isPositive && smoothIncrease || !isPositive && smoothDecrease) ApplySmoothVariation(variation);
            else ApplyAbruptVariation(variation);
        }

        void ApplyAbruptVariation(float variation)
        {
            transform.localScale = TransformScale(variation);
        }

        void ApplySmoothVariation(float variation)
        {
            if (_coroutine != null) StopCoroutine(_coroutine);
            _coroutine = StartCoroutine(SmoothVariation(transform.localScale.x, variation));
        }
    }
}
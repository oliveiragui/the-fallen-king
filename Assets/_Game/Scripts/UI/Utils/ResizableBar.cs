using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.Scripts.UI.Utils
{
    public class ResizableBar : MonoBehaviour
    {
        [SerializeField] bool smoothIncrease;
        [SerializeField] bool smoothDecrease;
        [SerializeField] float time;
        [SerializeField] Image image;

        Coroutine _coroutine;

        public float Variation
        {
            get => image.fillAmount;
            set => image.fillAmount = value;
        }

        IEnumerator SmoothVariation(float initial, float final)
        {
            float runningTime = 0;
            while (runningTime < time)
            {
                Variation = Mathf.SmoothStep(initial, final, runningTime);
                runningTime += Time.deltaTime;
                yield return null;
            }
        }

        public void ApplyVariation(float variation)
        {
            if (_coroutine != null) StopCoroutine(_coroutine);
            bool isPositive = variation > Variation;
            if (isActiveAndEnabled && (isPositive && smoothIncrease || !isPositive && smoothDecrease))
                ApplySmoothVariation(variation);
            else Variation = variation;
        }

        void ApplySmoothVariation(float variation)
        {
            if (_coroutine != null) StopCoroutine(_coroutine);
            _coroutine = StartCoroutine(SmoothVariation(Variation, variation));
        }
    }
}
using UnityEngine;

namespace ToRefactor.UI.StatusBar
{
    public class StatusBarManager : MonoBehaviour
    {
        [SerializeField] StatusBar restorationTrail;
        [SerializeField] StatusBar damageTrail;
        [SerializeField] StatusBar bar;

        [SerializeField] float total;
        float _current;

        public float Total
        {
            get => total;
            set => Total = value;
        }

        public float Current
        {
            get => _current;
            set
            {
                _current = value;
                float variation = _current / total;
                restorationTrail.ApplyVariation(variation);
                damageTrail.ApplyVariation(variation);
                bar.ApplyVariation(variation);
            }
        }

        void Awake()
        {
            _current = total;
        }

        void Reset()
        {
            restorationTrail.Reset();
            damageTrail.Reset();
            bar.Reset();
        }
    }
}
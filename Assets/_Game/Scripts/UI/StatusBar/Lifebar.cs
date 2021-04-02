using UnityEngine;

namespace _Game.Scripts.UI.StatusBar
{
    public class Lifebar : MonoBehaviour
    {
        [SerializeField] ResizableBar restorationTrail;
        [SerializeField] ResizableBar damageTrail;
        [SerializeField] ResizableBar bar;

        [SerializeField] float total;
        float _current;

        public float Total
        {
            get => total;
            set => total = value;
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
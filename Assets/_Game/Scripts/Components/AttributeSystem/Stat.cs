using UnityEngine.Events;

namespace _Game.Scripts.Components.AttributeSystem
{
    public class Stat : Attribute
    {
        public Stat(RawAttribute raw) : base(raw.Value, raw.Multiplier)
        {
            OnStatChanged = new OnStatChangedEvent();
            OnAttributeChanged.AddListener(Update);
            UpdateTotal();
            Current = Total;
        }

        public float Total { get; private set; }
        public float Current { get; private set; }
        public OnStatChangedEvent OnStatChanged { get; }

        void Update(float value, float multiplier)
        {
            UpdateTotal();
            OnStatChanged.Invoke(this);
        }

        void UpdateTotal()
        {
            Total = Value * (1 + Multiplier);
            if (Current > Total) Current = Total;
        }

        void UpdateCurrent(float value)
        {
            Current += value;
            if (Current < 0) Current = 0;
            if (Current > Total) Current = Total;
        }

        public void ApplyDamage(float value)
        {
            UpdateCurrent(value);
            OnStatChanged.Invoke(this);
        }

        public void ApplyDamageByMultiplier(float value, bool fromTotal = false)
        {
            if (fromTotal) ApplyDamage(Total * value);
            else ApplyDamage(Current * value);
        }
    }

    public class OnStatChangedEvent : UnityEvent<Stat> { }
}
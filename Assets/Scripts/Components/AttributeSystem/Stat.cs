using UnityEngine.Events;

namespace Components.AttributeSystem
{
    public class Stat : Attribute
    {
        float _total;

        public Stat(float value, float multiplier = 0, float finalValue = 0) :
            this(new RawAttribute(value, multiplier, finalValue)) { }

        public Stat(RawAttribute attribute) : base(attribute)
        {
            OnValueChanged = new OnValueChangedEvent();
            OnAttributeChanged.AddListener(attr =>
            {
                float difference = attr.Total - Total;
                Total = attr.Total;
                if (difference > 0)
                    ApplyDamage(difference);
                else
                    OnValueChanged.Invoke((Total, Current));
            });
            Total = Final.Total;
            Current = Total;
        }

        public float Total
        {
            get => _total > 0 ? _total : 0;
            set => _total = value;
        }

        public float Current { get; set; }

        public OnValueChangedEvent OnValueChanged { get; }

        public void ApplyDamage(float value, float multiplier = 0, bool fromTotal = false)
        {
            Current += value;
            if (fromTotal) Current = Total * (multiplier + 1);
            else Current *= multiplier + 1;
            OnValueChanged.Invoke((Total, Current));
        }
    }

    public class OnValueChangedEvent : UnityEvent<(float total, float current)> { }
}
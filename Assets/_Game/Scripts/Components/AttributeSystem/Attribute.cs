using System.Collections.Generic;
using UnityEngine.Events;

namespace Components.AttributeSystem
{
    public class Attribute : RawAttribute
    {
        readonly List<RawAttribute> _modifiers;

        public Attribute(float value, float multiplier) : base(value, multiplier)
        {
            _modifiers = new List<RawAttribute>();
            OnAttributeChanged = new AttributeChangedEvent();
        }

        public AttributeChangedEvent OnAttributeChanged { get; }

        public void AddModifier(Attribute modifier)
        {
            AddModifier(modifier as RawAttribute);
            modifier.OnAttributeChanged.AddListener(Update);
        }

        public bool RemoveModifier(Attribute modifier)
        {
            if (!RemoveModifier(modifier as RawAttribute)) return false;
            modifier.OnAttributeChanged.RemoveListener(Update);
            return true;
        }

        public void AddModifier(RawAttribute modifier)
        {
            _modifiers.Add(modifier);
            Update(modifier.Value, modifier.Multiplier);
        }

        public bool RemoveModifier(RawAttribute modifier)
        {
            if (!_modifiers.Remove(modifier)) return false;
            Update(-modifier.Value, -modifier.Multiplier);
            return true;
        }

        void Update(float value, float multiplier)
        {
            Value += value;
            Multiplier += multiplier;
            OnAttributeChanged.Invoke(value, multiplier);
        }
    }

    public class AttributeChangedEvent : UnityEvent<float, float> { }
}
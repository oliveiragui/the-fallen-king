using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Components.AttributeSystem
{
    [Serializable]
    public class Attribute
    {
        List<RawAttribute> modifiers;

        public Attribute(float value = 0, float multiplier = 0, float finalvalue = 0) :
            this(new RawAttribute(value, multiplier, finalvalue)) { }

        public Attribute(RawAttribute attribute)
        {
            Raw = attribute;
            Final = attribute;
            modifiers = new List<RawAttribute>();
            OnAttributeChanged = new AttributeChangedEvent();
        }

        [field: SerializeField] public RawAttribute Raw { get; private set; }
        public RawAttribute Final { get; private set; }
        public AttributeChangedEvent OnAttributeChanged { get; }

        public void AddModifier(RawAttribute modifier)
        {
            modifiers.Add(modifier);
            Calculate(modifier);
        }

        public void RemoveModifier(RawAttribute modifier)
        {
            if (modifiers.Remove(modifier)) Calculate(-modifier);
        }

        void Calculate(RawAttribute modifier)
        {
            Final += modifier;
            OnAttributeChanged.Invoke(Final);
        }
    }

    public class AttributeChangedEvent : UnityEvent<RawAttribute> { }
}
using System;
using UnityEngine;

namespace _Game.Scripts.Components.AttributeSystem
{
    [Serializable]
    public class RawAttribute
    {
        [SerializeField] float multiplier;
        [SerializeField] float value;

        public RawAttribute(float value, float multiplier)
        {
            this.value = value;
            this.multiplier = multiplier;
        }

        public float Multiplier
        {
            get => multiplier;
            protected set => multiplier = value;
        }

        public float Value
        {
            get => value;
            protected set => this.value = value;
        }

        public float CalcFactor(float attr)
        {
            return Value + Multiplier * attr;
        }
    }
}
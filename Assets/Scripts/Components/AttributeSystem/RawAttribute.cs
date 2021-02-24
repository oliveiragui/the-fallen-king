using System;
using UnityEngine;

namespace Components.AttributeSystem
{
    [Serializable]
    public struct RawAttribute
    {
        public RawAttribute(float baseValue, float multiplier, float finalValue)
        {
            BaseValue = baseValue;
            Multiplier = multiplier;
            FinalValue = finalValue;
        }

        [field: SerializeField] public float BaseValue { get; private set; }
        [field: SerializeField] public float Multiplier { get; private set; }
        [field: SerializeField] public float FinalValue { get; private set; }

        public float Total => FinalValue + BaseValue * (1 + Multiplier);

        public static RawAttribute operator +(RawAttribute left, RawAttribute right)
        {
            return new RawAttribute(left.BaseValue + right.BaseValue, left.Multiplier + right.Multiplier,
                right.FinalValue + left.FinalValue);
        }

        public static RawAttribute operator -(RawAttribute left, RawAttribute right)
        {
            return new RawAttribute(left.BaseValue + right.BaseValue, left.Multiplier + right.Multiplier,
                right.FinalValue + left.FinalValue);
        }

        public static RawAttribute operator -(RawAttribute self)
        {
            return new RawAttribute(-self.BaseValue, -self.Multiplier, -self.FinalValue);
        }
    }
}
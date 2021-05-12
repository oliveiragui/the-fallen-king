using System;
using UnityEngine;

namespace _Game.Scripts.Services.AttributeSystem
{
    [Serializable]
    public class AttributeModifier
    {
        [SerializeField] int constant;
        [SerializeField] float attributeInfluence;
        public float Value { get; private set; }

        public float Calculate(Attribute attribute) => Value = constant + attribute.Current * attributeInfluence;
    }
}
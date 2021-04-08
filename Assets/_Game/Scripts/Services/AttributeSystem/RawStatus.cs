using System;
using UnityEngine;

namespace _Game.Scripts.Components.AttributeSystem
{
    [Serializable]
    public class RawStatus
    {
        [SerializeField] RawAttribute life;
        [SerializeField] RawAttribute strength;
        [SerializeField] RawAttribute attackSpeed;
        [SerializeField] RawAttribute speed;

        public RawAttribute Life => life;
        public RawAttribute Strength => strength;
        public RawAttribute AttackSpeed => attackSpeed;
        public RawAttribute Speed => speed;
    }
}
using System;
using UnityEngine;

namespace Abilities
{
    [Serializable]
    public class AbilityAttributes
    {
        [SerializeField] float cooldown;
        [SerializeField] float range;
        public float Range => range;
        public float Cooldown => cooldown;
    }
}
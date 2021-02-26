using System;
using UnityEngine;

namespace Collection.Abilities.Collections.Habilidades
{
    [Serializable]
    public class AbilityAttributes
    {
        [SerializeField] float cooldown;
        [SerializeField] float range;
        [SerializeField] int maxCombo;

        public int MAXCombo => maxCombo;
        public float Range => range;
        public float Cooldown => cooldown;
    }
}
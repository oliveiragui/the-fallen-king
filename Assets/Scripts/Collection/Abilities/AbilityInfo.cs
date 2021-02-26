using System;
using UnityEngine;

namespace Collection.Abilities.Collections.Habilidades
{
    [Serializable]
    public class AbilityInfo
    {
        public string Name => name;
        public int Id => id;
        public string Description => description;
        public string SpriteText => spriteText;

        [SerializeField] string name;
        [SerializeField] int id;
        [SerializeField] string description;
        [SerializeField] string spriteText;
        [SerializeField] bool canBeInterruped;
        [SerializeField] bool canInterrupt;

        public bool CanInterrupt(AbilityModel other) => canInterrupt && other.Info.canBeInterruped;
    }
}
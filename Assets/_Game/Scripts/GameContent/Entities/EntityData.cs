using System;
using System.Collections.Generic;
using _Game.Scripts.Abilities;
using _Game.Scripts.Characters;
using _Game.Scripts.Entities.Components.Animation;
using _Game.Scripts.Weapons;
using UnityEngine;

namespace _Game.Scripts.Entities
{
    [Serializable]
    public class EntityData
    {
        [SerializeField] public Character associatedCharacter;
        public float speed;
        public float direction;
        public float lookDiretion;
        public float stoppingDistance;
        
        
        public bool conjuring;
        public bool inCombat;
        //public List<Ability> abilities;
        public Weapon weapon;
        public Ability ability;
        public bool UsingAbility { get; set; }
        public bool UsingCombo { get; set; }
    }
}
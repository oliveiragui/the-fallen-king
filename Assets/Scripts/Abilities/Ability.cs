using Abilities.Collections.Habilidades;
using Components.AttributeSystem;
using UnityEngine;

namespace Abilities
{
    public class Ability 
    {
        AbilityData _data;

        public int Id => _data.id;
        public string Description => _data.description;
        public RawAttribute Cooldown => _data.cooldown;
        public AbilityCombo[] Combo => _data.combo;
        public int MaxCombo => _data.combo.Length;
        public bool InUse { get; set; }

        public Ability(AbilityData data)
        {
            _data = data;
        }
        
    }
}
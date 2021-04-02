using System.Linq;
using _Game.Scripts.Abilities.Data;
using _Game.Scripts.Components.AttributeSystem;
using UnityEngine;

namespace _Game.Scripts.Abilities
{
    public class Ability : MonoBehaviour
    {
        public AbilityCombo[] Combo;
        public AbilityCombo ComboInUse;
        [SerializeField] AbilityData data;
        
        public Ability Setup(AbilityData data)
        {
            this.data = data;
            Combo = data.combo.Select(comboData => new AbilityCombo(comboData, this)).ToArray();
            return this;
        }

        public int AnimationId => data.animationId;
        public string Description => data.description;
        public RawAttribute Cooldown => data.cooldown;
        public int MaxCombo => data.combo.Length;
        public bool InUse { get; set; }

        public bool canBeInterruped => data.canBeInterruped;
        public bool canInterrupt => data.canInterrupt;

        public bool CanInterrupt(Ability other)
        {
            return canInterrupt && other.canBeInterruped;
        }
    }
}
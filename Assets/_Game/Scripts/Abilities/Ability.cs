using System.Linq;
using Components.AttributeSystem;
using UnityEngine;

namespace Abilities
{
    public class Ability : MonoBehaviour
    {
        public AbilityCombo[] Combo;
        [SerializeField] AbilityData data;
        
        public Ability Setup(AbilityData data)
        {
            this.data = data;
            Combo = data.combo.Select(comboData => new AbilityCombo(comboData, this)).ToArray();
            return this;
        }

        public int Id => data.id;
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
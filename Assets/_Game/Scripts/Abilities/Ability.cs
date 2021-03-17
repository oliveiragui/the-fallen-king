using System.Linq;
using Components.AttributeSystem;

namespace Abilities
{
    public class Ability
    {
        public readonly AbilityCombo[] Combo;
        readonly AbilityData _data;

        public Ability(AbilityData data)
        {
            _data = data;
            Combo = _data.combo.Select(comboData => new AbilityCombo(comboData)).ToArray();
        }

        public int Id => _data.id;
        public string Description => _data.description;
        public RawAttribute Cooldown => _data.cooldown;
        public int MaxCombo => _data.combo.Length;
        public bool InUse { get; set; }

        public bool canBeInterruped => _data.canBeInterruped;
        public bool canInterrupt => _data.canInterrupt;

        public bool CanInterrupt(Ability other)
        {
            return canInterrupt && other.canBeInterruped;
        }
    }
}
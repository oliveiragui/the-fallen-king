using _Game.Scripts.Abilities.Data;
using _Game.Scripts.Entities.Components.Action;
using UnityEngine;

namespace _Game.Scripts.Abilities
{
    public class AbilityCombo
    {
        readonly AbilityComboData _data;
        public readonly EntityAction Action;

        public AbilityCombo(AbilityComboData data, Ability ability)
        {
            _data = data;
            if (data.action)
                Action = Object.Instantiate(data.action.gameObject, ability.transform).GetComponent<EntityAction>();
        }

        public RawComboAttributes Attributes => _data.attributes;
        public float Factor3 => _data.factor3;
        public float Factor2 => _data.factor2;
        public float Factor1 => _data.factor1;
        public bool Castable => _data.castable;
    }
}
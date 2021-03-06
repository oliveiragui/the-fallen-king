using Components.AttributeSystem;
using UnityEngine;

namespace Abilities
{
    namespace Collections.Habilidades
    {
        [CreateAssetMenu(fileName = "Habilidade", menuName = "GameContent/Habilidade", order = 2)]
        public class AbilityData : ScriptableObject
        {
            [SerializeField] public int id;
            [SerializeField] public string description;
            [SerializeField] public bool canBeInterruped;
            [SerializeField] public bool canInterrupt;
            [SerializeField] public RawAttribute cooldown;
            [SerializeField] public AbilityCombo[] combo;

            public int Id => id;
            public string Description => description;
            public RawAttribute Cooldown => cooldown;
            public AbilityCombo[] Combo => combo;
            public int MaxCombo => combo.Length;

            public bool CanInterrupt(AbilityData other)
            {
                return canInterrupt && other.canBeInterruped;
            }
        }
    }
}
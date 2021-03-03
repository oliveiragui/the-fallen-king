using UnityEngine;

namespace Abilities
{
    namespace Collections.Habilidades
    {
        [CreateAssetMenu(fileName = "Habilidade", menuName = "GameContent/Habilidade", order = 2)]
        public class AbilityData : ScriptableObject
        {
            [SerializeField] int id;
            [SerializeField] string description;
            [SerializeField] bool canBeInterruped;
            [SerializeField] bool canInterrupt;
            [SerializeField] AbilityAttributes baseAttributes;
            [SerializeField] AbilityCombo[] combo;

            public int Id => id;
            public string Description => description;
            public AbilityAttributes BaseAttributes => baseAttributes;
            public AbilityCombo[] Combo => combo;
            public int MaxCombo => combo.Length;

            public bool CanInterrupt(AbilityData other)
            {
                return canInterrupt && other.canBeInterruped;
            }
        }
    }
}
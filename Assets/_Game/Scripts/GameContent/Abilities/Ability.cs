using System.Linq;
using _Game.Scripts.Components.AttributeSystem;
using _Game.Scripts.GameContent.Abilities.Data;
using UnityEngine;
using UnityEngine.Events;

namespace _Game.Scripts.GameContent.Abilities
{
    public class Ability : MonoBehaviour
    {
        public AbilityCombo[] Combos;
        public AbilityCombo Combo;
        [SerializeField] AbilityData data;
        
        [SerializeField] UnityEvent onAbilityUse;
      
        [SerializeField] UnityEvent onCooldownEnter;

        public Ability Setup(AbilityData data)
        {
            this.data = data;
            Combos = data.Combo.Select(comboData => new AbilityCombo(comboData, this)).ToArray();
            Combo = Combos[0];
            return this;
        }

        public AbilityData Data => data;
    }
}
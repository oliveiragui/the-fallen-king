using _Game.Scripts.Components.AttributeSystem;
using UnityEngine;

namespace _Game.Scripts.GameContent.Abilities.Data
{
    /*
     * TODO: Separate ability responsibility from combo responsibilit
     */

    [CreateAssetMenu(fileName = "Habilidade", menuName = "GameContent/Habilidade", order = 2)]
    public class AbilityData : ScriptableObject
    {
        [SerializeField] int animationId;
        [SerializeField] string description;
        [SerializeField] Sprite icon;
        [SerializeField] bool canBeInterruped;
        [SerializeField] bool canInterrupt;
        [SerializeField] RawAttribute cooldown;
        [SerializeField] AbilityComboData[] combo;

        public string Name => name;
        public int AnimationId => animationId;
        public string Description => description;
        public RawAttribute Cooldown => cooldown;
        public AbilityComboData[] Combo => combo;
        public int MaxCombo => combo.Length;
        public Sprite Icon => icon;
        
        public bool CanInterrupt(AbilityData other)
        {
            return canInterrupt && other.canBeInterruped;
        }
    }
}
using _Game.Scripts.Components.AttributeSystem;
using UnityEngine;

namespace _Game.Scripts.Abilities.Data
{
    /*
     * TODO: Create a instance for each ability
     * TOdo: Create a instance for each Weapon
     * TODO: Create a instance for each combo
     * TODO: Separate ability responsibility from combo responsibilit
     y
     */

    [CreateAssetMenu(fileName = "Habilidade", menuName = "GameContent/Habilidade", order = 2)]
    public class AbilityData : ScriptableObject
    {
        [SerializeField] public int animationId;
        [SerializeField] public string description;
        [SerializeField] public bool canBeInterruped;
        [SerializeField] public bool canInterrupt;
        [SerializeField] public RawAttribute cooldown;
        [SerializeField] public AbilityComboData[] combo;

        public int AnimationId => animationId;
        public string Description => description;
        public RawAttribute Cooldown => cooldown;
        public AbilityComboData[] Combo => combo;
        public int MaxCombo => combo.Length;
    }
}
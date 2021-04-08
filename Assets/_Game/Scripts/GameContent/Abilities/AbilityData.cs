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
        [SerializeField] public int animationId;
        [SerializeField] public string description;
        [SerializeField] public Sprite icon;
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
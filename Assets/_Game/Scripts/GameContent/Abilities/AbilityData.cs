using _Game.Scripts.Services.AttributeSystem;
using UnityEngine;

namespace _Game.Scripts.GameContent.Abilities
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
        [SerializeField] ParticleSystem[] particleEffects;

        public string Name => name;
        public int AnimationId => animationId;
        public string Description => description;
        public RawAttribute Cooldown => cooldown;
        public AbilityComboData[] Combo => combo;
        public int MaxCombo => combo.Length;
        public Sprite Icon => icon;
        public ParticleSystem[] ParticleEffects => particleEffects;
        public bool HaveParticles => particleEffects != null;
        public bool CanInterrupt => canInterrupt;
        public bool CanBeInterruped => canBeInterruped;

        public bool CanOverride(AbilityData other) => canInterrupt && other.canBeInterruped;
    }
}
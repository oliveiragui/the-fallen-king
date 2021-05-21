using System;
using _Game.Scripts.Services.AttributeSystem;
using UnityEngine;

namespace _Game.GameModules.Abilities.Scripts
{
    [CreateAssetMenu(fileName = "Ability", menuName = "GameContent/Ability", order = 2)]
    public class AbilityData : ScriptableObject
    {
        [SerializeField] bool canBeInterruped;
        [SerializeField] bool canInterrupt;
        [SerializeField] AbilityMetaData metaData;
        [SerializeField] Combo[] combo;
        [SerializeField] ParticleSystem[] particleEffects;
        [SerializeField] AudioSource[] sfx;
        [SerializeField] int range;
        [SerializeField] AttributeModifier cooldown;
        public AttributeModifier Cooldown => cooldown;
        public int Range => range;

        public string Name => name;
        public AbilityMetaData MetaData => metaData;
        public Combo[] Combo => combo;
        public ParticleSystem[] ParticleEffects => particleEffects;
        public bool CanInterrupt => canInterrupt;
        public bool CanBeInterruped => canBeInterruped;

        public AudioSource[] Sfx => sfx;

        public bool CanOverride(AbilityData other) => this != other && CanInterrupt && other.CanBeInterruped;
    }

    [Serializable]
    public class AbilityMetaData
    {
        [SerializeField] string description;
        [SerializeField] Sprite icon;
        public Sprite Icon => icon;
        public string Description => description;
    }
}
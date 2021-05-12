using System;
using _Game.GameModules.Abilities.Scripts;
using _Game.GameModules.Ammunition.Scripts;
using _Game.GameModules.Characters.Scripts;
using _Game.GameModules.Weapons.Scripts.Prefab;
using UnityEngine;

namespace _Game.GameModules.Weapons.Scripts
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "GameContent/Weapon", order = 1)]
    public class WeaponData : ScriptableObject
    {
        [SerializeField] string description;
        [SerializeField] Sprite icon;
        [SerializeField] AmmoData ammoData;
        [SerializeField] WeaponDefaultAnimation animations;
        [SerializeField] AbilityData[] abilities = new AbilityData[4];

        [SerializeField] RawCharacterStatus characterStatus;

        //[SerializeField] AnimatorOverrideController animatorController;
        [SerializeField] WeaponPrefabList prefabs;

        public WeaponDefaultAnimation Animations => animations;
        public string Description => description;
        public Sprite Icon => icon;
        public AmmoData AmmoData => ammoData;
        public AbilityData[] Abilities => abilities;

        public RawCharacterStatus CharacterStatus => characterStatus;

        // public AnimatorOverrideController AnimatorController => animatorController;
        public WeaponPrefabList Prefabs => prefabs;
    }

    [Serializable]
    public class WeaponDefaultAnimation
    {
        [SerializeField] AnimationClip idle;
        [SerializeField] AnimationClip walk;
        [SerializeField] AnimationClip run;
        public AnimationClip Idle => idle;
        public AnimationClip Walk => walk;
        public AnimationClip Run => run;
    }
}
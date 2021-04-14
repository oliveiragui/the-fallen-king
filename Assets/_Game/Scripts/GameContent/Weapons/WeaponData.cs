using _Game.Scripts.Components.AttributeSystem;
using _Game.Scripts.GameContent.Abilities.Data;
using _Game.Scripts.GameContent.Ammunition;
using _Game.Scripts.GameContent.Weapons.Prefab;
using UnityEngine;

namespace _Game.Scripts.GameContent.Weapons
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "GameContent/Weapon", order = 1)]
    public class WeaponData : ScriptableObject
    {
        public string Description => description;
        public Sprite Icon => icon;
        public AmmoData AmmoData => ammoData;
        public AbilityData[] Abilities => abilities;
        public RawStatus Status => status;
        public AnimatorOverrideController AnimatorController => animatorController;
        public WeaponPrefabList Prefabs => prefabs;

        [SerializeField] string description;
        [SerializeField] Sprite icon;
        [SerializeField] AmmoData ammoData;
        [SerializeField] AbilityData[] abilities = new AbilityData[4];
        [SerializeField] RawStatus status;
        [SerializeField] AnimatorOverrideController animatorController;
        [SerializeField] WeaponPrefabList prefabs;
        
    }
}
using _Game.Scripts.Abilities.Data;
using _Game.Scripts.Ammo;
using _Game.Scripts.Components.AttributeSystem;
using _Game.Scripts.Weapons.Prefab;
using UnityEngine;

namespace _Game.Scripts.Weapons
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "GameContent/Weapon", order = 1)]
    public class WeaponData : ScriptableObject
    {
        [SerializeField] public string description;
        [SerializeField] public AmmoData ammoData;
        [SerializeField] public AbilityData[] abilities = new AbilityData[4];
        [SerializeField] public RawStatus status;
        [SerializeField] public AnimatorOverrideController animatorController;
        [SerializeField] public WeaponPrefabList prefabs;
        public string Description => description;
        public RawStatus Status => status;
        public AnimatorOverrideController AnimatorController => animatorController;
        public WeaponPrefabList Prefabs => prefabs;
        public AbilityData[] Abilities => abilities;
    }
}
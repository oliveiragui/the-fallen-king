using Abilities;
using Components.AttributeSystem;
using UnityEngine;
using Weapons.Prefab;

namespace Weapons
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "GameContent/Weapon", order = 1)]
    public class WeaponData : ScriptableObject
    {
        [SerializeField] public string description;
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
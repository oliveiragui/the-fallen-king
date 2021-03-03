using Abilities.Collections.Habilidades;
using Components.AttributeSystem;
using UnityEngine;
using Weapons.Prefab;

namespace Weapons
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "GameContent/Weapon", order = 1)]
    public class WeaponData : ScriptableObject
    {
        [SerializeField] string description;
        [SerializeField] AbilityData[] abilities = new AbilityData[4];
        [SerializeField] RawStatus status;
        [SerializeField] AnimatorOverrideController animatorController;
        [SerializeField] WeaponPrefabList prefabs;
        public string Description => description;
        public RawStatus Status => status;
        public AnimatorOverrideController AnimatorController => animatorController;
        public WeaponPrefabList Prefabs => prefabs;
        public AbilityData[] Abilities => abilities;
    }
}
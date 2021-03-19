using System;
using System.Linq;
using Abilities;
using Components.AttributeSystem;
using UnityEngine;
using Weapons.Prefab;

namespace Weapons
{
    [Serializable]
    public class Weapon : MonoBehaviour
    {
        [SerializeField] WeaponData data;
        public Ability[] Abilities;

        public Weapon Setup(WeaponData data)
        {
            this.data = data;
            Abilities = data.abilities.Select(ability => gameObject.AddComponent<Ability>().Setup(ability)).ToArray();
            return this;
        }

        public string Description => data.description;
        public RawStatus Status => data.status;
        public AnimatorOverrideController AnimatorController => data.animatorController;
        public WeaponPrefabList Prefabs => data.prefabs;
    }
}
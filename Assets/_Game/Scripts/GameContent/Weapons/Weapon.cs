using System;
using System.Collections.Generic;
using System.Linq;
using _Game.Scripts.Abilities;
using _Game.Scripts.Ammo;
using _Game.Scripts.Components.AttributeSystem;
using _Game.Scripts.Weapons.Prefab;
using UnityEngine;

namespace _Game.Scripts.Weapons
{
    [Serializable]
    public class Weapon : MonoBehaviour
    {
        [SerializeField] WeaponData data;
        public List<Ability> Abilities;
        public AmmoData ammoData;

        public Weapon Setup(WeaponData data)
        {
            this.data = data;
            var abilitiesObject = new GameObject();
            abilitiesObject.name = "Abilities";
            abilitiesObject.transform.parent = transform;

            Abilities = new List<Ability>();
            
            foreach (var ability in data.abilities)
            {
                Abilities.Add(abilitiesObject.AddComponent<Ability>().Setup(ability));
            }

            ammoData = data.ammoData;
            return this;
        }

        public string Description => data.description;
        public RawStatus Status => data.status;
        public AnimatorOverrideController AnimatorController => data.animatorController;
        public WeaponPrefabList Prefabs => data.prefabs;
    }
}
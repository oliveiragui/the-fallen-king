using System;
using System.Collections.Generic;
using _Game.Scripts.GameContent.Abilities;
using _Game.Scripts.GameContent.Ammunition;
using _Game.Scripts.GameContent.Weapons.Prefab;
using UnityEngine;

namespace _Game.Scripts.GameContent.Weapons
{
    [Serializable]
    public class Weapon : MonoBehaviour
    {
        [SerializeField] WeaponData data;
        public List<Ability> Abilities;
        public AmmoData ammoData;
        public WeaponData Data => data;

        public Weapon Setup(WeaponData data)
        {
            this.data = data;
            var abilitiesObject = new GameObject();
            abilitiesObject.name = "Abilities";
            abilitiesObject.transform.parent = transform;

            Abilities = new List<Ability>();

            foreach (var ability in data.Abilities)
                Abilities.Add(abilitiesObject.AddComponent<Ability>().Setup(ability));

            ammoData = data.AmmoData;
            return this;
        }
        

    }
}
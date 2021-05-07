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

        public AnimatorOverrideController animatorController;

        public Weapon Setup(WeaponData data)
        {
            this.data = data;
            var abilitiesObject = new GameObject();
            abilitiesObject.name = "Abilities";
            abilitiesObject.transform.parent = transform;

            Abilities = new List<Ability>();

            animatorController = data.AnimatorController;

            for (int i = 0; i < data.Abilities.Length; i++)
            {
                var ability = data.Abilities[i];
                Debug.Log(ability);
                Abilities.Add(abilitiesObject.AddComponent<Ability>().Setup(ability));

                for (int j = 0; j < ability.Combo.Length; j++)
                {
                    var combo = ability.Combo[j];
                    animatorController[$"Habilidade {i+1} Combo {j+1} - 1"] = combo.beginningAnimation;
                    if (!combo.castable) continue;
                    animatorController[$"Habilidade {i+1} Combo {j+1} - 2"] = combo.middleAnimation;
                    animatorController[$"Habilidade {i+1} Combo {j+1} - 3"] = combo.endAnimation;
                }
            }

            ammoData = data.AmmoData;

            
            
            
            return this;
        }
        

    }
}
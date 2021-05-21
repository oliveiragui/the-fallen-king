﻿using System;
using System.Collections.Generic;
using _Game.GameModules.Abilities.Scripts;
using _Game.GameModules.Ammunition.Scripts;
using _Game.GameModules.Characters.Scripts;
using UnityEngine;

namespace _Game.GameModules.Weapons.Scripts
{
    [Serializable]
    public class Weapon : MonoBehaviour
    {
        [SerializeField] WeaponData data;
        public List<Ability> Abilities;
        public AmmoData ammoData;

        public AnimatorOverrideController animatorController;
        public WeaponData Data => data;

        public Weapon Setup(WeaponData data, RuntimeAnimatorController entityAnimator, CharacterStatus status)
        {
            this.data = data;
            var abilitiesObject = new GameObject();
            abilitiesObject.name = "Abilities";
            abilitiesObject.transform.parent = transform;

            SetupAnimator(entityAnimator);
            InstantiateAbilities(data.Abilities, abilitiesObject, status);
            ammoData = data.AmmoData;
            return this;
        }

        void SetupAnimator(RuntimeAnimatorController animator)
        {
            Abilities = new List<Ability>();
            animatorController = new AnimatorOverrideController(animator);
            animatorController["Equipado - Anda"] = data.Animations.Walk;
            animatorController["Equipado - Corre"] = data.Animations.Run;
            animatorController["Equipado - Idle"] = data.Animations.Idle;
        }

        void InstantiateAbilities(AbilityData[] abilities, GameObject root, CharacterStatus status)
        {
            for (var i = 0; i < abilities.Length; i++)
            {
                var ability = abilities[i];
                Abilities.Add(root.AddComponent<Ability>().Setup(ability, status));

                var abilityIndex = (i + 1).ToString();
                for (var j = 0; j < ability.Combo.Length; j++)
                {
                    var comboIndex = (j + 1).ToString();
                    var combo = ability.Combo[j];
                    animatorController[$"Habilidade {abilityIndex} Combo {comboIndex} - 1"] = combo.BeginningAnimation.Clip;
                    if (!combo.Castable) continue;
                    animatorController[$"Habilidade {abilityIndex} Combo {comboIndex} - 2"] = combo.MiddleAnimation.Clip;
                    animatorController[$"Habilidade {abilityIndex} Combo {comboIndex} - 3"] = combo.EndingAnimation.Clip;
                }
            }
        }
    }
}
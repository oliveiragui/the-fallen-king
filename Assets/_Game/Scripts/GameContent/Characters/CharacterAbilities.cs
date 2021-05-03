﻿using System.Collections.Generic;
using _Game.Scripts.GameContent.Abilities;
using _Game.Scripts.GameContent.Weapons;
using UnityEngine;

namespace _Game.Scripts.GameContent.Characters
{
    public class CharacterAbilities : MonoBehaviour
    {
        public List<Ability> Abilities { get; private set; }
        public Ability AbilityInUse { get; private set; }

        public void StartAbility(int index)
        {
            var nextAbility = Abilities[index];
            if (AbilityInUse && AbilityInUse != nextAbility) AbilityInUse.Finish();
            AbilityInUse = nextAbility;
            nextAbility.Use();
        }

        public bool CanUseAbility(int i)
        {
            return Abilities[i].CanBeUsed;
        }

        //public bool CanStopCasting(int i) => !AbilityInUse || Abilities[i].Equals(AbilityInUse);

        public bool StopCasting(int id)
        {
            if (AbilityInUse && !Abilities[id].Equals(AbilityInUse)) return false;
            else Abilities[id].StopConjuring();
            return true;
        }

        public void StopAbility()
        {
            if (AbilityInUse) AbilityInUse.Finish();
            AbilityInUse = null;
        }

        public void OnWeaponChange(Weapon weapon)
        {
            Abilities = weapon.Abilities;
        }
    }
}
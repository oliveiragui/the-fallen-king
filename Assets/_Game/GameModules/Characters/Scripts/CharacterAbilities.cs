using System.Collections.Generic;
using _Game.GameModules.Abilities.Scripts;
using _Game.GameModules.Weapons.Scripts;
using UnityEngine;

namespace _Game.GameModules.Characters.Scripts
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

        public bool CanUseAbility(int i) => Abilities[i].CanBeUsed;

        //public bool CanStopCasting(int i) => !AbilityInUse || Abilities[i].Equals(AbilityInUse);

        public void StopCasting(int id)
        {
            Abilities[id].StopConjuring();
        }

        public void StopAbility()
        {
            if (AbilityInUse) AbilityInUse.Finish();
            AbilityInUse = null;
        }

        public void OnWeaponChange(Weapon weapon)
        {
            if (weapon) Abilities = weapon.Abilities;
        }
    }
}
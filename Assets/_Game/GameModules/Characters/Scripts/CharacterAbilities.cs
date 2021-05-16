using System.Collections.Generic;
using _Game.GameModules.Abilities.Scripts;
using _Game.GameModules.Weapons.Scripts;
using UnityEngine;
using UnityEngine.Events;

namespace _Game.GameModules.Characters.Scripts
{
    public class CharacterAbilities : MonoBehaviour
    {
        [SerializeField] List<Ability> abilities;

        public List<Ability> Abilities
        {
            get => abilities;
            private set => abilities = value;
        }

        public Ability AbilityInUse { get; private set; }
        public RequestedAbilityEvent requestedAbility = new RequestedAbilityEvent();

        public void RequestAbility(int index)
        {
            var reqAbility = Abilities[index];

            if (!reqAbility.CanBeUsed) return;
            if (!CanUseAbility(index)) return;
            requestedAbility.Invoke(index + 1, reqAbility.CanOverride(AbilityInUse));
        }

        public void StartAbility(int index)
        {
            var nextAbility = Abilities[index];
            if (AbilityInUse) AbilityInUse.Finish();
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

    public class RequestedAbilityEvent : UnityEvent<int, bool> { }
}
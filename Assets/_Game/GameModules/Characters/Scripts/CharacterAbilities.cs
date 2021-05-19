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

        public bool usingAbility;
        public RequestedAbilityEvent requestedAbility = new RequestedAbilityEvent();
        public IndexedAbilityEvent startedAbility = new IndexedAbilityEvent();
        public IndexedAbilityEvent stopAbility = new IndexedAbilityEvent();
        public IndexedAbilityEvent stopCasting = new IndexedAbilityEvent();

        public List<Ability> Abilities
        {
            get => abilities;
            private set => abilities = value;
        }

        public Ability AbilityInUse { get; private set; }

        public void RequestAbility(int index)
        {
            var reqAbility = Abilities[index];

            if (!reqAbility.CanBeUsed) return;
            if (!CanUseAbility(index)) return;
            requestedAbility.Invoke(index + 1, reqAbility.CanOverride(AbilityInUse));
        }

        public void StartAbility(int index)
        {
            usingAbility = true;
            var nextAbility = Abilities[index];
            if (AbilityInUse) AbilityInUse.Finish();
            AbilityInUse = nextAbility;
            nextAbility.Use();
            startedAbility.Invoke(index);
        }

        public bool CanUseAbility(int i) => Abilities[i].CanBeUsed;

        public void StopCasting(int id)
        {
            Abilities[id].StopConjuring();
            stopCasting.Invoke(id);
        }

        public void StopAbility()
        {
            usingAbility = false;
            if (AbilityInUse) AbilityInUse.Finish();
            int value = Abilities.IndexOf(AbilityInUse);
            AbilityInUse = null;
            stopAbility.Invoke(value);
        }

        public void OnWeaponChange(Weapon weapon)
        {
            if (weapon) Abilities = weapon.Abilities;
        }
    }

    public class IndexedAbilityEvent : UnityEvent<int> { }

    public class RequestedAbilityEvent : UnityEvent<int, bool> { }
}
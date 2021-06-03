using System;
using _Game.GameModules.Abilities.Scripts;
using _Game.GameModules.Entities.Scripts;
using _Game.GameModules.Teams.Scripts;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace _Game.GameModules.Characters.Scripts
{
    public class Character : MonoBehaviour
    {
        [SerializeField] CharacterData data;
        [SerializeField] Entity entity;
        [SerializeField] CharacterStatus characterStatus;
        [SerializeField] CharacterPivotController pivotController;

        [FormerlySerializedAs("weapons")] [SerializeField]
        CharacterWeapons weaponStorage;

        [SerializeField] CharacterAbilities abilitySystem;

        public CharacterEvents events;
        [SerializeField] bool immortal;

        [SerializeField] bool _combatMode;

        bool dead;
        
        public Team Team { get; private set; }
        public CharacterData Data => data;
        public CharacterStatus CharacterStatus => characterStatus;
        public Entity Entity => entity;
        public CharacterWeapons WeaponStorage => weaponStorage;
        public CharacterAbilities AbilitySystem => abilitySystem;
        public ImpactResistance Resiliency => data.Resiliency;

        public bool CombatMode
        {
            set
            {
                if (value && !_combatMode) events.enterInCombat.Invoke();
                else if (!value && _combatMode) events.exitCombat.Invoke();
                _combatMode = value;
                entity.CombatMode = true;
            }
        }

        public void UsePivot(bool value)
        {
            pivotController.UsePivot(value);
        }

        #region Callbacks

        void OnEnterInCombat()
        {
            CombatMode = true;
        }

        void OnExitCombat()
        {
            CombatMode = false;
        }

        public void Cure()
        {
            characterStatus.Life.Current += 99999;
        }

        void OnStatusChanged(CharacterStatus characterStatus)
        {
            if (immortal) return;
            if (dead || !(characterStatus.Life.Current <= 0)) return;
            dead = true;
            events.death.Invoke(this);
        }

        void OnHit(AbilityHit abilityHit)
        {
            CharacterStatus.Life.Current += abilityHit.power;
            if (ImpactMatrix.Calc(abilityHit.impact, Data.Resiliency) > 1) AbilitySystem.StopAbility();
            OnEnterInCombat();
        }

        void OnStartAbility(int abilityIndex)
        {
            var combo = AbilitySystem.Abilities[abilityIndex].CurrentCombo;
            entity.SetCombo(AbilitySystem.Abilities[abilityIndex].CurrentComboID, combo);
            OnEnterInCombat();
        }

        void OnFinishAbility()
        {
            AbilitySystem.StopAbility();
        }

        void OnDeath(Character character)
        {
            OnExitCombat();
            entity.Alive = false;
        }

        #endregion

        #region Unity Functions

        void Awake()
        {
            Team = data.DefaultTeam;
        }

        void Start()
        {
            events.death.AddListener(OnDeath);

            WeaponStorage.onWeaponChange.AddListener(AbilitySystem.OnWeaponChange);
            WeaponStorage.onWeaponChange.AddListener(entity.OnWeaponChange);

            CharacterStatus.StatusChanged.AddListener(OnStatusChanged);
            CharacterStatus.StatusChanged.AddListener(entity.OnStatusChange);

            entity.startAbilityAnimation.AddListener(AbilitySystem.StartAbility);
            entity.endAbilityAnimation.AddListener(AbilitySystem.StopAbility);
            entity.hitReceived.AddListener(OnHit);

            AbilitySystem.requestedAbility.AddListener(entity.SetNextAbility);
            AbilitySystem.startedAbility.AddListener(OnStartAbility);
            AbilitySystem.stopCasting.AddListener(entity.StopCasting);

            AbilitySystem.OnWeaponChange(weaponStorage.WeaponInUse);
            entity.OnStatusChange(CharacterStatus);
            entity.OnWeaponChange(weaponStorage.WeaponInUse);

            events.onInstantiate.Invoke();
        }

        void OnEnable() => entity.enabled = true;

        void OnDisable() => Entity.enabled = false;

        #endregion
    }

    [Serializable]
    public class CharacterEvents
    {
        public UnityEvent onInstantiate;
        public UnityEvent onDestroy;
        public UnityEvent enterInCombat;
        public UnityEvent exitCombat;
        public UnityCharacterEvent death;
    }

    [Serializable]
    public class AbilityEvent : UnityEvent<Ability> { }

    [Serializable]
    public class UnityCharacterEvent : UnityEvent<Character> { }

    [Serializable]
    public class UnityEntityEvent : UnityEvent<Entity> { }
}
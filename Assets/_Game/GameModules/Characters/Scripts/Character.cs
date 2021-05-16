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

        [FormerlySerializedAs("weapons")] [SerializeField]
        CharacterWeapons weaponStorage;

        [SerializeField] CharacterAbilities abilitySystem;

        public CharacterEvents events;
        [SerializeField] bool immortal;

        bool dead;

        public Team Team { get; private set; }
        public CharacterStatus CharacterStatus => characterStatus;
        public Entity Entity => entity;
        public CharacterWeapons WeaponStorage => weaponStorage;
        public CharacterAbilities AbilitySystem => abilitySystem;
        public ImpactResistance Resiliency => data.Resiliency;

        [SerializeField] bool _combatMode;

        public bool CombatMode
        {
            get => _combatMode;
            set
            {
                if (value)
                {
                    if (!_combatMode) events.enterInCombat.Invoke();
                }
                else
                {
                    if (_combatMode) events.exitCombat.Invoke();
                }

                _combatMode = value;
                entity.CombatMode = value;
            }
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

        void OnStatusChanged(CharacterStatus characterStatus)
        {
            if (immortal) return;
            if (dead || !(characterStatus.Life.Current <= 0)) return;
            dead = true;
            events.death.Invoke(this);
        }

        void OnHit(AbilityHit abilityHit)
        {
            CharacterStatus.Life.Current += (int) abilityHit.power;
            AbilitySystem.StopAbility();
            OnEnterInCombat();
        }

        void OnStartAbility(int abilityIndex)
        {
            AbilitySystem.StartAbility(abilityIndex);
            entity.SetupAbility(AbilitySystem.AbilityInUse);
            OnEnterInCombat();
        }

        void OnFinishAbility()
        {
            AbilitySystem.StopAbility();
        }

        void OnDeath(Character character)
        {
            OnExitCombat();
            entity.Kill();
        }

        void OnEntityEnabled(Entity newEntity)
        {
            WeaponStorage.onWeaponChange.AddListener(entity.OnWeaponChange);
            entity.events.startAbility.AddListener(OnStartAbility);
            entity.events.finishAbility.AddListener(OnFinishAbility);
            entity.events.onHitReceived.AddListener(OnHit);
            entity.OnWeaponChange(weaponStorage.WeaponInUse);
        }

        void OnEntityDisabled(Entity newEntity)
        {
            WeaponStorage.onWeaponChange.RemoveListener(entity.OnWeaponChange);
            entity.events.startAbility.RemoveListener(OnStartAbility);
            entity.events.finishAbility.RemoveListener(OnFinishAbility);
            entity.events.onHitReceived.RemoveListener(OnHit);
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
            CharacterStatus.StatusChanged.AddListener(OnStatusChanged);
            AbilitySystem.OnWeaponChange(weaponStorage.WeaponInUse);
            events.onInstantiate.Invoke();
            BindEntity();
        }

        void BindEntity()
        {
            if (!entity) return;
            entity.events.onEnabled.AddListener(OnEntityEnabled);
            if (entity.enabled && entity.Ready) OnEntityEnabled(entity);
            entity.events.onDisabled.AddListener(OnEntityDisabled);
            if (!entity.enabled) OnEntityDisabled(entity);
        }

        void OnEnable()
        {
            entity.enabled = true;
        }

        void OnDisable()
        {
            Entity.enabled = false;
        }

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
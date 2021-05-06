using System;
using _Game.Scripts.GameContent.Abilities;
using _Game.Scripts.GameContent.Entities;
using _Game.Scripts.GameContent.Teams;
using _Game.Scripts.GameContent.Weapons;
using _Game.Scripts.Services.AttributeSystem;
using _Game.Scripts.Services.CombatSystem;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace _Game.Scripts.GameContent.Characters
{
    public class Character : MonoBehaviour
    {
        [SerializeField] CharacterData data;
        [SerializeField] Entity entity;

        [FormerlySerializedAs("weapons")] [SerializeField]
        CharacterWeapons weaponStorage;

        [SerializeField] CharacterAbilities abilitySystem;

        public CharacterEvents events;

        bool dead;
        [SerializeField] bool immortal;

        public Status Status { get; private set; }
        public Team Team { get; private set; }
        public Entity Entity => entity;
        public CharacterWeapons WeaponStorage => weaponStorage;
        public CharacterAbilities AbilitySystem => abilitySystem;

        public bool Ready { get; private set; }

        void KillEntity()
        {
            if (!immortal) entity.Kill();
        }

        #region Callbacks

        void OnAnyStatChanged(Status status)
        {
            if (immortal) return;
            if (dead || !(status.Life.Current <= 0)) return;
            entity.Kill();
            dead = true;
        }

        void OnHit(AbilityHit abilityHit)
        {
            Status.Life.ApplyDamage(abilityHit.power);
            AbilitySystem.StopAbility();
        }

        void OnStartAbility(int abilityIndex)
        {
            AbilitySystem.StartAbility(abilityIndex);
            entity.SetupAbility(AbilitySystem.AbilityInUse);
            entity.CombatMode = true;
        }

        void OnFinishAbility()
        {
            AbilitySystem.StopAbility();
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

        void Start()
        {
            Team = data.DefaultTeam;
            Status = new Status(data.RawStatus);
            WeaponStorage.onWeaponChange.AddListener(AbilitySystem.OnWeaponChange);
            Status.onAnyStatChanged.AddListener(OnAnyStatChanged);
            AbilitySystem.OnWeaponChange(weaponStorage.WeaponInUse);

            events.onInstantiate.Invoke();
            BindEntity();
        }

        void BindEntity()
        {
            if (!entity) return;
            entity.events.onEnabled.AddListener(OnEntityEnabled);
            if (entity.enabled) OnEntityEnabled(entity);
            entity.events.onDisabled.AddListener(OnEntityDisabled);
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
        public UnityEntityEvent onEntityBirth;
        public UnityEntityEvent onEntityDeath;
        public AbilityEvent startAbility;
        public AbilityEvent finishAbility;
    }

    [Serializable]
    public class AbilityEvent : UnityEvent<Ability> { }

    [Serializable]
    public class UnityCharacterEvent : UnityEvent<Character> { }

    [Serializable]
    public class UnityEntityEvent : UnityEvent<Entity> { }
}
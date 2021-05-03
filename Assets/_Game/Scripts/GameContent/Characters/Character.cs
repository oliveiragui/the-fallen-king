using System;
using _Game.Scripts.GameContent.Entities;
using _Game.Scripts.GameContent.Teams;
using _Game.Scripts.Services.AttributeSystem;
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

        public Status Status { get; private set; }
        public Team Team { get; private set; }
        public Entity Entity => entity;
        public CharacterWeapons WeaponStorage => weaponStorage;
        public CharacterAbilities AbilitySystem => abilitySystem;

        void Awake()
        {
            Team = data.DefaultTeam;
            Status = new Status(data.RawStatus);
        }

        void Start()
        {
            WeaponStorage.onWeaponChange.AddListener(AbilitySystem.OnWeaponChange);
            AbilitySystem.OnWeaponChange(weaponStorage.WeaponInUse);
            
            events.onInstantiate.Invoke();
            Status.onAnyStatChanged.AddListener(status =>
            {
                if (!dead && status.Life.Current <= 0)
                {
                    events.onDeath.Invoke(this);
                    dead = true;
                }
            });
        }

        void OnDestroy()
        {
            events.onDestroy.Invoke();
        }

        void KillEntity(Character character)
        {
            entity.Kill();
        }
    }

    [Serializable]
    public class CharacterEvents
    {
        public UnityEntityEvent onEntitySummon;
        public UnityEntityEvent onEntityDimmissed;
        public UnityCharacterEvent onDeath;
        public UnityEvent onInstantiate;
        public UnityEvent onDestroy;
    }

    [Serializable]
    public class UnityCharacterEvent : UnityEvent<Character> { }

    [Serializable]
    public class UnityEntityEvent : UnityEvent<Entity> { }
}
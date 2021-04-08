using System;
using _Game.Scripts.Components.AttributeSystem;
using _Game.Scripts.GameContent.Entities;
using _Game.Scripts.GameContent.Teams;
using UnityEngine;
using UnityEngine.Events;

namespace _Game.Scripts.GameContent.Characters
{
    public class Character : MonoBehaviour
    {
        [SerializeField] CharacterData data;
        [SerializeField] Entity entity;
        [SerializeField] CharacterWeapons weapons;

        public Status Status { get; private set; }
        public Team Team { get; private set; }
        public Entity Entity => entity;
        public CharacterWeapons Weapons => weapons;

        public CharacterEvents events;

        bool dead;

        void Awake()
        {
            Team = data.DefaultTeam;
            Status = new Status(data.RawStatus);
        }

        void Start()
        {
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
            entity.commands.Die();
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
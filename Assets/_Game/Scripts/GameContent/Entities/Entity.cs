using System;
using _Game.Scripts.Components.CombatSystem;
using _Game.Scripts.GameContent.Abilities;
using _Game.Scripts.GameContent.Ammunition;
using _Game.Scripts.GameContent.Characters;
using _Game.Scripts.GameContent.Entities.Components.Animation;
using _Game.Scripts.GameContent.Entities.Components.Audio;
using _Game.Scripts.GameContent.Entities.Components.Mesh;
using _Game.Scripts.GameContent.Entities.Components.Particle;
using _Game.Scripts.GameContent.Entities.Components.PhysicsSystem;
using _Game.Scripts.GameContent.Weapons;
using UnityEngine;
using UnityEngine.Events;

namespace _Game.Scripts.GameContent.Entities
{
    public class Entity : MonoBehaviour
    {
        [SerializeField] public EntityAnimation animations;
        [SerializeField] public EntityAudio entityAudio;
        [SerializeField] public EntityMesh mesh;
        [SerializeField] public EntityParticle particle;
        [SerializeField] public EntityCollision collision;
        [SerializeField] public EntityMovement movement;
        [SerializeField] public EntityCommands commands;

        [SerializeField] public Character associatedCharacter;
        public Quaternion lookDiretion;
        public bool inCombat;

        public CombatEvents events;

        bool ready;

        void OnActivate(Entity entity)
        {
            associatedCharacter.events.onEntitySummon.Invoke(entity);
            if (associatedCharacter.Weapons.WeaponInUse) commands.EquipWeapon(associatedCharacter.Weapons.WeaponInUse);
            associatedCharacter.Weapons.onWeaponChange.AddListener(commands.EquipWeapon);
        }

        void OnDeactivated(Entity entity)
        {
            associatedCharacter.events.onEntityDimmissed.Invoke(entity);
            associatedCharacter.Weapons.onWeaponChange.RemoveListener(commands.EquipWeapon);
        }

        #region Unity Functions

        void Start()
        {
            ready = true;
            OnActivate(this);
            events.onHitReceived.AddListener(ReceiveHit);
        }

        void FixedUpdate()
        {
            animations.Run(movement.Speed);
        }

        void OnEnable()
        {
            if (ready) OnActivate(this);
        }

        void OnDisable()
        {
            OnDeactivated(this);
        }

        void OnTriggerEnter(Collider other)
        {
            var body = other.attachedRigidbody;
            if (!body || !other.CompareTag("Bullet")) return;
            if (!body.transform.TryGetComponent(out Ammo ammo)) return;
            if (Equals(ammo.AbilityHit.origin)) return;
            if (associatedCharacter.Team.PlayerFriend == ammo.AbilityHit.origin.Team.PlayerFriend) return;
            ammo.Hit(this);
        }

        public void ReceiveHit(AbilityHit abilityHit)
        {
            associatedCharacter.Status.Life.ApplyDamage(abilityHit.power);
            animations.StartHit(abilityHit.impact);
        }

        #endregion
    }

    [Serializable]
    public class CombatEvents
    {
        [SerializeField] public HitReceiveEvent onHitReceived;
    }

    [Serializable]
    public class HitReceiveEvent : UnityEvent<AbilityHit> { }
}
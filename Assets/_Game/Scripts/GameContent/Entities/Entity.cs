using System;
using _Game.Scripts.CombatSystem;
using _Game.Scripts.Entities.Components.Animation;
using _Game.Scripts.Entities.Components.Audio;
using _Game.Scripts.Entities.Components.Mesh;
using _Game.Scripts.Entities.Components.Particle;
using _Game.Scripts.Entities.Components.PhysicsSystem;
using UnityEngine;
using UnityEngine.Events;

namespace _Game.Scripts.Entities
{
    public class Entity : MonoBehaviour
    {
        [SerializeField] public EntityBaseAnimation animations;
        [SerializeField] public EntityAudio entityAudio;
        [SerializeField] public EntityMesh mesh;
        [SerializeField] public EntityParticle particle;
        [SerializeField] public EntityCollision collision;
        [SerializeField] public EntityMovement movement;
        [SerializeField] public EntityCommands commands;
        [SerializeField] public GameObject actionContainer;

        public EntityData data;
        public CombatEvents events;

        void OnTriggerEnter(Collider other)
        {
            var body = other.attachedRigidbody;
            if (!body || !other.CompareTag("Bullet")) return;
            if (!body.transform.TryGetComponent(out Ammo.Ammo ammo)) return;
            if (Equals(ammo.AbilityHit.origin)) return;
            if (data.associatedCharacter.Team.PlayerFriend == ammo.AbilityHit.origin.Team.PlayerFriend) return;
            ammo.Hit(this);
        }
    }

    [Serializable]
    public class CombatEvents
    {
        [SerializeField] public HitReceiveEvent onHitReceived;
    }

    [Serializable]
    public class HitReceiveEvent : UnityEvent<AbilityHit> { }
}
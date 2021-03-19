using System;
using _Game.Scripts;
using Ammo;
using CombatSystem;
using Entities.Common;
using Entities.Default;
using UnityEngine;
using UnityEngine.Events;

namespace Entities.Combat
{
    public class CombatEntity : MonoBehaviour
    {
        public EntityComponents components;
        public CombatEntityData combatData;
        public EntityData defaultData;
        public CombatEvents events;

        void OnTriggerEnter(Collider other)
        {
            var body = other.attachedRigidbody;
            if (!body || !other.CompareTag("Bullet")) return;
            if (!other.attachedRigidbody.transform.TryGetComponent(out BaseAmmo ammo)) return;
            Debug.Log("tchau");
            //if (!ammo.combatEntity.components.collision.Hittable) return;
            if (Equals(ammo.AbilityHit.origin)) return;
            if (defaultData.associatedCharacter.Team.PlayerFriend == ammo.AbilityHit.origin.Team.PlayerFriend) return;
            ammo.Hit(this);
        }
    }

    [Serializable]
    public class CombatEvents
    {
        [SerializeField] public AbilityEnterEvent onAbilityEntered;
        [SerializeField] public AbilityExitEvent onAbilityExited;
        [SerializeField] public ComboEnterEvent onComboEntered;
        [SerializeField] public ComboExitEvent onComboExited;
        [SerializeField] public HitReceiveEvent onHitReceived;
    }

    [Serializable]
    public class AbilityEnterEvent : UnityEvent<int> { }

    [Serializable]
    public class AbilityExitEvent : UnityEvent { }

    [Serializable]
    public class ComboEnterEvent : UnityEvent<int> { }

    [Serializable]
    public class ComboExitEvent : UnityEvent { }

    [Serializable]
    public class HitReceiveEvent : UnityEvent<AbilityHit> { }
}
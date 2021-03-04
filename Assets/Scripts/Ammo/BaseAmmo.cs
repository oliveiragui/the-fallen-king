using System;
using System.Collections;
using Characters;
using CombatSystem;
using Entities;
using UnityEngine;

namespace Ammo
{
    [RequireComponent(typeof(Rigidbody))]
    public class BaseAmmo : MonoBehaviour
    {
        //TODO: Remover dependencia do character
        Damage damage;
        Character character;
        [SerializeField] Rigidbody rigidbody;

        bool hasCollider = false;

        public void Setup(Damage damage, Character character, Vector3 force)
        {
            this.damage = damage;
            this.character = character;
            rigidbody.AddForce(force);
        }

        void OnCollisionEnter(Collision other)
        {
            transform.parent = other.transform;
            StartCoroutine(SelfDestruction());
        }

        void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Hittable")) return;
            if (!other.attachedRigidbody.transform.TryGetComponent(out Entity otherEntity)) return;
            if (!otherEntity.physics.Hittable) return;
            if (otherEntity.associatedCharacter.Equals(character)) return;
            if (damage == null) return;
            if (hasCollider) return;
            hasCollider = true;
            if (otherEntity.associatedCharacter.Team.PlayerFriend == damage.team.PlayerFriend) return;
            
            otherEntity.CauseDamage(damage);
            transform.parent = other.transform;
            StartCoroutine(SelfDestruction());
        }

        IEnumerator SelfDestruction()
        {
            rigidbody.isKinematic = true;
            enabled = false;
            yield return new WaitForSeconds(4f);
            Destroy(transform.gameObject);
        }
    }
}
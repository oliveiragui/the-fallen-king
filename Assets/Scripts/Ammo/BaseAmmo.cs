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
        AbilityHit _abilityHit;
        Character character;
        [SerializeField] Rigidbody rigidbody;

        bool hasCollider = false;

        public void Setup(AbilityHit abilityHit, Character character, Vector3 force)
        {
            this._abilityHit = abilityHit;
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
            if (!otherEntity.collision.Hittable) return;
            if (otherEntity.associatedCharacter.Equals(character)) return;
            if (_abilityHit == null) return;
            if (hasCollider) return;
            hasCollider = true;
            if (_abilityHit.friendlyFire && otherEntity.associatedCharacter.Team.PlayerFriend == _abilityHit.team.PlayerFriend) return;

            otherEntity.ReceiveHit(_abilityHit);
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
using System.Collections;
using Characters;
using CombatSystem;
using Entities.Combat;
using UnityEngine;

namespace Ammo
{
    [RequireComponent(typeof(Rigidbody))]
    public class BaseAmmo : MonoBehaviour
    {
        [SerializeField] Rigidbody rigidbody;

        //TODO: Remover dependencia do character
        AbilityHit _abilityHit;
        Character character;

        bool hasCollider;

        void OnCollisionEnter(Collision other)
        {
            transform.parent = other.transform;
            StartCoroutine(SelfDestruction());
        }

        void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Hittable")) return;
            if (!other.attachedRigidbody.transform.TryGetComponent(out CombatEntity otherEntity)) return;
            if (!otherEntity.components.collision.Hittable) return;
            if (otherEntity.defaultData.associatedCharacter.Equals(character)) return;
            if (_abilityHit == null) return;
            if (hasCollider) return;
            hasCollider = true;
            if (_abilityHit.friendlyFire &&
                otherEntity.defaultData.associatedCharacter.Team.PlayerFriend == _abilityHit.team.PlayerFriend) return;

            otherEntity.events.onHitReceived.Invoke(_abilityHit);
            transform.parent = other.transform;
            StartCoroutine(SelfDestruction());
        }

        public void Setup(AbilityHit abilityHit, Character character, Vector3 force)
        {
            _abilityHit = abilityHit;
            this.character = character;
            rigidbody.AddForce(force);
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
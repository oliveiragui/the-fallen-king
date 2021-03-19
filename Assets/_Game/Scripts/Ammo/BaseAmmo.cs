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
        public AbilityHit AbilityHit { get; private set; }
        public Character Character { get; private set; }
        public bool HasCollided { get; private set; }

        void OnCollisionEnter(Collision other)
        {
            transform.parent = other.transform;
            StartCoroutine(SelfDestruction());
        }

        public void Hit(CombatEntity entity)
        {
            HasCollided = true;
            entity.events.onHitReceived.Invoke(AbilityHit);
            transform.parent = entity.transform;
            StartCoroutine(SelfDestruction());
        }

        public void Setup(AbilityHit abilityHit, Character character, Vector3 force)
        {
            AbilityHit = abilityHit;
            Character = character;
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
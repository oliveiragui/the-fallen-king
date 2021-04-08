using System;
using System.Collections;
using _Game.Scripts.Components.CombatSystem;
using _Game.Scripts.GameContent.Entities;
using UnityEngine;

namespace _Game.Scripts.GameContent.Ammunition
{
    [RequireComponent(typeof(Rigidbody))]
    public class Ammo : MonoBehaviour
    {
        [NonSerialized] public AmmoData data;

        Rigidbody _rigidbody;

        public AbilityHit AbilityHit { get; private set; }
        public bool HasCollided { get; private set; }

        Coroutine DeactivationTimer;

        #region Unity Functions

        void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        void OnCollisionEnter(Collision other)
        {
            OnHit(other.transform);
        }

        void OnEnable()
        {
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity= Vector3.zero;
            _rigidbody.isKinematic = false;
        }

        void OnDisable()
        {
            if (DeactivationTimer != null) StopCoroutine(DeactivationTimer);
            HasCollided = false;
        }

        #endregion

        public void Shot(AbilityHit hit, Vector3 force)
        {
            AbilityHit = hit;
            _rigidbody.AddForce(force);
        }

        public void Hit(Entity entity)
        {
            if (HasCollided) return;
            entity.events.onHitReceived.Invoke(AbilityHit);
            OnHit(entity.transform);
        }

        void OnHit(Transform target)
        {
            HasCollided = true;
            AttachToTarget(target);
            SetLifeTime(data.LifeTimeOnHit);
        }

        void AttachToTarget(Transform other)
        {
            _rigidbody.isKinematic = true;
            transform.parent = other;
        }

        public void SetLifeTime(float seconds)
        {
            if (DeactivationTimer != null) StopCoroutine(DeactivationTimer);
            DeactivationTimer = StartCoroutine(DeactivateAfterSeconds(seconds));
        }

        public IEnumerator DeactivateAfterSeconds(float seconds)
        {
            yield return new WaitForSeconds(seconds);
            data.Deactivate(this);
        }
    }
}
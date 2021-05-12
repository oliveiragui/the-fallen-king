using System;
using System.Collections;
using _Game.GameModules.Abilities.Scripts;
using _Game.GameModules.Entities.Scripts;
using UnityEngine;

namespace _Game.GameModules.Ammunition.Scripts
{
    [RequireComponent(typeof(Rigidbody))]
    public class Ammo : MonoBehaviour
    {
        [SerializeField] GameObject trail;
        Rigidbody _rigidbody;
        [NonSerialized] public AmmoData data;

        Coroutine DeactivationTimer;

        public AbilityHit AbilityHit { get; private set; }
        public bool HasCollided { get; private set; }

        public void Shot(AbilityHit hit, Vector3 force)
        {
            AbilityHit = hit;
            _rigidbody.AddForce(force);
            trail.SetActive(true);
        }

        public void Hit(Entity entity)
        {
            if (HasCollided) return;
            entity.Hit(AbilityHit);
            OnHit(entity.transform);
        }

        void OnHit(Transform target)
        {
            HasCollided = true;
            AttachToTarget(target);
            SetLifeTime(data.LifeTimeOnHit);
            trail.SetActive(false);
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
            _rigidbody.angularVelocity = Vector3.zero;
            _rigidbody.isKinematic = false;
        }

        void OnDisable()
        {
            if (DeactivationTimer != null) StopCoroutine(DeactivationTimer);
            HasCollided = false;
        }

        #endregion
    }
}
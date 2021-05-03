﻿using System;
using System.Collections;
using _Game.Scripts.GameContent.Abilities;
using _Game.Scripts.GameContent.Ammunition;
using _Game.Scripts.GameContent.Characters;
using _Game.Scripts.GameContent.Entities.Components.Audio;
using _Game.Scripts.GameContent.Entities.Components.Mesh;
using _Game.Scripts.GameContent.Entities.Components.Movement;
using _Game.Scripts.GameContent.Entities.Components.Particles;
using _Game.Scripts.GameContent.Entities.Components.PhysicsSystem;
using _Game.Scripts.GameContent.Weapons;
using _Game.Scripts.Services.CombatSystem;
using UnityEngine;
using UnityEngine.Events;

namespace _Game.Scripts.GameContent.Entities
{
    public class Entity : MonoBehaviour
    {
        [SerializeField] Animator animator;
        [SerializeField] EntityAudio entityAudio;
        [SerializeField] EntityMesh mesh;
        [SerializeField] EntityParticle particle;
        [SerializeField] EntityCollision collision;
        [SerializeField] public EntityMovement movement;

        public EntityEvents events;

        #region Parameters

        [SerializeField] string _floorName;
        [SerializeField] Character _character;

        int _requestedAbilityId;
        bool _combatMode;
        bool _canMove;

        bool _conjuring;
        AbilityHit _currentHit;

        public Character Character => _character;

        public bool Alive { get; set; } = true;
        Weapon Weapon { get; set; }
        public bool AutoMove { get; set; }
        public float Speed { get; private set; }
        public Quaternion Direction { get; private set; }
        public Quaternion LookDiretion { get; private set; }
        public float StoppingDistance { get; private set; }
        public Vector3 Destination { get; private set; }
        public bool Ready { get; private set; }

        public int RequestedAbilityId
        {
            get => _requestedAbilityId;
            set
            {
                _requestedAbilityId = value;
                animator.SetInteger(AnimatorParams.RequestedAbilityID, value);
            }
        }

        public bool CombatMode
        {
            get => _combatMode;
            set
            {
                mesh.EquipWeapons = value;
                animator.SetLayerWeight(1, value ? 1f : 0f);
            }
        }

        public bool CanMove
        {
            get => _canMove;

            set
            {
                animator.SetBool("Anda", value);
                _canMove = value;
            }
        }

        public string FloorName
        {
            get => _floorName;
            set => _floorName = value;
        }

        public AbilityHit CurrentHit
        {
            get => _currentHit;
            set
            {
                if (value)
                {
                    animator.SetBool(AnimatorParams.ReceivingHit, true);
                    animator.SetInteger(AnimatorParams.HitImpact, (int) value.impact + 2);
                }
                else
                {
                    animator.SetBool(AnimatorParams.ReceivingHit, false);
                    _currentHit = value;
                }
            }
        }

        #endregion

        #region Methods

        public void RequestAbility(int abilityIndex)
        {
            var abSys = Character.AbilitySystem;
            var reqAbility = Character.AbilitySystem.Abilities[abilityIndex];
            RequestedAbilityId = reqAbility.Data.AnimationId;
            if (!abSys.CanUseAbility(abilityIndex)) return;

            animator.SetTrigger(reqAbility.CanOverride(abSys.AbilityInUse)
                ? AnimatorParams.ForceAbility
                : AnimatorParams.RequestAbility);
            animator.SetBool(AnimatorParams.Cast, true);
        }

        public void StartAbility()
        {
            Character.AbilitySystem.StartAbility(RequestedAbilityId - 1);
            CombatMode = true;
            var combo = Character.AbilitySystem.AbilityInUse.CurrentCombo;

            animator.SetInteger(AnimatorParams.MaxCombo, Character.AbilitySystem.AbilityInUse.Data.MaxCombo);
            animator.SetInteger("Combo Atual", Character.AbilitySystem.AbilityInUse.CurrentComboID);
            animator.SetBool(AnimatorParams.Castable, combo.Castable);
            animator.SetFloat(AnimatorParams.ComboFactor1, combo.Factor1 * 1);
            if (!combo.Castable) return;
            animator.SetFloat(AnimatorParams.ComboFactor2, combo.Factor2 * 1);
            animator.SetFloat(AnimatorParams.ComboFactor3, combo.Factor3 * 1);
        }

        public void StopAbility()
        {
            Character.AbilitySystem.StopAbility();
        }

        public void StopCasting(int index)
        {
            animator.SetBool(AnimatorParams.Cast, !Character.AbilitySystem.StopCasting(index));
        }

        public void SetRotation(Quaternion direction)
        {
            LookDiretion = direction;
        }

        public void Move(float speed, Quaternion direction)
        {
            Speed = speed;
            Direction = direction;
            CanMove = true;
        }

        public void MoveTo(float speed, Vector3 destination, float stoppingDistance)
        {
            Speed = speed;
            Destination = destination;
            StoppingDistance = stoppingDistance;
            CanMove = true;
        }

        public void Stop()
        {
            CanMove = false;
        }

        public void PlayAbilityParticleEffect(int index)
        {
            particle.PlayAbilityEffect(0, index);
        }

        void ReceiveHit(AbilityHit abilityHit)
        {
            if (!abilityHit) return;
            Character.Status.Life.ApplyDamage(abilityHit.power);
            Character.AbilitySystem.StopAbility();
            CurrentHit = abilityHit;
            movement.Stop();
        }

        public void StopHit()
        {
            CurrentHit = null;
        }

        public void Kill()
        {
            Alive = false;
            animator.SetTrigger(AnimatorParams.Die);
        }

        public void PlayFootStepSound() => entityAudio.Play(FloorName);

        public void PlayFootStepParticleEffect() => particle.Play("FootStep");

        public void TurnToLookDirection() => movement.Rotation = LookDiretion;

        public void BecomeHittable()
        {
            collision.Hittable = true;
        }

        public void BecomeUnhittable()
        {
            collision.Hittable = false;
        }

        public void RotateWhileConjuring()
        {
            var abSys = Character.AbilitySystem;

            IEnumerator Rotate() => new WaitWhile(() =>
            {
                movement.Rotation = LookDiretion;
                return abSys.AbilityInUse.Conjuring;
            });

            StartCoroutine(Rotate());
        }

        void InvocaFlecha()
        {
            var tr = transform;
            var position = tr.position + new Vector3(0, 1, 0);
            var hit = new AbilityHit(-2, Vector3.zero, Character);
            var force = transform.forward.normalized * 800;

            Weapon.ammoData.Instantiate(position, tr.rotation).Shot(hit, force);
        }

        void EsferaDeDano()
        {
            var tr = transform;

            var hits = Physics.SphereCastAll(
                tr.position,
                2,
                tr.forward,
                0.01f,
                LayerMask.GetMask("Hittable"));

            foreach (var hit in hits)
            {
                if (!hit.collider.attachedRigidbody.transform.TryGetComponent(out Entity otherEntity)) continue;
                if (!otherEntity.collision.Hittable) continue;
                if (otherEntity.Character.Equals(Character)) continue;
                if (otherEntity.Character.Team.PlayerFriend ==
                    Character.Team.PlayerFriend) continue;
                otherEntity.events.onHitReceived.Invoke(new AbilityHit(-2, Vector3.zero,
                    Character));
            }
        }

        #endregion

        #region Unity Functions

        void Start()
        {
            Ready = true;
            Activate();
            OnWeaponChange(Character.WeaponStorage.WeaponInUse);
            events.onHitReceived.AddListener(ReceiveHit);
        }

        void Activate()
        {
            Character.events.onEntitySummon.Invoke(this);
            Character.WeaponStorage.onWeaponChange.AddListener(OnWeaponChange);
        }

        void OnWeaponChange(Weapon weapon)
        {
            if (weapon)
            {
                Weapon = weapon;
                animator.runtimeAnimatorController = weapon.Data.AnimatorController;
                mesh.SwitchWeapon(weapon.Data.Prefabs);
                particle.InstantiateAbilityEffects(weapon.Abilities.ToArray());
            }
        }

        void OnEnable()
        {
            if (Ready) Activate();
        }

        void OnDisable()
        {
            Character.events.onEntityDimmissed.Invoke(this);
            Character.WeaponStorage.onWeaponChange.RemoveListener(OnWeaponChange);
        }

        void OnAnimatorMove()
        {
            if (Character.AbilitySystem.AbilityInUse)
                movement.Speed = (animator.deltaPosition / Time.deltaTime).magnitude;
        }

        void OnTriggerEnter(Collider other)
        {
            DetectFloorName(other);
            DetectBulletHit(other);
        }

        void DetectBulletHit(Collider other)
        {
            var body = other.attachedRigidbody;
            if (!body || !other.CompareTag("Bullet")) return;
            if (!body.transform.TryGetComponent(out Ammo ammo)) return;
            if (Equals(ammo.AbilityHit.origin)) return;
            if (Character.Team.PlayerFriend == ammo.AbilityHit.origin.Team.PlayerFriend) return;
            ammo.Hit(this);
        }

        void DetectFloorName(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Floor"))
            {
                FloorName = other.tag;
            }
        }

        #endregion
    }

    [Serializable]
    public class EntityEvents
    {
        [SerializeField] public HitReceiveEvent onHitReceived;
    }

    [Serializable]
    public class HitReceiveEvent : UnityEvent<AbilityHit> { }
}
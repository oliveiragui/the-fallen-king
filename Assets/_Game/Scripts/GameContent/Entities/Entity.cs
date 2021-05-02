using System;
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
        [SerializeField] public EntityAudio entityAudio;
        [SerializeField] public EntityMesh mesh;
        [SerializeField] public EntityParticle particle;
        [SerializeField] public EntityCollision collision;
        [SerializeField] public EntityMovement movement;

        public EntityEvents events;

        [SerializeField] EntityData data;

        #region Parameters

        public Ability AbilityInUse => data.AbilityInUse;

        public float Speed => data.speed;
        public float StoppingDistance => data.stoppingDistance;
        public Quaternion Direction => data.moveDiretion;
        public Vector3 Destination => data.destination;
        public Character Character => data.associatedCharacter;

        public bool AutoMove
        {
            get => data.autoMove;
            set => data.autoMove = value;
        }

        #endregion

        #region Methods

        public void RequestAbility(int abilityIndex)
        {
            data.RequestedAbility = abilityIndex;
            var reqAbility = data.Abilities[abilityIndex];

            if (reqAbility.Cooldown > 0) return;

            if (!data.AbilityInUse || reqAbility.Equals(data.AbilityInUse))
                animator.SetTrigger(AnimatorParams.RequestAbility);
            else if (reqAbility.Data.CanInterrupt(data.AbilityInUse.Data))
                animator.SetTrigger(AnimatorParams.ForceAbility);

            if (!data.Conjuring) data.Conjuring = true;
        }

        public void LookAt(Quaternion direction)
        {
            data.lookDiretion = direction;
        }

        public void Move(float speed, Quaternion direction)
        {
            data.speed = speed;
            data.moveDiretion = direction;
            data.Move = true;
        }

        public void MoveTo(float speed, Vector3 destination, float stoppingDistance)
        {
            data.speed = speed;
            data.destination = destination;
            data.stoppingDistance = stoppingDistance;
            data.Move = true;
        }

        public void Stop()
        {
            data.Move = false;
        }

        public void StartRequestedAbility()
        {
            if (AbilityInUse && AbilityInUse != data.Abilities[data.RequestedAbility]) AbilityInUse.Finish();
            data.AbilityInUse = data.Abilities[data.RequestedAbility];
            data.AbilityInUse.Initialize(transform);
            data.AbilityInUse.Use();
            CombatMode = true;
        }

        // public void ConfigAbility(Ability ability)
        // {
        //     var combo = ability.Combos[ability.CurrentCombo];
        //     
        //     animator.SetBool(AnimatorParams.Castable, combo.Castable);
        //     animator.SetFloat(AnimatorParams.ComboFactor1, combo.Factor1 * 1);
        //     if (!combo.Castable) return;
        //     animator.SetFloat(AnimatorParams.ComboFactor2, combo.Factor2 * 1);
        //     animator.SetFloat(AnimatorParams.ComboFactor3, combo.Factor3 * 1);
        // }

        public void StopAbility()
        {
            if (AbilityInUse) AbilityInUse.Finish();
            data.AbilityInUse = null;
        }

        public void StopCasting(int index)
        {
            if (CanStopCasting(index)) data.Conjuring = false;
        }

        bool CanStopCasting(int i) => !data.AbilityInUse || data.Abilities[i].Equals(data.AbilityInUse);

        public void UseCombo(int index)
        {
            data.ComboInUse = data.AbilityInUse.Combos[index];
        }

        public void StopCombo()
        {
            data.ComboInUse = null;
        }

        public bool CombatMode
        {
            get => data.CombatMode;
            set
            {
                mesh.EquipWeapons = value;
                data.CombatMode = true;
                if(value) animator.SetTrigger(AnimatorParams.CombatMode);
            }
            
        }
        
        public void PlayAbilityParticleEffect(int index)
        {
            data.AbilityInUse.PlayParticleEffect(index);
        }

        void ReceiveHit(AbilityHit abilityHit)
        {
            if (!abilityHit) return;
            data.associatedCharacter.Status.Life.ApplyDamage(abilityHit.power);
            data.CurrentHit = abilityHit;
            data.AbilityInUse = null;
            movement.Stop();
        }

        public void StopHit()
        {
            data.CurrentHit = null;
        }

        public void Kill()
        {
            data.Alive = false;
            animator.SetTrigger(AnimatorParams.Die);
        }

        public void PlayFootStepSound() => entityAudio.Play(data.FloorName);

        public void PlayFootStepParticleEffect() => particle.Play("FootStep");

        public void TurnToLookDirection() => movement.Rotation = data.lookDiretion;

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
            IEnumerator Rotate() => new WaitWhile(() =>
            {
                movement.Rotation = data.lookDiretion;
                return data.Conjuring;
            });

            StartCoroutine(Rotate());
        }

        void InvocaFlecha()
        {
            var tr = transform;
            var position = tr.position + new Vector3(0, 1, 0);
            var hit = new AbilityHit(-2, Vector3.zero, data.associatedCharacter);
            var force = transform.forward.normalized * 800;

            data.Weapon
                .ammoData
                .Instantiate(position, tr.rotation)
                .Shot(hit, force);
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
                if (otherEntity.data.associatedCharacter.Equals(data.associatedCharacter)) continue;
                if (otherEntity.data.associatedCharacter.Team.PlayerFriend ==
                    data.associatedCharacter.Team.PlayerFriend) continue;
                otherEntity.events.onHitReceived.Invoke(new AbilityHit(-2, Vector3.zero,
                    data.associatedCharacter));
            }
        }

        #endregion

        #region Unity Functions

        void Start()
        {
            data.ready = true;
            Activate();
            OnWeaponChange(data.associatedCharacter.Weapons.WeaponInUse);
            events.onHitReceived.AddListener(ReceiveHit);
        }

        void Activate()
        {
            data.associatedCharacter.events.onEntitySummon.Invoke(this);
            data.associatedCharacter.Weapons.onWeaponChange.AddListener(OnWeaponChange);
        }

        void OnWeaponChange(Weapon weapon)
        {
            data.Weapon = weapon;
            if (weapon) mesh.SwitchWeapon(weapon.Data.Prefabs);
        }

        void OnEnable()
        {
            if (data.ready) Activate();
        }

        void OnDisable()
        {
            data.associatedCharacter.events.onEntityDimmissed.Invoke(this);
            data.associatedCharacter.Weapons.onWeaponChange.RemoveListener(OnWeaponChange);
        }

        void OnAnimatorMove()
        {
            if (data.AbilityInUse) movement.Speed = (animator.deltaPosition / Time.deltaTime).magnitude;
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
            if (data.associatedCharacter.Team.PlayerFriend == ammo.AbilityHit.origin.Team.PlayerFriend) return;
            ammo.Hit(this);
        }

        void DetectFloorName(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Floor"))
            {
                data.FloorName = other.tag;
                //Debug.Log("Current floor: " + other.tag);
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
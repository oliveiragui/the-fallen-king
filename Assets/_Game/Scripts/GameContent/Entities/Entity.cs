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
        [SerializeField] EntityAudio entityAudio;
        [SerializeField] EntityMesh mesh;
        [SerializeField] EntityParticle particle;
        [SerializeField] EntityCollision collision;
        [SerializeField] public EntityMovement movement;

        public EntityEvents events;

        #region Parameters

        [SerializeField] string _floorName;
        [SerializeField] Character _character;

        bool _combatMode;
        bool _canMove;
        bool _conjuring;

        public Character Character => _character;

        [field: SerializeField] public bool Alive { get; set; } = true;
        Weapon Weapon { get; set; }
        [field: SerializeField] public bool AutoMove { get; set; }
        [field: SerializeField] public float Speed { get; private set; }
        [field: SerializeField] public Quaternion Direction { get; private set; }
        [field: SerializeField] public Quaternion LookDiretion { get; private set; }
        [field: SerializeField] public float StoppingDistance { get; private set; }
        [field: SerializeField] public Vector3 Destination { get; private set; }
        [field: SerializeField] public bool Ready { get; private set; }

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

        #endregion

        #region Methods

        public void RequestAbility(int abilityIndex)
        {
            var abSys = Character.AbilitySystem;
            var reqAbility = Character.AbilitySystem.Abilities[abilityIndex];
            if (!abSys.CanUseAbility(abilityIndex)) return;

            animator.SetTrigger(reqAbility.CanOverride(abSys.AbilityInUse)
                ? AnimatorParams.ForceAbility
                : AnimatorParams.RequestAbility);
            animator.SetInteger(AnimatorParams.RequestedAbilityID, reqAbility.Data.AnimationId);
            animator.SetBool($"Conjurando Habilidade {reqAbility.Data.AnimationId}", true);
        }

        public void SetupAbility(Ability ability)
        {
            var combo = ability.CurrentCombo;
            animator.SetInteger("Combo Atual", ability.CurrentComboID);
            animator.SetBool(AnimatorParams.Castable, combo.Castable);
            animator.SetFloat(AnimatorParams.ComboFactor1, combo.Factor1 * 1);
            if (!combo.Castable) return;
            animator.SetFloat(AnimatorParams.ComboFactor2, combo.Factor2 * 1);
            animator.SetFloat(AnimatorParams.ComboFactor3, combo.Factor3 * 1);
        }

        public void StopCasting(int index)
        {
            var ability = Character.AbilitySystem.Abilities[index];
            ability.StopConjuring();
            animator.SetBool($"Conjurando Habilidade {ability.Data.AnimationId}", false);
        }

        public void LookAt(Quaternion direction)
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

        public void Hit(AbilityHit abilityHit)
        {
            if (!abilityHit) return;
            events.onHitReceived.Invoke(abilityHit);
            animator.SetBool(AnimatorParams.ReceivingHit, true);
            animator.SetInteger(AnimatorParams.HitImpact, (int) abilityHit.impact + 2);
            movement.Stop();
        }

        // public void StopHit()
        // {
        //     animator.SetBool(AnimatorParams.ReceivingHit, false);
        // }

        public void Kill()
        {
            Alive = false;
            animator.SetTrigger(AnimatorParams.Die);
        }

        public void PlayAbilityParticleEffect(int index)
        {
            particle.PlayAbilityEffect(0, index);
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
            if (!abSys.AbilityInUse) return;

            IEnumerator Rotate() => new WaitWhile(() =>
            {
                movement.Rotation = LookDiretion;
                return abSys.AbilityInUse.Conjuring;
            });

            StartCoroutine(Rotate());
        }

        void InvocaFlecha(float angle)
        {
            var tr = transform;
            var position = tr.position + new Vector3(0, 1, 0);
            var hit = new AbilityHit(-2, Vector3.zero, Character);
            var arrow = Weapon.ammoData.Instantiate(position,
                Quaternion.Euler(Vector3.up * (tr.rotation.eulerAngles.y + angle)));
            arrow.Shot(hit, arrow.transform.forward * 800);
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
                otherEntity.Hit(new AbilityHit(-2, Vector3.zero, Character));
            }
        }

        #endregion

        #region Callbacks
        
        public void OnWeaponChange(Weapon weapon)
        {
            if (weapon)
            {
                Weapon = weapon;
                animator.runtimeAnimatorController = weapon.Data.AnimatorController;
                mesh.SwitchWeapon(weapon.Data.Prefabs);
                particle.InstantiateAbilityEffects(weapon.Abilities.ToArray());
            }
        }

        #endregion

        #region Unity Functions

        void Start()
        {
            events.onBirth.Invoke(this);
            events.onEnabled.Invoke(this);
            Ready = true;
        }

        void OnEnable()
        {
            if (Ready) events.onEnabled.Invoke(this);
        }

        void OnDisable()
        {
            events.onDisabled.Invoke(this);
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
                FloorName = other.tag;
        }

        #endregion
    }

    [Serializable]
    public class EntityEvents
    {
        public UnityEntityEvent onEnabled;
        public UnityEntityEvent onDisabled;
        public HitReceiveEvent onHitReceived;
        public AbilityUseEvent startAbility;
        public UnityEvent finishAbility;
        public UnityEntityEvent enterCombat;
        public UnityEntityEvent exitCombat;
        public UnityEntityEvent onBirth;
        public UnityEntityEvent onDeathBeginning;
        public UnityEntityEvent onDeathEnding;
    }

    [Serializable]
    public class HitReceiveEvent : UnityEvent<AbilityHit> { }

    [Serializable]
    public class AbilityUseEvent : UnityEvent<int> { }
}
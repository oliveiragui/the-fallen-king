using System;
using System.Collections;
using _Game.GameModules.Abilities.Scripts;
using _Game.GameModules.Ammunition.Scripts;
using _Game.GameModules.Characters.Scripts;
using _Game.GameModules.Entities.Scripts.Components.Audio;
using _Game.GameModules.Entities.Scripts.Components.Mesh;
using _Game.GameModules.Entities.Scripts.Components.Movement;
using _Game.GameModules.Entities.Scripts.Components.Particles;
using _Game.GameModules.Entities.Scripts.Components.PhysicsSystem;
using _Game.GameModules.Weapons.Scripts;
using UnityEngine;
using UnityEngine.Events;
using Object = UnityEngine.Object;

namespace _Game.GameModules.Entities.Scripts
{
    public class Entity : MonoBehaviour
    {
        [SerializeField] public Animator animator;
        [SerializeField] public EntityAudio sound;
        [SerializeField] public EntityMesh mesh;
        [SerializeField] public EntityParticle particle;
        [SerializeField] public EntityCollision collision;
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

        public void SetNextAbility(int index, bool canOverride)
        {
            animator.SetTrigger(canOverride
                ? AnimatorParams.ForceAbility
                : AnimatorParams.RequestAbility);
            animator.SetInteger(AnimatorParams.RequestedAbilityID, index);
            animator.SetBool($"Conjurando Habilidade {index}", true);
        }

        public void SetupAbility(Ability ability)
        {
            var combo = ability.CurrentCombo;
            SetCombo(ability.CurrentComboID, combo.Castable, combo.Factor1 * 1, combo.Factor2 * 1, combo.Factor3 * 1);
        }

        public void SetCombo(int id, bool castable, float factor1, float factor2 = 1, float factor3 = 1)
        {
            animator.SetInteger("Combo Atual", id);
            animator.SetBool(AnimatorParams.Castable, castable);
            animator.SetFloat(AnimatorParams.ComboFactor1, factor1);
            if (!castable) return;
            animator.SetFloat(AnimatorParams.ComboFactor2, factor2);
            animator.SetFloat(AnimatorParams.ComboFactor3, factor3);
        }

        public void StopCasting(int index)
        {
            var ability = Character.AbilitySystem.Abilities[index];
            ability.StopConjuring();
            animator.SetBool($"Conjurando Habilidade {index + 1}", false);
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
            animator.SetTrigger("Recebe Hit");
            animator.SetInteger(AnimatorParams.HitImpact, HitResistanceMatrix(abilityHit.impact, Character.Resiliency));
            movement.Stop();
            PlayParticle("Blood");
        }

        int HitResistanceMatrix(HitImpact impact, ImpactResistance resiliency)
        {
            int[,] matrix = new int[4, 4]
            {
                // y - Resiliencia - weak,normal,strong,unbeatable
                {0, 0, 0, 0},
                {2, 1, 1, 1},
                {2, 2, 1, 1},
                {3, 3, 2, 1}
            };
            return matrix[(int) impact, (int) resiliency];
        }

        // public void StopHit()
        // {
        //     animator.SetBool(AnimatorParams.ReceivingHit, false);
        // }

        public void Kill()
        {
            Alive = false;
            animator.SetBool("Vivo", false);
        }

        public void PlayAbilityParticleEffect(int abilityIndex, int particleIndex)
        {
            particle.PlayAbilityEffect(abilityIndex, particleIndex);
        }

        public void StopAbilityParticleEffect(int abilityIndex, int particleIndex)
        {
            particle.PlayAbilityEffect(abilityIndex, particleIndex);
        }

        public void PlayFootStepSound() => sound.Play(FloorName);

        public void PlayFootStepParticleEffect() => particle.Play("FootStep");

        public void PlayParticle(string name) => particle.Play(name);

        public void StopParticle(string name) => particle.Stop(name);

        public void TurnToLookDirection() => movement.Rotation = LookDiretion;

        public void BecomeHittable()
        {
            collision.Hittable = true;
        }

        public void BecomeUnhittable()
        {
            collision.Hittable = false;
        }

        void ExecuteCommand(Object obj)
        {
            if (obj is EntityCommand command) command.Execute(this);
            else Debug.Log(obj.name + " Is not a Command");
        }

        #endregion

        #region Callbacks

        public void OnWeaponChange(Weapon weapon)
        {
            if (weapon)
            {
                Weapon = weapon;
                animator.runtimeAnimatorController = weapon.animatorController;
                mesh.SwitchWeapon(weapon.Data.Prefabs);
                particle.InstantiateAbilityEffects(weapon.Abilities.ToArray());
            }
        }

        #endregion

        #region Unity Functions

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
        public HitReceiveEvent onHitReceived;
        public AbilityUseEvent startAbilityAnimation;
        public UnityEvent endAbilityAnimation;
    }

    [Serializable]
    public class HitReceiveEvent : UnityEvent<AbilityHit> { }

    [Serializable]
    public class AbilityUseEvent : UnityEvent<int> { }
}
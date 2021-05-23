using System;
using _Game.GameModules.Abilities.Scripts;
using _Game.GameModules.Ammunition.Scripts;
using _Game.GameModules.Characters.Scripts;
using _Game.GameModules.Entities.Scripts.Commands;
using _Game.GameModules.Weapons.Scripts;
using UnityEngine;
using UnityEngine.Events;
using Object = UnityEngine.Object;

namespace _Game.GameModules.Entities.Scripts
{
    public class Entity : MonoBehaviour, IWeaponChangeListener, ICharacterStatusChangeListener
    {
        #region Components

        [SerializeField] public Animator animator;
        [SerializeField] public EntityAudio sound;
        [SerializeField] public EntityMesh mesh;
        [SerializeField] public EntityParticle particle;
        [SerializeField] public EntityMovement movement;

        [SerializeField] Collider hitDetectionCollider;

        public bool Hittable
        {
            get => hitDetectionCollider.enabled;
            set => hitDetectionCollider.enabled = value;
        }

        #endregion

        #region events

        public HitReceiveEvent hitReceived;
        public AbilityUseEvent startAbilityAnimation;
        public UnityEvent endAbilityAnimation;

        #endregion

        #region Parameters

        [SerializeField] string _floorName;
        [SerializeField] Character _character;
        [SerializeField] float stoppingDistance = 1;

        bool _canMove;

        public Character Character => _character;

        public bool UsingAbility { get; set; }
        [field: SerializeField] public bool Alive { get; set; } = true;
        [field: SerializeField] public bool AutoMove { get; set; }
        [field: SerializeField] public float CharacterSpeed { get; private set; }
        [field: SerializeField] public float AnimationSpeed { get; private set; }
        public float Speed => CharacterSpeed + AnimationSpeed;
        [field: SerializeField] public Quaternion Direction { get; set; }
        [field: SerializeField] public Quaternion LookDiretion { get; set; }
        public float StoppingDistance => stoppingDistance;
        [field: SerializeField] public Vector3 Destination { get; set; }
        [field: SerializeField] public bool Aim { get; private set; }
        [field: SerializeField] public bool ApplyRootMovement { get; set; }

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

        #region Callbacks

        public void OnWeaponChange(Weapon weapon)
        {
            if (!weapon) return;
            animator.runtimeAnimatorController = weapon.animatorController;
            mesh.SwitchWeapon(weapon.Data.Prefabs);
            particle.InstantiateAbilityEffects(weapon.Abilities.ToArray());
            sound.InstantiateAbilitySfx(weapon.Abilities.ToArray());
        }

        public void OnStatusChange(CharacterStatus status)
        {
            CharacterSpeed = status.Agility.Current;
        }

        void OnTriggerEnter(Collider other)
        {
            DetectFloorName(other);
            OnBulletCollision(other);
        }

        void OnBulletCollision(Collider other)
        {
            var body = other.attachedRigidbody;
            if (!body || !other.CompareTag("Bullet")) return;
            if (!body.transform.TryGetComponent(out Ammo ammo)) return;
            if (Equals(ammo.AbilityHit.origin)) return;
            if (Character.Team.PlayerFriend == ammo.AbilityHit.origin.Team.PlayerFriend) return;
            ammo.Hit(this);
        }

        #endregion

        #region Methods

        public void CombatMode(bool value)
        {
            mesh.EquipWeapons = value;
            animator.SetLayerWeight(1, value ? 1f : 0f);
        }

        public void SetNextAbility(int index, bool canOverride)
        {
            animator.SetTrigger(canOverride
                ? AnimatorParams.ForceAbility
                : AnimatorParams.RequestAbility);
            animator.SetInteger(AnimatorParams.RequestedAbilityID, index);
            animator.SetBool($"Conjurando Habilidade {index}", true);
        }

        public void SetCombo(int id, Combo combo)
        {
            Aim = combo.Aim;
            ApplyRootMovement = combo.ApplyRootMovement;
            animator.SetInteger("Combo Atual", id);
            animator.SetBool(AnimatorParams.Castable, combo.Castable);
            animator.SetFloat(AnimatorParams.ComboFactor1, combo.Factor1);
            animator.SetFloat(AnimatorParams.ComboFactor4, combo.Factor4);
            if (!combo.Castable) return;
            animator.SetFloat(AnimatorParams.ComboFactor2, combo.Factor2);
            animator.SetFloat(AnimatorParams.ComboFactor3, combo.Factor3);
        }

        public void StopCasting(int index)
        {
            animator.SetBool($"Conjurando Habilidade {index + 1}", false);
        }

        public void Hit(AbilityHit abilityHit)
        {
            if (!abilityHit) return;
            hitReceived.Invoke(abilityHit);
            animator.SetTrigger("Recebe Hit");
            animator.SetInteger(AnimatorParams.HitImpact, ImpactMatrix.Calc(abilityHit.impact, Character.Resiliency));
            PlayParticle("Blood");
        }

        public void Kill()
        {
            Alive = false;
            animator.SetBool("Vivo", false);
        }

        public void Move(Quaternion direction)
        {
            Direction = direction;
            CanMove = true;
        }

        public void MoveTo(Vector3 destination)
        {
            Destination = destination;
            CanMove = true;
        }

        public void StopWalking()
        {
            CanMove = false;
        }

        public void PlayFootStepParticleEffect() => particle.Play("FootStep");

        public void PlayParticle(string name) => particle.Play(name);

        public void StopParticle(string name) => particle.Stop(name);

        public void TurnToLookDirection() => movement.Rotation = LookDiretion;

        void DetectFloorName(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Floor")) FloorName = other.tag;
        }

        void ExecuteCommand(Object obj)
        {
            if (obj is EntityCommand command) command.Execute(this);
            else Debug.Log(obj.name + " Is not a Command");
        }

        #endregion
    }

    [Serializable]
    public class HitReceiveEvent : UnityEvent<AbilityHit> { }

    [Serializable]
    public class AbilityUseEvent : UnityEvent<int> { }
}
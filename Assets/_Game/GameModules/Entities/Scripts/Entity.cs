using System;
using _Game.GameModules.Abilities.Scripts;
using _Game.GameModules.Ammunition.Scripts;
using _Game.GameModules.Characters.Scripts;
using _Game.GameModules.Entities.Scripts.Behaviours;
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

        #endregion

        #region events

        public HitReceiveEvent hitReceived;
        public AbilityUseEvent startAbilityAnimation;
        public UnityEvent endAbilityAnimation;

        #endregion

        #region Parameters

        [SerializeField] Character _character;
        [SerializeField] string _floorName = "Wood Floor";
        float stoppingDistance = 1;
        float _inputSpeed;

        public Character Character => _character;

        public float StoppingDistance => stoppingDistance;

        public bool UsingAbility { get; set; }

        public bool AutoMove { get; set; }
        public Quaternion Direction { get; set; }
        public Quaternion LookDiretion { get; set; }
        public Vector3 Destination { get; set; }
        public bool Aim { get; private set; }
        public float CharacterSpeed { get; private set; }

        public bool Hittable
        {
            get => hitDetectionCollider.enabled;
            set => hitDetectionCollider.enabled = value;
        }

        public float Speed => CharacterSpeed * InputSpeed;

        public string FloorName
        {
            get => _floorName;
            set => _floorName = value;
        }

        public float InputSpeed
        {
            get => _inputSpeed;
            set => _inputSpeed = value;
        }

        public bool CombatMode
        {
            set
            {
                mesh.EquipWeapons = value;
                animator.SetLayerWeight(1, value ? 1f : 0f);
            }
        }

        public bool Alive
        {
            get => animator.GetBool("Vivo");
            set { animator.SetBool("Vivo", false); }
        }

        #endregion

        #region Methods

        public void StopWalking()
        {
            //movement.ApplyInputMovement = false;
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
            movement.ApplyAnimationRootMovement = combo.ApplyRootMovement;
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
            particle.Play("Blood");
            sound.PlayHitSound(abilityHit.type);
        }

        void DetectFloorName(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Floor")) FloorName = other.tag;
        }

        void ExecuteCommand(Object obj)
        {
            if (obj is EntityCommand command) command.Execute(this);
            else Debug.Log("Obj Is not a Command");
        }

        #endregion

        #region Callbacks

        public void OnWeaponChange(Weapon weapon)
        {
            if (!weapon) return;
            animator.runtimeAnimatorController = weapon.animatorController;
            mesh.SwitchWeapon(weapon.Data.Prefabs);
            particle.InstantiateAbilityEffects(weapon.Abilities.ToArray());
            sound.InstantiateAbilitySFX(weapon.Abilities.ToArray());
            animator.Rebind();
            foreach (var entityBehaviour in animator.GetBehaviours<EntityBehaviour>()) entityBehaviour.entity = this;
            animator.Update(0f);
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

        void Start()
        {
            foreach (var entityBehaviour in animator.GetBehaviours<EntityBehaviour>()) entityBehaviour.entity = this;
        }

        void Update()
        {
            animator.SetFloat("Velocidade", InputSpeed);
        }

        #endregion
    }

    [Serializable]
    public class HitReceiveEvent : UnityEvent<AbilityHit> { }

    [Serializable]
    public class AbilityUseEvent : UnityEvent<int> { }
}
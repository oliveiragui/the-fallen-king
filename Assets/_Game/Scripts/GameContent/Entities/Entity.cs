using System;
using System.Collections;
using System.Collections.Generic;
using _Game.Scripts.Components.CombatSystem;
using _Game.Scripts.GameContent.Abilities;
using _Game.Scripts.GameContent.Abilities.Data;
using _Game.Scripts.GameContent.Ammunition;
using _Game.Scripts.GameContent.Characters;
using _Game.Scripts.GameContent.Entities.Components.Animation;
using _Game.Scripts.GameContent.Entities.Components.Audio;
using _Game.Scripts.GameContent.Entities.Components.Mesh;
using _Game.Scripts.GameContent.Entities.Components.Particle;
using _Game.Scripts.GameContent.Entities.Components.PhysicsSystem;
using _Game.Scripts.GameContent.Weapons;
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

        [SerializeField] public Character associatedCharacter;
        public bool autoMove;
        public float speed;
        public Quaternion moveDiretion;
        public Quaternion lookDiretion;
        public float stoppingDistance;
        public Vector3 destination;
        public Weapon weapon;
        public Ability ability;
        public bool conjuring;
        public bool usingAbility;
        List<Ability> abilities;
        public bool inCombat;
        bool ready;

        void OnActivate(Entity entity)
        {
            associatedCharacter.events.onEntitySummon.Invoke(entity);
            associatedCharacter.Weapons.onWeaponChange.AddListener(OnWeaponChange);
        }

        void OnDeactivated(Entity entity)
        {
            associatedCharacter.events.onEntityDimmissed.Invoke(entity);
            associatedCharacter.Weapons.onWeaponChange.RemoveListener(EquipWeapon);
        }

        void OnWeaponChange(Weapon weapon)
        {
            if (weapon != null)
            {
                abilities = weapon.Abilities;
                EquipWeapon(weapon);
            }
            else
            {
                abilities = null;
            }
        }

        #region Unity Functions

        void Start()
        {
            ready = true;
            OnActivate(this);
            OnWeaponChange(associatedCharacter.Weapons.WeaponInUse);
            events.onHitReceived.AddListener(ReceiveHit);
        }

        void OnEnable()
        {
            if (ready) OnActivate(this);
        }

        void OnDisable()
        {
            OnDeactivated(this);
        }

        void OnAnimatorMove()
        {
            if (usingAbility) movement.Speed = (animator.deltaPosition / Time.deltaTime).magnitude;
        }

        void OnTriggerEnter(Collider other)
        {
            var body = other.attachedRigidbody;
            if (!body || !other.CompareTag("Bullet")) return;
            if (!body.transform.TryGetComponent(out Ammo ammo)) return;
            if (Equals(ammo.AbilityHit.origin)) return;
            if (associatedCharacter.Team.PlayerFriend == ammo.AbilityHit.origin.Team.PlayerFriend) return;
            ammo.Hit(this);
        }

        #endregion

        #region Parameter Setters

        public void TrocaController(Weapon weapon)
        {
            //animator.Rebind();
            //animator.Update(0);
            animator.runtimeAnimatorController = weapon.Data.AnimatorController;
            this.weapon = weapon;
        }

        #region Moviment

        public void Move()
        {
            animator.SetBool("Anda", true);
        }

        public void Stop()
        {
            animator.SetBool("Anda", false);
        }

        #endregion

        public void Die()
        {
            animator.SetTrigger(AnimatorParams.Morre);
        }

        public void CombatMode(bool value)
        {
            animator.SetLayerWeight(1, value ? 1f : 0f);
        }

        #region Ability
        
        public void UseAbility(int abilityIndex)
        {
            if (!CanUseAbility(abilities[abilityIndex].Data)) return;

            ability = abilities[abilityIndex];

            animator.SetInteger(AnimatorParams.HabilidadeID, ability.Data.AnimationId);
            animator.SetTrigger(AnimatorParams.UsaHabilidade);

            if (conjuring == false)
            {
                conjuring = true;
                animator.SetBool(AnimatorParams.Conjura, true);
            }
        }

        public void StopCasting(int index)
        {
            if (!usingAbility || abilities[index].Data.AnimationId != ability.Data.AnimationId) return;
            animator.SetBool(AnimatorParams.Conjura, false);
            conjuring = false;
        }

        bool CanUseAbility(AbilityData ability) =>
            !usingAbility || (this.ability.Data.AnimationId != ability.AnimationId
                              && !ability.CanInterrupt(this.ability.Data));

        public void StartAbility()
        {
            usingAbility = true;
            animator.SetInteger(AnimatorParams.ComboMaximo, ability.Data.MaxCombo);
            animator.SetBool(AnimatorParams.Conjuravel, ability.Combo.Castable);
            animator.SetFloat(AnimatorParams.ComboFactor1, ability.Combo.Factor1 * 1);

            if (!ability.Combo.Castable) return;
            animator.SetFloat(AnimatorParams.ComboFactor2, ability.Combo.Factor2 * 1);
            animator.SetFloat(AnimatorParams.ComboFactor3, ability.Combo.Factor3 * 1);
        }

        public void FinishAbility()
        {
            usingAbility = false;
        }
        
        #endregion

        void ReceiveHit(AbilityHit abilityHit)
        {
            associatedCharacter.Status.Life.ApplyDamage(abilityHit.power);
            Debug.Log("aa");
            StartHit(abilityHit.impact);
        }

        void StartHit(HitImpact impact)
        {
            animator.SetBool(AnimatorParams.ReceivingHit, true);
            animator.SetInteger(AnimatorParams.HitImpact, (int) impact + 2);
            //TODO: Remover esse +2;
            FinishAbility();
            movement.Stop();
        }

        public void StopHit()
        {
            animator.SetBool(AnimatorParams.ReceivingHit, false);
        }

        public void EntraEmCombate()
        {
            animator.SetTrigger(AnimatorParams.EnCombate);
        }

        #endregion

        public void PlayFootStepSound() => entityAudio.Play(collision.FloorName);

        public void TurnToLookDirection() => movement.Rotation = lookDiretion;

        public void EquipWeapon(Weapon weapon)
        {
            TrocaController(weapon);
            mesh.SwitchWeapon(weapon.Data.Prefabs);
            mesh.InCombat = true;
        }

        public void ApontaEnquantoConjura()
        {
            IEnumerator Aponta() => new WaitWhile(() =>
            {
                movement.Rotation = lookDiretion;
                return conjuring;
            });

            StartCoroutine(Aponta());
        }

        void InvocaFlecha()
        {
            var tr = transform;
            var position = tr.position + new Vector3(0, 1, 0);
            var hit = new AbilityHit(-2, Vector3.zero, associatedCharacter);
            var force = transform.forward.normalized * 800;

            weapon
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
                if (otherEntity.associatedCharacter.Equals(associatedCharacter)) continue;
                if (otherEntity.associatedCharacter.Team.PlayerFriend ==
                    associatedCharacter.Team.PlayerFriend) continue;
                otherEntity.events.onHitReceived.Invoke(new AbilityHit(-2, Vector3.zero,
                    associatedCharacter));
            }
        }
    }

    [Serializable]
    public class EntityEvents
    {
        [SerializeField] public HitReceiveEvent onHitReceived;
    }

    [Serializable]
    public class HitReceiveEvent : UnityEvent<AbilityHit> { }
}
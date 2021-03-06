using System;
using System.Collections;
using Abilities;
using Abilities.Collections.Habilidades;
using Ammo;
using Characters;
using CombatSystem;
using Entities.Animation;
using Entities.Animation.Systems;
using Entities.Audio;
using Entities.Mesh;
using Entities.Movement;
using Entities.Particle;
using Entities.PhysicsSystem;
using UnityEngine;
using Utils.Extension;
using Weapons;

namespace Entities
{
    public class Entity : MonoBehaviour
    {
        [SerializeField] new EntityAudio audio;
        [SerializeField] EntityMesh mesh;
        [SerializeField] EntityParticle particle;
        [SerializeField] public EntityCollision collision; // TODO: RETIRAR PÚBLICO
        [SerializeField] public EntityMovement movement; // TODO: RETIRAR PÚBLICO
        [SerializeField] public Character associatedCharacter;
        [SerializeField] EntityAnimations animations;

        public MoveParams playerMoveParams;
        public MoveParams hitParams;
        public MoveParams AbilityParams;

        public AbilityData currentAbility;
        public bool IsUsingAbility => !(currentAbility is null);
        public bool IsUsingCombo { get; set; }

        public bool IsReceivingImpact => false;

        bool _inCombat;

        public bool InCombat
        {
            get => _inCombat;
            set
            {
                _inCombat = value;
                mesh.InCombat = value;
                if (value) animations.combat.EquipWeapon();
                else animations.combat.UnequipWeapon();
            }
        }

        public void EquipaArma(WeaponData weapon)
        {
            animations.baseBaseAnimation.TrocaController(weapon.AnimatorController);
            mesh.SwitchWeapon(weapon);
            currentAbility = null;
        }

        public void ProximoCombo(AbilityCombo combo, float attackSpeed)
        {
            animations.abilities.SetupCombo(combo.Castable, combo.Factor1, combo.Factor2, combo.Factor3, attackSpeed);
        }

        public void UsaHabilidade(AbilityData ability)
        {
            if (!IsUsingAbility || (IsUsingAbility &&
                                    currentAbility.Id != ability.Id &&
                                    ability.CanInterrupt(currentAbility)))
            {
                animations.abilities.SetupAbility(ability.id, ability.combo.Length,
                    ability.cooldown.CalcFactor(associatedCharacter.Status.AttackSpeed.Current));
                currentAbility = ability;
            }

            if (currentAbility.Id != ability.Id) return;
            Conjurando = true;
            animations.abilities.Use();
            animations.abilities.EntraEmCombate();
            InCombat = true;
        }

        bool Conjurando { get; set; }

        public void ParaDeConjurar(int abilityID)
        {
            if (!currentAbility) return;
            Conjurando = false;
            if (currentAbility.Id == abilityID) animations.abilities.StopCasting();
        }

        public void ApontaEnquantoConjura()
        {
            IEnumerator Aponta()
            {
                return new WaitWhile(() =>
                {
                    transform.LookAt(FindObjectOfType<Camera>().MouseOnPlane());
                    return Conjurando;
                });
            }

            StartCoroutine(Aponta());
        }

        void Move(float speed, float direction)
        {
            //if (speed > 0.1) 
            movement.Direction = direction;
            animations.baseBaseAnimation.Run(speed);
            movement.Speed = speed;
            movement.Move();
        }

        void FixedUpdate()
        {
            // if (IsReceivingImpact) Move(hitParams.speed, hitParams.direction);
            // else if (IsUsingCombo) Move(AbilityParams.speed, AbilityParams.direction);
            // else Move(playerMoveParams.speed, playerMoveParams.direction);
            if (!IsReceivingImpact && !IsUsingCombo) Move(playerMoveParams.speed, playerMoveParams.direction);
        }

        public void MovimentaAte(Vector3 position)
        {
            movement.MoveTo(position);
        }

        public void ParaDeAndar()
        {
            animations.baseBaseAnimation.StopRun();
            movement.Stop();
        }

        public void ReceiveHit(AbilityHit abilityHit)
        {
            associatedCharacter.Status.Life.ApplyDamage(abilityHit.power);
            // outroAvatar.Particulas.TocaParticulasDeSangue();
            // Avatar.Audio.TocaSom(SlotSom.GolpeDeEspada);
        }

        void InvocaFlecha()
        {
            AmmoStorage
                .Arrow(transform.position + new Vector3(0, 1, 0), transform.rotation)
                .Setup(new AbilityHit(-2, Vector3.zero, associatedCharacter.Team), associatedCharacter,
                    transform.forward.normalized * 800);
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
                otherEntity.ReceiveHit(new AbilityHit(-2, Vector3.zero, associatedCharacter.Team));
            }
        }
    }

    [Serializable]
    public struct MoveParams
    {
        public float speed;
        public float direction;
        public float lookDiretion;
        public float stoppingDistance;

        public MoveParams(float speed, float direction, float lookDiretion, float stoppingDistance)
        {
            this.speed = speed;
            this.direction = direction;
            this.lookDiretion = lookDiretion;
            this.stoppingDistance = stoppingDistance;
        }
    }

    [Serializable]
    public class EntityAnimations
    {
        public EntityBaseAnimation baseBaseAnimation;
        public EntityAbilityAnimation abilities;
        public EntityCombatAnimation combat;
    }
}
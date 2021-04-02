using System.Collections;
using _Game.Scripts.Abilities;
using _Game.Scripts.Ammo;
using _Game.Scripts.CombatSystem;
using _Game.Scripts.Weapons;
using UnityEngine;

namespace _Game.Scripts.Entities
{
    public class EntityCommands : MonoBehaviour
    {
        public Entity entity;

        #region Movement Commands

        public void Move()
        {
            entity.movement.Speed = entity.data.speed;
            entity.movement.Direction = entity.data.direction;
            //entity.data.animations.Run(entity.data.speed);
            if (!entity.movement.AutoMovement) entity.movement.Move();
        }

        public void MoveTo(Vector3 position)
        {
            entity.movement.Speed = entity.data.speed;
            entity.movement.StoppingDistance = entity.data.stoppingDistance;
            entity.animations.Run(entity.data.speed);
            entity.movement.MoveTo(position);
        }

        public void StopMove()
        {
            entity.animations.StopRun();
            entity.movement.Stop();
        }

        public void TurnToLookDirection()
        {
            entity.transform.rotation = Quaternion.Euler(0, entity.data.lookDiretion, 0);
        }

        public void PlayFootStepSound()
        {
            string floorName = entity.collision.FloorName;
            if (floorName is null || floorName.Length < 1) return;
            entity.entityAudio.Play(floorName);
        }

        #endregion

        #region Ability Commands

        public void SetupAbility()
        {
            entity.data.UsingAbility = true;
            entity.animations.SetupAbility(entity.data.ability.AnimationId,
                entity.data.ability.MaxCombo,
                entity.data.ability.Cooldown
                    .CalcFactor(entity.data.associatedCharacter.Status.AttackSpeed.Current)
            );
        }

        public void FinishAbility()
        {
            entity.data.UsingAbility = false;
            //entity.data.ability = null;

            // if (entity.defaultData.CurrentCombo.action != null)
            //     entity.defaultData.defaultData.actionMachine.ExitAction();
        }

        public void SetupCombo(int comboId)
        {
            var combo = entity.data.ability.Combo[comboId];
            entity.animations.SetupCombo(combo.Castable, combo.Factor1, combo.Factor2, combo.Factor3, 1);
            CombatMode(true);
            entity.data.UsingCombo = true;
            entity.animations.EntraEmCombate();
            // ParaDeAndar();
        }

        public void FinishCombo()
        {
            entity.animations.StopCombo();
            entity.data.UsingCombo = false;
        }

        //TODO: Arrumar 
        public void UseAbility(Ability ability)
        {
            if (
                entity.data.UsingCombo && entity.data.ability.AnimationId != ability.AnimationId &&
                !ability.CanInterrupt(entity.data.ability)
            ) return;
            entity.data.ability = ability;
            entity.data.conjuring = true;
            entity.animations.UseAbility(ability.AnimationId);
            // if (entity.defaultData.CurrentCombo.action != null)
            //     entity.defaultData.defaultData.actionMachine.EnterAction(entity.defaultData.defaultData, entity.CurrentCombo.action);
        }

        public void StopConjuring(Ability ability)
        {
            if (!entity.data.UsingCombo || entity.data.ability.AnimationId != ability.AnimationId) return;
            entity.animations.StopCasting();
            entity.data.conjuring = false;
        }

        #endregion

        #region Combat Commands

        //TODO: Verificar a qual entidade pertence
        public void EquipWeapon(Weapon weapon)
        {
            entity.animations.TrocaController(weapon.AnimatorController);
            entity.mesh.SwitchWeapon(weapon.Prefabs);
            entity.data.weapon = weapon;
            //entity.data.abilities = weapon.Abilities;
        }

        public void CombatMode(bool value)
        {
            entity.data.inCombat = value;
            entity.mesh.InCombat = value;
            entity.animations.UseWeapon(value);
        }

        public void ReceiveHit(AbilityHit abilityHit)
        {
            entity.data.associatedCharacter.Status.Life.ApplyDamage(abilityHit.power);
            entity.animations.ReceiveHit(abilityHit.impact);
            // outroAvatar.Particulas.TocaParticulasDeSangue();
            // Avatar.Audio.TocaSom(SlotSom.GolpeDeEspada);
        }

        #endregion

        #region To Refatory

        public void ApontaEnquantoConjura()
        {
            IEnumerator Aponta()
            {
                return new WaitWhile(() =>
                {
                    entity.transform.rotation = Quaternion.Euler(0, entity.data.lookDiretion, 0);
                    return entity.data.conjuring;
                });
            }

            StartCoroutine(Aponta());
        }

        void InvocaFlecha()
        {
            var tr = transform;
            var position = tr.position + new Vector3(0, 1, 0);
            var hit = new AbilityHit(-2, Vector3.zero, entity.data.associatedCharacter);
            var force = transform.forward.normalized * 800;

            entity.data.weapon
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
                if (!hit.collider.transform.TryGetComponent(out Hittable hittable)) continue;
                if (!hittable.entity.collision.Hittable) continue;
                if (hittable.entity.data.associatedCharacter.Equals(entity.data.associatedCharacter)
                ) continue;
                if (hittable.entity.data.associatedCharacter.Team.PlayerFriend ==
                    entity.data.associatedCharacter.Team.PlayerFriend) continue;
                hittable.entity.events.onHitReceived.Invoke(new AbilityHit(-2, Vector3.zero,
                    entity.data.associatedCharacter));
            }
        }

        # endregion
    }
}
using System.Collections;
using Abilities;
using Ammo;
using CombatSystem;
using UnityEngine;
using Utils.Extension;
using Weapons;

namespace Entities.Combat
{
    public class CombatCommands : MonoBehaviour
    {
        public CombatEntity entity;

        #region Ability Commands

        public void SetupAbility(int id)
        {
            entity.combatData.UsingAbility = true;
            entity.combatData.CurrentAbility = entity.combatData.abilities[id - 1];
            entity.combatData.animations.SetupAbility(id,
                entity.combatData.CurrentAbility.MaxCombo,
                entity.combatData.CurrentAbility.Cooldown
                    .CalcFactor(entity.defaultData.associatedCharacter.Status.AttackSpeed.Current)
            );
        }

        public void FinishAbility()
        {
            entity.combatData.CurrentAbility = null;
            entity.combatData.UsingAbility = false;
            // if (entity.combatData.CurrentCombo.action != null)
            //     entity.combatData.defaultData.actionMachine.ExitAction();
        }

        public void SetupCombo(int comboId)
        {
            var combo = entity.combatData.CurrentAbility.Combo[comboId];
            entity.combatData.currentComboData = combo;
            entity.combatData.animations.SetupCombo(combo.Castable, combo.Factor1, combo.Factor2, combo.Factor3, 1);
            CombatMode(true);
            entity.combatData.UsingCombo = true;
            entity.combatData.animations.EntraEmCombate();
            // ParaDeAndar();
        }

        public void FinishCombo()
        {
            entity.combatData.animations.StopCombo();
            entity.combatData.currentComboData = null;
            entity.combatData.UsingCombo = false;
        }

        //TODO: Arrumar 
        public void UseAbility(Ability ability)
        {
            if (entity.combatData.UsingCombo
                && entity.combatData.CurrentAbility.Id != ability.Id
                && !ability.CanInterrupt(entity.combatData.CurrentAbility)
            ) return;
            entity.combatData.conjuring = true;
            entity.combatData.animations.Use(ability.Id);
            // if (entity.combatData.CurrentCombo.action != null)
            //     entity.combatData.defaultData.actionMachine.EnterAction(entity.combatData.defaultData, entity.CurrentCombo.action);
        }

        public void StopConjuring(int abilityID)
        {
            if (!entity.combatData.UsingCombo || entity.combatData.CurrentAbility.Id != abilityID) return;
            entity.combatData.animations.StopCasting();
            entity.combatData.conjuring = false;
        }

        #endregion

        #region Combat Commands

        //TODO: Verificar a qual entidade pertence
        public void EquipWeapon(Weapon weapon)
        {
            entity.defaultData.animations.TrocaController(weapon.AnimatorController);
            entity.components.mesh.SwitchWeapon(weapon.Prefabs);
            entity.combatData.abilities = weapon.Abilities;
        }

        public void CombatMode(bool value)
        {
            entity.combatData.inCombat = value;
            entity.components.mesh.InCombat = value;
            entity.combatData.animations.UseWeapon(value);
        }

        public void ReceiveHit(AbilityHit abilityHit)
        {
            entity.defaultData.associatedCharacter.Status.Life.ApplyDamage(abilityHit.power);
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

                    entity.transform.rotation = Quaternion.Euler(0, entity.defaultData.lookDiretion, 0);
                    return entity.combatData.conjuring;
                });
            }

            StartCoroutine(Aponta());
        }

        void InvocaFlecha()
        {
            AmmoStorage
                .Arrow(transform.position + new Vector3(0, 1, 0), transform.rotation)
                .Setup(new AbilityHit(-2, Vector3.zero, entity.defaultData.associatedCharacter.Team),
                    entity.defaultData.associatedCharacter,
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
                if (!hit.collider.attachedRigidbody.transform.TryGetComponent(out CombatEntity otherEntity)) continue;
                if (!otherEntity.components.collision.Hittable) continue;
                if (otherEntity.defaultData.associatedCharacter.Equals(entity.defaultData.associatedCharacter))
                    continue;
                otherEntity.events.onHitReceived.Invoke(new AbilityHit(-2, Vector3.zero,
                    entity.defaultData.associatedCharacter.Team));
            }
        }

        # endregion
    }
}
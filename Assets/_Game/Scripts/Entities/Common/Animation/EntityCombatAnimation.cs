using System;
using CombatSystem;
using UnityEngine;

namespace Entities.Common.Animation
{
    [Serializable]
    public class EntityCombatAnimation
    {
        [SerializeField] Animator animator;

        public void UseWeapon(bool value)
        {
            animator.SetLayerWeight(1, value ? 1f : 0f);
        }

        void SwitchLayer(int habilidadeID)
        {
            animator.SetLayerWeight(2, 0f);
            animator.SetLayerWeight(3, 0f);
            animator.SetLayerWeight(4, 0f);
            animator.SetLayerWeight(5, 0f);
            animator.SetLayerWeight(habilidadeID + 1, 1f);
        }

        public void SetupAbility(int abilityId, int maxCombo, float cooldown)
        {
            animator.SetInteger(EntityAnimationParameters.HabilidadeID, abilityId);
            animator.SetInteger(EntityAnimationParameters.ComboMaximo, maxCombo);
            animator.SetFloat(EntityAnimationParameters.Cooldown[abilityId - 1], cooldown);
            SwitchLayer(abilityId);
        }

        public void SetupCombo(bool castable, float factor1, float factor2, float factor3, float attackSpeed)
        {
            animator.SetBool(EntityAnimationParameters.Conjuravel, castable);
            animator.SetBool(EntityAnimationParameters.UsingCombo, true);
            animator.SetFloat(EntityAnimationParameters.ComboFactor1, factor1 * attackSpeed);
            animator.SetFloat(EntityAnimationParameters.ComboFactor2, factor2 * attackSpeed);
            animator.SetFloat(EntityAnimationParameters.ComboFactor3, factor3 * attackSpeed);
        }

        public void StopCombo()
        {
            animator.SetBool(EntityAnimationParameters.UsingCombo, false);
        }

        public void ReceiveHit(HitImpact impact)
        {
            animator.SetTrigger(EntityAnimationParameters.ReceiveHit);
            animator.SetBool(EntityAnimationParameters.ReceivingHit, true);
            animator.SetInteger(EntityAnimationParameters.HitImpact, (int) impact);
        }

        public void StopHit()
        {
            animator.SetBool(EntityAnimationParameters.ReceivingHit, false);
        }

        public void Use(int abilityId)
        {
            animator.SetInteger(EntityAnimationParameters.HabilidadeID, abilityId);
            animator.SetTrigger(EntityAnimationParameters.UsaHabilidade);
            if (animator.GetBool(EntityAnimationParameters.Conjuravel))
                animator.SetBool(EntityAnimationParameters.Conjura, true);
        }

        public void StopCasting()
        {
            animator.SetBool(EntityAnimationParameters.Conjura, false);
        }

        public void EntraEmCombate()
        {
            animator.SetTrigger(EntityAnimationParameters.EnCombate);
        }
    }
}
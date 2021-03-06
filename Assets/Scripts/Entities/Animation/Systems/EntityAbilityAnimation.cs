using System;
using UnityEngine;

namespace Entities.Animation.Systems
{
    [Serializable]
    public class EntityAbilityAnimation : MonoBehaviour
    {
        [SerializeField] Animator animator;
        
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
            animator.SetFloat(EntityAnimationParameters.ComboFactor1, factor1 * attackSpeed);
            animator.SetFloat(EntityAnimationParameters.ComboFactor2, factor2 * attackSpeed);
            animator.SetFloat(EntityAnimationParameters.ComboFactor3, factor3 * attackSpeed);
        }

        public void Use()
        {
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
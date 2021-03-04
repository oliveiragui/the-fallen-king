using System;
using Entities.Animation.Systems;
using UnityEngine;

namespace Entities.Animation
{
    [Serializable]
    public class EntityAnimation
    {
        [SerializeField] Animator animator;
        [SerializeField] AbilityAnimation ability;

        public EntityAnimation(Animator animator)
        {
            this.animator = animator;
            ability = new AbilityAnimation(animator);
        }

        public AbilityAnimation Ability => ability;

        public void TrocaController(AnimatorOverrideController controller)
        {
            animator.Rebind();
            animator.Update(0);
            animator.runtimeAnimatorController = controller;
        }

        public void Run(float velocidade)
        {
            animator.SetFloat(EntityAnimationParameters.Velocidade, velocidade);
        }

        public void StopRun()
        {
            animator.SetFloat(EntityAnimationParameters.Velocidade, 0);
        }

        public void Die()
        {
            animator.SetTrigger(EntityAnimationParameters.Morre);
        }

        public void EquipWeapon()
        {
            animator.SetLayerWeight(1, 1f);
        }

        public void UnequipWeapon()
        {
            animator.SetLayerWeight(1, 0f);
        }
    }
}
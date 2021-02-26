using System;
using Collection.Abilities.Collections.Habilidades;
using UnityEngine;

namespace Collection.Entities.Animation
{
    [Serializable]
    public class EntityAnimation
    {
        public AbilityAnimation Ability => ability;

        [SerializeField] Animator animator;
        [SerializeField] AbilityAnimation ability;

        public EntityAnimation(Animator animator)
        {
            this.animator = animator;
            ability = new AbilityAnimation(animator);
        }

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
            animator.SetTrigger(EntityAnimationParameters.SacaArma);
            animator.SetLayerWeight(1,1f);
        }

        public void UnequipWeapon()
        {
            animator.SetTrigger(EntityAnimationParameters.GuardaArma);
            animator.SetLayerWeight(1,0f);
        }
    }
}
using System;
using UnityEngine;

namespace Entities.Common.Animation
{
    [Serializable]
    public class EntityBaseAnimation
    {
        [SerializeField] Animator animator;

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
    }
}
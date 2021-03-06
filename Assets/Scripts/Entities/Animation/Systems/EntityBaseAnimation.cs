using System;
using Entities.Animation.Systems;
using UnityEngine;

namespace Entities.Animation
{
    [Serializable]
    public class EntityBaseAnimation : MonoBehaviour
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
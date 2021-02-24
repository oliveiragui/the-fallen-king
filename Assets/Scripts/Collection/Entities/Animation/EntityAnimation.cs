using System;
using UnityEngine;

namespace Collection.Entities.Animation
{
    [Serializable]
    public class EntityAnimation
    {
        [SerializeField] Animator animator;

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

        public void UsaHabilidade(int habilidadeID, bool conjuravel, int combo)
        {
            animator.SetLayerWeight(0,0f);
            animator.SetLayerWeight(2,1f);
            animator.SetTrigger(EntityAnimationParameters.UsaHabilidade);
            animator.SetInteger(EntityAnimationParameters.HabilidadeID, habilidadeID);
            animator.SetBool(EntityAnimationParameters.Conjuravel, conjuravel);
            animator.SetInteger(EntityAnimationParameters.ComboAtual, combo);
        }

        public void ParaDeConjurar()
        {
            animator.SetTrigger(EntityAnimationParameters.ParaDeConjurar);
        }

        public void EquipWeapon()
        {
            animator.SetTrigger(EntityAnimationParameters.SacaArma);
        }

        public void UnequipWeapon()
        {
            animator.SetTrigger(EntityAnimationParameters.GuardaArma);
        }
    }
}
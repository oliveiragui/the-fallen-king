using UnityEngine;

namespace _Game.Scripts.GameContent.Entities.Components.Animation
{
    public class AnimatorParams
    {
        public static readonly int Velocidade = Animator.StringToHash("Velocidade");

        // Hit -------------------------
        public static readonly int ReceivingHit = Animator.StringToHash("Recebendo Hit");
        public static readonly int HitImpact = Animator.StringToHash("Impacto do Hit");

        // HABILIDADE --------------------------
        public static readonly int HabilidadeID = Animator.StringToHash("ID habilidade");
        public static readonly int Conjuravel = Animator.StringToHash("Habilidade conjuravel");
        public static readonly int UsaHabilidade = Animator.StringToHash("Usa habilidade");
        public static readonly int ComboMaximo = Animator.StringToHash("Combo Maximo");
        public static readonly int Conjura = Animator.StringToHash("Conjurando");

        public static readonly int EnCombate = Animator.StringToHash("Em combate");

        public static readonly int ComboFactor1 = Animator.StringToHash("Ability Factor 1");
        public static readonly int ComboFactor2 = Animator.StringToHash("Ability Factor 2");
        public static readonly int ComboFactor3 = Animator.StringToHash("Ability Factor 3");

        // DANO ---------------------------------------------
        public static readonly int Morre = Animator.StringToHash("Morre");

        // ARMA ---------------------------------------------
    }
}
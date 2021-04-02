using UnityEngine;

namespace _Game.Scripts.Entities.Components.Animation
{
    public static class EntityAnimationParameters
    {
        public static readonly int Velocidade = Animator.StringToHash("Velocidade");
        // Hit -------------------------
        public static readonly int ReceiveHit = Animator.StringToHash("Recebe Hit");
        public static readonly int ReceivingHit = Animator.StringToHash("Recebendo Hit");
        public static readonly int HitImpact = Animator.StringToHash("Impacto do Hit");
        // HABILIDADE --------------------------
        public static readonly int HabilidadeID = Animator.StringToHash("ID habilidade");
        public static readonly int Conjuravel = Animator.StringToHash("Habilidade conjuravel");
        public static readonly int UsaHabilidade = Animator.StringToHash("Usa habilidade");
        public static readonly int VelocidadeDaHabilidade = Animator.StringToHash("Velocidade da habilidade");
        public static readonly int ComboMaximo = Animator.StringToHash("Combo Maximo");
        public static readonly int Conjura = Animator.StringToHash("Conjurando");

        public static readonly int EnCombate = Animator.StringToHash("Em combate");

        public static readonly int ComboFactor1 = Animator.StringToHash("Ability Factor 1");
        public static readonly int ComboFactor2 = Animator.StringToHash("Ability Factor 2");
        public static readonly int ComboFactor3 = Animator.StringToHash("Ability Factor 3");
        public static readonly int UsingCombo = Animator.StringToHash("Using Combo");

        public static readonly int[] Cooldown =
        {
            Animator.StringToHash("Cooldown 1"),
            Animator.StringToHash("Cooldown 2"),
            Animator.StringToHash("Cooldown 3"),
            Animator.StringToHash("Cooldown 4")
        };

        // DANO ---------------------------------------------
        public static readonly int Morre = Animator.StringToHash("Morre");

        // ARMA ---------------------------------------------
        public static readonly int GuardaArma = Animator.StringToHash("GuardaArma");
        public static readonly int SacaArma = Animator.StringToHash("SacaArma");
        public static readonly int CorreArmado = Animator.StringToHash("CorreArmado");
    }
}
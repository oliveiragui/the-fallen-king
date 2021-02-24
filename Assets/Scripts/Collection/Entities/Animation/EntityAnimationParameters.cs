using UnityEngine;

namespace Collection.Entities.Animation
{
    public static class EntityAnimationParameters
    {
        public static readonly int Velocidade = Animator.StringToHash("Velocidade");

        // HABILIDADE --------------------------
        public static readonly int HabilidadeID = Animator.StringToHash("ID habilidade");
        public static readonly int Conjuravel = Animator.StringToHash("Habilidade conjuravel");
        public static readonly int UsaHabilidade = Animator.StringToHash("Usa habilidade");
        public static readonly int VelocidadeDaHabilidade = Animator.StringToHash("Velocidade da habilidade");
        public static readonly int ComboAtual = Animator.StringToHash("Combo atual");
        public static readonly int ParaDeConjurar = Animator.StringToHash("Para de conjurar habilidade");

        // DANO ---------------------------------------------
        public static readonly int Morre = Animator.StringToHash("Morre");

        // ARMA ---------------------------------------------
        public static readonly int GuardaArma = Animator.StringToHash("GuardaArma");
        public static readonly int SacaArma = Animator.StringToHash("SacaArma");
        public static readonly int CorreArmado = Animator.StringToHash("CorreArmado");
    }
}
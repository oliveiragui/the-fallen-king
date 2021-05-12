using UnityEngine;

namespace _Game.GameModules.Entities.Scripts
{
    public class AnimatorParams
    {
        public static readonly int Velocidade = Animator.StringToHash("Velocidade");

        // Hit -------------------------
        public static readonly int ReceivingHit = Animator.StringToHash("Recebendo Hit");
        public static readonly int HitImpact = Animator.StringToHash("Impacto do Hit");
        public static readonly int Die = Animator.StringToHash("Morre");

        public static readonly int RequestedAbilityID = Animator.StringToHash("Requested Ability ID");
        public static readonly int RequestAbility = Animator.StringToHash("Request Ability");

        public static readonly int ForceAbility = Animator.StringToHash("Force Ability");

        // COMBO --------------------------
        public static readonly int Castable = Animator.StringToHash("Habilidade conjuravel");
        public static readonly int ComboFactor1 = Animator.StringToHash("Ability Factor 1");
        public static readonly int ComboFactor2 = Animator.StringToHash("Ability Factor 2");

        public static readonly int ComboFactor3 = Animator.StringToHash("Ability Factor 3");
        // DANO ---------------------------------------------

        // ARMA ---------------------------------------------
    }
}
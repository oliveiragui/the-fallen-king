using UnityEngine;

namespace _Game.Scripts.GameContent.Entities
{
    public class AnimatorParams
    {
        public static readonly int Velocidade = Animator.StringToHash("Velocidade");

        // Hit -------------------------
        public static readonly int ReceivingHit = Animator.StringToHash("Recebendo Hit");
        public static readonly int HitImpact = Animator.StringToHash("Impacto do Hit");

        public static readonly int RequestedAbilityID = Animator.StringToHash("Requested Ability ID");
        public static readonly int RequestAbility = Animator.StringToHash("Request Ability");
        public static readonly int ForceAbility = Animator.StringToHash("Force Ability");

        public static readonly int Cast = Animator.StringToHash("Conjurando");
        public static readonly int CombatMode = Animator.StringToHash("Em combate");

        // HABILIDADE --------------------------
        public static readonly int MaxCombo = Animator.StringToHash("Combo Maximo");

        // COMBO --------------------------
        public static readonly int Castable = Animator.StringToHash("Habilidade conjuravel");
        public static readonly int ComboFactor1 = Animator.StringToHash("Ability Factor 1");
        public static readonly int ComboFactor2 = Animator.StringToHash("Ability Factor 2");
        public static readonly int ComboFactor3 = Animator.StringToHash("Ability Factor 3");

        // DANO ---------------------------------------------
        public static readonly int Die = Animator.StringToHash("Morre");
        // ARMA ---------------------------------------------
    }
}
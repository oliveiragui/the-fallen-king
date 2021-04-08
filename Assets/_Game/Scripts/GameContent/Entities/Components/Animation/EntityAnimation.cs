using System;
using _Game.Scripts.Components.CombatSystem;
using _Game.Scripts.GameContent.Abilities;
using _Game.Scripts.GameContent.Weapons;
using UnityEngine;

namespace _Game.Scripts.GameContent.Entities.Components.Animation
{
    [Serializable]
    public class EntityAnimation : MonoBehaviour
    {
        [SerializeField] Animator animator;

        #region Static Animation Parameters

        static readonly int Velocidade = Animator.StringToHash("Velocidade");

        // Hit -------------------------
        static readonly int ReceiveHit = Animator.StringToHash("Recebe Hit");
        static readonly int ReceivingHit = Animator.StringToHash("Recebendo Hit");
        static readonly int HitImpact = Animator.StringToHash("Impacto do Hit");

        // HABILIDADE --------------------------
        static readonly int HabilidadeID = Animator.StringToHash("ID habilidade");
        static readonly int Conjuravel = Animator.StringToHash("Habilidade conjuravel");
        static readonly int UsaHabilidade = Animator.StringToHash("Usa habilidade");
        static readonly int VelocidadeDaHabilidade = Animator.StringToHash("Velocidade da habilidade");
        static readonly int ComboMaximo = Animator.StringToHash("Combo Maximo");
        static readonly int Conjura = Animator.StringToHash("Conjurando");

        static readonly int EnCombate = Animator.StringToHash("Em combate");

        static readonly int ComboFactor1 = Animator.StringToHash("Ability Factor 1");
        static readonly int ComboFactor2 = Animator.StringToHash("Ability Factor 2");
        static readonly int ComboFactor3 = Animator.StringToHash("Ability Factor 3");
        static readonly int UsingCombo = Animator.StringToHash("Using Combo");

        static readonly int[] Cooldown =
        {
            Animator.StringToHash("Cooldown 1"),
            Animator.StringToHash("Cooldown 2"),
            Animator.StringToHash("Cooldown 3"),
            Animator.StringToHash("Cooldown 4")
        };

        // DANO ---------------------------------------------
        static readonly int Morre = Animator.StringToHash("Morre");

        // ARMA ---------------------------------------------
        static readonly int GuardaArma = Animator.StringToHash("GuardaArma");
        static readonly int SacaArma = Animator.StringToHash("SacaArma");
        static readonly int CorreArmado = Animator.StringToHash("CorreArmado");

        #endregion

        public Weapon weapon;
        public Ability ability;
        public bool conjuring;
        public bool usingAbility;
        public bool usingCombo;

        #region Parameter Setters

        public void TrocaController(Weapon weapon)
        {
            animator.Rebind();
            animator.Update(0);
            animator.runtimeAnimatorController = weapon.AnimatorController;
            this.weapon = weapon;
        }

        public void Run(float velocidade)
        {
            animator.SetFloat(Velocidade, velocidade);
        }

        public void StopRun()
        {
            animator.SetFloat(Velocidade, 0);
        }

        public void Die()
        {
            animator.SetTrigger(Morre);
        }

        public void UseWeapon(bool value)
        {
            animator.SetLayerWeight(1, value ? 1f : 0f);
        }

        void SwitchLayer(int habilidadeID)
        {
            animator.SetLayerWeight(2, 0f);
            animator.SetLayerWeight(3, 0f);
            animator.SetLayerWeight(4, 0f);
            animator.SetLayerWeight(5, 0f);
            animator.SetLayerWeight(habilidadeID + 2, 1f);
        }

        public void UseAbility(Ability ability)
        {
            if (usingCombo && this.ability.AnimationId != ability.AnimationId && !ability.CanInterrupt(this.ability)) return;
            this.ability = ability;
            conjuring = true;
            animator.SetInteger(HabilidadeID, ability.AnimationId);
            animator.SetTrigger(UsaHabilidade);
            if (animator.GetBool(Conjuravel))
                animator.SetBool(Conjura, true);
        }

        public void StopCasting(int id)
        {
            if (!usingCombo || ability.AnimationId != id) return;
            animator.SetBool(Conjura, false);
            conjuring = false;
        }

        public void SetupAbility(Entity entity)
        {
            usingAbility = true;
            animator.SetInteger(HabilidadeID, ability.AnimationId);
            animator.SetInteger(ComboMaximo, ability.MaxCombo);
            animator.SetFloat(Cooldown[ability.AnimationId],
                ability.Cooldown.CalcFactor(entity.associatedCharacter.Status.AttackSpeed.Current));
            SwitchLayer(ability.AnimationId);
        }

        public void FinishAbility()
        {
            usingAbility = false;
        }

        public void SetupCombo(int comboId)
        {
            var combo = ability.Combo[comboId];
            usingCombo = true;
            animator.SetBool(Conjuravel, combo.Castable);
            animator.SetBool(UsingCombo, true);
            animator.SetFloat(ComboFactor1, combo.Factor1 * 1);
            animator.SetFloat(ComboFactor2, combo.Factor2 * 1);
            animator.SetFloat(ComboFactor3, combo.Factor3 * 1);
            EntraEmCombate();
        }

        public void StopCombo()
        {
            animator.SetBool(UsingCombo, false);
            usingCombo = false;
        }

        public void StartHit(HitImpact impact)
        {
            animator.SetTrigger(ReceiveHit);
            animator.SetBool(ReceivingHit, true);
            animator.SetInteger(HitImpact, (int) impact);
        }

        public void StopHit()
        {
            animator.SetBool(ReceivingHit, false);
        }

        public void EntraEmCombate()
        {
            animator.SetTrigger(EnCombate);
        }

        #endregion
    }
}
using System;
using Abilities;
using Abilities.Collections.Habilidades;
using UnityEngine;

namespace Entities.Animation.Systems
{
    [Serializable]
    public class AbilityAnimation
    {
        [SerializeField] Animator _animator;

        public AbilityAnimation(Animator animator)
        {
            _animator = animator;
        }

        void SwitchLayer(int habilidadeID)
        {
            _animator.SetLayerWeight(2, 0f);
            _animator.SetLayerWeight(3, 0f);
            _animator.SetLayerWeight(4, 0f);
            _animator.SetLayerWeight(5, 0f);
            _animator.SetLayerWeight(habilidadeID + 1, 1f);
        }

        public void SetupAbility(AbilityData ability)
        {
            _animator.SetInteger(EntityAnimationParameters.HabilidadeID, ability.Id);
            _animator.SetInteger(EntityAnimationParameters.ComboMaximo, ability.Combo.Length);
            _animator.SetFloat(EntityAnimationParameters.Cooldown[ability.Id - 1],
                1 / ability.BaseAttributes.Cooldown);
            SetupCombo(ability.Combo[0], 1);
            SwitchLayer(ability.Id);
        }

        public void SetupCombo(AbilityCombo abilityCombo, float attackSpeed)
        {
            _animator.SetBool(EntityAnimationParameters.Conjuravel, abilityCombo.Castable);
            _animator.SetFloat(EntityAnimationParameters.ComboFactor1, abilityCombo.Factor1 * attackSpeed);
            _animator.SetFloat(EntityAnimationParameters.ComboFactor2, abilityCombo.Factor2 * attackSpeed);
            _animator.SetFloat(EntityAnimationParameters.ComboFactor3, abilityCombo.Factor3 * attackSpeed);
        }

        public void Use()
        {
            _animator.SetTrigger(EntityAnimationParameters.UsaHabilidade);
            if (_animator.GetBool(EntityAnimationParameters.Conjuravel))
                _animator.SetBool(EntityAnimationParameters.Conjura, true);
        }

        public void StopCasting()
        {
            _animator.SetBool(EntityAnimationParameters.Conjura, false);
        }

        public void EntraEmCombate()
        {
            _animator.SetTrigger(EntityAnimationParameters.EnCombate);
        }
    }
}
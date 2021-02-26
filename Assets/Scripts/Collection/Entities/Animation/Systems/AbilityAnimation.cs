using System;
using Collection.Abilities.Collections.Habilidades;
using UnityEngine;

namespace Collection.Entities.Animation
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

        public void SetupAbility(AbilityModel ability)
        {
            _animator.SetInteger(EntityAnimationParameters.HabilidadeID, ability.Info.Id);
            _animator.SetInteger(EntityAnimationParameters.ComboMaximo, ability.Attributes.MAXCombo);
            _animator.SetFloat(EntityAnimationParameters.Cooldown[ability.Info.Id - 1],
                1 / ability.Attributes.Cooldown);
            SwitchLayer(ability.Info.Id);
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
            _animator.SetBool(EntityAnimationParameters.Conjura, true);
        }

        public void StopCasting(int habilidadeID)
        {
            if (_animator.GetInteger(EntityAnimationParameters.HabilidadeID) == habilidadeID)
                _animator.SetBool(EntityAnimationParameters.Conjura, false);
        }

        public void EquipWeapon()
        {
            _animator.SetTrigger(EntityAnimationParameters.SacaArma);
        }

        public void UnequipWeapon()
        {
            _animator.SetTrigger(EntityAnimationParameters.GuardaArma);
        }
    }
}
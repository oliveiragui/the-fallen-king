using System;
using System.Collections.Generic;
using _Game.Scripts.GameContent.Abilities;
using _Game.Scripts.GameContent.Characters;
using _Game.Scripts.GameContent.Weapons;
using _Game.Scripts.Services.CombatSystem;
using UnityEngine;

namespace _Game.Scripts.GameContent.Entities
{
    [Serializable]
    public class EntityData
    {
        [SerializeField] Animator animator;
        [SerializeField] public Character associatedCharacter;
        public bool autoMove;
        public float speed;
        public Quaternion moveDiretion;
        public Quaternion lookDiretion;
        public float stoppingDistance;
        public Vector3 destination;
        public bool ready;

        [SerializeField] string _floorName;
        bool _move;
        bool _combatMode;
        bool _conjuring;
        Weapon _weapon;
        Ability _abilityInUse;
        AbilityCombo _comboInUse;
        int _requestedAbility;
        AbilityHit _currentHit;

        public bool Alive { get; set; } = true;

        public List<Ability> Abilities { get; set; }

        public string FloorName
        {
            get => _floorName;
            set => _floorName = value;
        }

        public AbilityHit CurrentHit
        {
            get => _currentHit;
            set
            {
                if (value)
                {
                    animator.SetBool(AnimatorParams.ReceivingHit, true);
                    animator.SetInteger(AnimatorParams.HitImpact, (int) value.impact + 2);
                }
                else
                {
                    animator.SetBool(AnimatorParams.ReceivingHit, false);
                    _currentHit = value;
                }
            }
        }

        public Weapon Weapon
        {
            get => _weapon;
            set
            {
                _weapon = value;
                if (!_weapon) return;
                animator.runtimeAnimatorController = _weapon.Data.AnimatorController;
                Abilities = _weapon.Abilities;
                //mesh.SwitchWeapon(_weapon.Data.Prefabs);
            }
        }

        public int RequestedAbility
        {
            get => _requestedAbility;
            set
            {
                _requestedAbility = value;
                animator.SetInteger(AnimatorParams.RequestedAbilityID, Abilities[value].Data.AnimationId);
            }
        }

        public bool Conjuring
        {
            get => _conjuring;

            set
            {
                //if (value == _conjuring) return;
                _conjuring = value;
                animator.SetBool(AnimatorParams.Cast, value);
            }
        }

        public Ability AbilityInUse
        {
            get => _abilityInUse;

            set
            {
                _abilityInUse = value;
                if (!_abilityInUse) return;
                animator.SetInteger(AnimatorParams.MaxCombo, _abilityInUse.Data.MaxCombo);
            }
        }

        public AbilityCombo ComboInUse
        {
            get => _comboInUse;
            set
            {
                _comboInUse = value;
                if (!value) return;
                animator.SetBool(AnimatorParams.Castable, value.Castable);
                animator.SetFloat(AnimatorParams.ComboFactor1, value.Factor1 * 1);
                if (!value.Castable) return;
                animator.SetFloat(AnimatorParams.ComboFactor2, value.Factor2 * 1);
                animator.SetFloat(AnimatorParams.ComboFactor3, value.Factor3 * 1);
            }
        }

        public bool Move
        {
            get => _move;

            set
            {
                animator.SetBool("Anda", value);
                _move = value;
            }
        }

        public bool CombatMode
        {
            get => _combatMode;

            set
            {
                _combatMode = value;
                animator.SetLayerWeight(1, value ? 1f : 0f);
            }
        }
    }
}
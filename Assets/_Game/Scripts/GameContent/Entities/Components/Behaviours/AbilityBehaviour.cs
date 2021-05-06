using System;
using UnityEngine;

namespace _Game.Scripts.GameContent.Entities.Components.Behaviours
{
    public class AbilityBehaviour : StateMachineBehaviour
    {
        Entity _entity;
        [SerializeField] int abilityId;

        public override void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
        {
            if (_entity == null && !animator.transform.TryGetComponent(out _entity)) return;
            _entity.events.startAbility.Invoke(abilityId-1);
        }

        public override void OnStateMachineExit(Animator animator, int stateMachinePathHash)
        {
            if (_entity == null && !animator.transform.TryGetComponent(out _entity)) return;
            _entity.events.finishAbility.Invoke();
        }
    }
}
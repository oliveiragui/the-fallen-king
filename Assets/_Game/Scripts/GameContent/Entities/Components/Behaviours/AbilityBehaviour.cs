using System;
using UnityEngine;

namespace _Game.Scripts.GameContent.Entities.Components.Behaviours
{
    public class AbilityBehaviour : StateMachineBehaviour
    {
        Entity _entity;

        public override void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
        {
            if (_entity == null && !animator.transform.TryGetComponent(out _entity)) return;
            _entity.StartRequestedAbility();
        }

        public override void OnStateMachineExit(Animator animator, int stateMachinePathHash)
        {
            if (_entity == null && !animator.transform.TryGetComponent(out _entity)) return;
            _entity.StopAbility();
        }
    }
}
using UnityEngine;

namespace _Game.GameModules.Entities.Scripts.Behaviours
{
    public class ComboBehaviour : StateMachineBehaviour
    {
        Entity _entity;

        // public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        // {
        //     if (_entity == null && !animator.transform.TryGetComponent(out _entity)) return;
        //     _entity.UsingAbility = false;
        //     _entity.endAbilityAnimation.Invoke();
        // }
        
        public override void OnStateMachineExit(Animator animator, int stateMachinePathHash)
        {
            if (_entity == null && !animator.transform.TryGetComponent(out _entity)) return;
            _entity.UsingAbility = false;
            _entity.endAbilityAnimation.Invoke();
        }
    }
}
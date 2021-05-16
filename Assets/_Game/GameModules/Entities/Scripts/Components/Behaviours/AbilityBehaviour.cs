using UnityEngine;

namespace _Game.GameModules.Entities.Scripts.Components.Behaviours
{
    public class AbilityBehaviour : StateMachineBehaviour
    {
        [SerializeField] int abilityId;
        Entity _entity;

        public override void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
        {
            if (_entity == null && !animator.transform.TryGetComponent(out _entity)) return;
            _entity.events.startAbilityAnimation.Invoke(abilityId - 1);
        }

        public override void OnStateMachineExit(Animator animator, int stateMachinePathHash)
        {
            if (_entity == null && !animator.transform.TryGetComponent(out _entity)) return;
            _entity.events.endAbilityAnimation.Invoke();
        }
    }
}
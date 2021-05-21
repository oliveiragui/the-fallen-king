using UnityEngine;

namespace _Game.GameModules.Entities.Scripts.Behaviours
{
    public class AbilityBehaviour : StateMachineBehaviour
    {
        [SerializeField] int abilityId;
        Entity _entity;

        bool manualMove;

        public override void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
        {
            if (_entity == null && !animator.transform.TryGetComponent(out _entity)) return;
            _entity.UsingAbility = true;
            _entity.startAbilityAnimation.Invoke(abilityId - 1);
            _entity.movement.Rotation = !_entity.Aim ? _entity.Direction : _entity.LookDiretion;
            _entity.movement.Stop();
        }

        public override void OnStateMachineExit(Animator animator, int stateMachinePathHash)
        {
            if (_entity == null && !animator.transform.TryGetComponent(out _entity)) return;
            _entity.UsingAbility = false;
            _entity.endAbilityAnimation.Invoke();
        }
    }
}
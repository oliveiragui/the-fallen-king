using UnityEngine;

namespace _Game.Scripts.GameContent.Entities.Components.Animation.Behaviours
{
    public class AbilityBehaviour : StateMachineBehaviour
    {
        [SerializeField] int id;
        Entity _entity;

        public override void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
        {
            if (_entity == null && !animator.transform.TryGetComponent(out _entity)) return;
            _entity.StartAbility();
        }

        public override void OnStateMachineExit(Animator animator, int stateMachinePathHash)
        {
            if (_entity == null && !animator.transform.TryGetComponent(out _entity)) return;
            _entity.FinishAbility();
        }
    }
}
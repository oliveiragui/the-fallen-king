using UnityEngine;

namespace _Game.Scripts.GameContent.Entities.Components.Behaviours
{
    public class ComboBehaviour : StateMachineBehaviour
    {
        Entity _entity;
        [SerializeField] int comboIndex;

        public override void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
        {
            if (!_entity && !animator.transform.TryGetComponent(out _entity)) return;
            _entity.UseCombo(comboIndex);
        }

        public override void OnStateMachineExit(Animator animator, int stateMachinePathHash)
        {
            if (!_entity && !animator.transform.TryGetComponent(out _entity)) return;
            _entity.StopCombo();
        }
    }
}
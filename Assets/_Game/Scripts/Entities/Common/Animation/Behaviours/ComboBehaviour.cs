using Entities.Combat;
using UnityEngine;

namespace Entities.Common.Animation.Behaviours
{
    public class ComboBehaviour : StateMachineBehaviour
    {
        [SerializeField] int currentCombo;
        CombatEntity _entity;

        //TODO: Add state machine monobehaviour that can receive a state withe enter, update, fixedupdate exit 

        // OnStateMachineEnter is called when entering a state machine via its Entry Node
        public override void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
        {
            if (_entity == null && !animator.transform.TryGetComponent(out _entity)) return;
            _entity.events.onComboEntered.Invoke(currentCombo);
        }

        // OnStateMachineExit is called when exiting a state machine via its Exit Node
        public override void OnStateMachineExit(Animator animator, int stateMachinePathHash)
        {
            _entity.events.onComboExited.Invoke();
        }
    }
}
using UnityEngine;

namespace _Game.Scripts.GameContent.Entities.Components.Animation.Behaviours
{
    public class ComboBehaviour : StateMachineBehaviour
    {
        [SerializeField] int currentCombo;
        Entity _entity;

        //TODO: Add state machine monobehaviour that can receive a state withe enter, update, fixedupdate exit 

        // OnStateMachineEnter is called when entering a state machine via its Entry Node
        public override void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
        {
            if (_entity == null && !animator.transform.TryGetComponent(out _entity)) return;
            _entity.animations.SetupCombo(currentCombo);
            //_entity.movement.Stop();
            //_entity.commands.CanReceiveMoveInput = false;
        }

        // OnStateMachineExit is called when exiting a state machine via its Exit Node
        public override void OnStateMachineExit(Animator animator, int stateMachinePathHash)
        {
            _entity.animations.StopCombo();
            //_entity.commands.CanReceiveMoveInput = true;
        }
    }
}
using UnityEngine;

namespace _Game.Scripts.GameContent.Entities.Components.Behaviours
{
    public class CombatModeBehaviour : StateMachineBehaviour
    {
        Entity entity;

        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        public override void OnStateMachineExit(Animator animator, int stateMachinePathHash)
        {
            if (entity == null && !animator.TryGetComponent(out entity)) return;
            entity.ExitCombat();
        }
    }
}
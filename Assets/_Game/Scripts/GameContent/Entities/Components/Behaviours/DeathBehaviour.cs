using UnityEngine;

namespace _Game.Scripts.GameContent.Entities.Components.Behaviours
{
    public class DeathBehaviour : StateMachineBehaviour
    {
        Entity entity;

        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        // public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        // {
        //     if (!entity && !animator.TryGetComponent(out entity)) return;
        // }

        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (!entity && !animator.TryGetComponent(out entity)) return;
            Destroy(entity.gameObject);
        }
    }
}
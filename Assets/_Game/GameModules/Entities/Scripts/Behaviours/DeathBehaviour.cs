using UnityEngine;

namespace _Game.GameModules.Entities.Scripts.Behaviours
{
    public class DeathBehaviour : EntityBehaviour
    {
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            entity.movement.enabled = false;
            entity.Hittable = false;
        }

        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) { }
    }
}
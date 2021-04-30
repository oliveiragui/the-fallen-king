using UnityEngine;

namespace _Game.Scripts.IA.Behaviours
{
    public class Ataca : StateMachineBehaviour
    {
        public override void OnStateEnter(
            Animator animator, AnimatorStateInfo stateInfo, int layerIndex
        ) { }

        public override void OnStateExit(
            Animator animator, AnimatorStateInfo stateInfo, int layerIndex
        ) { }

        public override void OnStateUpdate(
            Animator animator, AnimatorStateInfo stateInfo, int layerIndex
        )
        {
            // if (entity.AbilityInUse)
            //     entity.StopCasting(0);
            // entity.RequestAbility(0);
        }

        public override void OnStateMove(
            Animator animator, AnimatorStateInfo stateInfo, int layerIndex
        ) { }

        public override void OnStateIK(
            Animator animator, AnimatorStateInfo stateInfo, int layerIndex
        ) { }
    }
}
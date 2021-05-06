using UnityEngine;

namespace _Game.Scripts.GameContent.Entities.Components.Behaviours
{
    public class HitBehaviour : StateMachineBehaviour
    {
        Entity _entity;

        // OnStateExit is called before OnStateExit is called on any state inside this state machine
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetBool(AnimatorParams.ReceivingHit, false);
        }
    }
}
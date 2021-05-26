using UnityEngine;

namespace _Game.GameModules.Entities.Scripts.Behaviours
{
    public class HitBehaviour : EntityBehaviour
    {
        [SerializeField] int hitImpact;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (hitImpact > 1) entity.movement.ApplyInputMovement = false;
        }

        // OnStateExit is called before OnStateExit is called on any state inside this state machine
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetBool(AnimatorParams.ReceivingHit, false);
        }
    }
}
using UnityEngine;

namespace _Game.GameModules.Entities.Scripts.Behaviours
{
    public class HitBehaviour : StateMachineBehaviour
    {
        Entity _entity;
        [SerializeField] int hitImpact;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (_entity == null && !animator.transform.TryGetComponent(out _entity)) return;
            if (hitImpact > 1) _entity.movement.Stop();
        }

        // OnStateExit is called before OnStateExit is called on any state inside this state machine
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetBool(AnimatorParams.ReceivingHit, false);
        }
    }
}
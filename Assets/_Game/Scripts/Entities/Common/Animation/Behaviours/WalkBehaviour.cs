using Entities.Common.Action;
using UnityEngine;

namespace Entities.Common.Animation.Behaviours
{
    public class WalkBehaviour : StateMachineBehaviour
    {
        EntityAction walkAction;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (!walkAction && !animator.transform.TryGetComponent(out walkAction)) return;
            walkAction.ActionEnter();
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (walkAction) walkAction.ActionExit();
        }
    }
}
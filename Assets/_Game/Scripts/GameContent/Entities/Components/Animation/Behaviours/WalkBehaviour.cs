using _Game.Scripts.Entities.Components.Action;
using UnityEngine;

namespace _Game.Scripts.Entities.Components.Animation.Behaviours
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
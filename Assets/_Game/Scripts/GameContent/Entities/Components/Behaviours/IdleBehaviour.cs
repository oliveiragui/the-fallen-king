using UnityEngine;

namespace _Game.Scripts.GameContent.Entities.Components.Animation.Behaviours
{
    public class IdleBehaviour : StateMachineBehaviour
    {
        Entity entity;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (entity == null && !animator.TryGetComponent(out entity)) return;
            entity.movement.Stop();
        }
    }
}
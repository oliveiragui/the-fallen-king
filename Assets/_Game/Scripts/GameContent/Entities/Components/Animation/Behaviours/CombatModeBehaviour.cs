using UnityEngine;

namespace _Game.Scripts.GameContent.Entities.Components.Animation.Behaviours
{
    public class CombatModeBehaviour : StateMachineBehaviour
    {
        Entity entity;

        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (entity == null && !animator.TryGetComponent(out entity)) return;
            if (!entity.inCombat) return;
            
            entity.inCombat = false;
            entity.mesh.InCombat = false;
            entity.animations.UseWeapon(false);
        }

        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (entity.inCombat) return;
            entity.inCombat = true;
            entity.mesh.InCombat = true;
            entity.animations.UseWeapon(true);
        }
    }
}
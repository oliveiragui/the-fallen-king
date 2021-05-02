using UnityEngine;

namespace _Game.Scripts.GameContent.Entities.Components.Behaviours
{
    public class HitBehaviour : StateMachineBehaviour
    {
        Entity _entity;

        // OnStateExit is called before OnStateExit is called on any state inside this state machine
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (_entity == null && !animator.transform.TryGetComponent(out _entity)) return;
            _entity.StopHit();
            _entity.CombatMode = true;
        }
    }
}
using UnityEngine;

namespace _Game.Scripts.IA.Behaviours
{
    public class Ataca : StateMachineBehaviour
    {
        IATest _test;
        [SerializeField] int abilityId;

        public override void OnStateEnter(
            Animator animator, AnimatorStateInfo stateInfo, int layerIndex
        )
        {
            if (!_test && !animator.transform.TryGetComponent(out _test)) return;
            _test.LookToTarget();

            _test.entity.RequestAbility(abilityId);
        }

        public override void OnStateUpdate(
            Animator animator, AnimatorStateInfo stateInfo, int layerIndex
        )
        {
            if (!_test && !animator.transform.TryGetComponent(out _test)) return;
            _test.LookToTarget();
        }

        public override void OnStateExit(
            Animator animator, AnimatorStateInfo stateInfo, int layerIndex
        )
        {
            if (!_test && !animator.transform.TryGetComponent(out _test)) return;
            _test.entity.StopCasting(abilityId);
        }
    }
}
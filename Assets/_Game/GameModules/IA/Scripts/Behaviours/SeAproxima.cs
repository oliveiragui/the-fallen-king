using UnityEngine;

namespace _Game.GameModules.IA.Scripts.Behaviours
{
    public class SeAproxima : StateMachineBehaviour
    {
        IATest _test;

        public override void OnStateExit(
            Animator animator, AnimatorStateInfo stateInfo, int layerIndex
        )
        {
            if (!_test && !animator.transform.TryGetComponent(out _test)) return;
            _test.LookToTarget();
            _test.entity.Stop();
        }

        public override void OnStateUpdate(
            Animator animator, AnimatorStateInfo stateInfo, int layerIndex
        )
        {
            if (!_test && !animator.transform.TryGetComponent(out _test)) return;
            _test.entity.MoveTo(3, _test.Target.transform.position, 0.5f);
        }
    }
}
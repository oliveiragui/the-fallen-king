using UnityEngine;

namespace _Game.GameModules.IA.Scripts.Behaviours
{
    public class SeAfasta : StateMachineBehaviour
    {
        IATest _test;

        public override void OnStateEnter(
            Animator animator, AnimatorStateInfo stateInfo, int layerIndex
        )
        {
            if (!_test && !animator.transform.TryGetComponent(out _test)) return;

            var position = _test.entity.transform.position;
            var destination = (position - _test.Target.transform.position).normalized * _test.minDistance + position;

            _test.LookToTarget();
            _test.entity.MoveTo(3, destination, 0.5f);
        }

        public override void OnStateExit(
            Animator animator, AnimatorStateInfo stateInfo, int layerIndex
        )
        {
            if (!_test && !animator.transform.TryGetComponent(out _test)) return;
            _test.LookToTarget();
            _test.entity.Stop();
        }
    }
}
using _Game.Scripts.Utils.Extension;
using UnityEngine;

namespace _Game.Scripts.IA.Behaviours
{
    public class Ataca : StateMachineBehaviour
    {
        IATest _test;

        public override void OnStateEnter(
            Animator animator, AnimatorStateInfo stateInfo, int layerIndex
        )
        {
            if (!_test && !animator.transform.TryGetComponent(out _test)) return;
            var position = _test.entity.transform.position;
            var direction = _test.Target.transform.position - position;
            _test.entity.SetRotation(Quaternion.Euler(Vector3.up * new Vector2(direction.x, direction.z).ToDegree()));
            _test.entity.RequestAbility(0);
        }

        public override void OnStateUpdate(
            Animator animator, AnimatorStateInfo stateInfo, int layerIndex
        )
        {
            if (!_test && !animator.transform.TryGetComponent(out _test)) return;
            var position = _test.entity.transform.position;
            var direction = _test.Target.transform.position - position;
            _test.entity.SetRotation(Quaternion.Euler(Vector3.up * new Vector2(direction.x, direction.z).ToDegree()));
        }

        public override void OnStateExit(
            Animator animator, AnimatorStateInfo stateInfo, int layerIndex
        )
        {
            if (!_test && !animator.transform.TryGetComponent(out _test)) return;
            _test.entity.StopCasting(0);
        }
    }
}
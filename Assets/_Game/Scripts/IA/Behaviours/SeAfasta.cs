using UnityEngine;
using _Game.Scripts.Utils.Extension;

namespace _Game.Scripts.IA.Behaviours
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
            var direction = position - _test.Target.transform.position;
            var destination = direction.normalized * _test.minDistance + position;

            _test.entity.LookAt(Quaternion.Euler(Vector3.up * new Vector2(direction.x, direction.z).ToDegree()));
            _test.entity.MoveTo(3, destination,0.5f);
        }

        public override void OnStateExit(
            Animator animator, AnimatorStateInfo stateInfo, int layerIndex
        )
        {
            if (!_test && !animator.transform.TryGetComponent(out _test)) return;
            var targetPos = _test.Target.transform.position;
            _test.entity.LookAt(Quaternion.Euler(Vector3.up * new Vector2(targetPos.x, targetPos.y).ToDegree()));
            _test.entity.Stop();
        }
        
    }
}
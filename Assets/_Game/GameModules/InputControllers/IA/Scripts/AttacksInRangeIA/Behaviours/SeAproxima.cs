using UnityEngine;

namespace _Game.GameModules.InputControllers.IA.Scripts.AttacksInRangeIA.Behaviours
{
    public class SeAproxima : IaBehaviour
    {
       // IATest _test;

        public override void OnStateEnter(
            Animator animator, AnimatorStateInfo stateInfo, int layerIndex
        )
        {
           // if (!_test && !animator.transform.TryGetComponent(out _test)) return;
            _test.entity.InputSpeed = 1;
        }

        public override void OnStateUpdate(
            Animator animator, AnimatorStateInfo stateInfo, int layerIndex
        )
        {
          //  if (!_test && !animator.transform.TryGetComponent(out _test)) return;
            _test.MoveTo(_test.Target.transform.position);
        }

        public override void OnStateExit(
            Animator animator, AnimatorStateInfo stateInfo, int layerIndex
        )
        {
         //  if (!_test && !animator.transform.TryGetComponent(out _test)) return;
            //_test.entity.StopWalking();
        }
    }
}
using _Game.GameModules.IA.Scripts.AttacksInRangeIA;
using UnityEngine;

namespace _Game.GameModules.IA.Scripts.FirstBossIA.Behaviours
{
    public class SeAproxima : StateMachineBehaviour
    {
        IATest _test;

        public override void OnStateExit(
            Animator animator, AnimatorStateInfo stateInfo, int layerIndex
        )
        {
            if (!_test && !animator.transform.TryGetComponent(out _test)) return;
            //_test.entity.StopWalking();
        }

        public override void OnStateUpdate(
            Animator animator, AnimatorStateInfo stateInfo, int layerIndex
        )
        {
            if (!_test && !animator.transform.TryGetComponent(out _test)) return;
            _test.entity.MoveTo(_test.Target.transform.position);
        }
    }
}
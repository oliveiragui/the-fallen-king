using UnityEngine;

namespace _Game.GameModules.InputControllers.IA.Scripts.AttacksInRangeIA.Behaviours
{
    public class SeAproxima : IaBehaviour
    {
        public override void OnStateEnter(
            Animator animator, AnimatorStateInfo stateInfo, int layerIndex
        )
        {
            _test.entity.InputSpeed = 1;
        }

        public override void OnStateUpdate(
            Animator animator, AnimatorStateInfo stateInfo, int layerIndex
        )
        {
            _test.MoveTo(_test.Target.transform.position);
        }
    }
}
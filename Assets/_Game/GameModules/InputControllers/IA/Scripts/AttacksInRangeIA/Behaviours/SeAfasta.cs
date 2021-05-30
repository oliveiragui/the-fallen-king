using UnityEngine;

namespace _Game.GameModules.InputControllers.IA.Scripts.AttacksInRangeIA.Behaviours
{
    public class SeAfasta : IaBehaviour
    {

        public override void OnStateEnter(
            Animator animator, AnimatorStateInfo stateInfo, int layerIndex
        )
        {
            _test.entity.InputSpeed = 1;
            var position = _test.entity.transform.position;
            var destination = (position - _test.Target.transform.position).normalized * _test.minDistance + position;
            _test.entity.Destination = destination;
        }
    }
}
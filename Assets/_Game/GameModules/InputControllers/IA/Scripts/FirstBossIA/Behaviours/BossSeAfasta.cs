using _Game.GameModules.IA.Scripts.AttacksInRangeIA;
using _Game.GameModules.IA.Scripts.AttacksInRangeIA.Behaviours;
using UnityEngine;

namespace _Game.GameModules.IA.Scripts.FirstBossIA.Behaviours
{
    public class BossSeAfasta : BossBehaviour
    {

        public override void OnStateEnter(
            Animator animator, AnimatorStateInfo stateInfo, int layerIndex
        )
        {
            if (!_test && !animator.transform.TryGetComponent(out _test)) return;

            var position = _test.entity.transform.position;
            var destination = (position - _test.Target.transform.position).normalized * _test.minDistance + position;
            _test.entity.Destination = destination;
        }
    }
}
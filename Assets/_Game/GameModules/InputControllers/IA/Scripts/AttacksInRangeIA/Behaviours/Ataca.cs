using UnityEngine;

namespace _Game.GameModules.InputControllers.IA.Scripts.AttacksInRangeIA.Behaviours
{
    public class Ataca : IaBehaviour
    {
        [SerializeField] int abilityId;
  

        public override void OnStateEnter(
            Animator animator, AnimatorStateInfo stateInfo, int layerIndex
        )
        {
            //if (!_test && !animator.transform.TryGetComponent(out _test)) return;
            _test.entity.Character.AbilitySystem.RequestAbility(abilityId);
            _test.entity.InputSpeed = 0;
            animator.SetBool($"Habilidade {abilityId + 1} finalizada", false);
        }

        public override void OnStateExit(
            Animator animator, AnimatorStateInfo stateInfo, int layerIndex
        )
        {
            //if (!_test && !animator.transform.TryGetComponent(out _test)) return;
            _test.entity.StopCasting(abilityId);
        }

        public override void OnStateUpdate(
            Animator animator, AnimatorStateInfo stateInfo, int layerIndex
        )
        {
            //if (!_test && !animator.transform.TryGetComponent(out _test)) return;
            if (abilityId != 2) return;
            if ((_test.Target.transform.position - _test.entity.transform.position).magnitude < 3)
                _test.entity.StopCasting(abilityId);
        }
    }
}
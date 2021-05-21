using UnityEngine;

namespace _Game.GameModules.IA.Scripts.Behaviours
{
    public class Ataca : StateMachineBehaviour
    {
        [SerializeField] int abilityId;
        IATest _test;

        public override void OnStateEnter(
            Animator animator, AnimatorStateInfo stateInfo, int layerIndex
        )
        {
            if (!_test && !animator.transform.TryGetComponent(out _test)) return;
            _test.entity.Character.AbilitySystem.RequestAbility(abilityId);
            _test.LookToTarget();
            animator.SetBool($"Habilidade {abilityId + 1} finalizada", false);
        }

        public override void OnStateExit(
            Animator animator, AnimatorStateInfo stateInfo, int layerIndex
        )
        {
            if (!_test && !animator.transform.TryGetComponent(out _test)) return;
            _test.entity.StopCasting(abilityId);
        }

        public override void OnStateUpdate(
            Animator animator, AnimatorStateInfo stateInfo, int layerIndex
        )
        {
            if (!_test && !animator.transform.TryGetComponent(out _test)) return;
            _test.LookToTarget();
            if (abilityId != 2) return;
            if ((_test.Target.transform.position - _test.entity.transform.position).magnitude < 3)
                _test.entity.StopCasting(abilityId);
        }
    }
}
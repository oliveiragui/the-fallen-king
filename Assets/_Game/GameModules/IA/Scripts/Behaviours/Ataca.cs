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
        }
    }
}
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
            _test.entity.Character.AbilitySystem.RequestAbility(abilityId);
            _test.entity.InputSpeed = 0;
        }

        public override void OnStateExit(
            Animator animator, AnimatorStateInfo stateInfo, int layerIndex
        )
        {
 
            _test.entity.StopCasting(abilityId);
        }
    }
}
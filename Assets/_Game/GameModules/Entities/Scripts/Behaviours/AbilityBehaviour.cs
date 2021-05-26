using UnityEngine;

namespace _Game.GameModules.Entities.Scripts.Behaviours
{
    public class AbilityBehaviour :EntityBehaviour
    {
        [SerializeField] int abilityId;


        //     bool manualMove;

        public override void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
        {
         
            entity.UsingAbility = true;
            entity.startAbilityAnimation.Invoke(abilityId - 1);
            entity.movement.Rotation = !entity.Aim ? entity.Direction : entity.LookDiretion;
            entity.movement.ApplyInputMovement = false;
        }

        // public override void OnStateMachineExit(Animator animator, int stateMachinePathHash)
        // {
        //     if (_entity == null && !animator.transform.TryGetComponent(out _entity)) return;
        //     _entity.UsingAbility = false;
        //     _entity.endAbilityAnimation.Invoke();
        // }
    }
}
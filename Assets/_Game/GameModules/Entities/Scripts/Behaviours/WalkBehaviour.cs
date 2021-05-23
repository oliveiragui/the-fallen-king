using UnityEngine;

namespace _Game.GameModules.Entities.Scripts.Behaviours
{
    public class WalkBehaviour : StateMachineBehaviour
    {
        Entity entity;

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (entity == null && !animator.TryGetComponent(out entity)) return;

            animator.SetFloat(AnimatorParams.Velocidade, entity.Speed);
            entity.movement.Speed = entity.Speed;
            entity.movement.Destination = entity.Destination;
            entity.movement.StoppingDistance = entity.StoppingDistance;
            entity.movement.Rotation = entity.Direction;
            entity.movement.Walking = true;
        }
    }
}
using UnityEngine;

namespace _Game.GameModules.Entities.Scripts.Components.Behaviours
{
    public class WalkBehaviour : StateMachineBehaviour
    {
        Entity entity;

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (entity == null && !animator.TryGetComponent(out entity)) return;

            animator.SetFloat(AnimatorParams.Velocidade, entity.Speed);
            entity.movement.Speed = entity.Speed;
            if (entity.AutoMove)
            {
                entity.movement.Destination = entity.Destination;
                entity.movement.StoppingDistance = entity.StoppingDistance;
            }
            else
            {
                entity.movement.Rotation = entity.Direction;
            }
        }
    }
}
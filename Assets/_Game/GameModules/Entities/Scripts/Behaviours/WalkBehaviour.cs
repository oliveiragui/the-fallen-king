using UnityEngine;

namespace _Game.GameModules.Entities.Scripts.Behaviours
{
    public class WalkBehaviour : EntityBehaviour
    {
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            entity.movement.ApplyInputMovement = true;
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            entity.movement.InputSpeed = entity.Speed;

            if (entity.AutoMove)
            {
                entity.movement.StoppingDistance = entity.StoppingDistance;
                entity.movement.Destination = entity.Destination;
            }
            else
            {
                entity.movement.Rotation = entity.Direction;
            }
        }
    }
}
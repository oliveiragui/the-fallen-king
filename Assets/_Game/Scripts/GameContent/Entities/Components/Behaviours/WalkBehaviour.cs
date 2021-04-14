using UnityEngine;

namespace _Game.Scripts.GameContent.Entities.Components.Animation.Behaviours
{
    public class WalkBehaviour : StateMachineBehaviour
    {
        Entity entity;

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (entity == null && !animator.TryGetComponent(out entity)) return;

            animator.SetFloat(AnimatorParams.Velocidade, entity.speed);
            entity.movement.Speed = entity.speed;
            if (entity.autoMove)
            {
                entity.movement.Destination = entity.destination;
                entity.movement.StoppingDistance = entity.stoppingDistance;
            }
            else
                entity.movement.Rotation = entity.moveDiretion;
        }
    }
}
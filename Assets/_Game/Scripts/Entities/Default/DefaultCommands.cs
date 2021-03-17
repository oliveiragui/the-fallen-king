using UnityEngine;

namespace Entities.Default
{
    public class DefaultCommands : MonoBehaviour
    {
        public DefaultEntity entity;

        #region Movement Commands

        public void Move()
        {
            entity.components.movement.Speed = entity.data.speed;
            entity.components.movement.Direction = entity.data.direction;
            //entity.data.animations.Run(entity.data.speed);
            if (!entity.components.movement.AutoMovement) entity.components.movement.Move();
        }

        public void MoveTo(Vector3 position)
        {
            entity.components.movement.Speed = entity.data.speed;
            entity.components.movement.StoppingDistance = entity.data.stoppingDistance;
            entity.data.animations.Run(entity.data.speed);
            entity.components.movement.MoveTo(position);
        }

        public void StopMove()
        {
            entity.data.animations.StopRun();
            entity.components.movement.Stop();
        }

        public void TurnToLookDirection()
        {
            entity.transform.rotation = Quaternion.Euler(0, entity.data.lookDiretion, 0);
        }

        #endregion
    }
}
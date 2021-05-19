using UnityEngine;

namespace _Game.GameModules.Entities.Scripts.Commands
{
    [CreateAssetMenu(fileName = "Move", menuName = "GameContent/Entities/Commands/Move",
        order = 0)]
    public class Move : EntityCommand
    {
        //[SerializeField] bool autoMove;
        [SerializeField] float modifier;
        
        public override void Execute(Entity entity)
        {
            entity.movement.Speed = entity.Speed * modifier;
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
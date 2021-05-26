using UnityEngine;

namespace _Game.GameModules.Entities.Scripts.Commands
{
    [CreateAssetMenu(fileName = "Move", menuName = "GameContent/Entities/Commands/Move",
        order = 0)]
    public class Move : EntityCommand
    {
        [SerializeField] bool allowInputModifier;
        [SerializeField] float modifier;

        public override void Execute(Entity entity)
        {
            entity.movement.ApplyInputMovement = true;
            entity.movement.InputSpeed = (allowInputModifier ? entity.Speed : entity.CharacterSpeed) * modifier;
            if (entity.AutoMove)
            {
                entity.movement.StoppingDistance = entity.StoppingDistance;
                entity.movement.Destination = entity.Destination;
            }
            else entity.movement.Rotation = entity.Direction;
        }
    }
}
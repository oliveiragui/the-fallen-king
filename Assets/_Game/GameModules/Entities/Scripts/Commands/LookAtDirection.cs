using System.Collections;
using UnityEngine;

namespace _Game.GameModules.Entities.Scripts.Commands
{
    [CreateAssetMenu(fileName = "Look at direction", menuName = "GameContent/Entities/Commands/Look at direction",
        order = 0)]
    public class LookAtDirection : EntityCommand
    {
        [SerializeField] Direction direction;

        public override void Execute(Entity entity)
        {
            entity.movement.Rotation = direction == Direction.Aim ? entity.LookDiretion : entity.Direction;
        }
    }

    public enum Direction
    {
        Aim,
        Movement,
    }
}
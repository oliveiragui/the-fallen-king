using System.Collections;
using UnityEngine;

namespace _Game.GameModules.Entities.Scripts
{
    [CreateAssetMenu(fileName = "Set if Can Receive Hit", menuName = "GameContent/Entities/Commands/Set if Can Receive Hit",
        order = 0)]
    public class SetIfCanReceiveHitCommand : EntityCommand
    {
        [SerializeField] bool canReceiveHitted;

        public override void Execute(Entity entity)
        {
            entity.collision.Hittable = canReceiveHitted;
        }
    }
}
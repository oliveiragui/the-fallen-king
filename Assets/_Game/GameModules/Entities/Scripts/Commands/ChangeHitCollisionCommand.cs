using UnityEngine;

namespace _Game.GameModules.Entities.Scripts.Commands
{
    [CreateAssetMenu(fileName = "Set if Can Receive Hit", menuName = "GameContent/Entities/Commands/Set if Can Receive Hit", order = 0)]
    public class ChangeHitCollisionCommand : EntityCommand
    {
        [SerializeField] bool canReceiveHitted;

        public override void Execute(Entity entity)
        {
            entity.Hittable = canReceiveHitted;
        }
    }
}
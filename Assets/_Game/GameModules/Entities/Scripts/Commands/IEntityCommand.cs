using UnityEngine;

namespace _Game.GameModules.Entities.Scripts
{
    public interface IEntityCommand
    {
        void Execute(Entity entity);
    }

    public class EntityCommand : ScriptableObject, IEntityCommand
    {
        public virtual void Execute(Entity entity) { }
    }
}
using UnityEngine;

namespace _Game.GameModules.Entities.Scripts.Commands
{
    public class EntityCommand : AnimationEventCreator, IEntityCommand
    {
        public virtual void Execute(Entity entity) { }

        public override AnimationEvent CreateEvent()
        {
            return new AnimationEvent()
            {
                functionName = "ExecuteCommand",
                objectReferenceParameter = this
            };
        }
    }
}
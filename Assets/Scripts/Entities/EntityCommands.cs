using UnityEngine;

namespace Entities
{
    public class EntityCommands : MonoBehaviour
    {
        Entity entity;

        public EntityCommands(Entity entity)
        {
            this.entity = entity;
        }
    }
}
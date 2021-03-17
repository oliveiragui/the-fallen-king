using Components.Storage.Custom;
using UnityEngine;

namespace Entities.Common.PhysicsSystem
{
    public class EntityCollision : MonoBehaviour
    {
        [SerializeField] ColliderStorage colliders;

        public bool Interactible
        {
            get => colliders["Interaction"].enabled;
            private set => colliders["Interaction"].enabled = value;
        }

        public bool Hittable
        {
            get => colliders["Hittable"].enabled;
            private set => colliders["Hittable"].enabled = value;
        }
    }
}
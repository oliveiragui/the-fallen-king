using _Game.Scripts.Services.Storage.Custom;
using UnityEngine;

namespace _Game.Scripts.GameContent.Entities.Components.PhysicsSystem
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
            set => colliders["Hittable"].enabled = value;
        }
    }
}
using _Game.Scripts.Services.Storage.Custom;
using UnityEngine;

namespace _Game.GameModules.Entities.Scripts
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
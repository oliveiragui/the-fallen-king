using _Game.Scripts.Components.Storage.Custom;
using UnityEngine;

namespace _Game.Scripts.GameContent.Entities.Components.PhysicsSystem
{
    public class EntityCollision : MonoBehaviour
    {
        [SerializeField] ColliderStorage colliders;
        [SerializeField] string floorName;

        public string FloorName
        {
            get => floorName;
            set => floorName = value;
        }

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

        void OnControllerColliderHit(ControllerColliderHit hit)
        {
            DetectFloorName(hit.collider);
        }

        string DetectFloorName(Component floor)
        {
            return floorName = floor.gameObject.layer == LayerMask.NameToLayer("Floor") ? floor.tag : null;
        }
    }
}
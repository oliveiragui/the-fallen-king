using Components.Move;
using Components.Storage.Custom;
using UnityEngine;

namespace Collection.Entities.Physics
{
    public class EntityPhysics : MonoBehaviour
    {
        [SerializeField] ColliderStorage colliders;
        
        bool Interactible
        {
            get => colliders["Interaction"].enabled;
            set => colliders["Interaction"].enabled = value;
        }

        bool Hittable
        {
            get => colliders["Hittable"].enabled;
            set => colliders["Hittable"].enabled = value;
        }
    }
}
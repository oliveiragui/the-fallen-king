using Entities.Common.Audio;
using Entities.Common.Mesh;
using Entities.Common.Particle;
using Entities.Common.PhysicsSystem;
using UnityEngine;

namespace Entities.Common
{
    public class EntityComponents : MonoBehaviour
    {
        [SerializeField] public new EntityAudio audio;
        [SerializeField] public EntityMesh mesh;
        [SerializeField] public EntityParticle particle;
        [SerializeField] public EntityCollision collision;
        [SerializeField] public EntityMovement movement;
        [SerializeField] public GameObject actionContainer;
    }
}
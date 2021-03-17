using Components.Storage.Custom;
using UnityEngine;

namespace Entities.Common.Particle
{
    public class EntityParticle : MonoBehaviour
    {
        [SerializeField] ParticleStorage particles;

        public void Play(int id)
        {
            particles[id].Play();
        }

        public void Play(string id)
        {
            particles[id].Play();
        }

        public void Stop(int id)
        {
            particles[id].Play();
        }

        public void Stop(string id)
        {
            particles[id].Stop();
        }
    }
}
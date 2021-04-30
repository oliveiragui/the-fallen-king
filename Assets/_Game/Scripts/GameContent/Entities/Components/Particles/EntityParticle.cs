using _Game.Scripts.Services.Storage.Custom;
using UnityEngine;

namespace _Game.Scripts.GameContent.Entities.Components.Particles
{
    public class EntityParticle : MonoBehaviour
    {
        [SerializeField] ParticleStorage particles;

        public void Play(string id)
        {
            particles[id].Play();
        }

        public void Stop(string id)
        {
            particles[id].Stop();
        }
    }
}
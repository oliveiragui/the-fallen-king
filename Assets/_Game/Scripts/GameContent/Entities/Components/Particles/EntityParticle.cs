using _Game.Scripts.GameContent.Abilities;
using _Game.Scripts.Services.Storage.Custom;
using UnityEngine;

namespace _Game.Scripts.GameContent.Entities.Components.Particles
{
    public class EntityParticle : MonoBehaviour
    {
        [SerializeField] ParticleStorage defaultParticles;
        [SerializeField] GameObject abilityParticleContainer;
        
        ParticleSystem[][] AbilityParticles = new ParticleSystem[4][];

        public void PlayAbilityEffect(int abilityIndex, int particleIndex)
        {
            AbilityParticles[abilityIndex][particleIndex].Play();
        }

        public void InstantiateAbilityEffects(Ability[] abilities)
        {
            foreach (Transform child in abilityParticleContainer.transform) Destroy(child.gameObject);
            for (var i = 0; i < abilities.Length; i++)
                AbilityParticles[i] = abilities[i].InstantiateParticles(abilityParticleContainer.transform);
        }

        public void Play(string id)
        {
            defaultParticles[id].Play();
        }

        public void Stop(string id)
        {
            defaultParticles[id].Stop();
        }
    }
}
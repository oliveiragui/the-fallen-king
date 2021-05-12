using _Game.GameModules.Abilities.Scripts;
using _Game.Scripts.Services.Storage.Custom;
using UnityEngine;

namespace _Game.GameModules.Entities.Scripts.Components.Particles
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
            AbilityParticles = new ParticleSystem[abilities.Length][];
            for (var i = 0; i < abilities.Length; i++)
            {
                AbilityParticles[i] = InstantiateParticles(abilities[i].Data);
            }
        }

        public ParticleSystem[] InstantiateParticles(AbilityData data)
        {
            var particleEffects = new ParticleSystem[data.ParticleEffects.Length];
            for (var i = 0; i < data.ParticleEffects.Length; i++)
                Instantiate(data.ParticleEffects[i].gameObject, abilityParticleContainer.transform).TryGetComponent(out particleEffects[i]);
            return particleEffects;
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
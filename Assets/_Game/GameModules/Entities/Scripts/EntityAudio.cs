using System;
using _Game.GameModules.Abilities.Scripts;
using _Game.Scripts.Services.Storage.Custom;
using UnityEngine;

namespace _Game.GameModules.Entities.Scripts
{
    [Serializable]
    public class EntityAudio : MonoBehaviour
    {
        [SerializeField] AudioSourceStorage audioSourceStorage;
        [SerializeField] GameObject soundEffectContainer;
        
        AudioSource[][] abilitySfx = new AudioSource[4][];

        public void PlayAbilityEffect(int abilityIndex, int particleIndex)
        {
            abilitySfx[abilityIndex][particleIndex].Play();
        }

        public void InstantiateAbilitySfx(Ability[] abilities)
        {
            foreach (Transform child in soundEffectContainer.transform) Destroy(child.gameObject);
            abilitySfx = new AudioSource[abilities.Length][];
            for (var i = 0; i < abilities.Length; i++) abilitySfx[i] = InstantiateSfx(abilities[i].Data);
        }
        
        public AudioSource[] InstantiateSfx(AbilityData data)
        {
            var sfxs = new AudioSource[data.Sfx.Length];
            for (var i = 0; i < data.Sfx.Length; i++)
                Instantiate(data.Sfx[i].gameObject, soundEffectContainer.transform)
                    .TryGetComponent(out sfxs[i]);
            return sfxs;
        }
        

        public void Play(string id)
        {
            if (id is null || id.Length < 1) return;
            audioSourceStorage[id].Play();
        }

        public void Stop(string id)
        {
            if (id is null || id.Length < 1) return;
            audioSourceStorage[id].Stop();
        }
    }
}
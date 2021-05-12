using System;
using _Game.Scripts.Services.Storage.Custom;
using UnityEngine;

namespace _Game.GameModules.Entities.Scripts.Components.Audio
{
    [Serializable]
    public class EntityAudio : MonoBehaviour
    {
        [SerializeField] AudioSourceStorage audioSourceStorage;

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
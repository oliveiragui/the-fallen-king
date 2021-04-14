using System;
using _Game.Scripts.Components.Storage.Custom;
using UnityEngine;

namespace _Game.Scripts.GameContent.Entities.Components.Audio
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
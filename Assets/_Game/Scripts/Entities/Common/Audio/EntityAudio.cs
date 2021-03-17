using System;
using Components.Storage.Custom;
using UnityEngine;

namespace Entities.Common.Audio
{
    [Serializable]
    public class EntityAudio : MonoBehaviour
    {
        [SerializeField] AudioStorage audioSources;

        public void Play(int id)
        {
            audioSources[id].Play();
        }

        public void Play(string id)
        {
            audioSources[id].Play();
        }

        public void Stop(int id)
        {
            audioSources[id].Play();
        }

        public void Stop(string id)
        {
            audioSources[id].Stop();
        }
    }
}
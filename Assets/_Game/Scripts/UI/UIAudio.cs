using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts.UI
{
    public class UIAudio : MonoBehaviour
    {
        [SerializeField] List<AudioSource> audioSources;

        public void PlaySound(int index)
        {
            audioSources[index].Play();
        }
    }
}
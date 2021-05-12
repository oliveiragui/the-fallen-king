using System.Collections.Generic;
using UnityEngine;

namespace _Game.GameModules.UI.Scripts
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
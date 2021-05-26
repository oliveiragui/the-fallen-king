using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts.Utils
{
    public class SoundTrackController : MonoBehaviour
    {
        [SerializeField] List<AudioSource> soudtracks;

        public void Play(int index)
        {
            soudtracks[index].Play();
        }
        
    
        public void Stop(int index)
        {
            soudtracks[index].Stop();
        }

    }
}
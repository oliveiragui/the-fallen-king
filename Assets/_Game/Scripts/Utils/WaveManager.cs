using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts.Utils
{
    public class WaveManager : MonoBehaviour
    {
        [SerializeField] List<Wave> waves;
        int currentWave;
        bool isStopped;

        void PlayInSequence()
        {
            currentWave = 0;
        }

        void PlayNext()
        {
            if (waves[currentWave].IsRunning) waves[currentWave].Stop();

      

            waves[currentWave].Play();
        }

        void Stop()
        {
            if (currentWave < waves.Count - 1) currentWave++;
            else currentWave = 0;
        }
    }
}
using UnityEngine;
using UnityEngine.Events;

namespace _Game.Scripts.Utils
{
    public class WaitWavesEnd : MonoBehaviour
    {
        [SerializeField] Wave[] waves;
        [SerializeField] int finishedWaves;
        [SerializeField] UnityEvent allWavesEnd;

        void Start()
        {
            foreach (var wave in waves)
            {
                wave.end.AddListener(CountWave);
            }
        }

        void CountWave()
        {
            finishedWaves++;
            if (finishedWaves < waves.Length) return;
            allWavesEnd.Invoke();
            finishedWaves = 0;
        }
    }
}
using _Game.Scripts.Services.ScoreSystem;
using TMPro;
using UnityEngine;

namespace _Game.Scripts.Utils.UI
{
    public class WaveHUD : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI scoreText;
        [SerializeField] TextMeshProUGUI remainingEnemies;
        [SerializeField] TextMeshProUGUI wave;
        [SerializeField] int maxWave;
        int _currentEnemies;

        void OnEnable()
        {
            scoreText.text = "0";
            OnWaveStart(0);
            CurrentEnimies = 0;
        }

        public void OnEnemieDeath()
        {
            CurrentEnimies--;
        }

        public int CurrentEnimies
        {
            get => _currentEnemies;
            set
            {
                remainingEnemies.text = value.ToString();
                _currentEnemies = value;
            }
        }

        public void OnWaveStart(int waveNumber)
        {
            wave.text = $"Onda {waveNumber} de {maxWave}";
        }

        public void OnScoreMarked(Score score)
        {
            scoreText.text = score.points.ToString();
        }
    }
}
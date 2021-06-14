using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.Scripts.Services.SceneState.UI
{
    public class PlayerProgressView : MonoBehaviour
    {
        public TextMeshProUGUI name;
        public TextMeshProUGUI description;
        public TextMeshProUGUI progressPercentage;
        public Image cover;
        [SerializeField] PlayerProgress progress;

        void Start()
        {
            progress.playerProgressChanged.AddListener(progress => UpdateUI(progress));
            UpdateUI(progress);
        }

        void UpdateUI(PlayerProgress progress)
        {
            cover.type = Image.Type.Sliced;
            if (progress.CurrentScene == null || progress.CurrentScene.name == "" || progress.CurrentScene.name == null)
            {
                name.text = "Sem dados armazenados";
                description.text = "Inicia uma aventura para ver seu progresso";
                cover.color = new Color(255,255,255,0);
                progressPercentage.text = "0%";
            }
            else
            {
                var data = progress.CurrentScene;
                name.text = data.name;
                description.text = data.description;
                cover.sprite = data.cover;
                cover.color = new Color(255,255,255,255);
                progressPercentage.text = $"{data.progress}%";
            }
        }
    }
}
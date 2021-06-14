using _Game.GameModules.UI.Scripts;
using _Game.Scripts.Services.EventSystem.Custom.Scene;
using UnityEngine;
using UnityEngine.Events;

namespace _Game.Scripts.Services
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] GameData data;
        public UnityEvent start;

        void Start()
        {
            start.Invoke();
        }

        public void OnLoadSceneRequested(SceneData sceneData)
        {
            data.RequestedSceneIndex = sceneData.index;
            LoadingScene.Load();
        }

        public void RequestScene(int index)
        {
            data.RequestedSceneIndex = index;
            LoadingScene.Load();
        }

        public void ExitGame()
        {
            Application.Quit();
        }
    }
}
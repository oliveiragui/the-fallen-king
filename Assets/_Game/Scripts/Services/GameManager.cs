using _Game.GameModules.UI.Scripts;
using _Game.Scripts.Services.EventSystem.Custom.Scene;
using UnityEngine;

namespace _Game.Scripts.Services
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] GameData data;

        public void OnLoadSceneRequested(SceneData sceneData)
        {
            data.RequestedSceneIndex = sceneData.index;
            LoadingScene.Load();
        }

        public void ExitGame()
        {
            Application.Quit();
        }
    }
}
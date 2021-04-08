using System.Collections;
using _Game.Scripts.UI;
using _Game.Scripts.Utils.Events;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Game.Scripts.Components
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
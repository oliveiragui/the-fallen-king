using System;
using System.Collections.Generic;
using _Game.Scripts.Services.SceneState;
using Michsky.LSS;
using UnityEngine;

namespace _Game.Scripts.Utils
{
    public class LoadingManagerHelper : MonoBehaviour
    {
        [SerializeField] LoadingScreenManager loadingScreenManager;
        [SerializeField] PlayerProgress playerProgress;

        [SerializeField] SceneData[] sceneData;

        Dictionary<string, SceneData> indexedSceneData;

        void Start()
        {
            indexedSceneData = new Dictionary<string, SceneData>();
            foreach (var data in sceneData) indexedSceneData.Add(data.name, data);
        }

        public void Load(string sceneName)
        {
            var scene = indexedSceneData[sceneName];
            loadingScreenManager.SetStyle(scene.style);
            loadingScreenManager.LoadScene(scene.name);
        }

        public void LoadLastScene()
        {
            Load(playerProgress.CurrentScene == null || playerProgress.CurrentScene.name == "" ||
                 playerProgress.CurrentScene.name == null
                ? "Village"
                : playerProgress.CurrentScene.sceneName);
        }
    }

    [Serializable]
    public class SceneData
    {
        public string name;
        public string style;
    }
}
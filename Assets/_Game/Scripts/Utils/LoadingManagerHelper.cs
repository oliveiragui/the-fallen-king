using System;
using System.Collections.Generic;
using Michsky.LSS;
using UnityEngine;

namespace _Game.Scripts.Utils
{
    public class LoadingManagerHelper : MonoBehaviour
    {
        [SerializeField] LoadingScreenManager loadingScreenManager;

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
    }

    [Serializable]
    public class SceneData
    {
        public string name;
        public string style;
    }
}
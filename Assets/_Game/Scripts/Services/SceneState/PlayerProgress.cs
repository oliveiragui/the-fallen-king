using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace _Game.Scripts.Services.SceneState
{
    public class PlayerProgress : MonoBehaviour
    {
        [SerializeField] List<SceneData> storiesScene;
        [NonSerialized] public PlayerProgressChangedEvent playerProgressChanged = new PlayerProgressChangedEvent();
        public SceneData CurrentScene;

        public float Progress => CurrentScene != null ? CurrentScene.progress : 0;

        public void ChangeScene(string sceneName)
        {
            var foundedData = storiesScene.First(data => data.sceneName == sceneName);
            CurrentScene = foundedData;
            SaveData();
            playerProgressChanged.Invoke(this);
        }

        public void DeletePersistentData()
        {
            CurrentScene = null;
            SaveData();
        }

        void Awake()
        {
            CurrentScene = null;
            LoadData();
        }

        void Start()
        {
            playerProgressChanged.Invoke(this);
        }

        public void SaveData()
        {
            SaveManager.SaveData("PlayerProgress", this);
            playerProgressChanged.Invoke(this);
        }

        public void LoadData()
        {
            SaveManager.LoadDataTo("PlayerProgress", this);
        }
    }

    [Serializable]
    public class PlayerProgressChangedEvent : UnityEvent<PlayerProgress> { }

    [Serializable]
    public class SceneData
    {
        public string name;
        public string sceneName;
        public int progress;
        public string description;
        public Sprite cover;
    }
}
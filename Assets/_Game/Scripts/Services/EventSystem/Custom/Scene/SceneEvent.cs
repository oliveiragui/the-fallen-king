using System;
using UnityEngine;

namespace _Game.Scripts.Services.EventSystem.Custom.Scene
{
    [CreateAssetMenu(fileName = "New Scene Event", menuName = "GameContent/Events/SceneEvent")]
    public class SceneEvent : GameEvent<SceneData> { }

    [Serializable]
    public class SceneData
    {
        public int index;
        public int name;
    }
}
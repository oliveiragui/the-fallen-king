using System;
using _Game.Scripts.Components.EventSystem;
using UnityEngine;

namespace _Game.Scripts.Utils.Events
{
    [CreateAssetMenu(fileName = "New Scene Event", menuName = "GameContent/Events/SceneEvent")]
    public class SceneEvent : GameEvent<SceneData>
    {
    }

    [Serializable]
    public class SceneData
    {
        public int index;
        public int name;
    }
}
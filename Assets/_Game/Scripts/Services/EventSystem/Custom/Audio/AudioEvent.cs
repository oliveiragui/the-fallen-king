using System;
using _Game.Scripts.Components.EventSystem;
using UnityEngine;

namespace _Game.Scripts.Utils.Events
{
    [CreateAssetMenu(fileName = "New Audio Event", menuName = "GameContent/Events/AudioEvent")]
    public class AudioEvent : GameEvent<AudioEventData> { }

    [Serializable]
    public class AudioEventData
    {
        public string parameterName;
        public float value;
    }
}
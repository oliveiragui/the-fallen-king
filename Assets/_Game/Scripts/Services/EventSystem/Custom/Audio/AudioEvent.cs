using System;
using UnityEngine;

namespace _Game.Scripts.Services.EventSystem.Custom.Audio
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
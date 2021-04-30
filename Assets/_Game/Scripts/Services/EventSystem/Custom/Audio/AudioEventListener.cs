﻿using System;
using UnityEngine;
using UnityEngine.Events;

namespace _Game.Scripts.Services.EventSystem.Custom.Audio
{
    public class AudioEventListener : MonoBehaviour, IGameEventListener<AudioEventData>
    {
        // The game event instance to register to.
        public AudioEvent audioEvent;

        // The unity event responce created for the event.
        public UnityAudioEvent Response;

        void OnEnable()
        {
            audioEvent.RegisterListener(this);
        }

        void OnDisable()
        {
            audioEvent.UnregisterListener(this);
        }

        public void RaiseEvent(AudioEventData data)
        {
            Response.Invoke(data);
        }
    }

    [Serializable]
    public class UnityAudioEvent : UnityEvent<AudioEventData> { }
}
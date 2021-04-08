﻿using System;
using _Game.Scripts.Components.EventSystem;
using UnityEngine;
using UnityEngine.Events;

namespace _Game.Scripts.Utils.Events
{
    public class SceneEventListener : MonoBehaviour, IGameEventListener<SceneData>
    {
        // The game event instance to register to.
        public SceneEvent GenericEvent;

        // The unity event responce created for the event.
        public UnitySceneEvent Response;

        void OnEnable()
        {
            GenericEvent.RegisterListener(this);
        }

        void OnDisable()
        {
            GenericEvent.UnregisterListener(this);
        }

        public void RaiseEvent(SceneData data)
        {
            Response.Invoke(data);
        }
    }

    [Serializable]
    public class UnitySceneEvent : UnityEvent<SceneData> { }
}
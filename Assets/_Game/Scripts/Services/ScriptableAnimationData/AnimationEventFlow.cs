using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using _Game.Scripts.Runtime.Reorderable;
using _Game.Scripts.Runtime.Reorderable.Attributes;
using _Game.Scripts.Utils.Serializables;
using UnityEngine;

namespace _Game.GameModules.Entities.Scripts
{
    [CreateAssetMenu(fileName = "Animation Clip Flow", menuName = "GameContent/Animation System/Clip Flow", order = 1)]
    public class AnimationEventFlow : ScriptableObject
    {
        [SerializeField] AnimationClip clip;
        [Reorderable] [SerializeField] Test events;
        [NonSerialized] bool _addedEvents = false;

        public AnimationClip Clip
        {
            get
            {
                if (_addedEvents) return clip;
                AddEventToClip();
                return clip;
            }
        }

        void AddEventToClip()
        {
            foreach (var evt in events)
            {
                if (!clip || !evt.ScriptableEvent) return;
                var unityEvent = evt.ScriptableEvent.CreateEvent();
                unityEvent.time = clip.length * evt.PercentageTime / 100;
                clip.AddEvent(unityEvent);
                _addedEvents = true;
            }
        }
    }

    [Serializable]
    public class Test : ReorderableArray<TimedScriptableAnimationEvent> { }

    [Serializable]
    public class TimedScriptableAnimationEvent : ISerializationCallbackReceiver
    {
        [SerializeField] [HideInInspector] string name;
        [SerializeField] [Range(0, 100)] float percentageTime;
        [SerializeField] AnimationEventCreator scriptableEvent;

        public float PercentageTime => percentageTime;
        public AnimationEventCreator ScriptableEvent => scriptableEvent;

        public void OnBeforeSerialize()
        {
            name = scriptableEvent ? scriptableEvent.name : "";
        }

        public void OnAfterDeserialize()
        {
            //     throw new NotImplementedException();
        }
    }
}
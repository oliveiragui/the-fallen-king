using System;
using _Game.Scripts.Utils.Serializables;
using UnityEngine;

namespace _Game.GameModules.Entities.Scripts
{
    [CreateAssetMenu(fileName = "Animation Clip Flow", menuName = "GameContent/Animation System/Clip Flow", order = 1)]
    public class AnimationEventFlow : ScriptableObject
    {
        [SerializeField] AnimationClip clip;
        [SerializeField] ScriptableAnimationEventsDictionary events;
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
                var unityEvent = evt.Value.CreateEvent();
                unityEvent.time = evt.Key;
                clip.AddEvent(unityEvent);
                _addedEvents = true;
            }
        }
    }
}
using System;
using UnityEngine;

namespace _Game.GameModules.Entities.Scripts
{
    [Serializable]
    public class AnimationEventCreator : ScriptableObject
    {
        public virtual AnimationEvent CreateEvent() => new AnimationEvent();
    }
}
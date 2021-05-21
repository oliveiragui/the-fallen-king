﻿using System;
using _Game.GameModules.Entities.Scripts;
using _Game.Scripts.Runtime.SerializableDictionary;

namespace _Game.Scripts.Utils.Serializables
{
    [Serializable]
    public class ScriptableAnimationEventsDictionary : SerializableDictionary<float, AnimationEventCreator> { }
}

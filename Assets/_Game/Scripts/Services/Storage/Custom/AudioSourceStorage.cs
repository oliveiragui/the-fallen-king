using System;
using _Game.Scripts.Runtime.SerializableDictionary;
using _Game.Scripts.Utils.Serializables;
using UnityEngine;

namespace _Game.Scripts.Components.Storage.Custom
{
    public class AudioSourceStorage : ObjectStorage<StringAudioSourceDictionary, AudioSource> { }

    [Serializable]
    public class StringAudioSourceDictionary : SerializableDictionary<string, AudioSource> { }
}
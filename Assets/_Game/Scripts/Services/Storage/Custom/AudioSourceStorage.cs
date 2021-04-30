using System;
using _Game.Scripts.Runtime.SerializableDictionary;
using UnityEngine;

namespace _Game.Scripts.Services.Storage.Custom
{
    public class AudioSourceStorage : ObjectStorage<StringAudioSourceDictionary, AudioSource> { }

    [Serializable]
    public class StringAudioSourceDictionary : SerializableDictionary<string, AudioSource> { }
}
using System;
using _Game.Scripts.Utils.MyBox.Attributes;
using _Game.Scripts.Utils.Serializables;
using UnityEngine;

namespace _Game.Scripts.Components.Storage.Custom
{
    public class AudioStorage : UnityObjectStorage<AudioSource>
    {
        [SerializeField] bool fillWithChidren;

        [ConditionalField("fillWithChidren", true)] [SerializeField]
        Container dictionary;

        void Awake()
        {
            if (fillWithChidren) SetDefault(GetComponentsInChildren<AudioSource>());
            else SetDefault(dictionary.audioSources);
            dictionary = null;
        }

        [Serializable]
        class Container
        {
            public StringAudioSourceDictionary audioSources;
        }
    }
}
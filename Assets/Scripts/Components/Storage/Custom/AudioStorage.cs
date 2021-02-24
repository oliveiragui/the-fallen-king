using System;
using Editor.Scripts.MyBox.Attributes;
using UnityEngine;
using Utils.Serializables;

namespace Components.Storage.Custom
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
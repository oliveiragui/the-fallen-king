using System;
using _Game.Scripts.Utils.MyBox.Attributes;
using _Game.Scripts.Utils.Serializables;
using UnityEngine;

namespace _Game.Scripts.Components.Storage.Custom
{
    public class ParticleStorage : UnityObjectStorage<ParticleSystem>
    {
        [SerializeField] bool fillWithChidren;

        [ConditionalField("fillWithChidren", true)] [SerializeField]
        Container dictionary;

        void Awake()
        {
            if (fillWithChidren) SetDefault(GetComponentsInChildren<ParticleSystem>());
            else SetDefault(dictionary.particles);
            dictionary = null;
        }

        [Serializable]
        class Container
        {
            public StringParticleDictionary particles;
        }
    }
}
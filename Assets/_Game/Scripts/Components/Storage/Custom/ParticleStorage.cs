﻿using System;
using UnityEngine;
using Utils.MyBox.Attributes;
using Utils.Serializables;

namespace Components.Storage.Custom
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
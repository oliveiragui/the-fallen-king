﻿using System;
using Editor.Scripts.MyBox.Attributes;
using UnityEngine;
using Utils.Serializables;

namespace Components.Storage.Custom
{
    public class ColliderStorage : UnityObjectStorage<Collider>
    {
        [SerializeField] bool fillWithChidren;

        [ConditionalField("fillWithChidren", true)] [SerializeField]
        Container dictionary;

        void Awake()
        {
            if (fillWithChidren) SetDefault(GetComponentsInChildren<Collider>());
            else SetDefault(dictionary.colliders);
            dictionary = null;
        }

        [Serializable]
        class Container
        {
            public StringColliderDictionary colliders;
        }
    }
}
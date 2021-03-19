﻿using System;
using UnityEngine;
using Utils.MyBox.Attributes;
using Utils.Serializables;

namespace Components.Storage.Custom
{
    public class GameObjectStorage : UnityObjectStorage<GameObject>
    {
        [SerializeField] bool fillWithChidren;

        [ConditionalField("fillWithChidren", true)] [SerializeField]
        Container dictionary;

        void Awake()
        {
            if (fillWithChidren) SetDefault(GetComponentsInChildren<GameObject>());
            else SetDefault(dictionary.gameObjects);
            dictionary = null;
        }

        [Serializable]
        class Container
        {
            public StringGameObjectDictionary gameObjects;
        }
    }
}
using System;
using _Game.Scripts.Utils.MyBox.Attributes;
using _Game.Scripts.Utils.Serializables;
using UnityEngine;

namespace _Game.Scripts.Components.Storage.Custom
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
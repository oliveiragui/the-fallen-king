using System;
using _Game.Scripts.Utils.MyBox.Attributes;
using _Game.Scripts.Utils.Serializables;
using UnityEngine;

namespace _Game.Scripts.Components.Storage.Custom
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
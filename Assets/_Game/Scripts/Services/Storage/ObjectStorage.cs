using _Game.Scripts.Runtime.SerializableDictionary;
using UnityEngine;

namespace _Game.Scripts.Components.Storage
{
    public class ObjectStorage<T, TK> : Storage<T, TK>
        where T : SerializableDictionary<string, TK> where TK : Object
    {
        [SerializeField] bool fillWithChidren;

        void Awake()
        {
            if (!fillWithChidren) return;
            foreach (var component in GetComponentsInChildren<TK>())
                Add(component.name, component);
        }
    }
}
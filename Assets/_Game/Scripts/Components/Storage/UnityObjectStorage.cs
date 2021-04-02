using System.Collections.Generic;
using System.Linq;
using _Game.Scripts.Utils;
using UnityEngine;

namespace _Game.Scripts.Components.Storage
{
    public class UnityObjectStorage<T> : MonoBehaviour
        where T : Object
    {
        public StringToHashDictionary<T> Components { get; } = new StringToHashDictionary<T>();

        public T this[int key]
        {
            get => Components[key];
            set => Components[key] = value;
        }

        public T this[string key]
        {
            get => Components[key];
            set => Components[key] = value;
        }

        public void SetDefault(IEnumerable<KeyValuePair<string, T>> initialValue)
        {
            Components.Clear();
            AddMultiple(initialValue);
        }

        public void SetDefault(IEnumerable<T> initialValue)
        {
            Components.Clear();
            AddMultiple(initialValue);
        }

        public void Add(string key, T value)
        {
            Components.Add(key, value);
        }

        public void Add(T item)
        {
            Components.Add(item.name, item);
        }

        public void AddMultiple(IEnumerable<KeyValuePair<string, T>> items)
        {
            foreach (var item in items) Add(item.Key, item.Value);
        }

        public void AddMultiple(IEnumerable<T> items)
        {
            items.ToList().ForEach(Add);
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using _Game.Scripts.Runtime.SerializableDictionary;
using UnityEngine;

namespace _Game.Scripts.Components.Storage
{
    public class Storage<T, TK> : MonoBehaviour where T : SerializableDictionary<string, TK>
    {
        [SerializeField] T dictionary;

        public TK this[string key]
        {
            get => dictionary[key];
            set => dictionary[key] = value;
        }

        public void Add(string key, TK value)
        {
            dictionary.Add(key, value);
        }

        public void AddMultiple(IEnumerable<KeyValuePair<string, TK>> items)
        {
            foreach (var item in items) Add(item.Key, item.Value);
        }
    }
}
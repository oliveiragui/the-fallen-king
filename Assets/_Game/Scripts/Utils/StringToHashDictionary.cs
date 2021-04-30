using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts.Utils
{
    [Serializable]
    public class StringToHashDictionary<T> : Dictionary<int, T>
    {
        public T this[string key]
        {
            get => this[Animator.StringToHash(key)];
            set => this[Animator.StringToHash(key)] = value;
        }

        public void Add(string key, T value)
        {
            Add(Animator.StringToHash(key), value);
        }

        public bool Remove(string key) => Remove(Animator.StringToHash(key));
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts.Runtime.Reorderable
{
    [Serializable]
    public abstract class ReorderableArray<T> : ICloneable, IList<T>, ICollection<T>, IEnumerable<T>
    {
        [SerializeField] List<T> array = new List<T>();

        public ReorderableArray()
            : this(0) { }

        public ReorderableArray(int length)
        {
            array = new List<T>(length);
        }

        public int Length => array.Count;

        public object Clone() => new List<T>(array);

        public T this[int index]
        {
            get => array[index];
            set => array[index] = value;
        }

        public bool IsReadOnly => false;

        public int Count => array.Count;

        public bool Contains(T value) => array.Contains(value);

        public int IndexOf(T value) => array.IndexOf(value);

        public void Insert(int index, T item)
        {
            array.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            array.RemoveAt(index);
        }

        public void Add(T item)
        {
            array.Add(item);
        }

        public void Clear()
        {
            array.Clear();
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            this.array.CopyTo(array, arrayIndex);
        }

        public bool Remove(T item) => array.Remove(item);

        public IEnumerator<T> GetEnumerator() => array.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => array.GetEnumerator();

        public void CopyFrom(IEnumerable<T> value)
        {
            array.Clear();
            array.AddRange(value);
        }

        public T[] ToArray() => array.ToArray();
    }
}
﻿using System;
using Runtime.Reorderable;
using UnityEngine;
using Weapons.Prefab;

namespace Utils.Serializables
{
    [Serializable]
    public class ReorderableAudioSourceList : ReorderableArray<AudioSource> { }

    [Serializable]
    public class ReorderableStringTransformList : ReorderableArray<StringTransformTuple> { }

    [Serializable]
    public class ReorderableWeaponPrefabs : ReorderableArray<WeaponPrefabData> { }

    [Serializable]
    public class StringTransformTuple : StringComponentTuple<Transform> { }

    [Serializable]
    public class StringComponentTuple<T> : Tuple<string, T> where T : Component { }

    [Serializable]
    public class Tuple<T, TK>
    {
        [SerializeField] T key;
        [SerializeField] TK value;
        public T Key => key;
        public TK Value => value;
    }
}
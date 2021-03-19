﻿using UnityEditor;
using UnityEngine;
using Utils.Serializables;

namespace Utils.SerializableDictionary.Example.Editor
{
    [CustomPropertyDrawer(typeof(StringAudioSourceDictionary))]
    [CustomPropertyDrawer(typeof(StringColliderDictionary))]
    [CustomPropertyDrawer(typeof(StringParticleDictionary))]
    [CustomPropertyDrawer(typeof(StringGameObjectDictionary))]
    public class AnySerializableDictionaryPropertyDrawer : SerializableDictionaryPropertyDrawer { }
}
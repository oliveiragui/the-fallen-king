using _Game.Scripts.Components.Storage.Custom;
using _Game.Scripts.Utils.Serializables;
using UnityEditor;
using UnityEngine;

namespace Utils.SerializableDictionary.Example.Editor
{
    [CustomPropertyDrawer(typeof(StringAudioSourceDictionary))]
    [CustomPropertyDrawer(typeof(StringColliderDictionary))]
    [CustomPropertyDrawer(typeof(StringParticleDictionary))]
    [CustomPropertyDrawer(typeof(StringGameObjectDictionary))]
    public class AnySerializableDictionaryPropertyDrawer : SerializableDictionaryPropertyDrawer { }
}
using System;
using _Game.Scripts.Runtime.SerializableDictionary;
using UnityEngine;

namespace _Game.Scripts.Services.Storage.Custom
{
    public class GameObjectStorage : ObjectStorage<StringGameObjectDictionary, GameObject> { }

    [Serializable]
    public class StringGameObjectDictionary : SerializableDictionary<string, GameObject> { }
}
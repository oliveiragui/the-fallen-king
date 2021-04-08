using System;
using _Game.Scripts.Runtime.SerializableDictionary;
using _Game.Scripts.Utils.MyBox.Attributes;
using _Game.Scripts.Utils.Serializables;
using UnityEngine;

namespace _Game.Scripts.Components.Storage.Custom
{
    public class GameObjectStorage : ObjectStorage<StringGameObjectDictionary,GameObject>
    {
    }
    
    [Serializable]
    public class StringGameObjectDictionary : SerializableDictionary<string, GameObject> { }
}
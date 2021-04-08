using System;
using _Game.Scripts.Runtime.SerializableDictionary;
using UnityEngine;

namespace _Game.Scripts.Components.Storage.Custom
{
    public class ColliderStorage : ObjectStorage<StringColliderDictionary, Collider> { }
    
    [Serializable]
    public class StringColliderDictionary : SerializableDictionary<string, Collider> { }

}
using System;
using _Game.Scripts.Runtime.SerializableDictionary;
using _Game.Scripts.Utils.MyBox.Attributes;
using _Game.Scripts.Utils.Serializables;
using UnityEngine;

namespace _Game.Scripts.Components.Storage.Custom
{
    public class ParticleStorage : ObjectStorage<StringParticleDictionary, ParticleSystem> { }

    [Serializable]
    public class StringParticleDictionary : SerializableDictionary<string, ParticleSystem> { }
}
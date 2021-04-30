using System;
using _Game.Scripts.Runtime.SerializableDictionary;
using UnityEngine;

namespace _Game.Scripts.Services.Storage.Custom
{
    public class ParticleStorage : ObjectStorage<StringParticleDictionary, ParticleSystem> { }

    [Serializable]
    public class StringParticleDictionary : SerializableDictionary<string, ParticleSystem> { }
}
using System;
using _Game.Scripts.Runtime.SerializableDictionary;
using UnityEngine;

namespace _Game.Scripts.Utils.Serializables
{
    // [Serializable]
    // public class AudioSom : SerializableDictionary<SlotSom, AudioSource> { }
    //
    // [Serializable]
    // public class SlotArmasPrefabs : SerializableDictionary<SlotMesh, GameObject> { }

    [Serializable]
    public class StringAudioSourceDictionary : SerializableDictionary<string, AudioSource> { }

    [Serializable]
    public class StringColliderDictionary : SerializableDictionary<string, Collider> { }

    [Serializable]
    public class StringParticleDictionary : SerializableDictionary<string, ParticleSystem> { }

    [Serializable]
    public class StringGameObjectDictionary : SerializableDictionary<string, GameObject> { }
}
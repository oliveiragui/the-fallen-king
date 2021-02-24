using System;
using Components.AttributeSystem;
using UnityEngine;

namespace Collection.Status.Player
{
    [Serializable]
    public class PlayerStatusModel
    {
        [SerializeField] public RawAttribute vida;
        [SerializeField] public RawAttribute forca;
        [SerializeField] public RawAttribute agilidade;
        [SerializeField] public RawAttribute velocidade;

        public RawAttribute Vida => vida;
        public RawAttribute Forca => forca;
        public RawAttribute Agilidade => agilidade;
        public RawAttribute Velocidade => velocidade;
    }
}
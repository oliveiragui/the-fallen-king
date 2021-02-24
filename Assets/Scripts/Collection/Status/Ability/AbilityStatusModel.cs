using System;
using Components.AttributeSystem;
using UnityEngine;

namespace Collection.Status.Ability
{
    [Serializable]
    public class AbilityStatusModel
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
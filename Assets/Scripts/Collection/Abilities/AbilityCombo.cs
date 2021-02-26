using System;
using Editor.Scripts.MyBox.Attributes;
using UnityEngine;

namespace Collection.Abilities.Collections.Habilidades
{
    [Serializable]
    public class AbilityCombo
    {
        [SerializeField] bool castable;
        [SerializeField] float factor1;

        [ConditionalField("castable")] [SerializeField]
        float factor2;

        [ConditionalField("castable")] [SerializeField]
        float factor3;

        public float Factor3 => factor3;
        public float Factor2 => factor2;
        public float Factor1 => factor1;
        public bool Castable => castable;
    }
}
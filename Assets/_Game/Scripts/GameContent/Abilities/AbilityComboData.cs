﻿using System;
using _Game.Scripts.Components.AttributeSystem;
using _Game.Scripts.GameContent.Entities.Components.Action;
using _Game.Scripts.Utils.MyBox.Attributes;
using UnityEngine;

namespace _Game.Scripts.GameContent.Abilities.Data
{
    [Serializable]
    public class AbilityComboData
    {
        [SerializeField] public RawComboAttributes attributes;
        [SerializeField] public EntityAction action;

        [SerializeField] public bool castable;
        [SerializeField] public float factor1;

        [ConditionalField("castable")] [SerializeField]
        public float factor2;

        [ConditionalField("castable")] [SerializeField]
        public float factor3;

        public RawComboAttributes Attributes => attributes;
        public float Factor3 => factor3;
        public float Factor2 => factor2;
        public float Factor1 => factor1;
        public bool Castable => castable;
    }

    [Serializable]
    public class RawComboAttributes
    {
        [SerializeField] public RawAttribute power;
        [SerializeField] public RawAttribute range;

        public RawAttribute Power => power;
        public RawAttribute Range => range;
    }
}
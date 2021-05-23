using System;
using _Game.GameModules.Entities.Scripts;
using _Game.Scripts.Utils.MyBox.Attributes;
using UnityEngine;

namespace _Game.GameModules.Abilities.Scripts
{
    [Serializable]
    public class Combo
    {
        [SerializeField] bool castable;
        [SerializeField] bool aim;
        [SerializeField] bool applyRootMovement;
        [SerializeField] AnimationEventFlow beginning;
        [SerializeField] float factor1;
        [ConditionalField("castable")] [SerializeField] AnimationEventFlow middle;
        [ConditionalField("castable")] [SerializeField] float factor2;
        [ConditionalField("castable")] [SerializeField] AnimationEventFlow ending;
        [ConditionalField("castable")] [SerializeField] float factor3;

        [SerializeField] AnimationClip endingTransition;
        [SerializeField] float factor4;

        public float Factor4 => factor4;
        public float Factor3 => factor3;
        public float Factor2 => factor2;
        public float Factor1 => factor1;
        public bool Castable => castable;

        public bool Aim => aim;

        public bool ApplyRootMovement => applyRootMovement;

        public AnimationEventFlow BeginningAnimation => beginning;

        public AnimationEventFlow MiddleAnimation => middle;

        public AnimationEventFlow EndingAnimation => ending;

        public AnimationClip EndingTransition => endingTransition;
    }
}
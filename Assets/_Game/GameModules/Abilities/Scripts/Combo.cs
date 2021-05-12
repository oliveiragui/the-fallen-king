using System;
using _Game.Scripts.Utils.MyBox.Attributes;
using UnityEngine;

namespace _Game.GameModules.Abilities.Scripts
{
    [Serializable]
    public class Combo
    {

        [SerializeField] bool castable;
        [SerializeField] AnimationClip beginningAnimation;
        [SerializeField] float factor1;

        [ConditionalField("castable")] [SerializeField] AnimationClip middleAnimation;
        [ConditionalField("castable")] [SerializeField] float factor2;
        [ConditionalField("castable")] [SerializeField] AnimationClip endAnimation;
        [ConditionalField("castable")] [SerializeField] float factor3;
        
        public float Factor3 => factor3;
        public float Factor2 => factor2;
        public float Factor1 => factor1;
        public bool Castable => castable;
        public AnimationClip BeginningAnimation => beginningAnimation;
        public AnimationClip MiddleAnimation => middleAnimation;
        public AnimationClip EndAnimation => endAnimation;
    }
}
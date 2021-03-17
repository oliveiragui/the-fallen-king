using System;
using Characters;
using Entities.Common.Animation;
using UnityEngine;

namespace Entities.Default
{
    [Serializable]
    public class EntityData : MonoBehaviour
    {
        [SerializeField] public Character associatedCharacter;
        [SerializeField] public EntityBaseAnimation animations;
        public float speed;
        public float direction;
        public float lookDiretion;
        public float stoppingDistance;
    }
}
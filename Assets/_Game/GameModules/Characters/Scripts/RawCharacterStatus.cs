using System;
using UnityEngine;
using UnityEngine.Events;
using Attribute = _Game.Scripts.Services.AttributeSystem.Attribute;

namespace _Game.GameModules.Characters.Scripts
{
    [Serializable]
    public class RawCharacterStatus
    {
        [SerializeField] Attribute life;
        [SerializeField] Attribute strength;
        [SerializeField] Attribute agility;

        public Attribute Life => life;
        public Attribute Strength => strength;
        public Attribute Agility => agility;

        public void AddAttributeChangeListener(UnityAction listener)
        {
            Life.attrChanged.AddListener(listener);
            Strength.attrChanged.AddListener(listener);
            Agility.attrChanged.AddListener(listener);
        }

        public void RemoveListener(UnityAction listener)
        {
            Life.attrChanged.RemoveListener(listener);
            Strength.attrChanged.RemoveListener(listener);
            Agility.attrChanged.RemoveListener(listener);
        }
    }
}
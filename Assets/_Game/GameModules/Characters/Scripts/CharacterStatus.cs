using System;
using UnityEngine;
using UnityEngine.Events;
using Attribute = _Game.Scripts.Services.AttributeSystem.Attribute;

namespace _Game.GameModules.Characters.Scripts
{
    public class CharacterStatus : MonoBehaviour
    {
        [SerializeField] RawCharacterStatus rawCharacterStatus;

        public StatusChangedEvent StatusChanged; //= new StatusChangedEvent();
        bool _dirty;

        public Attribute Life => rawCharacterStatus.Life;
        public Attribute Strength => rawCharacterStatus.Strength;
        public Attribute Agility => rawCharacterStatus.Agility;

        void Awake()
        {
            rawCharacterStatus.AddAttributeChangeListener(OnAttrChanged);
            Life.OnAttrChanged();
            Life.Current = Life.Total;
            Strength.OnAttrChanged();
            Strength.Current = Strength.Total;
            Agility.OnAttrChanged();
            Agility.Current = Agility.Total;
        }

        void Update()
        {
            if (!_dirty) return;
            _dirty = false;
            StatusChanged.Invoke(this);
        }

        void OnAttrChanged()
        {
            _dirty = true;
        }
    }

    [Serializable]
    public class StatusChangedEvent : UnityEvent<CharacterStatus> { }
}
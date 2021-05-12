using System;
using System.Collections.Generic;
using System.Linq;
using _Game.Scripts.Utils.Serializables;
using UnityEngine;
using UnityEngine.Events;

namespace _Game.Scripts.Services.AttributeSystem
{
    [Serializable]
    public class Attribute
    {
        public UnityEvent attrChanged = new UnityEvent();
        [NonSerialized] List<Attribute> _bonuses = new List<Attribute>();

        [SerializeField] float initial;
        [SerializeField] [ReadOnlyField] float total;
        [SerializeField] [ReadOnlyField] float current;

        public Attribute() : this(0) { }

        public Attribute(float raw)
        {
            Raw = raw;
            total = raw;
            current = total;
        }

        public float Raw
        {
            get => initial;
            set => initial = Mathf.Max(value, 0);
        }

        public float Total
        {
            get => total;
            private set
            {
                total = Mathf.Max(value, 0);
                current = Mathf.Clamp(current, 0, total);
                attrChanged.Invoke();
            }
        }

        public float Current
        {
            get => current;
            set
            {
                current = Mathf.Clamp(value, 0, total);
                attrChanged.Invoke();
            }
        }

        public void AddBonus(Attribute bonus)
        {
            if (_bonuses.Contains(bonus)) return;
            _bonuses.Add(bonus);
            bonus.attrChanged.AddListener(OnAttrChanged);
            OnAttrChanged();
        }

        public void RemoveBonus(Attribute bonus)
        {
            if (!_bonuses.Contains(bonus)) return;
            _bonuses.Remove(bonus);
            bonus.attrChanged.RemoveListener(OnAttrChanged);
            OnAttrChanged();
        }

        public void OnAttrChanged()
        {
            Total = initial + _bonuses.Sum(bonus1 => bonus1.Current);
        }
    }
}
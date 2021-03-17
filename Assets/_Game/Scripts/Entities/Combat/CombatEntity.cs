using System;
using CombatSystem;
using Entities.Common;
using Entities.Default;
using UnityEngine;
using UnityEngine.Events;

namespace Entities.Combat
{
    public class CombatEntity : MonoBehaviour
    {
        public EntityComponents components;
        public CombatEntityData combatData;
        public EntityData defaultData;
        public CombatEvents events;
    }

    [Serializable]
    public class CombatEvents
    {
        [SerializeField] public AbilityEnterEvent onAbilityEntered;
        [SerializeField] public AbilityExitEvent onAbilityExited;
        [SerializeField] public ComboEnterEvent onComboEntered;
        [SerializeField] public ComboExitEvent onComboExited;
        [SerializeField] public HitReceiveEvent onHitReceived;
    }

    [Serializable]
    public class AbilityEnterEvent : UnityEvent<int> { }

    [Serializable]
    public class AbilityExitEvent : UnityEvent { }

    [Serializable]
    public class ComboEnterEvent : UnityEvent<int> { }

    [Serializable]
    public class ComboExitEvent : UnityEvent { }

    [Serializable]
    public class HitReceiveEvent : UnityEvent<AbilityHit> { }
}
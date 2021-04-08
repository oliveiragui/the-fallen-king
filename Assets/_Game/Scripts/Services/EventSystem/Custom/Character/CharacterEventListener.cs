using System;
using _Game.Scripts.Components.EventSystem;
using _Game.Scripts.GameContent.Characters;
using UnityEngine;
using UnityEngine.Events;

namespace _Game.Scripts.Utils.Events
{
    public class CharacterEventListener : MonoBehaviour, IGameEventListener<Character>
    {
        // The game event instance to register to.
        public CharacterEvent GenericEvent;

        // The unity event responce created for the event.
        public UnityCharacterEvent Response;

        void OnEnable()
        {
            GenericEvent.RegisterListener(this);
        }

        void OnDisable()
        {
            GenericEvent.UnregisterListener(this);
        }

        public void RaiseEvent(Character character)
        {
            Response.Invoke(character);
        }
    }

    [Serializable]
    public class UnityCharacterEvent : UnityEvent<Character> { }
}
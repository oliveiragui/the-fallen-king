using System;
using UnityEngine;
using UnityEngine.Events;

namespace _Game.Scripts.Services.EventSystem.Custom.Character
{
    public class CharacterEventListener : MonoBehaviour, IGameEventListener<GameModules.Characters.Scripts.Character>
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

        public void RaiseEvent(GameModules.Characters.Scripts.Character character)
        {
            Response.Invoke(character);
        }
    }

    [Serializable]
    public class UnityCharacterEvent : UnityEvent<GameModules.Characters.Scripts.Character> { }
}
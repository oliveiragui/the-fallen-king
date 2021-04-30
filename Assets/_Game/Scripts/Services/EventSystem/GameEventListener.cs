using UnityEngine;
using UnityEngine.Events;

namespace _Game.Scripts.Services.EventSystem
{
    public class GameEventListener : MonoBehaviour, IGameEventListener
    {
        // The game event instance to register to.
        public GameEvent GameEvent;

        // The unity event responce created for the event.
        public UnityEvent Response;

        void OnEnable()
        {
            GameEvent.RegisterListener(this);
        }

        void OnDisable()
        {
            GameEvent.UnregisterListener(this);
        }

        public void RaiseEvent()
        {
            Response.Invoke();
        }
    }
}
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts.Components.EventSystem
{
    [CreateAssetMenu(fileName = "New Game Event", menuName = "GameContent/Events/GameEvent")]
    public class GameEvent : ScriptableObject
    {
        List<IGameEventListener> listeners = new List<IGameEventListener>();

        public void RegisterListener(IGameEventListener listener)
        {
            listeners.Add(listener);
        }

        public void UnregisterListener(IGameEventListener listener)
        {
            listeners.Remove(listener);
        }

        public void Raise()
        {
            foreach (var listener in listeners) listener.RaiseEvent();
        }
    }

    public class GameEvent<T> : ScriptableObject
    {
        List<IGameEventListener<T>> listeners = new List<IGameEventListener<T>>();

        public void RegisterListener(IGameEventListener<T> listener)
        {
            listeners.Add(listener);
        }

        public void UnregisterListener(IGameEventListener<T> listener)
        {
            listeners.Remove(listener);
        }

        public void Raise(T data)
        {
            foreach (var listener in listeners) listener.RaiseEvent(data);
        }
    }
}
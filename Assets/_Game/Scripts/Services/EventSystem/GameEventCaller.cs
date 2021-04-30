using UnityEngine;

namespace _Game.Scripts.Services.EventSystem
{
    public class GameEventCaller<T, TK> : MonoBehaviour where T : GameEvent<TK>
    {
        [SerializeField] protected TK data;

        public void SetData(TK data)
        {
            this.data = data;
        }

        public void Call(T gameEvent)
        {
            gameEvent.Raise(data);
        }
    }
}
using UnityEngine;

namespace _Game.Scripts.Utils.Events
{
    public class GameEventCaller<T, TK> : MonoBehaviour where T : GameEvent<TK>
    {
        [SerializeField] TK data;

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
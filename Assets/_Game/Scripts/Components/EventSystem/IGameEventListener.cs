namespace _Game.Scripts.Utils.Events
{
    public interface IGameEventListener<in T>
    {
        void RaiseEvent(T data);
    }
    
    public interface IGameEventListener
    {
        void RaiseEvent();
    }
}
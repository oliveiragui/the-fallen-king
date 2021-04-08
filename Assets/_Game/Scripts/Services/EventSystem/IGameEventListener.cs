namespace _Game.Scripts.Components.EventSystem
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
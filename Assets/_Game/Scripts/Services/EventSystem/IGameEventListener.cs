namespace _Game.Scripts.Services.EventSystem
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
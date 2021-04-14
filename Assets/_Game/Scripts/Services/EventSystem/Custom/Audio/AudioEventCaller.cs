using _Game.Scripts.Components.EventSystem;

namespace _Game.Scripts.Utils.Events
{
    public class AudioEventCaller : GameEventCaller<AudioEvent, AudioEventData>
    {
        public void SetValue(float value) => data.value = value;
    }
}
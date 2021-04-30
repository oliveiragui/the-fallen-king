namespace _Game.Scripts.Services.EventSystem.Custom.Audio
{
    public class AudioEventCaller : GameEventCaller<AudioEvent, AudioEventData>
    {
        public void SetValue(float value) => data.value = value;
    }
}
using _Game.Scripts.Services.EventSystem.Custom.Audio;
using UnityEngine;
using UnityEngine.Audio;

namespace _Game.Scripts.Utils
{
    public class AudioManager : MonoBehaviour
    {
        public static readonly string ambience = "AmbienceVol";
        public static readonly string fx = "FXVol";
        public static readonly string bgMusic = "BgMusicVol";
        public static readonly string dialog = "DialogVol";
        public static readonly string interfaceVol = "InterfaceVol";
        public static readonly string master = "MasterVol";
        [SerializeField] AudioMixer mixer;

        public float MasterVol
        {
            get => GetFloat(master);
            set => SetFloat(master, value);
        }

        public float AmbienceVol
        {
            get => GetFloat(ambience);
            set => SetFloat(ambience, value);
        }

        public float FxVol
        {
            get => GetFloat(fx);
            set => SetFloat(fx, value);
        }

        public float BgMusicVol
        {
            get => GetFloat(bgMusic);
            set => SetFloat(bgMusic, value);
        }

        public float InterfaceVol
        {
            get => GetFloat(interfaceVol);
            set => SetFloat(interfaceVol, value);
        }

        public float DialogVol
        {
            get => GetFloat(dialog);
            set => SetFloat(dialog, value);
        }

        public void SetVolume(AudioEventData data)
        {
            SetFloat(data.parameterName, data.value);
        }

        float GetFloat(string value)
        {
            mixer.GetFloat(value, out float parameter);
            return parameter;
        }

        void SetFloat(string parameter, float value)
        {
            mixer.SetFloat(parameter, value);
        }
    }
}
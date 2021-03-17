using System.Collections;
using UnityEngine;

namespace Utils
{
    [AddComponentMenu("Audio/Audio Pitch And Volume Randomizer")]
    [RequireComponent(typeof(AudioSource))]
    [DisallowMultipleComponent]
    public class AudioPitchAndVolumeRandomizer : MonoBehaviour
    {
        [SerializeField] [Range(0, 1)] float randomVolumeVariation;
        [SerializeField] [Range(0, 6)] float randomPitchVariation;

        Coroutine _coroutine;
        float initialPitch;
        float initialVolume;
        AudioSource source;

        void Awake()
        {
            source = GetComponent<AudioSource>();
        }

        void OnEnable()
        {
            if (_coroutine != null) StopCoroutine(_coroutine);
            StartCoroutine(Randomize());
        }

        void OnDisable()
        {
            if (_coroutine != null) StopCoroutine(_coroutine);
            source.pitch = initialPitch;
            source.volume = initialVolume;
        }

        public void SetRange(float pitch, float volume)
        {
            randomPitchVariation = pitch;
            randomVolumeVariation = volume;
        }

        IEnumerator Randomize()
        {
            initialPitch = source.pitch;
            initialVolume = source.volume;
            while (enabled)
            {
                yield return new WaitUntil(() => source.isPlaying);
                RandomizePitchAndVolume(source);
                float time = 0;
                yield return new WaitWhile(() =>
                {
                    time += Time.deltaTime;
                    return source.isPlaying && source.clip.length > time;
                });
            }
        }

        void RandomizePitchAndVolume(AudioSource source)
        {
            source.pitch = Normalize(Variation(initialPitch, randomPitchVariation), -3, 3);
            source.volume = Normalize(Variation(initialVolume, randomVolumeVariation), 0, 1);
        }

        static float Variation(float initialValue, float range)
        {
            return initialValue + Random.Range(-range, range) / 2;
        }

        static float Normalize(float value, float left, float right)
        {
            if (value < left) return 2 * left - value;
            if (value > right) return 2 * right - value;
            return value;
        }
    }
}
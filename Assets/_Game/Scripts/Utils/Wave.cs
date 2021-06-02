using System;
using _Game.GameModules.Characters.Scripts;
using UnityEngine;
using UnityEngine.Events;

namespace _Game.Scripts.Utils
{
    [Serializable]
    public class Wave : MonoBehaviour
    {
        [SerializeField] Character[] characters;
        [SerializeField] UnityEvent start;
        [SerializeField] UnityEvent end;
        [SerializeField] int deadCharacters;

        public bool IsRunning { get; private set; }

        public void Play()
        {
            foreach (var character in characters)
            {
                character.UsePivot(false);
                character.events.death.AddListener(OnCharacterDeath);
            }

            IsRunning = true;
            start.Invoke();
        }

        void OnCharacterDeath(Character charac)
        {
            deadCharacters++;
            if (deadCharacters >= characters.Length) Stop();
        }

        public void Stop()
        {
            foreach (var character in characters)
                character.events.death.RemoveListener(OnCharacterDeath);
            deadCharacters = 0;
            end.Invoke();
            IsRunning = false;
        }
    }
}
using _Game.GameModules.Characters.Scripts;
using UnityEngine;
using UnityEngine.Events;

namespace _Game.Scripts.Utils
{
    public class WatchLifeChange : MonoBehaviour
    {
        [SerializeField] int limitValue;
        [SerializeField] UnityEvent attributeBellowTargetValue;

        public void OnStatusChanged(CharacterStatus status)
        {
            if (limitValue > status.Life.Current) attributeBellowTargetValue.Invoke();
        }
        
    }
}
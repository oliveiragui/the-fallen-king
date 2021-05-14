using _Game.GameModules.Characters.Scripts;
using _Game.GameModules.UI.Scripts.Utils;
using UnityEngine;

namespace _Game.GameModules.UI.Scripts.HUD
{
    public class PlayerLife : MonoBehaviour, ICharacterStatusChangeListener
    {
        [SerializeField] Lifebar lifebar;

        public void OnStatusChange(CharacterStatus status)
        {
            lifebar.Total = status.Life.Total;
            lifebar.Current = status.Life.Current;
        }
    }
}
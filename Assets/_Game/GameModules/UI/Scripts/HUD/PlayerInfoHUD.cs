using _Game.GameModules.Abilities.Scripts;
using _Game.GameModules.Characters.Scripts;
using _Game.GameModules.UI.Scripts.Utils;
using UnityEngine;

namespace _Game.GameModules.UI.Scripts.HUD
{
    public class PlayerInfoHUD : MonoBehaviour
    {
        [SerializeField] Lifebar lifebar;
        [SerializeField] AbilityFeedbackPanel abilityFeedback;

        public void UpdateUI(CharacterStatus characterStatus, Ability[] abilities)
        {
            UpdateUI(characterStatus);
            UpdateUI(abilities);
        }

        public void UpdateUI(CharacterStatus characterStatus)
        {
            lifebar.Total = characterStatus.Life.Total;
            lifebar.Current = characterStatus.Life.Current;
        }

        public void UpdateUI(Ability[] abilities)
        {
            abilityFeedback.OnWeaponChange(abilities);
        }
    }
}
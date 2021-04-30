using _Game.Scripts.GameContent.Abilities;
using _Game.Scripts.Services.AttributeSystem;
using _Game.Scripts.UI.Utils;
using UnityEngine;

namespace _Game.Scripts.UI.HUD
{
    public class PlayerInfoHUD : MonoBehaviour
    {
        [SerializeField] Lifebar lifebar;
        [SerializeField] AbilityFeedbackPanel abilityFeedback;

        public void UpdateUI(Status status, Ability[] abilities)
        {
            UpdateUI(status);
            UpdateUI(abilities);
        }

        public void UpdateUI(Status status)
        {
            lifebar.Total = status.Life.Total;
            lifebar.Current = status.Life.Current;
        }

        public void UpdateUI(Ability[] abilities)
        {
            abilityFeedback.OnWeaponChange(abilities);
        }
    }
}
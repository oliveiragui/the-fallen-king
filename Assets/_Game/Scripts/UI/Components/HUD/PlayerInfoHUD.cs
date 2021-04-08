using _Game.Scripts.Components.AttributeSystem;
using _Game.Scripts.GameContent.Abilities;
using _Game.Scripts.GameContent.Characters;
using _Game.Scripts.GameContent.Weapons;
using _Game.Scripts.UI.Components.HUD;
using _Game.Scripts.UI.StatusBar;
using UnityEngine;

namespace _Game.Scripts.UI
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
            abilityFeedback.UpdateUI(abilities);
        }
    }
}
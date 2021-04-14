using System.Collections.Generic;
using _Game.Scripts.GameContent.Abilities;
using UnityEngine;

namespace _Game.Scripts.UI.Components.HUD
{
    public class AbilityFeedbackPanel : MonoBehaviour
    {
        [SerializeField] AbilityIcon[] abilityIcons;

        public string[] buttonsName = {"X", "Y", "B", "A"};

        public void UpdateUI(Ability[] abilities)
        {
            
            for (int i = 0; i < abilities.Length && i < buttonsName.Length && i < abilityIcons.Length; i++)
            {
                abilityIcons[i].UpdateUI(abilities[i], buttonsName[i]);
            }
        }
    }
}
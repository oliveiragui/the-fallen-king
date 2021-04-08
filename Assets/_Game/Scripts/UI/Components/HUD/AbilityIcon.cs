using _Game.Scripts.GameContent.Abilities;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.Scripts.UI
{
    public class AbilityIcon: MonoBehaviour
    {
        public Image icon;
        public TextMeshProUGUI buttonIcon;
        
        public void UpdateUI(Ability ability, string buttonName)
        {
            buttonIcon.text = $"<sprite=\"XboxOne\" name=\"XboxOne_{buttonName}\">";
            icon.sprite = ability.Sprite;
        }
    }
}
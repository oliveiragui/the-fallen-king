using _Game.Scripts.GameContent.Abilities;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.Scripts.UI.Pages.CharacterMenu.Tabs.Inventory
{
    public class SkillInfoView : MonoBehaviour
    {
        public TextMeshProUGUI title;
        public TextMeshProUGUI description;
        public TextMeshProUGUI buttonIcon;
        public Image icon;

        public void UpdateUI(AbilityData ability, string buttonName)
        {
            if (ability == null) return;

            title.text = ability.Name;
            description.text = ability.Description;
            buttonIcon.text = $"<sprite=\"XboxOne\" name=\"XboxOne_{buttonName}\">";
            icon.sprite = ability.Icon;
        }
    }
}
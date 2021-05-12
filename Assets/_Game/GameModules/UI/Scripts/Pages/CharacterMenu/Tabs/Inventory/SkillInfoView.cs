using _Game.GameModules.Abilities.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.GameModules.UI.Scripts.Pages.CharacterMenu.Tabs.Inventory
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
            description.text = ability.MetaData.Description;
            buttonIcon.text = $"<sprite=\"XboxOne\" name=\"XboxOne_{buttonName}\">";
            icon.sprite = ability.MetaData.Icon;
        }
    }
}
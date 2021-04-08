using _Game.Scripts.GameContent.Abilities;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.Scripts.UI
{
    public class SkillInfoView: MonoBehaviour
    {
        public TextMeshProUGUI title;
        public TextMeshProUGUI description;
        public TextMeshProUGUI buttonIcon;
        public Image icon;

        public void UpdateUI(Ability ability, string buttonName)
        {
            title.text = ability.Name;
            description.text = ability.Description;
            buttonIcon.text = $"<sprite=\"XboxOne\" name=\"XboxOne_{buttonName}\">";
            icon.sprite = ability.Sprite;
        }

    }
}
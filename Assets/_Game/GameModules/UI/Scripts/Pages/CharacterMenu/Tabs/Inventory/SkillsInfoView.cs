using _Game.GameModules.Abilities.Scripts;
using UnityEngine;

namespace _Game.GameModules.UI.Scripts.Pages.CharacterMenu.Tabs.Inventory
{
    public class SkillsInfoView : MonoBehaviour
    {
        public SkillInfoView[] skillsView;
        public string[] buttonsName = {"X", "Y", "B", "A"};

        public void UpdateUI(Ability[] abilities)
        {
            for (var i = 0; i < abilities.Length && i < buttonsName.Length && i < skillsView.Length; i++)
                skillsView[i].UpdateUI(abilities[i].Data, buttonsName[i]);
        }
    }
}
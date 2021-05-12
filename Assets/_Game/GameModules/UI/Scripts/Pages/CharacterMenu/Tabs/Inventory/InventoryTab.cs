using _Game.GameModules.Weapons.Scripts;
using UnityEngine;

namespace _Game.GameModules.UI.Scripts.Pages.CharacterMenu.Tabs.Inventory
{
    public class InventoryTab : MonoBehaviour
    {
        [SerializeField] SkillsInfoView skillsView;
        [SerializeField] InventoryButton mainWeaponButton;

        public void OnWeaponChange(Weapon weapon)
        {
            if (!weapon) return;
            mainWeaponButton.UpdateUI(weapon.Data.Icon);
            skillsView.UpdateUI(weapon.Abilities.ToArray());
        }
    }
}
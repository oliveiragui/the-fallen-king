using System.Collections.Generic;
using _Game.GameModules.Characters.Scripts;
using _Game.GameModules.UI.Scripts.Pages.CharacterMenu.Tabs.Inventory;
using _Game.GameModules.UI.Scripts.Pages.CharacterMenu.Tabs.WeaponSelection;
using _Game.GameModules.Weapons.Scripts;
using UnityEngine;

namespace _Game.GameModules.UI.Scripts.Pages.CharacterMenu
{
    public class CharacterMenuPage : MonoBehaviour
    {
        [SerializeField] StatusPanel statusPanel;

        [SerializeField] InventoryTab inventoryTab;
        [SerializeField] WeaponSelectionTab weaponSelectionTab;

        public void OnWeaponChange(Weapon weapon)
        {
            inventoryTab.OnWeaponChange(weapon);
        }

        public void OnWeaponChange(Weapon weapon, List<Weapon> weapons)
        {
            weaponSelectionTab.UpdateUI(weapons.ToArray(), weapons.IndexOf(weapon));
        }

        public void OnStatusChange(CharacterStatus characterStatus)
        {
            statusPanel.OnStatusChange(characterStatus);
        }

        public void OnCharacterBind(Character character)
        {
            weaponSelectionTab.OnCharacterBind(character);
        }
    }
}
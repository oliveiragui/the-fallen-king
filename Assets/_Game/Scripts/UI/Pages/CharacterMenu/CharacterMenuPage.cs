using System.Collections.Generic;
using _Game.Scripts.GameContent.Characters;
using _Game.Scripts.GameContent.Weapons;
using _Game.Scripts.Services.AttributeSystem;
using _Game.Scripts.UI.Pages.CharacterMenu.Tabs.Inventory;
using _Game.Scripts.UI.Pages.CharacterMenu.Tabs.WeaponSelection;
using UnityEngine;

namespace _Game.Scripts.UI.Pages.CharacterMenu
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

        public void OnStatusChange(Status status)
        {
            statusPanel.OnStatusChange(status);
        }

        public void OnCharacterBind(Character character)
        {
            weaponSelectionTab.OnCharacterBind(character);
        }
    }
}
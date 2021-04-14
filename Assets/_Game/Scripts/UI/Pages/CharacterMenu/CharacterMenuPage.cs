using System.Collections.Generic;
using _Game.Scripts.Components.AttributeSystem;
using _Game.Scripts.GameContent.Characters;
using _Game.Scripts.GameContent.Weapons;
using UnityEngine;

namespace _Game.Scripts.UI
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
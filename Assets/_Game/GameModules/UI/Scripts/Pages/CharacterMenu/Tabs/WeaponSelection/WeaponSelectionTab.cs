using _Game.GameModules.Characters.Scripts;
using _Game.GameModules.UI.Scripts.Utils;
using _Game.GameModules.Weapons.Scripts;
using UnityEngine;

namespace _Game.GameModules.UI.Scripts.Pages.CharacterMenu.Tabs.WeaponSelection
{
    public class WeaponSelectionTab : MonoBehaviour
    {
        [SerializeField] ExtendedButton previousWeaponButton;
        [SerializeField] ExtendedButton nextWeaponButton;
        [SerializeField] WeaponButton[] weaponButtons;
        [SerializeField] WeaponView weaponView;

        public void UpdateUI(Weapon[] weapons, int selectedWeapon)
        {
            for (var i = 0; i < weapons.Length && i < weaponButtons.Length; i++)
            {
                weaponButtons[i].background.sprite = weapons[i].Data.Icon;
                weaponButtons[i].border.SetActive(false);
            }

            // weaponButtons[selectedWeapon].border.SetActive(true);
            //weaponView.OnWeaponChange(weapons[selectedWeapon]);
        }

        public void OnCharacterBind(Character character)
        {
            // for (var i = 0; i < weaponButtons.Length && i < character.Weapons.weapons.Count; i++)
            // {
            //     var k = i;
            //     weaponButtons[i].button.onClick.AddListener(() => character.Weapons.UseWeapon(k));
            // }

            nextWeaponButton.onClick.AddListener(() => character.WeaponStorage.UseNext());
            previousWeaponButton.onClick.AddListener(() => character.WeaponStorage.UsePrevious());
        }
    }
}
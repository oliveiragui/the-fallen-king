using _Game.Scripts.GameContent.Characters;
using _Game.Scripts.GameContent.Weapons;
using UnityEngine;

namespace _Game.Scripts.UI
{
    public class WeaponSelectionTab : MonoBehaviour
    {
        [SerializeField] ExtendedButton previousWeaponButton;
        [SerializeField] ExtendedButton nextWeaponButton;
        [SerializeField] WeaponButton[] weaponButtons;
        [SerializeField] WeaponView weaponView;

        public void UpdateUI(Weapon[] weapons, int selectedWeapon)
        {
            for (int i = 0; i < weapons.Length && i < weaponButtons.Length; i++)
            {
                weaponButtons[i].background.sprite = weapons[i].Data.Icon;
                weaponButtons[i].border.SetActive(false);
            }

            weaponButtons[selectedWeapon].border.SetActive(true);
            weaponView.OnWeaponChange(weapons[selectedWeapon]);
        }

 
        public void OnCharacterBind(Character character)
        {
            
            // for (var i = 0; i < weaponButtons.Length && i < character.Weapons.weapons.Count; i++)
            // {
            //     var k = i;
            //     weaponButtons[i].button.onClick.AddListener(() => character.Weapons.UseWeapon(k));
            // }

            nextWeaponButton.onClick.AddListener(() => character.Weapons.UseNext());
            previousWeaponButton.onClick.AddListener(() => character.Weapons.UsePrevious());
        }
    }
}
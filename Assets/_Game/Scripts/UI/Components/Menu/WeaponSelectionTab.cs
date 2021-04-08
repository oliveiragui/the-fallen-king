using System;
using _Game.Scripts.GameContent.Characters;
using _Game.Scripts.GameContent.Weapons;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.Scripts.UI
{
    public class WeaponSelectionTab : MonoBehaviour
    {
        public ExtendedButton previousWeaponButton;
        public ExtendedButton nextWeaponButton;
        public WeaponButton[] weaponButtons;
        [SerializeField] WeaponView weaponView;

        
        public void UpdateUI(Weapon[] weapons, int selectedWeapon)
        {
            for (int i = 0; i < weapons.Length && i < weaponButtons.Length; i++)
            {
                weaponButtons[i].icon.sprite = weapons[i].Icon;
                weaponButtons[i].border.SetActive(false);
            }
            weaponButtons[selectedWeapon].border.SetActive(true);
            weaponView.UpdateUI(weapons[selectedWeapon]);
        }
    }

    [Serializable]
    public class WeaponButton
    {
        //public TextMeshProUGUI icon;
        public Image icon;
        public GameObject border;
        public ExtendedButton button;
    }
}
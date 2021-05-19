using _Game.GameModules.Weapons.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.GameModules.UI.Scripts.HUD.Main_HUD
{
    public class SelectedWeaponIcon : MonoBehaviour, IWeaponChangeListener
    {
        [SerializeField] Image icon;

        public void OnWeaponChange(Weapon weapon)
        {
            icon.sprite = weapon.Data.Icon;
        }
    }
}
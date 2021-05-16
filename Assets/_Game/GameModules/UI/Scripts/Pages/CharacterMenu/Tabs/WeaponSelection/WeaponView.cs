﻿using _Game.GameModules.Weapons.Scripts;
using TMPro;
using UnityEngine;

namespace _Game.GameModules.UI.Scripts.Pages.CharacterMenu.Tabs.WeaponSelection
{
    public class WeaponView : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI title;
        [SerializeField] TextMeshProUGUI description;

        public void OnWeaponChange(Weapon weapon)
        {
            title.text = weapon.name;
            description.text = weapon.Data.Description;
        }
    }
}
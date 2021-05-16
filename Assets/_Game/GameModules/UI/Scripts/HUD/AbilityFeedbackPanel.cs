﻿using _Game.GameModules.Abilities.Scripts;
using _Game.GameModules.Weapons.Scripts;
using UnityEngine;

namespace _Game.GameModules.UI.Scripts.HUD
{
    public class AbilityIconsPanel : MonoBehaviour,IWeaponChangeListener
    {
        [SerializeField] AbilityIcon[] abilityIcons;

        public void OnWeaponChange(Weapon weapon)
        {
            foreach (var icon in abilityIcons) icon.OnWeaponChange(weapon);
        }
    }
}
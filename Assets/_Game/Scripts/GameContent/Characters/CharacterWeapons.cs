using System;
using System.Collections.Generic;
using _Game.Scripts.GameContent.Weapons;
using UnityEngine;
using UnityEngine.Events;

namespace _Game.Scripts.GameContent.Characters
{
    [Serializable]
    public class CharacterWeapons : MonoBehaviour
    {
        public List<Weapon> weapons;
        [SerializeField] Weapon weaponInUse;
        public Weapon WeaponInUse { get => weaponInUse; private set => weaponInUse = value; }
        
        public WeaponChangeEvent onWeaponChange;

        void Awake()
        {
            weapons = new List<Weapon>();
            onWeaponChange = new WeaponChangeEvent();
        }

        public void Add(WeaponData weaponData)
        {
            var weapon = new GameObject();
            weapon.name = weaponData.name;
            weapon.transform.parent = gameObject.transform;
            weapons.Add(weapon.AddComponent<Weapon>().Setup(weaponData));
        }

        public void UseWeapon(int index)
        {
            if (index >= weapons.Count) return;
            WeaponInUse = weapons[index];
            onWeaponChange.Invoke(WeaponInUse);
        }
        
        public void UseWeapon(Weapon weapon)
        {
            if (weapons.IndexOf(weapon) < 0) return;
            WeaponInUse = weapon;
            onWeaponChange.Invoke(WeaponInUse); 
        }

        public void UseNext()
        {
            int nextWeaponIndex = weapons.IndexOf(WeaponInUse) + 1;
            nextWeaponIndex = (nextWeaponIndex >= weapons.Count) ? 0 : nextWeaponIndex;
           UseWeapon(nextWeaponIndex);
        }

        public void UsePrevious()
        {
            int previousWeaponIndex = weapons.IndexOf(WeaponInUse) - 1;
            previousWeaponIndex = (previousWeaponIndex < 0) ? weapons.Count - 1 : previousWeaponIndex;
           UseWeapon(previousWeaponIndex);
        }

        public void Unequip()
        {
            UseWeapon(-1);
        }
    }

    [Serializable]
    public class WeaponChangeEvent : UnityEvent<Weapon> { }
    
}
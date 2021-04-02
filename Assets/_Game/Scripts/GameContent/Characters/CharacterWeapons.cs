using System;
using System.Collections.Generic;
using _Game.Scripts.Weapons;
using UnityEngine;
using UnityEngine.Events;

namespace _Game.Scripts.Characters
{
    [Serializable]
    public class CharacterWeapons : MonoBehaviour
    {
        public List<Weapon> weapons;
        public Weapon WeaponInUse { get; private set; }
        public SwitchWeaponEvent switchWeaponEvent;

        void Awake()
        {
            weapons = new List<Weapon>();
            switchWeaponEvent = new SwitchWeaponEvent();
        }

        public void Add(WeaponData weaponData)
        {
            var weapon = new GameObject();
            weapon.name = weaponData.name;
            weapon.transform.parent = gameObject.transform;
            weapons.Add(weapon.AddComponent<Weapon>().Setup(weaponData));
        }

        public Weapon UseWeapon(int index)
        {
            WeaponInUse = weapons[index];
            switchWeaponEvent.Invoke(WeaponInUse);
            return WeaponInUse;
        }

        public Weapon UseNext()
        {
            int nextWeaponIndex = weapons.IndexOf(WeaponInUse) + 1;
            nextWeaponIndex = (nextWeaponIndex >= weapons.Count) ? 0 : nextWeaponIndex;
            return UseWeapon(nextWeaponIndex);
        }

        public Weapon UsePrevious()
        {
            int previousWeaponIndex = weapons.IndexOf(WeaponInUse) - 1;
            previousWeaponIndex = (previousWeaponIndex < 0) ? weapons.Count - 1 : previousWeaponIndex;
            return UseWeapon(previousWeaponIndex);
        }

        public void Unequip()
        {
            UseWeapon(-1);
        }
    }

    [Serializable]
    public class SwitchWeaponEvent : UnityEvent<Weapon> { }
}
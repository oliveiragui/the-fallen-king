using System;
using _Game.Scripts.Runtime.Reorderable.Attributes;
using _Game.Scripts.Utils.Serializables;
using UnityEngine;

namespace _Game.Scripts.Weapons.Prefab
{
    [Serializable]
    public class WeaponPrefabList
    {
        [Reorderable] [SerializeField] ReorderableWeaponPrefabs alwaysOn;
        [Reorderable] [SerializeField] ReorderableWeaponPrefabs idle;
        [Reorderable] [SerializeField] ReorderableWeaponPrefabs inCombat;
        public ReorderableWeaponPrefabs AlwaysOn => alwaysOn;
        public ReorderableWeaponPrefabs Idle => idle;
        public ReorderableWeaponPrefabs InCombat => inCombat;
    }
}
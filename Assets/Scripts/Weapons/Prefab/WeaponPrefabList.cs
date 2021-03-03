using System;
using Runtime.Reorderable.Attributes;
using UnityEngine;
using Utils.Serializables;

namespace Weapons.Prefab
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
using System;
using Collection.Abilities;
using Collection.Abilities.Collections.Habilidades;
using Collection.Status.Player;
using Collections.Avatares.Componentes;
using Runtime.Attributes;
using UnityEngine;
using Utils.Serializables;

namespace Collection.Weapons
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "GameContent/Weapon", order = 1)]
    public class WeaponModel : ScriptableObject
    {
        public WeaponInfo Info => info;
        public PlayerStatusModel Attributes => attributes;
        public AnimatorOverrideController AnimatorController => animatorController;
        public WeaponPrefabList Prefabs => prefabs;
        public AbilityModel[] Abilities => abilities;

        [SerializeField] WeaponInfo info;
        [SerializeField] AbilityModel[] abilities = new AbilityModel[4];
        [SerializeField] PlayerStatusModel attributes;
        [SerializeField] AnimatorOverrideController animatorController;
        [SerializeField] WeaponPrefabList prefabs;
    }

    [Serializable]
    public class WeaponInfo
    {
        public string Name => name;
        public string Description => description;

        [SerializeField] string name;
        [SerializeField] string description;
    }

    [Serializable]
    public class WeaponPrefabList
    {
        public ReorderableWeaponPrefabs AlwaysOn => alwaysOn;
        public ReorderableWeaponPrefabs Idle => idle;
        public ReorderableWeaponPrefabs InCombat => inCombat;

        [Reorderable] [SerializeField] ReorderableWeaponPrefabs alwaysOn;
        [Reorderable] [SerializeField] ReorderableWeaponPrefabs idle;
        [Reorderable] [SerializeField] ReorderableWeaponPrefabs inCombat;
    }

    [Serializable]
    public class WeaponPrefab
    {
        [SerializeField] string slot;
        [SerializeField] public GameObject gameObject;
        [SerializeField] Vector3 position;
        [SerializeField] Vector3 rotation;

        public string Slot => slot;
        public GameObject GameObject => gameObject;
        public Vector3 Position => position;
        public Vector3 Rotation => rotation;
    }
}
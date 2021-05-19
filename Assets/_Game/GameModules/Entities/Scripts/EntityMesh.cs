using System.Collections.Generic;
using _Game.GameModules.Weapons.Scripts.Prefab;
using _Game.Scripts.Services.Storage.Custom;
using UnityEngine;

namespace _Game.GameModules.Entities.Scripts
{
    public class EntityMesh : MonoBehaviour
    {
        [SerializeField] GameObjectStorage slots;

        readonly List<GameObject> alwaysOn = new List<GameObject>();
        readonly List<GameObject> idle = new List<GameObject>();
        readonly List<GameObject> inCombat = new List<GameObject>();
        bool _equipWeapons;

        public bool EquipWeapons
        {
            get => _equipWeapons;
            set
            {
                _equipWeapons = value;
                foreach (var prefab in inCombat) prefab.SetActive(value);
                foreach (var prefab in idle) prefab.SetActive(!value);
            }
        }

        public void SwitchWeapon(WeaponPrefabList weaponPrefabs)
        {
            ClearSlots();
            if (weaponPrefabs == null) return;
            FillSlots(weaponPrefabs);
            EquipWeapons = false;
        }

        void FillSlots(WeaponPrefabList weaponPrefabs)
        {
            foreach (var prefab in weaponPrefabs.AlwaysOn) alwaysOn.Add(Instantiate(prefab));
            foreach (var prefab in weaponPrefabs.Idle) idle.Add(Instantiate(prefab));
            foreach (var prefab in weaponPrefabs.InCombat) inCombat.Add(Instantiate(prefab));
        }

        GameObject Instantiate(WeaponPrefabData weaponPrefabData)
        {
            var instance = Instantiate(weaponPrefabData.gameObject, slots[weaponPrefabData.Slot].transform);
            instance.transform.localPosition = weaponPrefabData.Position;
            instance.transform.localEulerAngles = weaponPrefabData.Rotation;
            return instance;
        }

        void ClearSlots()
        {
            ClearList(alwaysOn);
            ClearList(idle);
            ClearList(inCombat);
        }

        static void ClearList(ICollection<GameObject> prefabs)
        {
            foreach (var weaponPrefab in prefabs) Destroy(weaponPrefab);
            if (prefabs.Count > 0) prefabs.Clear();
        }
    }
}
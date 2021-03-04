using System.Collections.Generic;
using Components.Storage.Custom;
using UnityEngine;
using Weapons;
using Weapons.Prefab;

namespace Entities.Mesh
{
    public class EntityMesh : MonoBehaviour
    {
        [SerializeField] GameObjectStorage slots;

        readonly List<GameObject> alwaysOn = new List<GameObject>();
        readonly List<GameObject> idle = new List<GameObject>();
        readonly List<GameObject> inCombat = new List<GameObject>();
        bool _inCombat;

        public bool InCombat
        {
            get => _inCombat;
            set
            {
                _inCombat = value;
                foreach (var prefab in inCombat) prefab.SetActive(value);
                foreach (var prefab in idle) prefab.SetActive(!value);
            }
        }

        public void SwitchWeapon(WeaponData weapon)
        {
            ClearSlots();
            if (weapon == null) return;
            FillSlots(weapon);
            InCombat = false;
        }

        void FillSlots(WeaponData weapon)
        {
            foreach (var prefab in weapon.Prefabs.AlwaysOn) alwaysOn.Add(Instantiate(prefab));
            foreach (var prefab in weapon.Prefabs.Idle) idle.Add(Instantiate(prefab));
            foreach (var prefab in weapon.Prefabs.InCombat) inCombat.Add(Instantiate(prefab));
        }

        GameObject Instantiate(WeaponPrefabData weaponPrefabData)
        {
            var instance = Instantiate(weaponPrefabData.gameObject, slots[weaponPrefabData.Slot].transform);
            transform.localPosition = weaponPrefabData.Position;
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
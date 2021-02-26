using System.Collections.Generic;
using System.Linq;
using Collection.Weapons;
using Components.Storage.Custom;
using UnityEngine;

namespace Collection.Entities.Mesh
{
    public class EntityMesh : MonoBehaviour
    {
        bool _inCombat;
        [SerializeField] GameObjectStorage slots;

        List<GameObject> alwaysOn = new List<GameObject>();
        List<GameObject> idle = new List<GameObject>();
        List<GameObject> inCombat = new List<GameObject>();

        public void SwitchWeapon(WeaponModel weapon)
        {
            ClearSlots();
            if (weapon == null) return;
            FillSlots(weapon);
            InCombat = false;
        }

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

        void FillSlots(WeaponModel weapon)
        {
            foreach (var prefab in weapon.Prefabs.AlwaysOn) alwaysOn.Add(Instantiate(prefab));
            foreach (var prefab in weapon.Prefabs.Idle) idle.Add(Instantiate(prefab));
            foreach (var prefab in weapon.Prefabs.InCombat) inCombat.Add(Instantiate(prefab));
        }

        GameObject Instantiate(WeaponPrefab prefab)
        {
            var instance = Instantiate(prefab.gameObject, slots[prefab.Slot].transform);
            transform.localPosition = prefab.Position;
            instance.transform.localEulerAngles = prefab.Rotation;

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
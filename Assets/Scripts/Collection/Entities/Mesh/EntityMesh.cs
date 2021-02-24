using Collection.Weapons;
using Components.Storage.Custom;
using UnityEngine;

namespace Collection.Entities.Mesh
{
    public class EntityMesh : MonoBehaviour
    {
        [SerializeField] GameObjectStorage slots;

        public void SwitchWeapon(WeaponModel weapon)
        {
            foreach (var slot in slots.Components.Values) CleanSlot(slot);

            if (weapon == null) return;

            foreach (var prefab in weapon.Prefabs) FillSlot(prefab);
        }

        void FillSlot(WeaponPrefab prefab)
        {
            Instantiate(prefab.gameObject, slots[prefab.Slot].transform);
        }

        void CleanSlot(GameObject slot)
        {
            foreach (Transform child in slot.transform)
                Destroy(child);
        }
    }
}
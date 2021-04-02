using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts.Ammo
{
    [CreateAssetMenu(fileName = "New Ammo", menuName = "GameContent/Ammo/Basic Ammo")]
    public class AmmoData : ScriptableObject
    {
        GameObject ammoStorage;
        public float LifeTime = 10;
        public float LifeTimeOnHit = 3;
        [SerializeField] GameObject model;

        Stack<Ammo> ammunitionStorage = new Stack<Ammo>();

        public Ammo Instantiate(Vector3 position, Quaternion rotation)
        {
            var ammo = (ammunitionStorage.Count > 0)
                ? ammunitionStorage.Pop()
                : Instantiate(model).GetComponent<Ammo>();
            Activate(ammo, position, rotation);
            ammo.SetLifeTime(LifeTime);
            return ammo;
        }

        void Activate(Ammo ammo, Vector3 position, Quaternion rotation)
        {
            var transform = ammo.transform;
            ammo.data = this;
            ammo.gameObject.SetActive(true);
            transform.position = position;
            transform.rotation = rotation;
        }

        public void Deactivate(Ammo ammo)
        {
            if (ammoStorage == null)
            {
                ammoStorage = new GameObject();
                ammoStorage.name = "Ammo Storage";
            }
            ammo.transform.parent = ammoStorage.transform;
            ammo.gameObject.SetActive(false);
            ammunitionStorage.Push(ammo);
        }
    }
}
using System;
using UnityEngine;

namespace _Game.Scripts.GameContent.Weapons.Prefab
{
    [Serializable]
    public class WeaponPrefabData
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
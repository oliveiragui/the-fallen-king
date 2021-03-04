using UnityEngine;

namespace Ammo
{
    public class AmmoStorage : MonoBehaviour
    {
        [SerializeField] BaseAmmo arrow;

        static BaseAmmo _arrow;

        public static BaseAmmo Arrow(Vector3 position, Quaternion rotation) =>
            Instantiate(_arrow.gameObject, position, rotation).GetComponent<BaseAmmo>();

        void Awake()
        {
            _arrow = arrow;
        }
    }
}
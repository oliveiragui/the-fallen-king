using UnityEngine;

namespace Ammo
{
    public class AmmoStorage : MonoBehaviour
    {
        static BaseAmmo _arrow;
        [SerializeField] BaseAmmo arrow;

        void Awake()
        {
            _arrow = arrow;
        }

        public static BaseAmmo Arrow(Vector3 position, Quaternion rotation)
        {
            return Instantiate(_arrow.gameObject, position, rotation).GetComponent<BaseAmmo>();
        }
    }
}
using UnityEngine;
using Utils;

namespace Weapons
{
    public class Ammunition : ScriptableObject
    {
        [SerializeField] StringToHashDictionary<GameObject> Components { get; }

        public GameObject this[int key] => Components[key];

        public GameObject this[string key] => Components[key];
    }
}
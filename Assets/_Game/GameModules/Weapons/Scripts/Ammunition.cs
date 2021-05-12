using _Game.Scripts.Utils;
using UnityEngine;

namespace _Game.GameModules.Weapons.Scripts
{
    public class Ammunition : ScriptableObject
    {
        [SerializeField] StringToHashDictionary<GameObject> Components { get; }

        public GameObject this[int key] => Components[key];

        public GameObject this[string key] => Components[key];
    }
}
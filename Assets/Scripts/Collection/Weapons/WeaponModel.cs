using System;
using Collection.Abilities;
using Collection.Status.Player;
using Collections.Avatares.Componentes;
using UnityEngine;

namespace Collection.Weapons
{
    [CreateAssetMenu(fileName = "Arma", menuName = "GameContent/Arma", order = 1)]
    public class WeaponModel : ScriptableObject
    {
        [SerializeField] int animationID;
        [SerializeField] AbilitySet abilities;
        [SerializeField] PlayerStatusModel atributosBase;
        [SerializeField] WeaponPrefab[] prefabs;

        public string Name => name;
        public int AnimationID => animationID;
        public AbilitySet Abilities => abilities;
        public PlayerStatusModel AtributosBase => atributosBase;
        public WeaponPrefab[] Prefabs => prefabs;
    }

    [Serializable]
    public class WeaponPrefab
    {
        [SerializeField] string slot;
        [SerializeField] public GameObject gameObject;

        public string Slot => slot;
        public GameObject GameObject => gameObject;
    }
}
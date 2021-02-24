using System;
using Collection.Abilities.Collections.Habilidades;
using UnityEngine;

namespace Collection.Abilities
{
    [Serializable]
    public class AbilitySet
    {
        [SerializeField] public AbilityModel ataque1;
        [SerializeField] public AbilityModel ataque2;
        [SerializeField] public AbilityModel ataque3;
        [SerializeField] public AbilityModel esquiva;
    }
}
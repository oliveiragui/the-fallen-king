using Abilities.Collections.Habilidades;
using UnityEngine;

namespace Entities
{
    public class EntityParams : MonoBehaviour
    {
        bool _inCombat;
        public AbilityData currentAbility;
        public bool IsUsingAbility => !(currentAbility is null);
        float _speed;
        float _direction;
    }
}
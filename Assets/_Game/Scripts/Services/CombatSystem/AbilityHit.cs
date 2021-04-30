using _Game.Scripts.GameContent.Characters;
using UnityEngine;

namespace _Game.Scripts.Services.CombatSystem
{
    public class AbilityHit
    {
        public readonly HitImpact impact;
        public readonly Character origin;
        public readonly float power;

        public AbilityHit(
            float power, Vector3 direction, Character origin, HitImpact impact = HitImpact.None
        )
        {
            this.power = power;
            this.origin = origin;
            this.impact = impact;
        }

        public static implicit operator bool(AbilityHit hit) =>
            // assuming, that 1 is true;
            // somehow this method should deal with value == null case
            hit != null;
    }
}
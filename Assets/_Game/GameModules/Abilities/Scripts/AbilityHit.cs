using _Game.GameModules.Characters.Scripts;
using UnityEngine;

namespace _Game.GameModules.Abilities.Scripts
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

        public static implicit operator bool(AbilityHit hit) => hit != null;
    }
}
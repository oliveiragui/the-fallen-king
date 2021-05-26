using _Game.GameModules.Characters.Scripts;
using UnityEngine;

namespace _Game.GameModules.Abilities.Scripts
{
    public class AbilityHit
    {
        public readonly HitImpact impact;
        public readonly HitType type;
        public readonly Character origin;
        public readonly float power;
        public readonly Vector3 direction;

        public AbilityHit(
            float power, Vector3 direction, Character origin, HitImpact impact = HitImpact.None,
            HitType type = HitType.None
        )
        {
            this.direction = direction;
            this.power = power;
            this.origin = origin;
            this.impact = impact;
            this.type = type;
        }

        public static implicit operator bool(AbilityHit hit) => hit != null;
    }

    public enum HitType
    {
        None,
        MetalSword,
        MetalAxe,
        Arrow
    }
}
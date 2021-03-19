using Characters;
using Teams;
using UnityEngine;

namespace CombatSystem
{
    public class AbilityHit
    {
        public readonly HitImpact impact;
        public readonly float power;
        public readonly Character origin;

        public AbilityHit(
            float power, Vector3 direction, Character origin, HitImpact impact = HitImpact.None
        )
        {
            this.power = power;
            this.origin = origin;
            this.impact = impact;
        }
    }
}
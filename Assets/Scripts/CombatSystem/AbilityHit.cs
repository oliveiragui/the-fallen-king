using Teams;
using UnityEngine;

namespace CombatSystem
{
    public class AbilityHit
    {
        public readonly Vector3 direction;
        public readonly Team team;
        public readonly float power;
        public readonly bool friendlyFire;
        public readonly HitImpact impact;

        public AbilityHit(
            float power, Vector3 direction, Team team, HitImpact impact = HitImpact.None, bool friendlyFire = false
        )
        {
            this.power = power;
            this.direction = direction;
            this.team = team;
            this.friendlyFire = friendlyFire;
            this.impact = impact;
        }
    }
}
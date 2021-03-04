using Teams;
using UnityEngine;

namespace CombatSystem
{
    public class Damage
    {
        public readonly Vector3 direction;
        public readonly Team team;
        public readonly float value;

        public Damage(float value, Vector3 direction, Team team)
        {
            this.value = value;
            this.direction = direction;
            this.team = team;
        }
    }
}
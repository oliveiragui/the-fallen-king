using System.Numerics;
using Teams;

namespace CombatSystem
{
    public class Damage
    {
        public readonly Vector3 direction;
        public readonly Team origin;
        public readonly float value;

        public Damage(float value, Vector3 direction, Team origin)
        {
            this.value = value;
            this.direction = direction;
            this.origin = origin;
        }
    }
}
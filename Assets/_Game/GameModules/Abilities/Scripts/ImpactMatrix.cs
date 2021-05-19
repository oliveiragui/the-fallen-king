namespace _Game.GameModules.Abilities.Scripts
{
    public static class ImpactMatrix
    {
        static readonly int[,] matrix = new int[4, 4]
        {
            //  R > weak,normal,strong,unbeatable
            //I
            /*None*/ {0, 0, 0, 0},
            /*Weak*/ {2, 1, 1, 1},
            /*Normal*/ {2, 2, 1, 1},
            /*Strong*/ {3, 3, 2, 1}
            /*
                * Result:
                * 0 - nothing happens
                * 1 - Just visual animation
                * 2 - Interrupt current animation
                * 3 - interrupts current animation and moves away from the Hit
            */
        };

        public static int Calc(HitImpact impact, ImpactResistance resiliency) => matrix[(int) impact, (int) resiliency];
    }
}
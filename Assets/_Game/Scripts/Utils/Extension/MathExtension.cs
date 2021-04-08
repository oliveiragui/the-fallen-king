using UnityEngine;

namespace _Game.Scripts.Utils.Extension
{
    public static class MathExtension
    {
        public static float ToRad(float x, float y) => Mathf.Atan2(x, y);
        
        public static float ToRad(this Vector2 vector) => ToRad(vector.x, vector.y);

        public static float ToDegree(float x, float y) => ToRad(x, y) * Mathf.Rad2Deg;

        public static float ToDegree(this Vector2 vector) => ToDegree(vector.x, vector.y);
    }
}
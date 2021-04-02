using UnityEngine;

namespace _Game.Scripts.Utils.Extension
{
    public static class MathExtension
    {
        public static float ToRad(this Vector2 direcao)
        {
            return Mathf.Atan2(-direcao.normalized.y, direcao.normalized.x);
        }

        public static float ToDegree(this Vector2 direcao)
        {
            return ToRad(direcao) * Mathf.Rad2Deg;
        }
    }
}
using _Game.Scripts.Utils.Extension;
using UnityEngine;

namespace _Game.Scripts.Utils
{
    public class AimAssist : Singleton<AimAssist>
    {
        [SerializeField] Camera mainCamera;

        public static Vector3 DirecaoVetorialDaMira(Transform origin)
        {
            var dest = Instance.mainCamera.MouseOnPlane();
            dest.y = origin.position.y;
            return dest;
        }

        public static float DirecaoDaMira(Transform origin)
        {
            var charPosition = origin.position;
            var dest = Instance.mainCamera.MouseOnPlane();

            var mira = new Vector2(dest.x - charPosition.x, dest.z - charPosition.z);
            return mira.ToDegree();
        }
    }
}
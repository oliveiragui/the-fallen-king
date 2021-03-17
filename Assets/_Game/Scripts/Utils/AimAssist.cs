using UnityEngine;
using Utils.Extension;

namespace Utils
{
    public class AimAssist : Singleton<AimAssist>
    {
        [SerializeField] Camera camera;

        public static Vector3 DirecaoVetorialDaMira(Transform origin)
        {
            var dest = Instance.camera.MouseOnPlane();
            dest.y = origin.position.y;
            return dest;
        }

        public static float DirecaoDaMira(Transform origin)
        {
            var charPosition = origin.position;
            var dest = Instance.camera.MouseOnPlane();

            var mira = new Vector2(dest.x - charPosition.x, dest.z - charPosition.z);
            return mira.ToDegree();
        }
    }
}
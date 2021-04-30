using UnityEngine;

namespace _Game.Scripts.Utils.Extension
{
    public static class CameraExtension
    {
        static Plane xzPlane = new Plane(Vector3.up, new Vector3(0, 1, 0));

        public static Vector3 MouseOnPlane(this Camera camera)
        {
            // calculates the intersection of a ray through the mouse pointer with a static x/z plane for example for movement etc, source: http://unifycommunity.com/wiki/index.php?title=Click_To_Move
            var mouseray = camera.ScreenPointToRay(Input.mousePosition);
            float hitdist;

            if (xzPlane.Raycast(mouseray, out hitdist))
                // check for the intersection point between ray and plane
                return mouseray.GetPoint(hitdist);

            return hitdist < -1.0f ? mouseray.GetPoint(-hitdist) : Vector3.zero;
        }
    }
}
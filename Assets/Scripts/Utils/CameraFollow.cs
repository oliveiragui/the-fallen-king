using UnityEngine;

namespace Utils
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] Transform target;
        [SerializeField] Vector3 offset = new Vector3(0, 0, 0);
        [SerializeField] float smoothFactor = 0.5f;

        void FixedUpdate()
        {
            if (target)
                transform.position = Vector3.Lerp(transform.position, target.position + offset, smoothFactor);
        }
    }
}
using UnityEngine;

namespace _Game.Scripts.UI.HUD
{
    public class Minimap : MonoBehaviour
    {
        public Transform player;

        void LateUpdate()
        {
            if (player != null)
            {
                Vector3 newPosition = player.position;
                newPosition.y = transform.position.y;
                transform.position = newPosition;
            }
        }

        public void SetPlayer(Transform transform)
        {
            if (player != null)
            {
                player = transform;
            }
        }
    }
}

using UnityEngine;

namespace _Game.Scripts.UI.HUD
{
    public class Minimap : MonoBehaviour
    {
        public Transform player;

        void LateUpdate()
        {
            Vector3 newPosition = player.position;
            newPosition.y = transform.position.y;
            transform.position = newPosition;

            //transform.rotation = Quaternion.Euler(90f, player.eulerAngles.y, 0f);
        }

        public void SetPlayer(Transform transform)
        {
            player = transform;
        }
    }
}

using _Game.Scripts.GameContent.Weapons;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.Scripts.UI
{
    public class InventoryButton: MonoBehaviour
    {
        [SerializeField] Image background;

        public void UpdateUI(Sprite sprite)
        {
            background.sprite = sprite;
        }
    }
}
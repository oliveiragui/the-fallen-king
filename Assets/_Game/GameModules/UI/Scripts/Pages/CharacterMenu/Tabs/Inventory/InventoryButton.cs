using UnityEngine;
using UnityEngine.UI;

namespace _Game.GameModules.UI.Scripts.Pages.CharacterMenu.Tabs.Inventory
{
    public class InventoryButton : MonoBehaviour
    {
        [SerializeField] Image background;

        public void UpdateUI(Sprite sprite)
        {
            background.sprite = sprite;
        }
    }
}
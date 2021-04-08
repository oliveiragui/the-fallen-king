using _Game.Scripts.GameContent.Weapons;
using TMPro;
using UnityEngine;

namespace _Game.Scripts.UI
{
    public class WeaponView : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI title;
        [SerializeField] TextMeshProUGUI description;

        public void UpdateUI(Weapon weapon)
        {
            title.text = weapon.name;
            description.text = weapon.Description;
        }
    }
}
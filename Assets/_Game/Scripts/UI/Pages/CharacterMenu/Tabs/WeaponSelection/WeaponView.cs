using _Game.Scripts.GameContent.Weapons;
using TMPro;
using UnityEngine;

namespace _Game.Scripts.UI
{
    public class WeaponView : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI title;
        [SerializeField] TextMeshProUGUI description;

        public void OnWeaponChange(Weapon weapon)
        {
            title.text = weapon.name;
            description.text = weapon.Data.Description;
        }
    }
}
using _Game.Scripts.GameContent.Weapons;
using UnityEngine;

namespace _Game.Scripts.UI
{
    public class InventoryTab : MonoBehaviour
    {
        [SerializeField] SkillsInfoView skillsView;
        [SerializeField] InventoryButton mainWeaponButton;
        
        public void OnWeaponChange(Weapon weapon)
        {
            if (!weapon) return;
            mainWeaponButton.UpdateUI(weapon.Data.Icon);
            skillsView.UpdateUI( weapon.Abilities.ToArray());
        }
    }
}
using _Game.Scripts.GameContent.Characters;
using _Game.Scripts.UI.StatusBar;
using UnityEngine;

namespace _Game.Scripts.UI
{
    public class CharacterUIBind : MonoBehaviour
    {
        [SerializeField] Character character;
        [SerializeField] PlayerInfoHUD playerInfoHUD;
        [SerializeField] StatusPanel statusPanel;
        [SerializeField] WeaponSelectionTab weaponSelectionTab;
        [SerializeField] SkillsInfoView skillsInfoView;

        public bool binded;

        void Start()
        {
            statusPanel.UpdateUI(character.Status);
            Bind(character);
        }

        public void Bind(Character unbindedCharacter)
        {
            if (binded) Unbind(character);
            OnBind(unbindedCharacter);
        }

        void OnBind(Character unbindedCharacter)
        {
            binded = true;
            character = unbindedCharacter;
            character.Weapons.onWeaponChange.AddListener(weapon =>
            {
                weaponSelectionTab.UpdateUI(
                    character.Weapons.weapons.ToArray(),
                    character.Weapons.weapons.IndexOf(weapon)
                );
                skillsInfoView.UpdateUI(weapon.Abilities.ToArray());
                playerInfoHUD.UpdateUI(weapon.Abilities.ToArray());
            });

            for (int i = 0; i < weaponSelectionTab.weaponButtons.Length && i < character.Weapons.weapons.Count; i++)
            {
                int k = i;
                weaponSelectionTab.weaponButtons[i].button.onClick.AddListener(() => character.Weapons.UseWeapon(k));
            }
            weaponSelectionTab.nextWeaponButton.onClick.AddListener(() => character.Weapons.UseNext());
            weaponSelectionTab.previousWeaponButton.onClick.AddListener(() => character.Weapons.UsePrevious());

            character.Status.onAnyStatChanged.AddListener(status =>
            {
                statusPanel.UpdateUI(status);
                playerInfoHUD.UpdateUI(status);
            });
            character.Weapons.UseWeapon(character.Weapons.WeaponInUse);
            
            
            
        }

        public void Unbind(Character character) { }
    }
}
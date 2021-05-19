using _Game.GameModules.Characters.Scripts;
using _Game.GameModules.Weapons.Scripts;
using UnityEngine;

namespace _Game.GameModules.UI.Scripts.HUD.Main_HUD
{
    public class MainHUD : MonoBehaviour, IWeaponChangeListener, ICharacterStatusChangeListener
    {
        [SerializeField] Character character;
        [SerializeField] SelectedWeaponIcon weaponIcon;
        [SerializeField] AbilityIconsPanel abilityIcons;
        [SerializeField] PlayerLife playerLife;

        void Start()
        {
            BindCharacter(character);
        }

        public void BindCharacter(Character character)
        {
            character.WeaponStorage.onWeaponChange.AddListener(OnWeaponChange);
            if (character.WeaponStorage.WeaponInUse) OnWeaponChange(character.WeaponStorage.WeaponInUse);

            character.CharacterStatus.StatusChanged.AddListener(OnStatusChange);
            OnStatusChange(character.CharacterStatus);
        }

        public void OnWeaponChange(Weapon weapon)
        {
            weaponIcon.OnWeaponChange(weapon);
            abilityIcons.OnWeaponChange(weapon);
        }

        public void OnStatusChange(CharacterStatus status)
        {
            playerLife.OnStatusChange(status);
        }
    }
}
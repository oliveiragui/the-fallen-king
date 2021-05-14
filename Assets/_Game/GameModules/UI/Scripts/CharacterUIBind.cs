using _Game.GameModules.Characters.Scripts;
using _Game.GameModules.UI.Scripts.HUD;
using _Game.GameModules.UI.Scripts.Pages.CharacterMenu;
using _Game.GameModules.Weapons.Scripts;
using UnityEngine;

namespace _Game.GameModules.UI.Scripts
{
    public class CharacterUIBind : MonoBehaviour
    {
        [SerializeField] Character character;
        [SerializeField] CharacterMenuPage characterMenuPage;
        public bool binded;

        void Start()
        {
            character.events.onInstantiate.AddListener(() => Bind(character));
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

            character.WeaponStorage.onWeaponChange.AddListener(OnWeaponChange);
            character.CharacterStatus.StatusChanged.AddListener(OnStatusChange);
            characterMenuPage.OnCharacterBind(character);

            OnWeaponChange(character.WeaponStorage.WeaponInUse);
            OnStatusChange(character.CharacterStatus);
        }

        void OnWeaponChange(Weapon weapon)
        {
            if (!weapon) return;
            characterMenuPage.OnWeaponChange(weapon);
            characterMenuPage.OnWeaponChange(weapon, character.WeaponStorage.weapons);
        }

        void OnStatusChange(CharacterStatus characterStatus)
        {
            characterMenuPage.OnStatusChange(characterStatus);
        }

        public void Unbind(Character character) { }
    }
}
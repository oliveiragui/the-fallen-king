using _Game.Scripts.GameContent.Characters;
using _Game.Scripts.GameContent.Weapons;
using _Game.Scripts.Services.AttributeSystem;
using _Game.Scripts.UI.HUD;
using _Game.Scripts.UI.Pages.CharacterMenu;
using UnityEngine;

namespace _Game.Scripts.UI
{
    public class CharacterUIBind : MonoBehaviour
    {
        [SerializeField] Character character;
        [SerializeField] CharacterMenuPage characterMenuPage;
        [SerializeField] PlayerInfoHUD playerInfoHUD;
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
            character.Status.onAnyStatChanged.AddListener(OnStatusChange);
            characterMenuPage.OnCharacterBind(character);

            OnWeaponChange(character.WeaponStorage.WeaponInUse);
            OnStatusChange(character.Status);
        }

        void OnWeaponChange(Weapon weapon)
        {
            if (!weapon) return;
            characterMenuPage.OnWeaponChange(weapon);
            characterMenuPage.OnWeaponChange(weapon, character.WeaponStorage.weapons);
            playerInfoHUD.UpdateUI(weapon.Abilities.ToArray());
        }

        void OnStatusChange(Status status)
        {
            characterMenuPage.OnStatusChange(status);
            playerInfoHUD.UpdateUI(status);
        }

        public void Unbind(Character character) { }
    }
}
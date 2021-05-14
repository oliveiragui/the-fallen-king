using System.Collections.Generic;
using _Game.GameModules.Characters.Scripts;
using _Game.GameModules.Entities.Scripts;
using UnityEngine;

namespace _Game.GameModules.UI.Scripts.HUD
{
    public class CharacterHUDBinder : MonoBehaviour
    {
        [SerializeField] CharacterHUD playerHud;


        public void OnCharacterInstantiated(Character character)
        {
            Instantiate(playerHud.gameObject).GetComponent<CharacterHUD>().BindCharacter(character);
        }
    }
}
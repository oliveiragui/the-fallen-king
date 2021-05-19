using System;
using _Game.GameModules.Characters.Scripts;
using UnityEngine;

namespace _Game.GameModules.UI.Scripts.HUD
{
    public class CharacterHUDBinder : MonoBehaviour
    {
        [SerializeField] GameObject hudCanvas;
        [SerializeField] MinionHUD minionHud;
        [SerializeField] BossHUD bossHud;

        public void OnCharacterInstantiated(Character character)
        {
            switch (character.Data.Level)
            {
                case CharacterLevel.Minion:
                    Instantiate(minionHud.gameObject).GetComponent<MinionHUD>().BindCharacter(character);
                    break;
                case CharacterLevel.Boss:
                    Instantiate(bossHud.gameObject, hudCanvas.transform).GetComponent<BossHUD>().BindCharacter(character);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
using System;
using _Game.GameModules.Characters.Scripts;
using _Game.GameModules.UI.Scripts.Utils;
using TMPro;
using UnityEngine;

namespace _Game.GameModules.UI.Scripts.HUD
{
    public class BossHUD : MonoBehaviour, ICharacterStatusChangeListener
    {
        [SerializeField] TextMeshProUGUI bossName;
        [SerializeField] Lifebar lifebar;

        void Awake()
        {
            gameObject.SetActive(false);
        }

        public void BindCharacter(Character character)
        {
            bossName.text = character.Data.name;
            character.events.enterInCombat.AddListener(() => gameObject.SetActive(true));
            character.events.exitCombat.AddListener(() => gameObject.SetActive(false));
            character.CharacterStatus.StatusChanged.AddListener(OnStatusChange);
        }

        public void OnStatusChange(CharacterStatus status)
        {
            lifebar.Total = status.Life.Total;
            lifebar.Current = status.Life.Current;
        }
    }
}
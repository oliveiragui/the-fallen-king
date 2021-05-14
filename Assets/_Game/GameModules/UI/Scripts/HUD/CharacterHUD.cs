using System;
using System.Collections;
using _Game.GameModules.Characters.Scripts;
using _Game.GameModules.Entities.Scripts;
using _Game.GameModules.UI.Scripts.Utils;
using UnityEngine;

namespace _Game.GameModules.UI.Scripts.HUD
{
    public class CharacterHUD : MonoBehaviour, ICharacterStatusChangeListener
    {
        [SerializeField] Lifebar lifebar;
        Entity entity;

        void Awake()
        {
            lifebar.gameObject.SetActive(false);
        }

        public void BindCharacter(Character character)
        {
            character.Entity.events.onHitReceived.AddListener((a) => lifebar.gameObject.SetActive(true));
            //character.events.enterInCombat.AddListener(() => lifebar.enabled = true);
            character.events.exitCombat.AddListener(() => lifebar.enabled = false);
            character.events.onEntityDeath.AddListener((entity2) => Destroy(gameObject));
            character.CharacterStatus.StatusChanged.AddListener(OnStatusChange);
            entity = character.Entity;
        }

        public void OnStatusChange(CharacterStatus status)
        {
            lifebar.Total = status.Life.Total;
            lifebar.Current = status.Life.Current;
        }

        void FixedUpdate()
        {
            if (entity) transform.position = entity.transform.position;
        }
    }
}
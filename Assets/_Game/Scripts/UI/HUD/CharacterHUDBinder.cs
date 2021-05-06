﻿using System.Collections.Generic;
using _Game.Scripts.GameContent.Characters;
using _Game.Scripts.GameContent.Entities;
using _Game.Scripts.Services.AttributeSystem;
using UnityEngine;

namespace _Game.Scripts.UI.HUD
{
    public class CharacterHUDBinder : MonoBehaviour
    {
        [SerializeField] CharacterHUD playerHud;
        Dictionary<Character, CharacterHUD> HUDs;

        void Awake()
        {
            HUDs = new Dictionary<Character, CharacterHUD>();
        }
        

        public void OnCharacterInstantiated(Character character)
        {
            var hud = Instantiate(playerHud.gameObject).GetComponent<CharacterHUD>();
            HUDs.Add(character, hud);
            character.Status.onAnyStatChanged.AddListener(hud.UpdateLife);
            var entity = character.Entity;
            if (entity)
            {
                entity.events.onEnabled.AddListener(hud.FollowEntity);
                entity.events.onEnabled.AddListener(ShowEntityHUD);
                entity.events.onDisabled.AddListener(hud.StopFollowEntity);
                entity.events.onDisabled.AddListener(HideEntityHUD);
                if (entity.isActiveAndEnabled) hud.FollowEntity(character.Entity);
            }
        }

        public void OnCharacterDestroyed(Character character)
        {
            var hud = HUDs[character];
            var entity = character.Entity;
            
            character.Status.onAnyStatChanged.RemoveListener(hud.UpdateLife);
            entity.events.onEnabled.RemoveListener(hud.FollowEntity);
            entity.events.onEnabled.RemoveListener(ShowEntityHUD);
            entity.events.onDisabled.RemoveListener(hud.StopFollowEntity);
            entity.events.onDisabled.RemoveListener(HideEntityHUD);
            HUDs.Remove(character);
            Destroy(hud.gameObject);
        }

        public void ShowEntityHUD(Entity entity)
        {
            var character = entity.Character;
            if (!HUDs.ContainsKey(character)) HUDs[character].enabled = true;
        }

        public void HideEntityHUD(Entity entity)
        {
            HUDs[entity.Character].enabled = true;
        }
    }
}
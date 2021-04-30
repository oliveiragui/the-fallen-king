using System.Collections.Generic;
using _Game.Scripts.GameContent.Characters;
using _Game.Scripts.GameContent.Entities;
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

        public void BindCharacterHUD(Character character)
        {
            var hud = Instantiate(playerHud.gameObject).GetComponent<CharacterHUD>();
            HUDs.Add(character, hud);

            character.Status.onAnyStatChanged.AddListener(hud.UpdateLife);
            character.events.onEntitySummon.RemoveListener(hud.FollowEntity);
            character.events.onEntityDimmissed.AddListener(hud.StopFollowEntity);

            if (character.Entity) hud.FollowEntity(character.Entity);

            character.events.onEntitySummon.AddListener(ShowEntityHUD);
            character.events.onEntityDimmissed.AddListener(HideEntityHUD);
        }

        public void UnbindCharacterHUD(Character character)
        {
            var hud = HUDs[character];
            character.Status.onAnyStatChanged.RemoveListener(hud.UpdateLife);
            character.events.onEntitySummon.RemoveListener(ShowEntityHUD);
            character.events.onEntityDimmissed.RemoveListener(HideEntityHUD);
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
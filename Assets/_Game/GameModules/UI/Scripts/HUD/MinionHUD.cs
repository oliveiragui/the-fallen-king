using _Game.GameModules.Characters.Scripts;
using _Game.GameModules.Entities.Scripts;
using _Game.GameModules.UI.Scripts.Utils;
using UnityEngine;

namespace _Game.GameModules.UI.Scripts.HUD
{
    public class MinionHUD : MonoBehaviour, ICharacterStatusChangeListener
    {
        [SerializeField] Lifebar lifebar;
        Entity entity;

        void Awake()
        {
            lifebar.gameObject.SetActive(false);
        }

        void FixedUpdate()
        {
            if (entity) transform.position = entity.transform.position;
        }

        public void OnStatusChange(CharacterStatus status)
        {
            lifebar.Total = status.Life.Total;
            lifebar.Current = status.Life.Current;
        }

        public void BindCharacter(Character character)
        {
            character.Entity.hitReceived.AddListener(a => lifebar.gameObject.SetActive(true));
            character.events.death.AddListener(entity2 => Destroy(gameObject));
            character.CharacterStatus.StatusChanged.AddListener(OnStatusChange);
            entity = character.Entity;
        }
    }
}
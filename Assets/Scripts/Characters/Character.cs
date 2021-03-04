using Components.AttributeSystem;
using Components.InventorySystem;
using Entities;
using Teams;
using UnityEngine;

namespace Characters
{
    public class Character : MonoBehaviour
    {
        [SerializeField] CharacterData data;
        [SerializeField] Entity entity;
        Inventory inventory;

        public Status Status { get; private set; }
        public Entity Entity => entity;
        public Team Team { get; set; }

        void Awake()
        {
            Team = data.DefaultTeam;
            Status = new Status(data.RawStatus);
        }
    }
}
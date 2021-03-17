using Components.AttributeSystem;
using Components.InventorySystem;
using Entities.Default;
using Teams;
using UnityEngine;

namespace Characters
{
    public class Character : MonoBehaviour
    {
        [SerializeField] CharacterData data;
        [SerializeField] DefaultEntity defaultEntity;
        Inventory inventory;

        public Status Status { get; private set; }
        public Team Team { get; private set; }
        public DefaultEntity DefaultEntity => defaultEntity;

        void Awake()
        {
            Team = data.DefaultTeam;
            Status = new Status(data.RawStatus);
        }
    }
}
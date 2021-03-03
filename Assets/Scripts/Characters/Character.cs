using Components.AttributeSystem;
using Components.InventorySystem;
using Entities;
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

        void Awake()
        {
            Status = new Status(data.RawStatus);
        }
    }
}
using System;
using System.Linq;
using _Game.Scripts.Components.AttributeSystem;
using _Game.Scripts.Entities;
using _Game.Scripts.Teams;
using _Game.Scripts.Weapons;
using UnityEngine;
using UnityEngine.Events;

namespace _Game.Scripts.Characters
{
    public class Character : MonoBehaviour
    {
        [SerializeField] CharacterData data;
        [SerializeField] Entity entity;
        [SerializeField] CharacterWeapons weapons;

        public Status Status { get; private set; }
        public Team Team { get; private set; }
        public Entity Entity => entity;
        public CharacterWeapons Weapons => weapons;

        void Awake()
        {
            Team = data.DefaultTeam;
            Status = new Status(data.RawStatus);
        }

        void Start()
        {
            weapons.switchWeaponEvent.AddListener((weapon) => entity.commands.EquipWeapon(weapon));
        }
    }

}
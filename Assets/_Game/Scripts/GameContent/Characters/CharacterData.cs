using _Game.Scripts.Components.AttributeSystem;
using _Game.Scripts.GameContent.Teams;
using UnityEngine;

namespace _Game.Scripts.GameContent.Characters
{
    [CreateAssetMenu(fileName = "Character", menuName = "GameContent/Character", order = 1)]
    public class CharacterData : ScriptableObject
    {
        [SerializeField] RawStatus rawStatus;
        [SerializeField] Team defaultTeam;

        public RawStatus RawStatus => rawStatus;
        public Team DefaultTeam => defaultTeam;
    }
}
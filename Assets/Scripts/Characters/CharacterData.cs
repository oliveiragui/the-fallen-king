using Components.AttributeSystem;
using Teams;
using UnityEngine;

namespace Characters
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
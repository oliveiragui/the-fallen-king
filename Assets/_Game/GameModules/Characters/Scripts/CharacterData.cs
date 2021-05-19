using _Game.GameModules.Abilities.Scripts;
using _Game.GameModules.Teams.Scripts;
using UnityEngine;

namespace _Game.GameModules.Characters.Scripts
{
    [CreateAssetMenu(fileName = "Character", menuName = "GameContent/Character", order = 1)]
    public class CharacterData : ScriptableObject
    {
        [SerializeField] RawCharacterStatus rawCharacterStatus;
        [SerializeField] Team defaultTeam;
        [SerializeField] ImpactResistance resiliency;
        [SerializeField] CharacterLevel level;
        [SerializeField] RuntimeAnimatorController entityAnimator;

        public CharacterLevel Level => level;
        public RawCharacterStatus RawCharacterStatus => rawCharacterStatus;
        public Team DefaultTeam => defaultTeam;
        public ImpactResistance Resiliency => resiliency;
        public RuntimeAnimatorController EntityAnimator => entityAnimator;
    }

    public enum CharacterLevel
    {
        Minion,
        Boss
    }
}
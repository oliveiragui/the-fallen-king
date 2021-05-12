using UnityEngine;

namespace _Game.GameModules.Teams.Scripts
{
    [CreateAssetMenu(fileName = "Team", menuName = "GameContent/Team", order = 1)]
    public class Team : ScriptableObject
    {
        [SerializeField] bool playerFriend;
        [SerializeField] bool aggressive;

        public bool PlayerFriend => playerFriend;
        public bool Aggressive => aggressive;
    }
}
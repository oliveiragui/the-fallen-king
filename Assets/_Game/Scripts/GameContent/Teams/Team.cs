using UnityEngine;

namespace _Game.Scripts.GameContent.Teams
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
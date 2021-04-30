using UnityEngine;

namespace _Game.Scripts.Services
{
    [CreateAssetMenu(fileName = "New Game Data", menuName = "GameContent/Manager/Data")]
    public class GameData : ScriptableObject
    {
        public int RequestedSceneIndex;
    }
}
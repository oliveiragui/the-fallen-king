using UnityEngine;

namespace _Game.Scripts.Components
{
    [CreateAssetMenu(fileName = "New Game Data", menuName = "GameContent/Manager/Data")]
    public class GameData : ScriptableObject
    {
        public int RequestedSceneIndex;
    }
}
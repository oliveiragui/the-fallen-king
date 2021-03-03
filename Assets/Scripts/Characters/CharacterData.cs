using Components.AttributeSystem;
using UnityEngine;

namespace Characters
{
    [CreateAssetMenu(fileName = "Character", menuName = "GameContent/Character", order = 1)]
    public class CharacterData : ScriptableObject
    {
        [SerializeField] RawStatus rawStatus;
        public RawStatus RawStatus => rawStatus;
    }
}
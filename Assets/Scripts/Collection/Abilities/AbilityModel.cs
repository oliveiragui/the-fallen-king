using UnityEngine;

namespace Collection.Abilities
{
    namespace Collections.Habilidades
    {
        [CreateAssetMenu(fileName = "Habilidade", menuName = "GameContent/Habilidade", order = 2)]
        public class AbilityModel : ScriptableObject
        {
            public AbilityInfo Info => info;
            public AbilityAttributes Attributes => attributes;
            public AbilityCombo[] Combo => combo;

            [SerializeField] AbilityInfo info;
            [SerializeField] AbilityAttributes attributes;
            [SerializeField] AbilityCombo[] combo = new AbilityCombo[3];
        }
    }
}
using System;
using UnityEngine;

namespace Collection.Abilities
{
    namespace Collections.Habilidades
    {
        [CreateAssetMenu(fileName = "Habilidade", menuName = "GameContent/Habilidade", order = 2)]
        public class AbilityModel : ScriptableObject
        {
            [SerializeField] string nome;
            [SerializeField] string descricao;
            [SerializeField] int animationID;
            [SerializeField] string spriteText;
            [SerializeField] bool bloqueavel;
            [SerializeField] bool bloqueante;
            [SerializeField] StatusHabilidade status;
            public string Nome => nome;
            public string Descricao => descricao;
            public int AnimationID => animationID;
            public string SpriteText => spriteText;
            public bool Bloqueavel => bloqueavel;
            public bool Bloqueante => bloqueante;
            public StatusHabilidade Status => status;

            public bool CanInterrupt(AbilityModel other)
            {
                return Bloqueante && other.bloqueavel;
            }
        }

        [Serializable]
        public class StatusHabilidade
        {
            [field: SerializeField] public float Alcance { get; private set; }
            [field: SerializeField] public float Velocidade { get; private set; }
            [field: SerializeField] public float TempoDeRecarga { get; private set; }
        }
    }
}
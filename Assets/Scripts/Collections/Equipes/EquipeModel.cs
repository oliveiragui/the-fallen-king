using System;
using UnityEngine;

namespace Collections.Equipes
{
    [Serializable]
    public class EquipeModel
    {
        [SerializeField] bool npc;
        [SerializeField] bool aliado;
        [SerializeField] bool lutador;
        [SerializeField] bool agressivo;
        public bool Npc => npc;
        public bool Aliado => aliado;
        public bool Lutador => lutador;
        public bool Agressivo => agressivo;
    }
}
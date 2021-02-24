using System;
using UnityEngine;

namespace Collections.Controles.Sprites
{
    [Serializable]
    public class SpriteBotao
    {
        [SerializeField] string conjunto;
        [SerializeField] string ataque1;
        [SerializeField] string ataque2;
        [SerializeField] string ataque3;
        [SerializeField] string esquiva;
        [SerializeField] string menuDeJogo;
        [SerializeField] string menuPrincipal;
        [SerializeField] string confirma;
        [SerializeField] string cancela;

        public string Ataque1 => Template(conjunto, ataque1);
        public string Ataque2 => Template(conjunto, ataque2);
        public string Ataque3 => Template(conjunto, ataque3);
        public string Esquiva => Template(conjunto, esquiva);
        public string MenuDeJogo => Template(conjunto, menuDeJogo);
        public string MenuPrincipal => Template(conjunto, menuPrincipal);
        public string Confirma => Template(conjunto, confirma);
        public string Cancela => Template(conjunto, cancela);

        static string Template(string nome, string botao)
        {
            return $"<sprite=\"{nome}\" name=\"{botao}\">";
        }
    }
}
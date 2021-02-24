using System;
using System.Collections.Generic;
using System.Linq;
using ToRefactor.Controladores.Enums;
using UnityEngine.SceneManagement;
using Utils;

namespace ToRefactor.Gerenciadores
{
    public class GerenciadorDeJogo : Singleton<GerenciadorDeJogo>
    {
        public Dictionary<JogadorID, Jogador> Jogadores;

        public static bool EmPartida => SceneManager.GetActiveScene().buildIndex > 0;

        void Awake()
        {
            Jogadores = Enum
                .GetValues(typeof(JogadorID))
                .Cast<JogadorID>()
                .ToDictionary<JogadorID, JogadorID, Jogador>(id => id, id => null);
        }

        public static void ReiniciaFase()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
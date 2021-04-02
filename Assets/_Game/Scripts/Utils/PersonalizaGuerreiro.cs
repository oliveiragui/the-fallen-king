using UnityEngine;

namespace _Game.Scripts.Utils
{
    public class PersonalizaGuerreiro : MonoBehaviour
    {
        [SerializeField] ModeloGuerreiro modelo;
        [SerializeField] ModeloGuerreiro cor;
    }

    internal enum ModeloGuerreiro
    {
        Guerreiro1,
        Guerreiro2,
        Guerreiro3,
        Soldado1,
        Soldado2
    }

    internal enum CoresGuerreiro
    {
        Azul,
        Preto,
        Laranja,
        Amarelo
    }
}
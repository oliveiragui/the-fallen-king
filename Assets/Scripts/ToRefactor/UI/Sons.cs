using UnityEngine;

namespace ToRefactor.UI
{
    public class Sons : MonoBehaviour
    {
        public static AudioSource SomDeConfirmacao;
        public static AudioSource SomDeRecusar;
        public static AudioSource SomDeSelecionar;

        public static AudioSource SomDeAbrirInventario;
        public static AudioSource SomDeFecharInventario;
        public static AudioSource SomDeTrocarDeArma;

        public static AudioSource SomDeSino;
        public AudioSource somDeConfirmacao;
        public AudioSource somDeRecusar;
        public AudioSource somDeSelecionar;

        public AudioSource somDeAbrirInventario;
        public AudioSource somDeFecharInventario;
        public AudioSource somDeTrocarDeArma;

        public AudioSource somDeSino;

        void Awake()
        {
            SomDeConfirmacao = somDeConfirmacao;
            SomDeRecusar = somDeRecusar;
            SomDeSelecionar = somDeSelecionar;
            SomDeAbrirInventario = somDeAbrirInventario;
            SomDeFecharInventario = somDeFecharInventario;
            SomDeTrocarDeArma = somDeTrocarDeArma;
            SomDeSino = somDeSino;
        }
    }
}
using TMPro;
using UnityEngine;

namespace ToRefactor.UI
{
    public class CaixaDeDialogo : MonoBehaviour
    {
        public TextMeshProUGUI Texto;
        public GameObject CaixaDeTexto;

        public void Mostra(string mensagem)
        {
            Texto.text = mensagem;
            CaixaDeTexto.SetActive(true);
        }
    }
}
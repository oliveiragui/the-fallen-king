using ToRefactor.UI;
using UnityEngine;

namespace ToRefactor.Interagiveis
{
    public class DialogoDeArea : MonoBehaviour
    {
        [SerializeField] AreaInteragivel areaInteragivel;
        public CaixaDeDialogo dialogo;
        void Update() => dialogo.CaixaDeTexto.SetActive(areaInteragivel.jogadorDentro);
    }
}
using ToRefactor.UI.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ToRefactor.UI.Menus.Principal
{
    public class MenuIniciaJogo : MonoBehaviour
    {
        [SerializeField] MenuPrincipal menuPrincipal;
        [SerializeField] ExtendedButton btnIniciar;
        [SerializeField] ExtendedButton btnVoltar;

        [SerializeField] ExtendedButton btnCarrega;
        [SerializeField] ExtendedButton btnApaga;

        void Start()
        {
            btnVoltar.onSubmit.AddListener(data => Volta());

            btnIniciar.onSelected.AddListener(data => Inicia());
            btnVoltar.onSelected.AddListener(data => Volta());
        }

        public void Inicia()
        {
            SceneManager.LoadScene(1);
        }

        public void Volta()
        {
            menuPrincipal.AbreMenuPrincipal();
        }
    }
}
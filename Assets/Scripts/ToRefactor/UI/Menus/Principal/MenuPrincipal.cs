using ToRefactor.Gerenciadores;
using ToRefactor.UI.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ToRefactor.UI.Menus.Principal
{
    public class MenuPrincipal : MonoBehaviour
    {
        [SerializeField] ExtendedButton btnComeca;
        [SerializeField] ExtendedButton btnConfiguracoes;
        [SerializeField] ExtendedButton btnCreditos;
        [SerializeField] ExtendedButton btnSair;

        [SerializeField] GameObject MenuInicial;
        [SerializeField] GameObject MenuConfiguracoes;
        [SerializeField] GameObject MenuCreditos;
        [SerializeField] GameObject MenuIniciaJogo;

        public bool MenuAberto { get; private set; }

        void Start()
        {
            btnConfiguracoes.onSubmit.AddListener(data => AbreMenuConfiguracoes());
            btnCreditos.onSubmit.AddListener(data => AbreMenuCreditos());

            if (!GerenciadorDeJogo.EmPartida)
            {
                btnComeca.onSubmit.AddListener(data => AbreMenuIniciaJogo());
                btnSair.onSubmit.AddListener(data => SairDoJogo());
            }
            else
            {
                btnComeca.onSubmit.AddListener(data => AbreMenuPrincipal());
                btnSair.onSubmit.AddListener(data => SairDaCena());
            }
        }

        void SairDaCena()
        {
            SceneManager.LoadScene(0);
        }

        void SairDoJogo()
        {
            Application.Quit();
        }

        public void AbreMenuPrincipal()
        {
            FechaMenus();
            MenuInicial.SetActive(true);
            Interface.EventSystemGamepad.SetSelectedGameObject(btnComeca.gameObject);
            MenuAberto = true;
        }

        public void FechaMenuPrincipal()
        {
            FechaMenus();
            MenuAberto = false;
        }

        public void AbreMenuIniciaJogo()
        {
            FechaMenus();
            MenuIniciaJogo.SetActive(true);
        }

        public void AbreMenuConfiguracoes()
        {
            FechaMenus();
            MenuConfiguracoes.SetActive(true);
        }

        public void AbreMenuCreditos()
        {
            FechaMenus();
            MenuCreditos.SetActive(true);
        }

        public void FechaMenus()
        {
            MenuInicial.SetActive(false);
            MenuConfiguracoes.SetActive(false);
            MenuCreditos.SetActive(false);
            MenuIniciaJogo.SetActive(false);
        }
    }
}
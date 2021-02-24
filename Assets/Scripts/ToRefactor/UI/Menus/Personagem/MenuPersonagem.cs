using ToRefactor.Gerenciadores;
using ToRefactor.UI.Menus.Personagem.Abas;
using ToRefactor.UI.Utils;
using UnityEngine;

namespace ToRefactor.UI.Menus.Personagem
{
    public class MenuPersonagem : MonoBehaviour
    {
        public Sons sons;

        [SerializeField] GameObject menuPersonagem;

        [SerializeField] GameObject abaInventario;
        [SerializeField] GameObject abaHabilidades;
        [SerializeField] GameObject abaObjetivos;

        [SerializeField] ExtendedButton btnInventario;
        [SerializeField] ExtendedButton btnHabilidades;
        [SerializeField] ExtendedButton btnObjetivos;
        [SerializeField] ExtendedButton btnVoltar;

        public AbaHabilidade AbaHabilidade;
        public AbaInventario AbaInventario;

        [SerializeField] Jogador jogador;
        public bool MenuAberto { get; private set; }

        void Start()
        {
            btnInventario.onSelected.AddListener(data => AbreAbaInventario());
            btnHabilidades.onSelected.AddListener(data => AbreAbaHabilidades());
            btnObjetivos.onSelected.AddListener(data => AbreAbaObjetivos());
            btnVoltar.onSubmit.AddListener(data => FechaMenuDeJogo());
        }

        public void AbreMenuDeJogo()
        {
            MenuAberto = true;
            Interface.EventSystemGamepad.SetSelectedGameObject(btnInventario.gameObject);
            jogador.Controlador.InputAvatarHabilidado(false);
            Sons.SomDeAbrirInventario.Play();
            menuPersonagem.SetActive(true);
        }

        public void FechaMenuDeJogo()
        {
            MenuAberto = false;
            menuPersonagem.SetActive(false);
            jogador.Controlador.InputAvatarHabilidado(true);
            Sons.SomDeFecharInventario.Play();
        }

        void AbreAbaInventario()
        {
            FechaAbas();
            abaInventario.SetActive(true);
        }

        void AbreAbaHabilidades()
        {
            FechaAbas();
            abaHabilidades.SetActive(true);
        }

        void AbreAbaObjetivos()
        {
            FechaAbas();
            abaObjetivos.SetActive(true);
        }

        void FechaAbas()
        {
            abaInventario.SetActive(false);
            abaHabilidades.SetActive(false);
            abaObjetivos.SetActive(false);
        }
    }
}
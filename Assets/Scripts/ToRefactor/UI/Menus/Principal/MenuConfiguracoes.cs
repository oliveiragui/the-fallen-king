using ToRefactor.UI.Utils;
using UnityEngine;

namespace ToRefactor.UI.Menus.Principal
{
    public class MenuConfiguracoes : MonoBehaviour
    {
        [SerializeField] MenuPrincipal menuPrincipal;
        [SerializeField] ExtendedButton btnReseta;
        [SerializeField] ExtendedButton btnVoltar;

        [SerializeField] ExtendedButton btnGeral;
        [SerializeField] ExtendedButton btnControle;
        [SerializeField] ExtendedButton btnAudio;
        [SerializeField] ExtendedButton btnGraficos;

        [SerializeField] GameObject abaGeral;
        [SerializeField] GameObject abaControle;
        [SerializeField] GameObject abaAudio;
        [SerializeField] GameObject abaGraficos;

        void Start()
        {
            btnReseta.onSubmit.AddListener(data => Reseta());
            btnVoltar.onSubmit.AddListener(data => Volta());

            btnGeral.onSelected.AddListener(data => AbreAbaGeral());
            btnControle.onSelected.AddListener(data => AbreAbaControle());
            btnAudio.onSelected.AddListener(data => AbreAbaAudio());
            btnGraficos.onSelected.AddListener(data => AbreAbaGraficos());
        }

        public void AbreAbaGeral()
        {
            DesativaMenus();
            abaGeral.SetActive(true);
        }

        public void AbreAbaControle()
        {
            DesativaMenus();
            abaControle.SetActive(true);
        }

        public void AbreAbaAudio()
        {
            DesativaMenus();
            abaAudio.SetActive(true);
        }

        public void AbreAbaGraficos()
        {
            DesativaMenus();
            abaGraficos.SetActive(true);
        }

        public void Reseta() { }

        public void Volta()
        {
            menuPrincipal.AbreMenuPrincipal();
        }

        void DesativaMenus()
        {
            abaGeral.SetActive(false);
            abaControle.SetActive(false);
            abaAudio.SetActive(false);
            abaGraficos.SetActive(false);
        }
    }
}
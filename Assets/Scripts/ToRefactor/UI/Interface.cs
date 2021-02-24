using ToRefactor.UI.Menus.Personagem;
using ToRefactor.UI.Menus.Principal;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ToRefactor.UI
{
    public class Interface : MonoBehaviour
    {
        [SerializeField] EventSystem _eventSystemTeclado;
        [SerializeField] EventSystem _eventSystemGamepad;
        [SerializeField] MenuPrincipal _menuPrincipal;
        [SerializeField] MenuPersonagem _menuPersonagem;

        public static EventSystem EventSystemTeclado { get; private set; }
        public static EventSystem EventSystemGamepad { get; private set; }
        public static MenuPersonagem MenuPersonagem { get; private set; }
        public static MenuPrincipal MenuPrincipal { get; private set; }

        void Awake()
        {
            EventSystemTeclado = _eventSystemTeclado;
            EventSystemGamepad = _eventSystemGamepad;
            MenuPersonagem = _menuPersonagem;
            MenuPrincipal = _menuPrincipal;
        }
    }
}
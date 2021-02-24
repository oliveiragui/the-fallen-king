using Collections.Controles.Utils;
using ToRefactor.UI;
using UnityEngine;

namespace ToRefactor.Controladores.Utils
{
    public class InputInterface : MonoBehaviour
    {
        Controlador _controlador;
        ControleDinamico _controle;

        void Update()
        {
            if (_controle != null) ProcessaInput();
        }

        public void Configura(Controlador controlador)
        {
            _controlador = controlador;
            _controle = controlador.Controle;
        }

        void ProcessaInput()
        {
            if (_controle.ativo.GetButtonDown(Botao.MenuDeJogo))
            {
                if (Interface.MenuPersonagem.MenuAberto)
                {
                    _controlador.InputAvatarHabilidado(true);
                    Interface.MenuPersonagem.FechaMenuDeJogo();
                }
                else
                {
                    _controlador.InputAvatarHabilidado(false);
                    Interface.MenuPersonagem.AbreMenuDeJogo();
                }
            }

            if (_controle.ativo.GetButtonDown(Botao.MenuPrincipal))
            {
                if (Interface.MenuPrincipal.MenuAberto)
                {
                    _controlador.InputAvatarHabilidado(true);
                    Interface.MenuPrincipal.FechaMenuPrincipal();
                }
                else
                {
                    _controlador.InputAvatarHabilidado(false);
                    Interface.MenuPrincipal.AbreMenuPrincipal();
                }
            }
        }
    }
}
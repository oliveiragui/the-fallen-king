using Collections.Avatares;
using Collections.Controles.Utils;
using UnityEngine;
using Utils;
using Utils.Extension;

namespace ToRefactor.Controladores.Utils
{
    public class InputAvatar : MonoBehaviour
    {
        public bool podeUsarHabilidade = true;
        public bool podeAndar = true;
        public bool podeMirar = true;

        [SerializeField] AvatarController _avatar;
        ControleDinamico _controle;

        bool _usandoGamepad;

        void Update()
        {
            ProcessaInput();
        }

        float MiraDoMouse()
        {
            return AimAssist.DirecaoDaMira(_avatar.transform) + 90;
        }

        public void Configura(ControleDinamico controle, AvatarController avatar)
        {
            _controle = controle;
            _avatar = avatar;
        }

        void ProcessaInput()
        {
            var direcao = new Vector2(_controle.ativo.GetAxisRaw(Eixo.Horizontal),
                _controle.ativo.GetAxisRaw(Eixo.Vertical));
            float eixoPrimario = direcao.ToDegree() + 45;

            if (podeMirar)
                _avatar.Comando.OlhaParaDirecao(eixoPrimario, UsandoGamepad() ? eixoPrimario : MiraDoMouse());

            if (podeUsarHabilidade && _controle.ativo.GetButtonDown(Botao.Esquiva))
                _avatar.Comando.UsaHabilidade(_avatar.entidade.Habilidades[0].Modelo.AnimationID);
            else if (podeUsarHabilidade && _controle.ativo.GetButtonDown(Botao.Ataque1))
                _avatar.Comando.UsaHabilidade(_avatar.entidade.Habilidades[1].Modelo.AnimationID);
            else if (podeUsarHabilidade && _controle.ativo.GetButtonDown(Botao.Ataque2))
                _avatar.Comando.UsaHabilidade(_avatar.entidade.Habilidades[2].Modelo.AnimationID);
            else if (podeUsarHabilidade && _controle.ativo.GetButtonDown(Botao.Ataque3))
                _avatar.Comando.UsaHabilidade(_avatar.entidade.Habilidades[3].Modelo.AnimationID);
            else if (podeAndar && direcao.sqrMagnitude > 0.01) _avatar.Comando.Movimenta();
            else _avatar.Comando.Idle();

            if (_controle.ativo.GetButtonUp(Botao.Ataque1) ||
                _controle.ativo.GetButtonUp(Botao.Ataque2) ||
                _controle.ativo.GetButtonUp(Botao.Ataque3) ||
                _controle.ativo.GetButtonUp(Botao.Esquiva))
                _avatar.Comando.ConjuraHabilidade(false);
        }

        bool UsandoGamepad()
        {
            return _controle.ativo.perfil.Tipo == TipoDeControle.Gamepad;
        }
    }
}
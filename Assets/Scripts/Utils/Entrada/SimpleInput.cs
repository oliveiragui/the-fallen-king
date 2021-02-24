using Collections.Avatares;
using UnityEngine;
using Utils.Extension;

namespace Utils.Entrada
{
    public class SimpleInput : MonoBehaviour
    {
        public bool PodeReceberInput = true;
        public bool podeUsarHabilidade = true;
        public bool podeAndar = true;
        public bool podeMirar = true;

        [SerializeField] AvatarController _avatar;

        bool _usandoGamepad;

        void Update()
        {
            if (PodeReceberInput) ProcessaInput();
        }

        float MiraDoMouse()
        {
            return AimAssist.DirecaoDaMira(_avatar.transform) + 90;
        }

        void ProcessaInput()
        {
            var direcao = new Vector2(Input.GetAxisRaw("P1KHorizontal"), Input.GetAxisRaw("P1KVertical"));
            float eixoPrimario = direcao.ToDegree() + 45;

            if (podeMirar) _avatar.Comando.OlhaParaDirecao(eixoPrimario, _usandoGamepad ? eixoPrimario : MiraDoMouse());
            if (podeUsarHabilidade && Input.GetButtonDown("P1KEsquiva"))
                _avatar.Comando.UsaHabilidade(0);
            else if (podeUsarHabilidade && Input.GetButtonDown("P1KAtaque1"))
                _avatar.Comando.UsaHabilidade(1);
            else if (podeUsarHabilidade && Input.GetButtonDown("P1KAtaque2"))
                _avatar.Comando.UsaHabilidade(2);
            else if (podeUsarHabilidade && Input.GetButtonDown("P1KAtaque3"))
                _avatar.Comando.UsaHabilidade(3);
            else if (podeAndar && direcao.sqrMagnitude > 0.01) _avatar.Comando.Movimenta();
            else _avatar.Comando.Idle();

            if (Input.GetButtonUp("P1KAtaque1") ||
                Input.GetButtonUp("P1KAtaque2") ||
                Input.GetButtonUp("P1KAtaque3") ||
                Input.GetButtonUp("P1KEsquiva"))
                _avatar.Comando.ConjuraHabilidade(false);
        }
    }
}
using Collections.Avatares;
using Collections.Avatares.Componentes;
using UnityEngine;

namespace Collections.Acoes
{
    public class MovimentoAction : AvatarAction
    {
        AvatarAnimacao _avatarAnimacao;
        float _contador;

        public override void Inicializa(AvatarController avatar)
        {
            base.Inicializa(avatar);
            _avatarAnimacao = Avatar.Animacao;
        }

        public override void OnStateEntered()
        {
            base.OnStateEntered();
            _avatarAnimacao.Corre(Avatar.entidade.status.VelocMovimento.Valor / 100);
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            _contador += Time.deltaTime;
            if (_contador > 0.095)
            {
                _contador = 0;
                Avatar.Audio.TocaSom(SlotSom.PassosGrama);
            }
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
            Avatar.Movimentacao.MovimentoSimples(Avatar.Parametros.DirecaoMovimento,
                Avatar.entidade.status.VelocMovimento.Valor * Time.fixedDeltaTime);
        }

        public override void OnStateExited()
        {
            base.OnStateExited();
            _avatarAnimacao.ParaDeCorrer();
            _contador = 0;
        }
    }
}
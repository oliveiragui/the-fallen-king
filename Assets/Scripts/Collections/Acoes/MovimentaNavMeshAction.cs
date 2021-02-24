using Collections.Avatares;
using Collections.Avatares.Componentes;
using UnityEngine;

namespace Collections.Acoes
{
    public class MovimentaNavMeshAction : AvatarAction
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
            float velocidadeNormal = Avatar.entidade.status.VelocMovimento.Valor / 100;

            _avatarAnimacao.Corre(velocidadeNormal);
            Avatar.Movimentacao.MoveAte(Avatar.Parametros.PontoMovimento, velocidadeNormal * 1.7f, 3);
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
            Avatar.Movimentacao.MudaPontoFinal(Avatar.Parametros.PontoMovimento);
            if (Avatar.Movimentacao.Parado) Avatar.Parametros.EmMovimento = false;
        }

        public override void OnStateExited()
        {
            base.OnStateExited();
            Avatar.Movimentacao.ParaMovimento();
            _avatarAnimacao.ParaDeCorrer();
            _contador = 0;
        }
    }
}
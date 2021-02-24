using System;
using Collections.Habilidades;

namespace Collections.Acoes
{
    [Serializable]
    public class AbilityAction : AvatarAction
    {
        public HabilidadeController Habilidade { get; set; }

        public override void OnStateEntered()
        {
            base.OnStateEntered();

            Avatar.Parametros.EmMovimento = false;
            Avatar.Parametros.atacando = true;
            Avatar.Parametros.UsaMesh = false;

            Avatar.Animacao.UsaHabilidade(1 / Habilidade.Modelo.Status.Duracao, Habilidade.Modelo.AnimationID,
                Habilidade.Parametros.comboAtual);
            Avatar.Animacao.ConjuraHabilidade(Avatar.Parametros.Conjurando);

            Habilidade.Cronometro.Inicia();
            Avatar.HabilidadeAtual = Habilidade;
        }

        public override void OnStateExited()
        {
            base.OnStateExited();
            Avatar.Parametros.Conjurando = false;
            Avatar.Parametros.atacando = false;
            Avatar.Animacao.ConjuraHabilidade(false);
            Avatar.HabilidadeAtual = null;
            //habilidade.Enterrompe();
        }
    }
}
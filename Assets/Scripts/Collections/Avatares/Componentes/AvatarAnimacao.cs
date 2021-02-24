using UnityEngine;

namespace Collections.Avatares.Componentes
{
    public class AvatarAnimacao
    {
        readonly Animator _anim;

        public AvatarAnimacao(Animator anim)
        {
            _anim = anim;
        }

        public void Corre(float velocidade)
        {
            _anim.SetFloat(AnimParams.Velocidade, velocidade);
        }

        public void ParaDeCorrer()
        {
            _anim.SetFloat(AnimParams.Velocidade, 0);
        }

        public void Morre()
        {
            _anim.SetTrigger(AnimParams.Morre);
        }

        public void UsaHabilidade(float duracao, int habilidadeID, int combo)
        {
            _anim.SetFloat("Duracao Da Habilidade", duracao);
            _anim.SetInteger("Combo", combo);
            _anim.SetTrigger("Usa Habilidade");
            _anim.SetInteger("Tipo De Habilidade", habilidadeID);
        }

        public void TrocaArma(int armaID)
        {
            _anim.SetInteger("Tipo De Arma", armaID);
        }

        public void ConjuraHabilidade(bool conjura)
        {
            _anim.SetBool("Conjurando Habilidade", conjura);
        }
    }

    public static class AnimParams
    {
        public static readonly int Velocidade = Animator.StringToHash("Velocidade");
        public static readonly int Morre = Animator.StringToHash("Morre");
        public static readonly int TipoDeArma = Animator.StringToHash("Tipo De Arma");
        public static readonly int TipoDeHabilidade = Animator.StringToHash("Tipo De Habilidade");
        public static readonly int AtaqueRapido = Animator.StringToHash("AtaqueRapido");
        public static readonly int AtaqueRapidoDuracao = Animator.StringToHash("Duracao AtaqueRapido");
        public static readonly int Esquiva = Animator.StringToHash("Esquiva");
        public static readonly int EsquivaDuracao = Animator.StringToHash("Duracao Esquiva");
        public static readonly int UsandoEscudoEspada = Animator.StringToHash("Usando Escudo E Espada");
        public static readonly int UsandoArco = Animator.StringToHash("Usando Arco");
    }
}
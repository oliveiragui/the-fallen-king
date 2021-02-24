using ToRefactor.Atributos;
using UnityEngine;

namespace Collections.Avatares.Componentes
{
    public class AvatarComando
    {
        readonly AvatarController _avatarBase;

        public AvatarComando(AvatarController avatarBase)
        {
            _avatarBase = avatarBase;
        }

        public void Movimenta()
        {
            _avatarBase.Parametros.EmMovimento = true;
            _avatarBase.Parametros.UsaMesh = false;
        }

        public void ConjuraHabilidade(bool conjuracao)
        {
            _avatarBase.Parametros.Conjurando = conjuracao;
        }

        public void MovimentaAtePonto(Vector3 ponto)
        {
            _avatarBase.Parametros.EmMovimento = true;
            _avatarBase.Parametros.UsaMesh = true;
            _avatarBase.Parametros.PontoMovimento = ponto;
        }

        public void OlhaParaDirecao(float direcaoMovimento, float direcaoOlhar)
        {
            _avatarBase.Parametros.DirecaoMovimento = direcaoMovimento;
            _avatarBase.Parametros.DirecaoOlhar = direcaoOlhar;
        }

        public void Idle()
        {
            _avatarBase.Parametros.EmMovimento = false;
        }

        public void UsaHabilidade(int index)
        {
            _avatarBase.Parametros.gatilho.Arma();
            _avatarBase.Parametros.HabilidadeSolicitada = index;
            ConjuraHabilidade(true);
        }

        public void Dano(float dano)
        {
            _avatarBase.entidade.status.VidaAtual.AdicionaBonus(new Atributo(-dano));
            if (_avatarBase.entidade.status.VidaAtual.Valor <= 0) _avatarBase.Parametros.Vivo = false;
        }
    }
}
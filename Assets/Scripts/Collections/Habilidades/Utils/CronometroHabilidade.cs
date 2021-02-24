using Collections.Avatares;
using Utils;

namespace Collections.Habilidades.Utils
{
    public class CronometroHabilidade
    {
        Cronometro _cronometro;
        HabilidadeController _habilidade;

        public void Configura(AvatarController avatar, HabilidadeController habilidade)
        {
            _habilidade = habilidade;

            _cronometro = new Cronometro(avatar);
            _cronometro.AoZerarTempo.AddListener(Finaliza);
            _cronometro.AoAtualizaCronometro.AddListener(Atualiza);
        }

        public void Inicia()
        {
            _habilidade.Parametros.EmUso = true;
            _habilidade.Parametros.TempoCorrido = 0;
            _cronometro.Inicia(_habilidade.Modelo.Status.Duracao);
        }

        public void Espera()
        {
            _cronometro.Pausa();
        }

        public void Continua()
        {
            _cronometro.Resume();
        }

        public void Cancela()
        {
            _habilidade.Parametros.EmUso = false;
            _cronometro.Finaliza();
        }

        public void Atualiza(float tempo)
        {
            _habilidade.Parametros.TempoCorrido = tempo;
        }

        public void Finaliza()
        {
            _habilidade.Parametros.EmUso = false;
            _habilidade.Cooldown.Inicia();
        }
    }
}
using Collections.Avatares;
using Utils;

namespace Collections.Habilidades.Utils
{
    public class CronometroCooldown
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
            _habilidade.Parametros.EmRecarga = true;
            _habilidade.Parametros.TempoDeRecargaRestante = 0;
            _cronometro.Inicia(_habilidade.Modelo.Status.Duracao);
        }

        public void Atualiza(float tempo)
        {
            _habilidade.Parametros.TempoDeRecargaRestante = tempo;
        }

        public void Finaliza()
        {
            _habilidade.Parametros.EmRecarga = false;
        }
    }
}
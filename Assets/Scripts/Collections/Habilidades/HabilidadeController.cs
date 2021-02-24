using Collections.Avatares;
using Collections.Habilidades.Utils;

namespace Collections.Habilidades
{
    public class HabilidadeController
    {
        public HabilidadeController(HabilidadeModel habilidadeModel)
        {
            Modelo = habilidadeModel;
            Parametros = new HabilidadeParams();
            Cronometro = new CronometroHabilidade();
            Cooldown = new CronometroCooldown();
        }

        public HabilidadeModel Modelo { get; }
        public HabilidadeParams Parametros { get; }
        public CronometroHabilidade Cronometro { get; }
        public CronometroCooldown Cooldown { get; }

        public void Configura(AvatarController avatar)
        {
            Cronometro.Configura(avatar, this);
            Cooldown.Configura(avatar, this);
        }
    }
}
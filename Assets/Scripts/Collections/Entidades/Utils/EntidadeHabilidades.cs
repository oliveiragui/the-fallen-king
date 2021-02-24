using Collections.Armas;
using Collections.Habilidades;

namespace Collections.Entidades.Utils
{
    public class EntidadeHabilidades
    {
        public HabilidadeController[] controladores;

        public EntidadeHabilidades()
        {
            controladores = new HabilidadeController[4];
        }

        public void AtualizaHabilidades(ArmaController arma)
        {
            if (arma == null) return;

            controladores[0] = new HabilidadeController(arma.Modelo.Ataque1);
            controladores[1] = new HabilidadeController(arma.Modelo.Ataque2);
            controladores[2] = new HabilidadeController(arma.Modelo.Ataque3);
            controladores[3] = new HabilidadeController(arma.Modelo.Esquiva);
        }
    }
}
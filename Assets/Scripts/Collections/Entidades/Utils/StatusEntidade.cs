using ToRefactor.Atributos;

namespace Collections.Entidades.Utils
{
    public class StatusEntidade
    {
        public StatusEntidade(float vida, float forca, float velocMovimento)
        {
            VidaTotal = new Atributo(vida);
            Forca = new Atributo(forca);
            VelocMovimento = new Atributo(velocMovimento);
            VidaAtual = new AtributoComposto(0);
            VidaAtual.AdicionaDependencia(VidaTotal, 1);
        }

        public Atributo VidaTotal { get; }
        public AtributoComposto VidaAtual { get; }
        public Atributo Forca { get; }
        public Atributo VelocMovimento { get; }
    }
}
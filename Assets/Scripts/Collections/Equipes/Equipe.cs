namespace Collections.Equipes
{
    public class Equipe
    {
        public bool agressivo;
        public bool aliado;
        public bool lutador;
        public bool npc;

        public Equipe(EquipeModel modelo)
        {
            npc = modelo.Npc;
            aliado = modelo.Aliado;
            lutador = modelo.Lutador;
            agressivo = modelo.Agressivo;
        }
    }
}
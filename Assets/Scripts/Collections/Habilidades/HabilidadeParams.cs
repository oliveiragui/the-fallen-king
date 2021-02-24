namespace Collections.Habilidades
{
    public class HabilidadeParams
    {
        public int comboAtual;
        public bool EmRecarga;
        public bool EmUso;
        public float TempoCorrido;
        public float TempoDeRecargaRestante;
        public bool PodeSerUsada => !(EmRecarga || EmUso);
    }
}
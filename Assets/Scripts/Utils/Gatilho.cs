namespace Utils
{
    public class Gatilho
    {
        public bool Armado { get; private set; }

        public void Arma()
        {
            Armado = true;
        }

        public bool Dispara()
        {
            if (!Armado) return false;
            Armado = false;
            return true;
        }
    }
}
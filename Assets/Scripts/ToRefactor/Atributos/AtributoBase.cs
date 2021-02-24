namespace ToRefactor.Atributos
{
    public abstract class AtributoBase
    {
        public AtributoBase(float valorBase, float multiplicador = 0)
        {
            Base = valorBase;
            Multiplicador = multiplicador;
        }

        public float Base { get; set; }
        public float Multiplicador { get; set; }
    }
}
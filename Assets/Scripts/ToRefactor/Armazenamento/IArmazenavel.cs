namespace ToRefactor.Armazenamento
{
    public interface IArmazenavel : IElementoRepresentavel
    {
        bool Descartavel { get; }
    }
}
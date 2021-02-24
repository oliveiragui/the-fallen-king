namespace ToRefactor.Armazenamento
{
    public interface IElementoRepresentavel
    {
        string Nome { get; }
        string Descricao { get; }
        string SpriteText { get; }
    }
}
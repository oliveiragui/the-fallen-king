namespace _Game.GameModules.Characters.Scripts
{
    public interface ICharacterStatusChangeListener
    {
        void OnStatusChange(CharacterStatus status);
    }
}
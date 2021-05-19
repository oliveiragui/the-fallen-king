using UnityEngine;

namespace _Game.GameModules.Entities.Scripts.Commands
{
    [CreateAssetMenu(fileName = "Play Particle", menuName = "GameContent/Entities/Commands/Play Particle", order = 0)]
    public class SoundPlayerCommand : EntityCommand
    {
        [SerializeField] string soundName;
        [SerializeField] bool play;

        public override void Execute(Entity entity)
        {
            if (play) entity.sound.Play(soundName);
            else entity.sound.Stop(soundName);
        }
    }
}
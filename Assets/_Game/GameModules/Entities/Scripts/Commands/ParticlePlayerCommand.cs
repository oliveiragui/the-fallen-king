using _Game.Scripts.Utils.MyBox.Attributes;
using UnityEngine;

namespace _Game.GameModules.Entities.Scripts
{
    [CreateAssetMenu(fileName = "Play Particle", menuName = "GameContent/Entities/Commands/Play Particle", order = 0)]
    public class ParticlePlayerCommand : EntityCommand
    {
        [SerializeField] bool predefinedSound;
        [SerializeField] EntitySoundType soundType;
        [ConditionalField("predefinedSound", true)] [SerializeField] string particleName;
        [SerializeField] bool play;

        public override void Execute(Entity entity)
        {
            if (predefinedSound) PredefinedSoundPlay(entity);
            else CustomSoundPlay(entity);
        }

        void PredefinedSoundPlay(Entity entity)
        {
            switch (soundType)
            {
                case EntitySoundType.footstep:
                    entity.particle.Play(entity.FloorName, play);
                    break;
            }
        }

        void CustomSoundPlay(Entity entity)
        {
            if (play) entity.particle.Play(particleName);
            else entity.particle.Stop(particleName);
        }
    }

    public enum EntitySoundType
    {
        footstep
    }
}
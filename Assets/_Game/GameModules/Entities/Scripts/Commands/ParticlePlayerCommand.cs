using System;
using _Game.Scripts.Utils.MyBox.Attributes;
using UnityEngine;

namespace _Game.GameModules.Entities.Scripts.Commands
{
    [CreateAssetMenu(fileName = "Play Particle", menuName = "GameContent/Entities/Commands/Play Particle", order = 0)]
    public class ParticlePlayerCommand : EntityCommand
    {
        [SerializeField] bool predefinedSound;
        [SerializeField] EntityParticleType particleType;
        [ConditionalField("predefinedSound", true)] [SerializeField] string particleName;
        [SerializeField] PlayerState state;

        public override void Execute(Entity entity)
        {
            if (predefinedSound) PredefinedParticlePlay(entity);
            else CustomSoundPlay(entity);
        }

        void PredefinedParticlePlay(Entity entity)
        {
            switch (particleType)
            {
                case EntityParticleType.footstep:
                    entity.particle.Play(entity.FloorName, state == PlayerState.play);
                    break;
            }
        }

        void CustomSoundPlay(Entity entity)
        {
            if (state == PlayerState.play) entity.particle.Play(particleName);
            else entity.particle.Stop(particleName);
        }
    }

    public enum EntityParticleType
    {
        footstep
    }

    public enum PlayerState
    {
        play,
        stop
    }
}
using System.Collections;
using UnityEngine;

namespace _Game.GameModules.Entities.Scripts
{
    [CreateAssetMenu(fileName = "Play Particle", menuName = "GameContent/Entities/Commands/Play Particle", order = 0)]
    public class PlayParticle : EntityCommand
    {
        [SerializeField] int abilityIndex;
        [SerializeField] int particleIndex;

        public override void Execute(Entity entity)
        {
            entity.PlayAbilityParticleEffect(abilityIndex, particleIndex);
        }
    }
}
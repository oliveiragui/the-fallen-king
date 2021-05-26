using UnityEngine;

namespace _Game.GameModules.Entities.Scripts.Commands
{
    [CreateAssetMenu(fileName = "Play Ability Particle", menuName = "GameContent/Entities/Commands/Play Ability Particle", order = 0)]
    public class PlayAbilityParticle : EntityCommand
    {
        [SerializeField] int particleIndex;

        public override void Execute(Entity entity)
        {
            entity.particle.PlayAbilityEffect(entity.Character.AbilitySystem.Abilities.IndexOf(entity.Character.AbilitySystem.AbilityInUse), particleIndex);
        }
    }
}
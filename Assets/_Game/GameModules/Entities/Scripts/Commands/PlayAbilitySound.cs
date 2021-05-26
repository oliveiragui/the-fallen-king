using UnityEngine;

namespace _Game.GameModules.Entities.Scripts.Commands
{
    [CreateAssetMenu(fileName = "Play Ability Sound", menuName = "GameContent/Entities/Commands/Play Ability Sound", order = 0)]
    public class PlayAbilitySound : EntityCommand
    {
        [SerializeField] int soundIndex;

        public override void Execute(Entity entity)
        {
            entity.sound.PlayAbilitySFX(entity.Character.AbilitySystem.Abilities.IndexOf(entity.Character.AbilitySystem.AbilityInUse), soundIndex);
        }
    }
}
using System.Collections;
using UnityEngine;

namespace _Game.GameModules.Entities.Scripts.Commands
{
    [CreateAssetMenu(fileName = "Execute While Casting",
        menuName = "GameContent/Entities/Commands/Execute While Casting",
        order = 0)]
    public class ExecuteWhileCasting : EntityCommand
    {
        [SerializeField] int abilityIndex;
        [SerializeField] EntityCommand command;

        public override void Execute(Entity entity)
        {
            if (!entity.Character.AbilitySystem.usingAbility) return;
            entity.StartCoroutine(ExecuteOther(entity));
        }

        IEnumerator ExecuteOther(Entity entity) => new WaitWhile(() =>
        {
            command.Execute(entity);
            //return entity.animator.GetBool($"Conjurando Habilidade 3");
            return entity.animator.GetBool($"Conjurando Habilidade {(abilityIndex+1).ToString()}");
        });
    }
}
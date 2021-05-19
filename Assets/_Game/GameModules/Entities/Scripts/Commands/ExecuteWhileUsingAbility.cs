using System.Collections;
using UnityEngine;

namespace _Game.GameModules.Entities.Scripts.Commands
{
    
    [CreateAssetMenu(fileName = "Execute While Using Ability", menuName = "GameContent/Entities/Commands/Execute While Using Ability",
        order = 0)]
    public class ExecuteWhileUsingAbility : EntityCommand
    {
        [SerializeField] EntityCommand command;

        public override void Execute(Entity entity)
        {
            if (!entity.UsingAbility) return;
            entity.StartCoroutine(ExecuteOther(entity));
        }

        IEnumerator ExecuteOther(Entity entity) => new WaitWhile(() =>
        {
            command.Execute(entity);
            return entity.UsingAbility;
        });
    }
}
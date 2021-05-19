﻿using System.Collections;
using UnityEngine;

namespace _Game.GameModules.Entities.Scripts.Commands
{
    [CreateAssetMenu(fileName = "Move While Casting", menuName = "GameContent/Entities/Commands/FollowWhileCasting",
        order = 0)]
    public class FollowWhileCasting : EntityCommand
    {
        [SerializeField] int abilityIndex;

        public override void Execute(Entity entity)
        {
            if (!entity.Character.AbilitySystem.usingAbility) return;
            entity.movement.Rotation = entity.LookDiretion;
            entity.StartCoroutine(Rotate(entity));
        }

        IEnumerator Rotate(Entity entity) => new WaitWhile(() =>
        {
            entity.movement.Rotation = entity.LookDiretion;
            return entity.animator.GetBool($"Conjurando Habilidade {abilityIndex.ToString()}");
        });
    }
}
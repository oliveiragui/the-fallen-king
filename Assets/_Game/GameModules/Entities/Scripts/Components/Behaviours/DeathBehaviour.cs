﻿using UnityEngine;

namespace _Game.GameModules.Entities.Scripts.Components.Behaviours
{
    public class DeathBehaviour : StateMachineBehaviour
    {
        Entity entity;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (!entity && !animator.TryGetComponent(out entity)) return;
            entity.Stop();
            entity. movement.enabled = false;
            entity.collision.Hittable = false;
        }

        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (!entity && !animator.TryGetComponent(out entity)) return;
        }
    }
}
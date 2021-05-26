using System;
using UnityEngine;

namespace _Game.GameModules.Entities.Scripts.Behaviours
{
    public class EntityBehaviour: StateMachineBehaviour
    {
        [NonSerialized] public Entity entity;
    }
}
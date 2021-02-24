using System.Collections.Generic;
using UnityEngine;

namespace Components.StateMachinePattern
{
    public abstract class State : MonoBehaviour, IState
    {
        public List<StateTransition> Transitions { get; } = new List<StateTransition>();
        public State NextState { get; set; }
        public bool CanTrasitionToSelf { get; set; } = true;
        public bool HasFinished { get; set; } = true;

        public virtual void OnStateEntered() { }

        public virtual void OnUpdate() { }

        public virtual void OnFixedUpdate() { }

        public virtual void OnStateExited() { }
    }
}
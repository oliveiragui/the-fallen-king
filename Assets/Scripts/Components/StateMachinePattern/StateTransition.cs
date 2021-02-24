using System;

namespace Components.StateMachinePattern
{
    public class StateTransition
    {
        public readonly Func<bool> decision;
        public readonly State nextState;

        public StateTransition(State nextState, Func<bool> decision)
        {
            this.nextState = nextState;
            this.decision = decision;
        }
    }
}
using System.Collections.Generic;

namespace Components.StateMachinePattern
{
    public interface IState
    {
        List<StateTransition> Transitions { get; }
        State NextState { get; set; }
        bool CanTrasitionToSelf { get; set; }
        bool HasFinished { get; set; }

        void OnStateEntered();

        void OnUpdate();

        void OnFixedUpdate();

        void OnStateExited();
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Components.StateMachinePattern
{
    public class SateMachine : MonoBehaviour
    {
        List<StateTransition> _anyStateTransitions;
        IState _currentState;
        IState _defaultState;
        bool CurrentStateHasFinished => _currentState == null || _currentState.HasFinished;

        void Awake()
        {
            _anyStateTransitions = new List<StateTransition>();
        }

        public void Reset()
        {
            _currentState = null;
        }

        void Update()
        {
            EnterOnNextIfCurrentHasFinihed();
            _currentState.OnUpdate();
            ExitCurrentIfShouldTransition(_anyStateTransitions.Concat(_currentState.Transitions));
        }

        void FixedUpdate()
        {
            EnterOnNextIfCurrentHasFinihed();
            _currentState.OnFixedUpdate();
            ExitCurrentIfShouldTransition(_anyStateTransitions.Concat(_currentState.Transitions));
        }

        public event Action<IState> OnStateChanged;

        public void SetState(IState state)
        {
            _defaultState = state;
        }

        public void SetAnyStateTransitions(params StateTransition[] transitions)
        {
            _anyStateTransitions = transitions.ToList();
        }

        public void SetAnyStateTransitions(IEnumerable<StateTransition> transitions)
        {
            _anyStateTransitions = transitions.ToList();
        }

        void ExitCurrentIfShouldTransition(IEnumerable<StateTransition> transitions)
        {
            foreach (var transition in transitions.Where(transition =>
                transition.decision() &&
                (_currentState.CanTrasitionToSelf || !transition.nextState.Equals(_currentState))))
            {
                ExitState(transition);
                return;
            }
        }

        void EnterOnNextIfCurrentHasFinihed()
        {
            if (!CurrentStateHasFinished) return;

            _currentState = _currentState is null ? _defaultState : _currentState.NextState;

            OnStateChanged?.Invoke(_currentState);

            if (_currentState is null) return;
            EnterState();
        }

        void EnterState()
        {
            _currentState.HasFinished = false;
            _currentState.NextState = null;
            _currentState.OnStateEntered();
        }

        void ExitState(StateTransition transition)
        {
            _currentState.NextState = transition.nextState;
            _currentState.HasFinished = true;
            _currentState.OnStateExited();
        }
    }
}
using _Project.Code.Runtime.Utility.Conditions;
using _Project.Code.Runtime.Utility.StateMachineCore.Node;
using _Project.Code.Runtime.Utility.StateMachineCore.States;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _Project.Code.Runtime.Utility.StateMachineCore
{
    public class StateMachine<TState> : State, IDisposable where TState : class, IState
    {
        private readonly List<StateNode<TState>> _states;
        private StateNode<TState> _currentState;
        private bool _isRunning;

        protected TState CurrentState => _currentState.State;

        public StateMachine<TState> AddState(TState state)
        {
            _states.Add(new StateNode<TState>(state));

            return this;
        }

        public StateMachine<TState> AddTransition(TState fromState, TState toState, ICondition condition)
        {
            StateNode<TState> from = _states.First(stateNode => stateNode.State == fromState);
            StateNode<TState> to = _states.First(stateNode => stateNode.State == toState);

            from.AddTransition(new Transition<TState>(to, condition));

            return this;
        }

        public void Update(float deltaTime)
        {
            if (_isRunning == false)
                return;

            foreach (Transition<TState> transition in _currentState.Transitions)
            {
                if (transition.Condition.IsCompleate())
                {
                    SwichState(transition.ToState);
                    break;
                }
            }
        }

        public override void Enter()
        {
            if (_currentState == null)
                SwichState(_states[0]);
            else
                _currentState.State.Enter();

            _isRunning = true;
        }

        public override void Exit()
        {
            _currentState?.State.Exit();
            _isRunning = false;
        }

        public void Dispose()
        {
            foreach (StateNode<TState> stateNode in _states)
                stateNode.Dispose();

            _states.Clear();
        }

        private void SwichState(StateNode<TState> nextState)
        {
            _currentState?.State.Exit();
            _currentState = nextState;
            _currentState.State.Enter();
        }
    }
}

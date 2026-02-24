using _Project.Code.Runtime.Utility.StateMachineCore.States;
using System;
using System.Collections.Generic;

namespace _Project.Code.Runtime.Utility.StateMachineCore.Node
{
    public class StateNode<TState> : IDisposable where TState : class, IState
    {
        private readonly List<Transition<TState>> _transitions = new();

        public StateNode(TState state)
        {
            State = state;
        }

        public TState State { get; }
        public IReadOnlyList<Transition<TState>> Transitions => _transitions;

        public void AddTransition(Transition<TState> transition)
        {
            _transitions.Add(transition);
        }

        public void Dispose()
        {
            if (State is IDisposable disposable)
                disposable.Dispose();
        }
    }
}

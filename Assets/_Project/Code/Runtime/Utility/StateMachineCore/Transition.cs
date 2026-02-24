using _Project.Code.Runtime.Utility.Conditions;
using _Project.Code.Runtime.Utility.StateMachineCore.Node;
using _Project.Code.Runtime.Utility.StateMachineCore.States;

namespace _Project.Code.Runtime.Utility.StateMachineCore
{
    public class Transition<TState>where TState : class, IState
    {
        public Transition(StateNode<TState> toState, ICondition condition)
        {
            ToState = toState;
            Condition = condition;
        }

        public StateNode<TState> ToState { get; }
        public ICondition Condition { get; }
    }
}

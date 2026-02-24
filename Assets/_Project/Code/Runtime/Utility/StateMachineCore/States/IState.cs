using _Project.Code.Runtime.Utility.Reactive.Event;

namespace _Project.Code.Runtime.Utility.StateMachineCore.States
{
    public interface IState
    {
        IReadOnlyReactiveEvent Entered { get; }
        IReadOnlyReactiveEvent Exited { get; }

        void Enter();
        void Exit();
    }
}

using _Project.Code.Runtime.Utility.Reactive.Event;

namespace _Project.Code.Runtime.Utility.StateMachineCore.States
{
    public abstract class State : IState
    {
        public IReadOnlyReactiveEvent Entered => _entered;
        public IReadOnlyReactiveEvent Exited => _exited;

        private ReactiveEvent _entered = new ReactiveEvent();
        private ReactiveEvent _exited = new ReactiveEvent();

        public virtual void Enter()
        {
            _entered.Invoke();
        }

        public virtual void Exit()
        {
            _exited.Invoke();
        }
    }
}

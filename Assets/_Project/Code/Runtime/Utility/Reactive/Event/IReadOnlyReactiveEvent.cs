using System;

namespace _Project.Code.Runtime.Utility.Reactive.Event
{
    public interface IReadOnlyReactiveEvent
    {
        public IDisposable Subscribe(Action action);
    }

    public interface IReadOnlyReactiveEvent<T>
    {
        public IDisposable Subscribe(Action<T> action);
    }

    public interface IReadOnlyReactiveEvent<T, K>
    {
        public IDisposable Subscribe(Action<T, K> action);
    }
}

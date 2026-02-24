using System;

namespace _Project.Code.Runtime.Utility.Reactive.Variable
{
    public interface IReadOnlyReactiveVariable<T>
    {
        T Value { get; }
        public IDisposable Subscribe(Action<T> action);
    }

    public interface IReadOnlyReactiveVariable<T, TK>
    {
        T Value { get; }
        public IDisposable Subscribe(Action<T, T> action);
    }
}

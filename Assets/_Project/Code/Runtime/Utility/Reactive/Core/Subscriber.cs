using System;
using UnityEngine;

namespace _Project.Code.Runtime.Utility.Reactive.Core
{

    public class Subscriber : IDisposable
    {
        private readonly Action<Subscriber> _onDispose;
        private readonly Action _action;

        public Subscriber(Action<Subscriber> onDispose, Action action)
        {
            _onDispose =  onDispose;
            _action = action;
        }

        public void Invoke()
        {
            _action?.Invoke();
        }

        public void Dispose()
        {
            _onDispose?.Invoke(this);
        }
    }

    public class Subscriber<T> : IDisposable
    {  
        private readonly Action<Subscriber<T>> _onDispose;
        private readonly Action<T> _action;

        public Subscriber(Action<Subscriber<T>> onDispose, Action<T> action)
        {
            _onDispose = onDispose;
            _action = action;
        }

        public void Invoke(T arg1)
        {
            _action?.Invoke(arg1);
        }

        public void Dispose()
        {
            _onDispose?.Invoke(this);
        }
    }

    public class Subscriber<T, K> : IDisposable
    {
        private readonly Action<Subscriber<T, K>> _onDispose;
        private readonly Action<T, K> _action;

        public Subscriber(Action<Subscriber<T, K>> onDispose, Action<T, K> action)
        {
            _onDispose =  onDispose;
            _action = action;
        }

        public void Invoke(T arg1, K arg2)
        {
            _action?.Invoke(arg1, arg2);
        }

        public void Dispose()
        {
            _onDispose?.Invoke(this);
        }
    }
}

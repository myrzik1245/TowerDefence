using _Project.Code.Runtime.Utility.Reactive.Core;
using System;
using System.Collections.Generic;

namespace _Project.Code.Runtime.Utility.Reactive.Variable
{
    public class ReactiveVariable<T> : IReadOnlyReactiveVariable<T>
    {
        private readonly IEqualityComparer<T> _comparer;
        private readonly List<Subscriber<T>> _subscribers = new();
        private readonly List<Subscriber<T>> _toRemove = new();
        private readonly List<Subscriber<T>> _toAdd = new();

        private T _value;

        public ReactiveVariable(T value = default) : this(EqualityComparer<T>.Default, value)
        {
        }

        public ReactiveVariable(IEqualityComparer<T> comparer, T value)
        {
            _comparer = comparer;
            _value = value;
        }

        public T Value
        {
            get => _value;
            set
            {
                T oldValue = _value;
                _value = value;

                if (_comparer.Equals(_value, oldValue) == false)
                    Invoke(_value);
            }
        }

        public IDisposable Subscribe(Action<T> action)
        {
            Subscriber<T> subscriber = new Subscriber<T>(Remove, action);
            _toAdd.Add(subscriber);


            return subscriber;
        }

        private void Invoke(T value)
        {
            foreach (Subscriber<T> subscriber in _toAdd)
                _subscribers.Add(subscriber);

            foreach (Subscriber<T> subscriber in _toRemove)
                _subscribers.Remove(subscriber);

            _toAdd.Clear();
            _toRemove.Clear();

            foreach (Subscriber<T> subscriber in _subscribers)
                subscriber.Invoke(_value);
        }

        private void Remove(Subscriber<T> subscriber)
        {
            _toRemove.Add(subscriber);
        }
    }

    public class ReactiveVariable<T, TK> : IReadOnlyReactiveVariable<T, TK> where TK : T
    {
        private readonly IEqualityComparer<T> _comparer;
        private readonly List<Subscriber<T, T>> _subscribers = new();
        private readonly List<Subscriber<T, T>> _toRemove = new();
        private readonly List<Subscriber<T, T>> _toAdd = new();

        private T _value;

        public ReactiveVariable(T value = default) : this(EqualityComparer<T>.Default, value)
        {
        }

        public ReactiveVariable(IEqualityComparer<T> comparer, T value)
        {
            _comparer = comparer;
            _value = value;
        }

        public T Value
        {
            get => _value;
            set
            {
                T oldValue = _value;
                _value = value;

                if (_comparer.Equals(_value, oldValue) == false)
                    Invoke(oldValue, _value);
            }
        }

        public IDisposable Subscribe(Action<T, T> action)
        {
            Subscriber<T, T> subscriber = new Subscriber<T, T>(Remove, action);
            _toAdd.Add(subscriber);


            return subscriber;
        }

        private void Invoke(T oldValue, T newValue)
        {
            foreach (Subscriber<T, T> subscriber in _toAdd)
                _subscribers.Add(subscriber);

            foreach (Subscriber<T, T> subscriber in _toRemove)
                _subscribers.Remove(subscriber);

            _toAdd.Clear();
            _toRemove.Clear();

            foreach (Subscriber<T, T> subscriber in _subscribers)
                subscriber.Invoke(oldValue, newValue);
        }

        private void Remove(Subscriber<T, T> subscriber)
        {
            _toRemove.Add(subscriber);
        }
    }
}

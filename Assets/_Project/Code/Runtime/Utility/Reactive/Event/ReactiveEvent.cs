using _Project.Code.Runtime.Utility.Reactive.Core;
using System;
using System.Collections.Generic;

namespace _Project.Code.Runtime.Utility.Reactive.Event
{
    public class ReactiveEvent : IReadOnlyReactiveEvent
    {
        private readonly List<Subscriber> _subscribers = new();
        private readonly List<Subscriber> _toRemove = new();
        private readonly List<Subscriber> _toAdd = new();

        public IDisposable Subscribe(Action action)
        {
            Subscriber subscriber = new Subscriber(Remove, action);
            _toAdd.Add(subscriber);


            return subscriber;
        }

        public void Invoke()
        {
            foreach (Subscriber subscriber in _toAdd)
                _subscribers.Add(subscriber);

            foreach (Subscriber subscriber in _toRemove)
                _subscribers.Remove(subscriber);

            _toAdd.Clear();
            _toRemove.Clear();

            foreach (Subscriber subscriber in _subscribers)
                subscriber.Invoke();
        }

        private void Remove(Subscriber subscriber)
        {
            _toRemove.Add(subscriber);
        }
    }

    public class ReactiveEvent<T> : IReadOnlyReactiveEvent<T>
    {
        private readonly List<Subscriber<T>> _subscribers = new();
        private readonly List<Subscriber<T>> _toRemove = new();
        private readonly List<Subscriber<T>> _toAdd = new();

        public IDisposable Subscribe(Action<T> action)
        {
            Subscriber<T> subscriber = new Subscriber<T>(Remove, action);
            _toAdd.Add(subscriber);


            return subscriber;
        }

        public void Invoke(T arg)
        {
            foreach (Subscriber<T> subscriber in _toAdd)
                _subscribers.Add(subscriber);

            foreach (Subscriber<T> subscriber in _toRemove)
                _subscribers.Remove(subscriber);

            _toAdd.Clear();
            _toRemove.Clear();

            foreach (Subscriber<T> subscriber in _subscribers)
                subscriber.Invoke(arg);
        }

        private void Remove(Subscriber<T> subscriber)
        {
            _toRemove.Add(subscriber);
        }
    }

    public class ReactiveEvent<T, K> : IReadOnlyReactiveEvent<T, K>
    {
        private readonly List<Subscriber<T, K>> _subscribers = new();
        private readonly List<Subscriber<T, K>> _toRemove = new();
        private readonly List<Subscriber<T, K>> _toAdd = new();

        public IDisposable Subscribe(Action<T, K> action)
        {
            Subscriber<T, K> subscriber = new Subscriber<T, K>(Remove, action);
            _toAdd.Add(subscriber);


            return subscriber;
        }

        public void Invoke(T arg1, K arg2)
        {
            foreach (Subscriber<T, K> subscriber in _toAdd)
                _subscribers.Add(subscriber);

            foreach (Subscriber<T, K> subscriber in _toRemove)
                _subscribers.Remove(subscriber);

            _toAdd.Clear();
            _toRemove.Clear();

            foreach (Subscriber<T, K> subscriber in _subscribers)
                subscriber.Invoke(arg1, arg2);
        }

        private void Remove(Subscriber<T, K> subscriber)
        {
            _toRemove.Add(subscriber);
        }
    }
}

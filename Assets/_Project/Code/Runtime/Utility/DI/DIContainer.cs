using System;
using System.Collections.Generic;

namespace _Project.Code.Runtime.Utility.DI
{
    public class DIContainer : IInitializable, IDisposable
    {
        private readonly DIContainer _parent;
        private readonly Dictionary<Type, DIRegistration> _container = new();
        private readonly List<Type> _requests = new();

        public DIContainer(DIContainer parent = null)
        {
            _parent = parent;
        }

        public bool HasRegistration<T>()
        {
            if (_container.ContainsKey(typeof(T)))
                return true;

            return false;
        }

        public IDIOptions Register<T>(Func<DIContainer, T> factory)
        {
            DIRegistration registration = new DIRegistration(container => factory.Invoke(container));
            
            if (HasRegistration<T>())
                throw new InvalidOperationException($"Registration {typeof(T)} already exists");

            _container.Add(typeof(T), registration);

            return registration;
        }

        public T Resolve<T>()
        {
            if (_requests.Contains(typeof(T)))
                throw new InvalidOperationException($"Cycle requested for {typeof(T)}");
           
            try
            {
                _requests.Add(typeof(T));

                if (_container.TryGetValue(typeof(T), out DIRegistration registration))
                    return (T)registration.CreateInstance(this);

                if (_parent != null)
                    return _parent.Resolve<T>();
            }
            finally
            {
               _requests.Remove(typeof(T));
            }

            throw new InvalidOperationException($"Not found registration for {typeof(T)}");
        }

        public void Initialize()
        {
            foreach (DIRegistration registration in _container.Values)
            {
                if (registration.IsLazy == false)
                    registration.CreateInstance(this);

                registration.Initialize();
            }
        }

        public void Dispose()
        {
            foreach (DIRegistration registration in _container.Values)
                registration.Dispose();
        }
    }
}

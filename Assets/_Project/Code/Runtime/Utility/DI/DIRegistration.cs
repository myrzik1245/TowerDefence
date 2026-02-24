using System;

namespace _Project.Code.Runtime.Utility.DI
{
    public class DIRegistration : IDIOptions, IInitializable, IDisposable
    {
        private readonly Func<DIContainer, object> _factory;

        private object _instance;
        private bool _isAsSingle = false;

        public DIRegistration(Func<DIContainer, object> factory)
        {
            _factory = factory;
        }

        public bool IsLazy { get; private set; } = true;

        public object CreateInstance(DIContainer container)
        {
            if (_isAsSingle)
            {
                if (_instance == null)
                    _instance = _factory?.Invoke(container);

                return _instance;
            }
            
            return _factory?.Invoke(container);
        }

        public IDIOptions NonLazy()
        {
            IsLazy = false;

            return this;
        }

        public IDIOptions AsSingle()
        {
            _isAsSingle = true;

            return this;
        }

        public void Initialize()
        {
            if (_instance is IInitializable initializable)
                initializable.Initialize();
        }

        public void Dispose()
        {
            if (_instance is IDisposable disposable)
                disposable.Dispose();
        }
    }
}

using _Project.Code.Runtime.Utility.ConfigManagment.Loaders;
using System;
using System.Collections;
using System.Collections.Generic;

namespace _Project.Code.Runtime.Utility.ConfigManagment
{
    public class ConfigsProvider
    {
        private readonly IConfigLoader[] _loaders;
        private readonly Dictionary<Type, object> _configs = new Dictionary<Type, object>();

        public ConfigsProvider(params IConfigLoader[] loaders)
        {
            _loaders = loaders;
        }

        public IEnumerator LoadAsync()
        {
            foreach (IConfigLoader loader in _loaders)
                yield return loader.LoadAsync((type, config) =>_configs.Add(type, config));
        }

        public T GetConfig<T>() where T : class
        {
            if (_configs.TryGetValue(typeof(T), out object config))
                return (T)config;

            throw new InvalidOperationException($"Config: {typeof(T)} not found");
        }
    }
}

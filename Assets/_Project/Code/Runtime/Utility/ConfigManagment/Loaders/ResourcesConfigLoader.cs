using _Project.Code.Runtime.Utility.AssetsManagment;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Project.Code.Runtime.Utility.ConfigManagment.Loaders
{
    public class ResourcesConfigLoader : IConfigLoader
    {
        private readonly ResourcesAssetsLoader _resourcesAssetsLoader;
        private readonly Dictionary<Type, string> _pathsMap = new Dictionary<Type, string>()
        {
        };

        public ResourcesConfigLoader(ResourcesAssetsLoader resourcesAssetsLoader)
        {
            _resourcesAssetsLoader = resourcesAssetsLoader;
        }

        public IEnumerator LoadAsync(Action<Type, object> onLoad)
        {
            foreach (KeyValuePair<Type, string> path in _pathsMap)
            {
                ScriptableObject config = _resourcesAssetsLoader.Load<ScriptableObject>(path.Value);
                onLoad?.Invoke(path.Key, config);

                yield return null;
            }
        }
    }
}
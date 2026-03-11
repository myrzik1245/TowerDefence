using _Project.Code.Runtime.Configs;
using _Project.Code.Runtime.Configs.Characters;
using _Project.Code.Runtime.Configs.Level;
using _Project.Code.Runtime.Configs.Mine;
using _Project.Code.Runtime.Configs.Shop;
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
            { typeof(TowerConfig), "Configs/Characters/Tower/TowerConfig" },
            { typeof(BomberConfig), "Configs/Characters/Bomber/BomberConfig" },
            { typeof(LevelsConfig), "Configs/Levels/LevelsConfig" },
            { typeof(MineConfig), "Configs/DefenceObjects/Mine/MineConfig" },
            { typeof(ShopConfig), "Configs/Shop/ShopConfig" },
            { typeof(BonusConfig), "Configs/Bonus/BonusConfig" },
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
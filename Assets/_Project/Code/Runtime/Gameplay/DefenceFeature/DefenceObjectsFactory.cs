using _Project.Code.Runtime.Configs.Mine;
using _Project.Code.Runtime.Gameplay.DefenceFeature.Objects;
using _Project.Code.Runtime.Gameplay.ExplosionFeature;
using _Project.Code.Runtime.Gameplay.TeamFeature;
using _Project.Code.Runtime.Utility.AssetsManagment;
using _Project.Code.Runtime.Utility.DI;
using UnityEngine;

namespace _Project.Code.Runtime.Gameplay.DefenceFeature
{
    public class DefenceObjectsFactory
    {
        private readonly ResourcesAssetsLoader _resourcesAssetsLoader;
        private readonly ExplosionsFactory _explosionsFactory;

        public DefenceObjectsFactory(DIContainer container)
        {
            _resourcesAssetsLoader = container.Resolve<ResourcesAssetsLoader>();
            _explosionsFactory = container.Resolve<ExplosionsFactory>();
        }

        public Mine CreateMine(Vector3 position, MineConfig config, TeamsType teamType)
        {
            Mine prefab = _resourcesAssetsLoader.Load<Mine>("Gameplay/DefenceObjects/Mine");
            Mine instance = Object.Instantiate(prefab, position, Quaternion.identity);
            
            instance.Initialize(
                _explosionsFactory.Create(config.Explosion),
                teamType,
                config);
            
            return instance;
        }
    }
}

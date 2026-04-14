using _Project.Code.Runtime.Configs.Characters;
using _Project.Code.Runtime.Gameplay.AttackFeature.Core;
using _Project.Code.Runtime.Gameplay.StatsFeature;
using _Project.Code.Runtime.Gameplay.TeamFeature;
using _Project.Code.Runtime.Utility.AssetsManagment;
using _Project.Code.Runtime.Utility.CoroutineManagment;
using _Project.Code.Runtime.Utility.DI;
using UnityEngine;
using Object = UnityEngine.Object;

namespace _Project.Code.Runtime.Gameplay.Characters
{
    public class CharactersFactory
    {
        private readonly ResourcesAssetsLoader _resourcesAssetsLoader;
        private readonly AttackFactory _attackFactory;
        private readonly ICoroutinePerformer _coroutinePerformer;
        private readonly StatsFactory _statsFactory;

        public CharactersFactory(DIContainer container)
        {
            _resourcesAssetsLoader = container.Resolve<ResourcesAssetsLoader>();
            _attackFactory = container.Resolve<AttackFactory>();
            _coroutinePerformer = container.Resolve<ICoroutinePerformer>();
            _statsFactory = container.Resolve<StatsFactory>();
        }

        public Shooter CreateShooter(ShooterConfig config, Vector3 position, TeamsType teamType)
        {
            Shooter prefab = _resourcesAssetsLoader.Load<Shooter>(config.PrefabPath);
            Shooter instance = Object.Instantiate(prefab, position, Quaternion.identity);
            
            Stats stats = _statsFactory.CreateShooterStats(config);
            
            instance.Initialize(
                _coroutinePerformer,
                teamType,
                stats,
                _attackFactory.CreateExplosionAttack(
                    config.ExplosionConfig,
                    instance,
                    stats),
                config.MovementConfigData.Speed,
                config.MovementConfigData.Smooth,
                config.RotatorConfigData.Speed,
                config.SpawnTime,
                config.AttackTime);
            
            return instance;
        }
        
        public Tower CreateTower(TowerConfig towerConfig, Vector3 position, TeamsType teamType)
        {
            Tower prefab = _resourcesAssetsLoader.Load<Tower>(towerConfig.PrefabPath);
            Tower instance = Object.Instantiate(prefab, position, Quaternion.identity);

            Stats stats = _statsFactory.CreateTowerStats(towerConfig);
            
            instance.Initialize(
                _coroutinePerformer,
                stats,
                _attackFactory.CreateExplosionAttack(
                    towerConfig.ExplosionConfig,
                    instance,
                    stats),
                teamType,
                towerConfig.SpawnTime,
                towerConfig.AttackTime);
            
            return instance;
        }

        public Bomber CreateBomber(BomberConfig bomberConfig, Vector3 position, TeamsType teamType)
        {
            Bomber prefab = _resourcesAssetsLoader.Load<Bomber>(bomberConfig.PrefabPath);
            Bomber instance = Object.Instantiate(prefab, position, Quaternion.identity);

            Stats stats = _statsFactory.CreateBomberStats(bomberConfig);
            
            instance.Initialize(
                _coroutinePerformer,
                stats,
                _attackFactory.CreateExplosionAttack(
                    bomberConfig.ExplosionConfig, 
                    instance,
                    stats),
                bomberConfig.MovementConfigData.Speed,
                bomberConfig.MovementConfigData.Smooth,
                bomberConfig.RotatorConfigData.Speed,
                teamType,
                bomberConfig.SpawnTime);
            
            return instance;
        }
    }
}

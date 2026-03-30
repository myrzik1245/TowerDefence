using _Project.Code.Runtime.Configs.Characters;
using _Project.Code.Runtime.Gameplay.AttackFeature;
using _Project.Code.Runtime.Gameplay.AttackFeature.Core;
using _Project.Code.Runtime.Gameplay.TeamFeature;
using _Project.Code.Runtime.Utility.AssetsManagment;
using _Project.Code.Runtime.Utility.CoroutineManagment;
using _Project.Code.Runtime.Utility.DI;
using UnityEditor.Networking.PlayerConnection;
using UnityEngine;
using Object = UnityEngine.Object;

namespace _Project.Code.Runtime.Gameplay.Characters
{
    public class CharactersFactory
    {
        private readonly ResourcesAssetsLoader _resourcesAssetsLoader;
        private readonly AttackFactory _attackFactory;
        private readonly ICoroutinePerformer _coroutinePerformer;

        public CharactersFactory(DIContainer container)
        {
            _resourcesAssetsLoader = container.Resolve<ResourcesAssetsLoader>();
            _attackFactory = container.Resolve<AttackFactory>();
            _coroutinePerformer = container.Resolve<ICoroutinePerformer>();
        }

        public Shooter CreateShooter(ShooterConfig config, Vector3 position, TeamsType teamType)
        {
            Shooter prefab = _resourcesAssetsLoader.Load<Shooter>(config.PrefabPath);
            Shooter instance = Object.Instantiate(prefab, position, Quaternion.identity);
            instance.Initialize(
                _coroutinePerformer,
                teamType,
                _attackFactory.CreateExplosionAttack(
                    config.ExplosionConfig,
                    instance.GetComponent<ITeam>()),
                config.HealthConfigData.StartHealth,
                config.HealthConfigData.MaxHealth,
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
            instance.Initialize(
                _coroutinePerformer,
                towerConfig.HealthConfigData.StartHealth,
                towerConfig.HealthConfigData.MaxHealth,
                _attackFactory.CreateExplosionAttack(
                    towerConfig.ExplosionConfig,
                    instance.GetComponent<ITeam>()),
                teamType,
                towerConfig.SpawnTime,
                towerConfig.AttackTime);
            
            return instance;
        }

        public Bomber CreateBomber(BomberConfig bomberConfig, Vector3 position, TeamsType teamType)
        {
            Bomber prefab = _resourcesAssetsLoader.Load<Bomber>(bomberConfig.PrefabPath);
            Bomber instance = Object.Instantiate(prefab, position, Quaternion.identity);

            instance.Initialize(
                _coroutinePerformer,
                bomberConfig.HealthConfigData.StartHealth,
                bomberConfig.HealthConfigData.MaxHealth,
                _attackFactory.CreateExplosionAttack(
                    bomberConfig.ExplosionConfig, 
                    instance.GetComponent<ITeam>()),
                bomberConfig.MovementConfigData.Speed,
                bomberConfig.MovementConfigData.Smooth,
                bomberConfig.RotatorConfigData.Speed,
                teamType,
                bomberConfig.SpawnTime);
            
            return instance;
        }
    }
}

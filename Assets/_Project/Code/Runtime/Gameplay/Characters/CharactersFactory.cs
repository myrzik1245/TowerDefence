using _Project.Code.Runtime.Configs.Characters;
using _Project.Code.Runtime.Gameplay.AttackFeature;
using _Project.Code.Runtime.Gameplay.TeamFeature;
using _Project.Code.Runtime.Utility.AssetsManagment;
using _Project.Code.Runtime.Utility.DI;
using UnityEngine;
using Object = UnityEngine.Object;

namespace _Project.Code.Runtime.Gameplay.Characters
{
    public class CharactersFactory
    {
        private readonly ResourcesAssetsLoader _resourcesAssetsLoader;
        private readonly AttackFactory _attackFactory;

        public CharactersFactory(DIContainer container)
        {
            _resourcesAssetsLoader = container.Resolve<ResourcesAssetsLoader>();
            _attackFactory = container.Resolve<AttackFactory>();
        }
        
        public Tower CreateTower(TowerConfig towerConfig, Vector3 position, TeamsType teamType)
        {
            Tower prefab = _resourcesAssetsLoader.Load<Tower>(towerConfig.PrefabPath);
            Tower instance = Object.Instantiate(prefab, position, Quaternion.identity);
            instance.Initialize(
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

using _Project.Code.Runtime.Configs.Defence;
using _Project.Code.Runtime.Gameplay.AI.Brains;
using _Project.Code.Runtime.Gameplay.AttackFeature.Core;
using _Project.Code.Runtime.Gameplay.DefenceFeature.Objects;
using _Project.Code.Runtime.Gameplay.ExplosionFeature;
using _Project.Code.Runtime.Gameplay.TeamFeature;
using _Project.Code.Runtime.Utility.AssetsManagment;
using _Project.Code.Runtime.Utility.CoroutineManagment;
using _Project.Code.Runtime.Utility.DI;
using System.Collections.Generic;
using UnityEngine;

namespace _Project.Code.Runtime.Gameplay.DefenceFeature
{
    public class DefenceObjectsFactory
    {
        private readonly ResourcesAssetsLoader _resourcesAssetsLoader;
        private readonly ExplosionsFactory _explosionsFactory;
        private readonly ICoroutinePerformer _coroutinePerformer;
        private readonly AttackFactory _attackFactory;
        private readonly BrainsFactory _brainsFactory;
        private readonly List<IClearOnStage> _clearOnStages = new();

        public DefenceObjectsFactory(DIContainer container)
        {
            _resourcesAssetsLoader = container.Resolve<ResourcesAssetsLoader>();
            _explosionsFactory = container.Resolve<ExplosionsFactory>();
            _coroutinePerformer = container.Resolve<ICoroutinePerformer>();
            _attackFactory = container.Resolve<AttackFactory>();
            _brainsFactory = container.Resolve<BrainsFactory>();
        }

        public Puddle CreatePuddle(Vector3 position, PuddleConfig config, TeamsType teamsType)
        {
            Puddle prefab = _resourcesAssetsLoader.Load<Puddle>("Gameplay/DefenceObjects/Puddle");
            Puddle instance = Object.Instantiate(prefab, position, Quaternion.identity);
            
            instance.Initialize(
                config.Cooldown,
                _explosionsFactory.Create(config.Explosion),
                teamsType);
            
            _clearOnStages.Add(instance);
            
            return instance;
        }
        
        public Turret CreateTurret(Vector3 position, TurretConfig config, TeamsType teamType)
        {
            Turret prefab = _resourcesAssetsLoader.Load<Turret>("Gameplay/DefenceObjects/Turret");
            Turret instance = Object.Instantiate(prefab, position, Quaternion.identity);

            instance.Initialize(
                config.RotationSpeed,
                _coroutinePerformer,
                _attackFactory.CreateExplosionAttack(config.ExplosionAttackConfig, instance),
                config.AttackTime,
                teamType);
            
            _brainsFactory.CreateTurretAIBrain(instance, config);
            
            return instance;
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

        public void Clear()
        {
            foreach (IClearOnStage clearable in _clearOnStages)
                clearable.Release();
            
            _clearOnStages.Clear();
        }
    }
}

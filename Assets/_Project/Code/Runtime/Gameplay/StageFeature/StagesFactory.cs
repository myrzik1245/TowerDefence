using _Project.Code.Runtime.Configs.Level;
using _Project.Code.Runtime.Configs.Stages;
using _Project.Code.Runtime.Gameplay.SpawnerFeature;
using _Project.Code.Runtime.Utility.DI;
using System;

namespace _Project.Code.Runtime.Gameplay.StageFeature
{
    public class StagesFactory
    {
        private readonly SpawnersFactory _spawnersFactory;
        
        public StagesFactory(DIContainer container)
        {
            _spawnersFactory = container.Resolve<SpawnersFactory>();
        }
        
        public IStage Create(StageConfig config)
        {
            switch (config)
            {
                case KillAllEnemyStageConfig killAllEnemyStageConfig:
                    return CreateKillAllEnemyStage(killAllEnemyStageConfig);
                
                default:
                    throw new ArgumentException("Unknown stage config");
            }
        }

        private IStage CreateKillAllEnemyStage(KillAllEnemyStageConfig config)
        {
            return new KillAllEnemyStage(
                config,
                _spawnersFactory.CreateRadiusSpawner(config.SpawnRange)
            );
        }
    }
}

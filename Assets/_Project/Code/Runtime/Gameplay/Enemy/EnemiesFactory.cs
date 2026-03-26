using _Project.Code.Runtime.Configs.Characters;
using _Project.Code.Runtime.Gameplay.AI.Brains;
using _Project.Code.Runtime.Gameplay.Characters;
using _Project.Code.Runtime.Gameplay.TeamFeature;
using _Project.Code.Runtime.Utility.ConfigManagment;
using _Project.Code.Runtime.Utility.DI;
using System;
using UnityEngine;

namespace _Project.Code.Runtime.Gameplay.Enemy
{
    public class EnemiesFactory
    {
        private readonly CharactersFactory _charactersFactory;
        private readonly BrainsFactory _brainsFactory;
        private readonly ConfigsProvider _configsProvider;
        
        public EnemiesFactory(DIContainer container)
        {
            _charactersFactory = container.Resolve<CharactersFactory>();
            _brainsFactory = container.Resolve<BrainsFactory>();
            _configsProvider = container.Resolve<ConfigsProvider>();
        }

        public ICharacter Create(CharacterConfig config, Vector3 position)
        {
            switch (config)
            {
                case BomberConfig bomberConfig:
                    Bomber bomber = _charactersFactory.CreateBomber(bomberConfig, position, TeamsType.Enemy);
                    _brainsFactory.CreateBomberAIBrain(bomber);
                    return bomber;
                
                case ShooterConfig shooterConfig:
                    Shooter shooter = _charactersFactory.CreateShooter(shooterConfig, position, TeamsType.Enemy);
                    _brainsFactory.CreateShooterAIBrain(shooter);
                    return shooter;
                
                default:
                    throw new ArgumentException($"Unknown config {config.GetType()}");
            }
        }
    }
}

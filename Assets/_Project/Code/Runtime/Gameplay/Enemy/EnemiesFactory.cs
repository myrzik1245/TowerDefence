using _Project.Code.Runtime.Configs.Characters;
using _Project.Code.Runtime.Gameplay.AI.Brains;
using _Project.Code.Runtime.Gameplay.Characters;
using _Project.Code.Runtime.Gameplay.TeamFeature;
using _Project.Code.Runtime.Utility.DI;
using _Project.Code.Runtime.Utility.Reactive.List;
using System;
using UnityEngine;

namespace _Project.Code.Runtime.Gameplay.Enemy
{
    public class EnemiesFactory
    {
        private readonly CharactersFactory _charactersFactory;
        private readonly BrainsFactory _brainsFactory;
        private readonly ReactiveList<ICharacter> _enemies = new();
        
        public EnemiesFactory(DIContainer container)
        {
            _charactersFactory = container.Resolve<CharactersFactory>();
            _brainsFactory = container.Resolve<BrainsFactory>();
        }
        
        public IReadOnlyReactiveList<ICharacter> Enemies => _enemies;

        public ICharacter Create(CharacterConfig config, Vector3 position)
        {
            switch (config)
            {
                case BomberConfig bomberConfig:
                    Bomber bomber = _charactersFactory.CreateBomber(bomberConfig, position, TeamsType.Enemy);
                    _brainsFactory.CreateBomberAIBrain(bomber);
                    _enemies.Add(bomber);
                    return bomber;
                
                case ShooterConfig shooterConfig:
                    Shooter shooter = _charactersFactory.CreateShooter(shooterConfig, position, TeamsType.Enemy);
                    _brainsFactory.CreateShooterAIBrain(shooter, shooterConfig);
                    _enemies.Add(shooter);
                    return shooter;
                
                default:
                    throw new ArgumentException($"Unknown config {config.GetType()}");
            }
        }
    }
}

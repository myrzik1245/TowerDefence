using _Project.Code.Runtime.Configs.Characters;
using _Project.Code.Runtime.Gameplay.AI.Brains;
using _Project.Code.Runtime.Gameplay.Characters;
using _Project.Code.Runtime.Gameplay.TeamFeature;
using _Project.Code.Runtime.Utility.ConfigManagment;
using _Project.Code.Runtime.Utility.DI;
using _Project.Code.Runtime.Utility.Reactive.Event;
using UnityEngine;

namespace _Project.Code.Runtime.Gameplay.MainHero
{
    public class MainHeroFactory
    {
        private readonly CharactersFactory _charactersFactory;
        private readonly BrainsFactory _brainsFactory;
        private readonly ConfigsProvider _configsProvider;
        private ReactiveEvent<ICharacter> _heroSpawned = new ReactiveEvent<ICharacter>();
        
        public MainHeroFactory(DIContainer container)
        {
            _charactersFactory = container.Resolve<CharactersFactory>();
            _brainsFactory = container.Resolve<BrainsFactory>();
            _configsProvider = container.Resolve<ConfigsProvider>();
        }

        public IReadOnlyReactiveEvent<ICharacter> HeroSpawned => _heroSpawned;
        
        public Tower CreateTower(Vector3 position)
        {
            TowerConfig config = _configsProvider.GetConfig<TowerConfig>();
            Tower instance = _charactersFactory.CreateTower(config, position, TeamsType.Player);
            
            _brainsFactory.CreateInputTowerBrain(instance);
            _heroSpawned?.Invoke(instance);
            
            return instance;
        }
    }
}

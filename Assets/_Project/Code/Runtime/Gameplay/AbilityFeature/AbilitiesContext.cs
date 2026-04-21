using _Project.Code.Runtime.Gameplay.AbilityFeature.Abilities;
using _Project.Code.Runtime.Gameplay.GameLoop;
using _Project.Code.Runtime.Gameplay.MainHero;
using _Project.Code.Runtime.Gameplay.StageFeature;
using _Project.Code.Runtime.Utility.DI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _Project.Code.Runtime.Gameplay.AbilityFeature
{
    public class AbilitiesContext : IInitializable, IDisposable
    {
        private readonly StageService _stageService;
        private readonly MainHeroService _mainHeroService;
        private readonly List<IDisposable> _disposables = new();
        private readonly List<IAbility> _abilities = new();
        private IDisposable _mainHeroSpawnedSubscription;

        public AbilitiesContext(StageService stageService, MainHeroService mainHeroService)
        {
            _stageService = stageService;
            _mainHeroService = mainHeroService;
        }

        public void AddAbility(IAbility ability)
        {
            _abilities.Add(ability);
        }

        public void RemoveAbility(IAbility ability)
        {
            if (_abilities.Contains(ability))
                _abilities.Remove(ability);
        }
        
        public void Initialize()
        {
            _disposables.Add(_stageService.StageStarted.Subscribe(OnStageStarted));
            
            _mainHeroSpawnedSubscription = _mainHeroService.MainHeroSpawned.Subscribe(mainHero 
                => _disposables.Add(mainHero.Attacked.Subscribe(OnMainHeroAttacked)));
        }

        public void Dispose()
        {
            _mainHeroSpawnedSubscription?.Dispose();
            
            foreach (IDisposable disposable in _disposables)
                disposable?.Dispose();
            
            _disposables.Clear();
        }

        private void OnStageStarted()
        {
            List<IAbility> abilities = FilterAbilities(GameEvents.StageStarted);
            
            abilities.ForEach(ability => ability.Execute());
        }

        private void OnMainHeroAttacked()
        {
            List<IAbility> abilities = FilterAbilities(GameEvents.MainHeroAttacked);
            
            abilities.ForEach(ability => ability.Execute());
        }

        private List<IAbility> FilterAbilities(GameEvents gameEvent)
        {
            return _abilities
                .Where(ability => ability.GameEvent == gameEvent)
                .ToList();
        }
    }
}

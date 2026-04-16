using _Project.Code.Runtime.Utility.Reactive.Event;
using System;

namespace _Project.Code.Runtime.Gameplay.MainHero
{
    public class MainHeroService : IDisposable
    {
        public IMainHero MainHero { get; private set; }
        private readonly IDisposable _subscription;
        private readonly ReactiveEvent<IMainHero> _mainHeroSpawned = new();

        public MainHeroService(MainHeroFactory mainHeroFactory)
        {
            _subscription = mainHeroFactory.HeroSpawned.Subscribe(hero => 
            {
                if (hero is IMainHero mainHero == false)
                    throw new InvalidOperationException();
                
                MainHero = mainHero;
                _mainHeroSpawned.Invoke(MainHero);
                _subscription?.Dispose();
            });
        }
        
        public IReadOnlyReactiveEvent<IMainHero> MainHeroSpawned => _mainHeroSpawned;
        
        public void Dispose()
        {
            _subscription?.Dispose();
        }
    }
}

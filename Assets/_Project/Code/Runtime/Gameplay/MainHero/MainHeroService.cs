using _Project.Code.Runtime.Gameplay.Characters;
using System;

namespace _Project.Code.Runtime.Gameplay.MainHero
{
    public class MainHeroService : IDisposable
    {
        public ICharacter MainHero { get; private set; }

        private IDisposable _subscription;
        
        public MainHeroService(MainHeroFactory mainHeroFactory)
        {
            _subscription = mainHeroFactory.HeroSpawned.Subscribe(hero => 
            {
                MainHero = hero;
                _subscription?.Dispose();
            });
        }
        
        public void Dispose()
        {
            _subscription?.Dispose();
        }
    }
}

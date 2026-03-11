using _Project.Code.Runtime.Gameplay.Characters;

namespace _Project.Code.Runtime.Gameplay.MainHero
{
    public class MainHeroService
    {
        public ICharacter MainHero { get; private set; }

        public MainHeroService(MainHeroFactory mainHeroFactory)
        {
            mainHeroFactory.HeroSpawned.Subscribe(hero => MainHero = hero);
        }
    }
}

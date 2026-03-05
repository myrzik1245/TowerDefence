using _Project.Code.MainHero;
using _Project.Code.Runtime.Configs.Level;
using _Project.Code.Runtime.Enemy;
using _Project.Code.Runtime.Gameplay.AI;
using _Project.Code.Runtime.Gameplay.AI.Brains;
using _Project.Code.Runtime.Gameplay.AttackFeature;
using _Project.Code.Runtime.Gameplay.Characters;
using _Project.Code.Runtime.Gameplay.ExplosionFeature;
using _Project.Code.Runtime.Gameplay.SpawnerFeature;
using _Project.Code.Runtime.Gameplay.StageFeature;
using _Project.Code.Runtime.Utility.ConfigManagment;
using _Project.Code.Runtime.Utility.DI;
using _Project.Code.Runtime.Utility.SceneManagment.SceneInputArgs;

namespace _Project.Code.Runtime.Infrastructure.Registrations
{
    public class GameplayRegistrations
    {
        private static GameplayInputArgs _gameplayInputArgs;
        
        public static void Register(DIContainer gameplayContainer, GameplayInputArgs gameplayInputArgs)
        {
            _gameplayInputArgs = gameplayInputArgs;
            
            gameplayContainer.Register(CreateCharactersFactory).AsSingle();
            gameplayContainer.Register(CreateExplosionsFactory).AsSingle();
            gameplayContainer.Register(CreateAttackFactory).AsSingle();
            gameplayContainer.Register(CreateStageFactory).AsSingle();
            gameplayContainer.Register(CreateSpawner).AsSingle();
            gameplayContainer.Register(CreateBrainsFactory).AsSingle();
            gameplayContainer.Register(CreateBrainsContext).AsSingle();
            gameplayContainer.Register(CreateMainHeroFactory).AsSingle();
            gameplayContainer.Register(CreateEnemiesFactory).AsSingle();
            gameplayContainer.Register(CreateStageService).AsSingle();

            gameplayContainer.Initialize();
        }

        private static StageService CreateStageService(DIContainer c)
        {
            return new StageService(
                c.Resolve<StagesFactory>(),
                c.Resolve<ConfigsProvider>().GetConfig<LevelsConfig>().Levels[_gameplayInputArgs.Level]);
        }
        
        private static EnemiesFactory CreateEnemiesFactory(DIContainer c)
        {
            return new EnemiesFactory(c);
        }
        
        private static MainHeroFactory CreateMainHeroFactory(DIContainer c)
        {
            return new MainHeroFactory(c);
        }
        
        private static BrainsContext CreateBrainsContext(DIContainer c)
        {
            return new BrainsContext();
        }
        
        private static BrainsFactory CreateBrainsFactory(DIContainer c)
        {
            return new BrainsFactory(c);
        }
        
        private static SpawnersFactory CreateSpawner(DIContainer c)
        {
            return new SpawnersFactory(c);
        }
        
        private static StagesFactory CreateStageFactory(DIContainer c)
        {
            return new StagesFactory(c);
        }
        
        private static AttackFactory CreateAttackFactory(DIContainer c)
        {
            return new AttackFactory(c);
        }

        private static ExplosionsFactory CreateExplosionsFactory(DIContainer c)
        {
            return new ExplosionsFactory(c);
        }

        private static CharactersFactory CreateCharactersFactory(DIContainer c)
        {
            return new CharactersFactory(c);
        }
    }
}
